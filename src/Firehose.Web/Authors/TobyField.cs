using System;
using System.Collections.Generic;
using Firehose.Web.Infrastructure;

namespace Firehose.Web.Authors
{
	public class TobyField : IAmACommunityMember
	{
		public string FirstName => "Toby";

		public string LastName => "Field";

		public string ShortBioOrTagLine => "Xamarin Mobile Developer";

		public string StateOrRegion => "Brisbane, Australia";

		public string EmailAddress => "toby@xamarinhowto.com";

		public string TwitterHandle => "xamarinhowto";

		public string GravatarHash => "879c533b4aebd704e39156198447cd3a";

		public string GitHubHandle => "xamarinhowto";

		public GeoPosition Position => new GeoPosition(-27.468450, 153.024184);

		public Uri WebSite => new Uri("https://xamarinhowto.com/");

		public IEnumerable<Uri> FeedUris { get { yield return new Uri("https://xamarinhowto.com/feed/"); } }

		public string FeedLanguageCode => "en";
	}
}