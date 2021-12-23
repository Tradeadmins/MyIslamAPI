using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyIslamWebApp.Enums
{
    public enum ApplicationTypes
    {
        JavaScript = 0,
        NativeConfidential = 1
    };

    public enum AppLangauges
    {
        Arabic = 1,
        English = 2,
        Turkey = 3,
        Malay = 4
    };

    public enum SubscriptionTypes
    {
        Trail = 0,
        Monthly = 1,
        Yearly = 2
    };

    public enum EventCategory
    {
        Education = 1,
        Funeral = 2,
        SocailGathering = 3,
        KidsFriendly = 4,
        Others = 5
    };
}