﻿using System;
using System.Collections.Generic;
using Firehose.Web.Infrastructure;

namespace Firehose.Web.Authors
{
    public class MichaelRidland : IAmAXamarinMVP
    {
        public string FirstName => "Michael";

        public string LastName => "Ridland";

        public string StateOrRegion => "Sydney, Australia";

        public string EmailAddress => "michael@xam-consulting.com";

        public string ShortBioOrTagLine => "";

        public Uri WebSite => new Uri("http://www.michaelridland.com");

        public IEnumerable<Uri> FeedUris
        {
            get { yield return new Uri("http://www.michaelridland.com/feed/"); }
        }

        public string TwitterHandle => "rid00z";

        public string GravatarHash => "";
        public string GitHubHandle => string.Empty;
        public GeoPosition Position => new GeoPosition(-33.8688200, 151.2092960);
    }
}
