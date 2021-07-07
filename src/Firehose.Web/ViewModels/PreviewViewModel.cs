using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Xml;
using Firehose.Web.Extensions;
using Firehose.Web.Infrastructure;
using Humanizer;

namespace Firehose.Web.ViewModels
{
    public class PreviewViewModel
    {
        public List<PreviewModelItem> Items { get; }

		public PreviewViewModel(SyndicationFeed feed, IAmACommunityMember[] authors)
		{
			bool MatchesAuthorUrls(IAmACommunityMember author, IEnumerable<Uri> urls, SyndicationItem item)
			{
				var authorHosts = author.FeedUris.Select(au => au.Host.ToLowerInvariant()).Concat(new[] { author.WebSite.Host.ToLowerInvariant() }).ToArray();
				var feedBurnerAuthors = author.FeedUris.Where(au => au.Host.Contains("feeds.feedburner")).ToList();
				var mediumAuthors = author.FeedUris.Where(au => au.Host.Contains("medium.com")).ToList();
				var youtubeAuthors = author.FeedUris.Where(au => au.Host.Contains("youtube.com")).ToList();

				foreach (var itemUrl in urls)
				{
					var host = itemUrl.Host.ToLowerInvariant();

					if(host.Contains("medium.com"))
					{
						if(itemUrl.Segments.Count() >= 3)
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

					if(host.Contains("youtube.com")) //need to match youtube channel
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

			var items = new List<PreviewModelItem>();
			foreach(var item in feed.Items)
			{
				var author = authors.FirstOrDefault(a => MatchesAuthorUrls(a, item.Links.Select(l => l.Uri), item));

				string authorName;

				if (author != null)
				{
					authorName = $"{author.FirstName} {author.LastName}".Trim();
				}
				// If no author was matched, extract the name from the RSS feed, something is better than nothing right?!
				else
				{
					var creator = item.ElementExtensions.FirstOrDefault(x => x.OuterName == "creator" && x.OuterNamespace == "http://purl.org/dc/elements/1.1/");
					if (creator != null)
					{
						authorName = creator.GetObject<XmlElement>().Value ?? string.Empty;
					}
					else
					{
						authorName = string.Join(", ", item.Authors.Select(a => $"{a.Name} {a.Email}".Trim()));
					}
				}

				var link = item.Links.FirstOrDefault()?.Uri.ToString() ?? string.Empty;
				var html = item.Content?.ToHtml() ?? item.Summary?.ToHtml() ?? string.Empty;
				var titleSuffix = string.IsNullOrEmpty(author.TwitterHandle) 
									? string.Empty
									: ", by @" + author.TwitterHandle;

				var previewItem = new PreviewModelItem
				{
					AuthorName = authorName,
					Gravatar = author?.GravatarImage(),
					Title = item.Title.Text + titleSuffix,
					Link = link,
					Body = html.Sanitize(),
					PublishDate = item.PublishDate.Humanize()
				};

				items.Add(previewItem);
			}

			Items = items;
		}
    }

	public class PreviewModelItem
	{
		public string AuthorName { get; set; }
		public string Gravatar { get; set; }
		public string Title { get; set; }
		public string Link { get; set; }
		public string Body { get; set; }
		public string PublishDate { get; set; }
	}
}