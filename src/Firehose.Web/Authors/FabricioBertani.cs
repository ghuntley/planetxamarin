using Firehose.Web.Infrastructure;
using System;
using System.Collections.Generic;

namespace Firehose.Web.Authors
{
    public class FabricioBertani : IAmACommunityMember
    {
        public string FirstName => "Fabricio";

        public string LastName => "Bertani";

        public string StateOrRegion => "Argentina";

        public string EmailAddress => "fabri_bertani@hotmail.com";

        public string ShortBioOrTagLine => "I'm a mobile application developer, mainly focused on multiplatform technologies such as Xamarin.Forms and Flutter.";

        public Uri WebSite => new Uri("https://medium.com/@fabricio.bertani");

        public string TwitterHandle => "fabribertani";

        public string GitHubHandle => "fabribertani";

        public string GravatarHash => "17344ace1d9171a57ab46211e1719b8d";

        public IEnumerable<Uri> FeedUris { get { yield return new Uri("https://medium.com/feed/@fabricio.bertani"); } }

        public GeoPosition Position => new GeoPosition(-32.9477132, -60.6304658);

        public string FeedLanguageCode => "en";
    }
}