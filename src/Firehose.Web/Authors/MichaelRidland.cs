using System;
using System.Collections.Generic;
using Firehose.Web.Infrastructure;

namespace Firehose.Web.Authors
{
    public class MichaelRidland : IAmAMicrosoftMVP
    {
        public string FirstName => "Michael";
        public string LastName => "Ridland";
        public string StateOrRegion => "Sydney, Australia";
        public string EmailAddress => "michael@xam-consulting.com";
        public string ShortBioOrTagLine => "Xamarin Contractor/Consultant | Founder XAM Consulting (xam-consulting.com) | Creator of FreshMvvm";
        public Uri WebSite => new Uri("https://michaelridland.com");

        public IEnumerable<Uri> FeedUris
        {
            get { yield return new Uri("https://michaelridland.com/feed/"); }
        }

        public string TwitterHandle => "rid00z";
        public string GravatarHash => "3c07e56045d18f4f290eb4983031309d";
        public string GitHubHandle => "rid00z";
        public GeoPosition Position => new GeoPosition(-25.348875, 131.035000);
        public string FeedLanguageCode => "en";
    }
}