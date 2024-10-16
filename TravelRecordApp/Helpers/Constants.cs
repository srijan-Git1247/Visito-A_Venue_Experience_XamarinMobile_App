using System;
using System.Collections.Generic;
using System.Text;

namespace TravelRecordApp.Helpers
{
    public class Constants
    {
        public const string VENUE_SEARCH ="https://api.foursquare.com/v3/places/search?ll={0},{1}";
        public const string CLIENT_ID = "ZTTEXJ0E2IQJW0BRVCEVHAS5SX5QODFM1VWKFYMIXUP5A3SY";
        public const string CLIENT_SECRET = "2XMQ1DLPR01L322VZ5C23AU3DAQOIXMDGVKHVV3DWORHYJL4";
        public const string API_KEY = "fsq3LHkqiYsePSOsMyR2nR0X4suzDgvtYyoEnMySyqX9ANk=";
        public const int RADIUS = 4000;
        public const int LIMIT = 5;
    }
}
