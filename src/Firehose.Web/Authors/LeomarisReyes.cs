using System;
using System.Collections.Generic;
using Firehose.Web.Infrastructure;

namespace Firehose.Web.Authors
{
    public class LeomarisReyes : IAmACommunityMember
    {
            public string FirstName => "Leomaris";
            public string LastName => "Reyes";
            public string ShortBioOrTagLine => "is a software engineer";
            public string StateOrRegion => "Dominican Republic";
            public string EmailAddress => "reyes.leomaris@gmail.com";
            public string TwitterHandle => "leomarisreyes11";
            public string GravatarHash => "ae78e84a683611c7b72c9ba829c125e0";
            public string GitHubHandle => "LeomarisReyes";
            public GeoPosition Position => new GeoPosition(18.470880, -69.911525);
            public Uri WebSite => new Uri("https://askxammy.com/");
            public IEnumerable<Uri> FeedUris { get { yield return new Uri("https://askxammy.com/feed"); } }
            public string FeedLanguageCode => "en";
    }
}