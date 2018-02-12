﻿using System;
using System.Linq;
using System.Reflection;
using System.ServiceModel.Syndication;
using System.Web.Mvc;
using BlogMonster.Web;
using Firehose.Web.Infrastructure;

namespace Firehose.Web.Controllers
{
    public class FeedController : BaseController
    {
        private readonly CombinedFeedSource _combinedFeedSource;

        public FeedController(CombinedFeedSource combinedFeedSource)
        {
            _combinedFeedSource = combinedFeedSource;
        }

        [Route("feed")]
        public RssFeedResult Index(int? numPosts = 50, string lang = "")
        {
            var feed = GetFeed(numPosts, lang);
            return new RssFeedResult(feed);
        }

        private SyndicationFeed GetFeed(int? numPosts, string lang)
        {
            SyndicationFeed originalFeed = null;

            try
            {
                originalFeed = _combinedFeedSource.Feed;
                if (numPosts == null) return originalFeed;

                var bloggers = _combinedFeedSource.Bloggers.Where(b => b.FeedLanguageCode == lang);

                var items = _combinedFeedSource.Bloggers.SelectMany().Feed.Items
                    .OrderByDescending(item => item.PublishDate)
                    .Take((int)numPosts)
                    .ToArray();

                var shorterFeed = originalFeed.Clone(false);
                var itemsField = shorterFeed.GetType().GetField("items", BindingFlags.Instance | BindingFlags.NonPublic);
                itemsField.SetValue(shorterFeed, items);

                return shorterFeed;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);

                return originalFeed;
            }
        }
    }
}