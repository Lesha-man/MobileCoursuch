using System;
using System.Collections.Generic;
using System.Text;

namespace MobileCoursuch
{
    [Serializable]
    public class UriImage
    {
        public int id { get; set; }
        public string name { get; set; }
        public Uri url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public int userId { get; set; }
    }
}
