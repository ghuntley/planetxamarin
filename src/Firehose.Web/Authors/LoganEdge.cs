using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using Firehose.Web.Infrastructure;

namespace Firehose.Web.Authors
{
    public class JohnWilson : IAmACommunityMember, IFilterMyBlogPosts
    {
        public string FirstName => "Logan";

        public string LastName => "Edge";

        public string StateOrRegion => "Tainan, Taiwan";

        public string EmailAddress => "finianex@gamil.com";

        public string ShortBioOrTagLine => "I'm not wolverine!";

        public Uri WebSite => new Uri("http://loganedge.tw/");

        public IEnumerable<Uri> FeedUris
        {
            get { yield return new Uri("http://www.loganedge.tw/feeds/posts/default"); }
        }

        public string TwitterHandle => "";

        public string GravatarHash => "";
        public string GitHubHandle => "";
        public GeoPosition Position => new GeoPosition(23.1229948,120.1062325);
        
        public bool Filter(SyndicationItem item)
        {
            // Here you filter out the given item by the criteria you want, i.e.
            // this filters out posts that do not have Xamarin in the title
            return item.Title.Text.ToLowerInvariant().Contains("xamarin");
        }
        
    }
}
