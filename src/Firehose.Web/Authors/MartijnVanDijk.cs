using System;
using System.Collections.Generic;
using Firehose.Web.Infrastructure;

namespace Firehose.Web.Authors
{
    public class MartijnVanDijk : IAmAMicrosoftMVP
    {
		public string FirstName => "Martijn";
        public string LastName => "van Dijk";
		public string StateOrRegion => "Amsterdam, Netherlands";
		public string EmailAddress => "mhvdijk@gmail.com";
        public string ShortBioOrTagLine => "is working at Baseflow.com on Apps and MvvmCross";
		public Uri WebSite => new Uri("https://medium.com/@martijn00");

        public IEnumerable<Uri> FeedUris
        {
			get { yield return new Uri("https://medium.com/feed/@martijn00"); }
        }

		public string TwitterHandle => "mhvdijk";
		public string GravatarHash => "22155f520ab611cf04f76762556ca3f5";
		public string GitHubHandle => "martijn00";
		public GeoPosition Position => new GeoPosition(52.3702160, 4.8951680);
        public string FeedLanguageCode => "en";
    }
}