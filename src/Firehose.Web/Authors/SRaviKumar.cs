using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using Firehose.Web.Infrastructure;

namespace Firehose.Web.Authors
{
    public class SRaviKumar : IAmACommunityMember, IFilterMyBlogPosts
    {
        public string FirstName => "S Ravi";
        public string LastName => "Kumar";
        public string ShortBioOrTagLine => string.Empty;
        public string StateOrRegion => "Greater Noida, India";
        public string EmailAddress => "srkrathore@gmail.com";
        public string TwitterHandle => "srkrathore";
        public string GravatarHash => "";
        public GeoPosition Position => new GeoPosition(28.4743880, 77.5039900);

        public Uri WebSite => new Uri("http://err2solution.com/");

        public IEnumerable<Uri> FeedUris
        {
            get { yield return new Uri("http://err2solution.com/feed/"); }
        }

        public string GitHubHandle => string.Empty;

        public bool Filter(SyndicationItem item)
        {
            // This filters out only the posts that have the "xamarin" category
            return item.Categories.Any(c => c.Name.ToLowerInvariant().Equals("xamarin"));
        }
    }
}