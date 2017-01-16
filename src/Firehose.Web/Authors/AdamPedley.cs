﻿using Firehose.Web.Infrastructure;
using System;
using System.Collections.Generic;

namespace Firehose.Web.Authors
{
    public class AdamPedley : IAmAMicrosoftMVP
    {
        public string FirstName => "Adam";

        public string LastName => "Pedley";

        public string StateOrRegion => "Melbourne, Australia";

        public string EmailAddress => "adam.pedley@gmail.com";

        public string Title => "Xamarin Developer | Microsoft MVP";

        public Uri WebSite => new Uri("https://xamarinhelp.com/");

        public IEnumerable<Uri> FeedUris
        {
            get { yield return new Uri("https://xamarinhelp.com/feed/"); }
        }

        public string TwitterHandle => "adpedley";

        public DateTime FirstAwarded => new DateTime(2016, 10, 1);
    }
}