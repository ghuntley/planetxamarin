using System;
using System.Collections.Generic;
using Firehose.Web.Infrastructure;

namespace Firehose.Web.Authors
{
    public class KerryLothrop : IAmAMicrosoftMVP
    {
        public string FirstName => "Kerry W.";
        public string LastName => "Lothrop";
        public string ShortBioOrTagLine => "";
        public string StateOrRegion => "Frankfurt, Germany";
        public string EmailAddress => string.Empty;
        public string TwitterHandle => "kwlothrop";
        public Uri WebSite => new Uri("https://kerry.lothrop.de/");

        public IEnumerable<Uri> FeedUris
        {
            get { yield return new Uri("https://kerry.lothrop.de/en/feed/"); }
        }

        public string GravatarHash => "250241b2800a1de895a75ce039bcfef4";
        public string GitHubHandle => string.Empty;
        public GeoPosition Position => new GeoPosition(50.1307615, 8.568736);
        public string FeedLanguageCode => "en";
    }
}