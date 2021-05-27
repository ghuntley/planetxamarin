using System;
using System.Collections.Generic;
using Firehose.Web.Infrastructure;

namespace Firehose.Web.Authors
{
    public class TomaszCielecki : IAmAMicrosoftMVP
    {
        public IEnumerable<Uri> FeedUris
        {
            get { yield return new Uri("https://blog.ostebaronen.dk/feed.xml"); }
        }

        public string FirstName => "Tomasz";
        public string LastName => "Cielecki";
        public string StateOrRegion => "Copenhagen, Denmark";
        public string EmailAddress => "tomasz@ostebaronen.dk";
        public string ShortBioOrTagLine => "Open Source all the things!";
        public Uri WebSite => new Uri("https://blog.ostebaronen.dk");
        public string TwitterHandle => "Cheesebaron";
        public string GitHubHandle => "Cheesebaron";
        public string GravatarHash => "f780d57997526876b0625e517c1e0884";
        public GeoPosition Position => new GeoPosition(55.8019193, 12.523124);
        public string FeedLanguageCode => "en";
    }
}