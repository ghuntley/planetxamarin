using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using Firehose.Web.Infrastructure;

namespace Firehose.Web.Authors
{
  public class JesseLiberty : IAmACommunityMember, IAmAXamarinMVP, IFilterMyBlogPosts
  {
    public string FirstName => "Jesse";
    public string LastName => "Liberty";
    public string ShortBioOrTagLine => "See http://jesseliberty.me";
    public string StateOrRegion => "Massachusetts";
    public string EmailAddress => "jesseliberty@gmail.com";
    public string TwitterHandle => "jesseliberty";
    public string GravatarHash => "78d5b6609fe5a80ce67e9f971833a6c3";
    public string GitHubHandle => "JesseLiberty";
    public GeoPosition Position => new GeoPosition(42.4703963,-71.4477468,15);
    public Uri WebSite => new Uri("http://jesseliberty.me");
    public IEnumerable<Uri> FeedUris { get { yield return new Uri("http://feeds.feedburner.com/JesseLiberty-SilverlightGeek"); } }
    
     public bool Filter(SyndicationItem item)
      {
          // This filters out only the posts that have the "xamarin" category
          return item.Categories.Any(c => c.Name.ToLowerInvariant().Equals("xamarin"));
      }
    }
}