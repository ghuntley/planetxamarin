﻿using System;
using System.Collections.Generic;
using Firehose.Web.Infrastructure;

public class KymPhillpotts : IWorkAtXamarinOrMicrosoft
{
    public string FirstName => "Kym";
    public string LastName => "Phillpotts";
    public string ShortBioOrTagLine => "is one of the Xamarin University instructors";
    public string StateOrRegion => "Melbourne, Australia";
    public string EmailAddress => "kphillpotts@gmail.com";
    public string TwitterHandle => "kphillpotts";
    public string GravatarHash => "";

    public Uri WebSite => new Uri("http://kymphillpotts.com/");

    public IEnumerable<Uri> FeedUris
    {
        get { yield return new Uri("http://kymphillpotts.com/rss/"); }
    }

    public string GitHubHandle => string.Empty;
    public GeoPosition Position => new GeoPosition(-37.8136280, 144.9630580);
}
