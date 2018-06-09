using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Models
{
    public class Owner
    {
        public string Name { get; set; }
        public string AvatarUrl { get; set; }
        public string Url { get; set; }

        public Owner()
        {

        }

        public Owner(string name, string avatarUrl, string url)
        {
            this.Name = name;
            this.AvatarUrl = avatarUrl;
            this.Url = url;
        }
    }
}