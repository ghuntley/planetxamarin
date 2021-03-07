using System;
using System.Collections.Generic;
using Firehose.Web.Infrastructure;

namespace Firehose.Web.Authors
{
	public class Respawnsive : IAmACommunityMember
	{
		public string FirstName => "Respawnsive";
		public string LastName => string.Empty;
		public string ShortBioOrTagLine => "Web & cross-platform mobile app agency";
		public string StateOrRegion => "France";
		public string EmailAddress => "contact@respawnsive.com";
		public string TwitterHandle => "respawnsive";
		public string GravatarHash => "3858d889298f07b484319cc815cbf012";
		public string GitHubHandle => "Respawnsive";
		public GeoPosition Position => new GeoPosition(45.188529, 5.724524);
		public Uri WebSite => new Uri("https://respawnsive.com/");
		public IEnumerable<Uri> FeedUris { get { yield return new Uri("https://www.respawnsive.com/category/xamarin-en/feed/"); } }
		public string FeedLanguageCode => "en";
	}
}