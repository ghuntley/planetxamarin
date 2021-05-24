using Firehose.Web.Extensions;
using Microsoft.Extensions.Caching.Memory;
using Polly;
using Polly.Caching;
using Polly.Caching.Memory;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Xml;

namespace Firehose.Web.Infrastructure
{
	public class NewCombinedFeedSource
    {
        private static HttpClient httpClient;
        private static AsyncRetryPolicy retryPolicy;
		private static AsyncCachePolicy cachePolicy;

        public IEnumerable<IAmACommunityMember> Tamarins { get; }

        public NewCombinedFeedSource(IEnumerable<IAmACommunityMember> tamarins)
        {
            EnsureHttpClient();

            Tamarins = tamarins;

			if (retryPolicy == null)
			{
				// cache in memory for an hour
				var memoryCache = new MemoryCache(new MemoryCacheOptions());
				var memoryCacheProvider = new MemoryCacheProvider(memoryCache);
				cachePolicy = Policy.CacheAsync(memoryCacheProvider, TimeSpan.FromHours(1), OnCacheError);

				// retry policy with max 2 retries, delay by x*x^1.2 where x is retry attempt
				// this will ensure we don't retry too quickly
				retryPolicy = Policy.Handle<FeedReadFailedException>()
					.WaitAndRetryAsync(2, retry => TimeSpan.FromSeconds(retry * Math.Pow(1.2, retry)));
			}
        }

		private void OnCacheError(Context arg1, string arg2, Exception arg3)
		{
			Logger.Error(arg3, $"Failed caching item: {arg1} - {arg2}");
		}

		private void EnsureHttpClient()
        {
            if (httpClient == null)
            {
                httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.UserAgent.Add(
                    new ProductInfoHeaderValue("PlanetXamarin", $"{GetType().Assembly.GetName().Version}"));
                httpClient.Timeout = TimeSpan.FromSeconds(15);

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;
            }
        }

		private async Task<SyndicationFeed> LoadFeedInternal(int? numberOfItems, string languageCode = "mixed")
		{
			IEnumerable<IAmACommunityMember> tamarins;
			if (languageCode == null || languageCode == "mixed") // use all tamarins
			{
				tamarins = Tamarins;
			}
			else
			{
				tamarins = Tamarins.Where(t => CultureInfo.CreateSpecificCulture(t.FeedLanguageCode).Name == languageCode);
			}

			var feedTasks = tamarins.SelectMany(t => TryReadFeeds(t, GetFilterFunction(t)));

			var syndicationItems = await Task.WhenAll(feedTasks).ConfigureAwait(false);
			var combinedFeed = GetCombinedFeed(syndicationItems.SelectMany(f => f), languageCode, tamarins, numberOfItems);
			return combinedFeed;
		}

        public Task<SyndicationFeed> LoadFeed(int? numberOfItems, string languageCode = "mixed")
        {
			return cachePolicy.ExecuteAsync(context => LoadFeedInternal(numberOfItems, languageCode), new Context($"feed{numberOfItems}{languageCode}"));
        }

        private IEnumerable<Task<IEnumerable<SyndicationItem>>> TryReadFeeds(IAmACommunityMember tamarin, Func<SyndicationItem, bool> filter)
        {
            return tamarin.FeedUris.Select(uri => TryReadFeed(tamarin, uri.AbsoluteUri, filter));
        }

        private async Task<IEnumerable<SyndicationItem>> TryReadFeed(IAmACommunityMember tamarin, string feedUri, Func<SyndicationItem, bool> filter)
        {
            try
            {
                return await retryPolicy.ExecuteAsync(context => ReadFeed(feedUri, filter), new Context(feedUri)).ConfigureAwait(false);
            }
            catch (FeedReadFailedException ex)
            {
                Logger.Error(ex, $"{tamarin.FirstName} {tamarin.LastName}'s feed of {ex.Data["FeedUri"]} failed to load.");
            }

            return new SyndicationItem[0];
        }

        private async Task<IEnumerable<SyndicationItem>> ReadFeed(string feedUri, Func<SyndicationItem, bool> filter)
        {
            HttpResponseMessage response;
            try
            {
                response = await httpClient.GetAsync(feedUri).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    using (var feedStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                    using (var reader = XmlReader.Create(feedStream))
                    {
                        var feed = SyndicationFeed.Load(reader);
                        var filteredItems = feed.Items
                            .Where(item => TryFilter(item, filter));

                        return filteredItems;
                    }
                }
            }
            catch (HttpRequestException hex)
            {
                throw new FeedReadFailedException("Loading remote syndication feed failed", hex)
                    .WithData("FeedUri", feedUri);
            }
            catch (WebException ex)
            {
                throw new FeedReadFailedException("Loading remote syndication feed timed out", ex)
                    .WithData("FeedUri", feedUri);
            }
            catch (XmlException ex)
            {
                throw new FeedReadFailedException("Failed parsing remote syndication feed", ex)
                    .WithData("FeedUri", feedUri);
            }
            catch (TaskCanceledException ex)
            {
                throw new FeedReadFailedException("Reading feed timed out", ex)
                    .WithData("FeedUri", feedUri);
            }
			catch (OperationCanceledException opcex)
			{
				throw new FeedReadFailedException("Reading feed timed out", opcex)
					.WithData("FeedUri", feedUri);
			}

            throw new FeedReadFailedException("Loading remote syndication feed failed.")
                .WithData("FeedUri", feedUri)
                .WithData("HttpStatusCode", (int)response.StatusCode);
        }

        private SyndicationFeed GetCombinedFeed(IEnumerable<SyndicationItem> items, string languageCode, 
            IEnumerable<IAmACommunityMember> tamarins, int? numberOfItems)
        {
			DateTimeOffset GetMaxTime(SyndicationItem item)
			{
				return new[] { item.PublishDate.UtcDateTime, item.LastUpdatedTime.UtcDateTime }.Max();
			}

			var orderedItems = items
                .Where(item =>
					GetMaxTime(item) <= DateTimeOffset.UtcNow)
				.OrderByDescending(item => GetMaxTime(item));

			var feed = new SyndicationFeed(
                ConfigurationManager.AppSettings["RssFeedTitle"],
                ConfigurationManager.AppSettings["RssFeedDescription"],
                new Uri(ConfigurationManager.AppSettings["BaseUrl"] ?? "https://planetxamarin.com"),
                numberOfItems.HasValue ? orderedItems.Take(numberOfItems.Value) : orderedItems)
            {
                ImageUrl = new Uri(ConfigurationManager.AppSettings["RssFeedImageUrl"] ?? "https://planetxamarin.com/Content/Logo.png"),
                Copyright = new TextSyndicationContent("The copyright for each post is retained by its author."),
                Language = languageCode,
				LastUpdatedTime = DateTimeOffset.UtcNow
            };

            AddTwitterHandlesToFeed(feed);        

            foreach(var tamarin in tamarins)
            {
                feed.Contributors.Add(new SyndicationPerson(
                    tamarin.EmailAddress, $"{tamarin.FirstName} {tamarin.LastName}", tamarin.WebSite.ToString()));
            }

            return feed;
        }

        private void AddTwitterHandlesToFeed(SyndicationFeed feed)
        {
            foreach (var item in feed.Items)
            {
                var author = Tamarins.FirstOrDefault(a => MatchesAuthorUrls(a, item.Links.Select(l => l.Uri), item));
                if (!string.IsNullOrWhiteSpace(item.Title.Type) && author != null && !string.IsNullOrEmpty(author.TwitterHandle))
                {
                    item.Title = new TextSyndicationContent(item.Title.Text + " @" + author.TwitterHandle, GetTextKind(item.Title.Type));
                }
                else if (author != null && !string.IsNullOrEmpty(author.TwitterHandle))
                {
                    item.Title = new TextSyndicationContent(item.Title.Text + " @" + author.TwitterHandle);
                }
            }
        }

        private bool MatchesAuthorUrls(IAmACommunityMember author, IEnumerable<Uri> urls, SyndicationItem item)
        {
            var authorHosts = author.FeedUris.Select(au => au.Host.ToLowerInvariant()).Concat(new[] { author.WebSite.Host.ToLowerInvariant() }).ToArray();
            var feedBurnerAuthors = author.FeedUris.Where(au => au.Host.Contains("feeds.feedburner")).ToList();
            var mediumAuthors = author.FeedUris.Where(au => au.Host.Contains("medium.com")).ToList();
            var youtubeAuthors = author.FeedUris.Where(au => au.Host.Contains("youtube.com")).ToList();

            foreach (var itemUrl in urls)
            {
                var host = itemUrl.Host.ToLowerInvariant();

                if (host.Contains("medium.com"))
                {
                    if (itemUrl.Segments.Count() >= 3)
                    {
                        var mediumId = itemUrl.Segments[1].Trim('/');
                        return mediumAuthors.Any(fba => fba.AbsoluteUri.Contains(mediumId));
                    }
                }

                if (host.Contains("feedproxy.google")) //  feed burner is messed up :(
                {
                    // url will look like:
                    // feedproxy.google.com/~r/<feedburnerId>/~3/bgJNuDXwkU0/O
                    if (itemUrl.Segments.Count() >= 5)
                    {
                        var feedBurnerId = itemUrl.Segments[2].Trim('/');
                        return feedBurnerAuthors.Any(fba => fba.AbsoluteUri.Contains(feedBurnerId));
                    }
                }

                if (host.Contains("youtube.com")) //need to match youtube channel
                {
                    var channel = item?.Authors?.FirstOrDefault()?.Uri;
                    if (channel == null)
                        return false;

                    var id = channel.Replace("https://www.youtube.com/channel/", string.Empty);

                    return youtubeAuthors.Any(yt => yt.AbsoluteUri.Contains(id));
                }

                if (authorHosts.Contains(host))
                    return true;

                if (authorHosts.Contains(host.Replace("www.", "")))
                    return true;

                if (authorHosts.Contains(host.Insert(0, "www.")))
                    return true;
            }

            return false;
        }

        TextSyndicationContentKind GetTextKind(string type)
        {
            if (type == "text")
                return (TextSyndicationContentKind)Enum.Parse(typeof(TextSyndicationContentKind), "Plaintext", true);
            else
                return (TextSyndicationContentKind)Enum.Parse(typeof(TextSyndicationContentKind), type, true);
        }
        
        private static Func<SyndicationItem, bool> GetFilterFunction(IAmACommunityMember tamarin)
        {
            if (tamarin is IFilterMyBlogPosts filterMyBlogPosts)
            {
                return filterMyBlogPosts.Filter;
            }

            return null;
        }

        private static bool TryFilter(SyndicationItem item, Func<SyndicationItem, bool> filterFunc)
        {
            try
            {
                if (filterFunc != null)
                    return filterFunc(item);
            }
            catch (Exception)
            {
            }

            // the authors' filter is derped or has no filter
            // try some sane defaults
            return item.ApplyDefaultFilter();
        }
    }

    public class FeedReadFailedException : Exception
    {
        public FeedReadFailedException(string message) 
            : base(message)
        {
        }

        public FeedReadFailedException(string message, Exception inner) 
            : base(message, inner)
        {
        }
    }

	internal static class ExceptionExtensions
	{
		public static TException WithData<TException>(this TException exception, string key, object value) where TException : Exception
		{
			exception.Data[key] = value;
			return exception;
		}
	}
}