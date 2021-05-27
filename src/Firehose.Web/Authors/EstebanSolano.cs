using System;
using System.Collections.Generic;
using Firehose.Web.Infrastructure;

namespace Firehose.Web.Authors
{
    public class EstebanSolano : IAmAMicrosoftMVP
    {
        public string FirstName => "Esteban";
        public string LastName => "Solano";
        public string StateOrRegion => "Cartago, Costa Rica";
        public string EmailAddress => "stvansolano@outlook.com";
        public string ShortBioOrTagLine => "is a passionate software community guy from Costa Rica who enjoys learning, talk and help others to learn C# and Xamarin";
        public Uri WebSite => new Uri("https://stvansolano.github.io/");

        public IEnumerable<Uri> FeedUris
        {
            get { yield return new Uri("https://stvansolano.github.io/atom.xml"); }
        }

        public string TwitterHandle => "stvansolano";
        public string GravatarHash => "d02d96057c4cd905d60d14549b00db0d";
        public string GitHubHandle => "stvansolano";
        public GeoPosition Position => new GeoPosition(9.9322992, -84.0815271);
        public string FeedLanguageCode => "es";
    }
}