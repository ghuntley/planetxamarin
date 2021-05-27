using System;
using System.Collections.Generic;
using Firehose.Web.Infrastructure;

namespace Firehose.Web.Authors
{
	public class AlejandroRuiz : IAmACommunityMember
	{
		public string FirstName => "Alejandro";

		public string LastName => "Ruiz";

		public string ShortBioOrTagLine => "Alejandro Ruiz is a Xamarin MVP, C# & Open Source Lover";

		public string StateOrRegion => "Guadalajara, Mexico";

		public string EmailAddress => "alejandro@alejandroruizvarela.com";

		public Uri WebSite => new Uri("https://alejandroruizvarela.blogspot.mx");

		public IEnumerable<Uri> FeedUris
		{
			get { yield return new Uri("https://alejandroruizvarela.blogspot.mx/rss.xml"); }
		}

		public string TwitterHandle => "alejandroruizva";


		public string GravatarHash => "35d0fff7dbc133e9fe2075aa14205a57";
		public string GitHubHandle => "AlejandroRuiz";

		public GeoPosition Position => new GeoPosition(20.6668200, -103.3918200);

        public string FeedLanguageCode => "es";
	}
}
