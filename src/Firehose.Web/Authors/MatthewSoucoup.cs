using System;
using System.Collections.Generic;
using Firehose.Web.Infrastructure;

namespace Firehose.Web.Authors
{
    public class MatthewSoucoup : IAmAMicrosoftMVP
    {
        public IEnumerable<Uri> FeedUris
        {
            get { yield return new Uri("https://codemilltech.com/feed/"); }
        }

        public string FirstName => "Matthew";
        public string LastName => "Soucoup";
        public string StateOrRegion => "Madison, WI";
        public string EmailAddress => "msoucoup@codemilltech.com";
        public string ShortBioOrTagLine => "";
        public Uri WebSite => new Uri("https://codemilltech.com");
        public string TwitterHandle => "codemillmatt";
        public string GravatarHash => "df69069a0bffd2dae5a8700a1bef7bfd";
        public string GitHubHandle => "codemillmatt";
        public GeoPosition Position => new GeoPosition(43.0730520, -89.4012300);
        public string FeedLanguageCode => "en";
    }
}