using System;
using System.Collections.Generic;
using Firehose.Web.Infrastructure;

namespace Firehose.Web.Authors
{
    public class RyanDavis : IAmACommunityMember
    {
        public IEnumerable<Uri> FeedUris
        {
            get { yield return new Uri("https://ryandavis.io/rss/"); }
        }

        public string FirstName => "Ryan";
        public string LastName => "Davis";
        public string StateOrRegion => "Brisbane, Australia";
        public string EmailAddress => "ryandavis.au@gmail.com";
        public string ShortBioOrTagLine => "knows how to 🎉";
        public Uri WebSite => new Uri("https://ryandavis.io");
        public string TwitterHandle => "rdavis_au";
        public string GitHubHandle => "rdavisau";
        public string GravatarHash => "d351762ec451e252b20ff860dfcded91d351762ec451e252b20ff860dfcded91";
        public GeoPosition Position => new GeoPosition(-27.4697710, 153.0251240);
        public string FeedLanguageCode => "en";
    }
}