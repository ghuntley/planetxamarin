using System;
using System.Collections.Generic;
using Firehose.Web.Infrastructure;

namespace Firehose.Web.Authors
{
    public class GregShackles : IAmAMicrosoftMVP
    {
        public string FirstName => "Greg";
        public string LastName => "Shackles";
        public string ShortBioOrTagLine => "knows a thing (or two) about mobile testing";
        public string EmailAddress => "greg@gregshackles.com";
        public string TwitterHandle => "gshackles";
        public string GravatarHash => "6d7b45031bf22823060849d494343a8c";
        public string StateOrRegion => "New York, NY";
        public Uri WebSite => new Uri("https://gregshackles.com");

        public IEnumerable<Uri> FeedUris
        {
            get { yield return new Uri("https://gregshackles.com/rss/"); }
        }

        public string GitHubHandle => "gshackles";
        public GeoPosition Position => new GeoPosition(40.7127840, -74.0059410);
        public string FeedLanguageCode => "en";
    }
}