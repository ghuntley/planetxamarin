﻿using System;
using System.Collections.Generic;
using Firehose.Web.Infrastructure;

namespace Firehose.Web.Authors
{
    public class GoneMobile : IAmACommunityMember
    {
        public string FirstName => "Gone";

        public string LastName => "Mobile";

        public string StateOrRegion => "Internet";

        public string EmailAddress => "gonemobilecast@gmail.com";

        public string ShortBioOrTagLine => "is a development podcast focused on mobile development hosted by Jon Dick and Greg Shackles.";

        // TEMPORARY WORKAROUND - We'll get https support shortly, for now we have it for the feed at least
        public Uri WebSite => new Uri("http://gonemobile.io");

        public IEnumerable<Uri> FeedUris
        {
            // TEMPORARY WORKAROUND - We'll have proper https support in fireside soon and can use https://gonemobile.io/rss at that point
            get { yield return new Uri("https://gonemobile.fireside.fm/rss"); }
        }

        public string TwitterHandle => "gonemobilecast";
        public GeoPosition Position => new GeoPosition(51.2537750, -85.3232140);
        public string GravatarHash => "cb611c5ecd9a53b2af53a9d50d83c3c5";
        public string GitHubHandle => string.Empty;
    }
}
