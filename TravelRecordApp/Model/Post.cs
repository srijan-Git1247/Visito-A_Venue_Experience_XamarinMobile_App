
using System;
using System.Collections.Generic;
using System.Text;

namespace TravelRecordApp.Model
{
    public class Post
    {
      
        public string Experience
        {
            get;
            set;

        }
      
        public string ID
        {
            get;
            set;
        }

        public string VenueName{ get; set; }
        public string Categoryid { get; set; }
        public string Categoryname { get; set; }
        public string Address { get; set; }
        public string  FormattedAddress { get; set; }
        public string Postcode { get; set; }



        public string UserID { get; set; }
    }
}
