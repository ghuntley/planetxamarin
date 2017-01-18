﻿using Firehose.Web.Infrastructure;
using System;
using System.Collections.Generic;

namespace Firehose.Web.Authors
{
    public class MergeConflict : IAmACommunityMember
    {
        public string FirstName => "Merge";

        public string LastName => "Conflict";

        public string StateOrRegion => "Seattle, WA";

        public string EmailAddress => "mergeconflictfm@gmail.com";

        public string ShortBioOrTagLine => "Podcast";

        public Uri WebSite => new Uri("http://mergeconflict.fm");

        public IEnumerable<Uri> FeedUris
        {
            get { yield return new Uri("http://simplecast.com/podcasts/2117/rss"); }
        }

        public string TwitterHandle => "mergeconflictfm";

        public string GravatarHash => "24527eb9b29a8adbfc4155db4044dd3c";
    }
}
