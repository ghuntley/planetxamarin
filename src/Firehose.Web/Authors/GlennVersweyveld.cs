using Firehose.Web.Infrastructure;
using System;
using System.Collections.Generic;

namespace Firehose.Web.Authors
{
    public class GlennVersweyveld : IAmACommunityMember
    {
        public string FirstName => "Glenn";
        public string LastName => "Versweyveld";
        public string ShortBioOrTagLine => "'SH10151' this is the serial number of our orbital gun";
        public string StateOrRegion => "Antwerp, Belgium";
        public string EmailAddress => "Glenn.Versweyveld@gmail.com";
        public string TwitterHandle => "depechie";
        public GeoPosition Position => new GeoPosition(51.091310, 4.548315);
        public Uri WebSite => new Uri("https://blog.depechie.com");

        public IEnumerable<Uri> FeedUris
        {
            get { yield return new Uri("https://blog.depechie.com/index.xml"); }
        }

        public string GravatarHash => "d3ac2144120f923f23c49ee4ce4437ab";
        public string GitHubHandle => "depechie";
		public string FeedLanguageCode => "en";
    }
}
