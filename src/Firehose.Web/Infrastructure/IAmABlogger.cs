using System;
using System.Collections.Generic;

namespace Firehose.Web.Infrastructure
{
    public class GeoPosition
    {
        public static GeoPosition Empty = new GeoPosition(-1337, 42);

        public double Lat { get; }
        public double Lng { get; }
        
        public GeoPosition(double lat, double lng)
        {
            Lat = lat;
            Lng = lng;
        }
    }

    public interface IAmACommunityMember
    {
        string FirstName { get; }
        string LastName { get; }
        string StateOrRegion { get; }
        string EmailAddress { get; }
        string ShortBioOrTagLine { get; }
        Uri WebSite { get; }
        string TwitterHandle { get; }
        string GitHubHandle { get; }
        string GravatarHash { get; }
        IEnumerable<Uri> FeedUris { get; }
        GeoPosition Position { get; }

        // In ISO 639-1, lowercase, 2 letters
        // https://en.wikipedia.org/wiki/List_of_ISO_639-1_codes
        string FeedLanguageCode { get; }
    }

    public interface IWorkAtXamarinOrMicrosoft : IAmACommunityMember
    {
    }

    public interface IAmAMicrosoftMVP : IAmACommunityMember
    {
    }

    public interface IAmAPodcast : IAmACommunityMember
    {
    }
    
    public interface IAmANewsletter : IAmACommunityMember
    {
    }

    public interface IAmAFrameworkForXamarin : IAmACommunityMember
    {
    }

    public interface IAmAYoutuber : IAmACommunityMember
    {
    }
}