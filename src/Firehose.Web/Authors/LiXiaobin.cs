﻿using Firehose.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;

namespace Firehose.Web.Authors
{
    public class LiXiaobin : IAmACommunityMember, IFilterMyBlogPosts
    {
        public string FirstName => "Li";
        public string LastName => "Xiaobin";
        public string ShortBioOrTagLine => "";
        public string StateOrRegion => "Jiangxi";
        public string EmailAddress => "newblifes@hotmail.com";
        public string TwitterHandle => "newblifes";
        public string GravatarHash => string.Empty;
        public string GitHubHandle => "NewBLife";
        public GeoPosition Position => new GeoPosition(27.424025, 114.552699);
        public Uri WebSite => new Uri("http://www.cnblogs.com/lixiaobin/");
        public IEnumerable<Uri> FeedUris { get { yield return new Uri("http://feed.cnblogs.com/blog/u/93829/rss"); } }

        public bool Filter(SyndicationItem item)
        {
            return item.Categories.Any(c => c.Name.ToLowerInvariant().Contains("xamarin"));
        }
    }
}
