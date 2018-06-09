using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GitHubSearchLib.Classes;

namespace Backend.Models
{
    public class Repository
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public bool IsFavorite { get; set; }

        public Owner Owner { get; set; }

        public Repository()
        {

        }

        public Repository(Item item) : this()
        {
            if (item != null)
            {
                this.Name = item.name;
                this.Description = item.description;
                this.Url = item.html_url;

                if (item.owner != null)
                {
                    this.Owner = new Owner(item.owner.login, item.owner.avatar_url, item.owner.html_url);
                }

                this.Key = string.Format("{0}-{1}", this.Owner.Name, this.Name).Replace(".",string.Empty);
            }
        }
    }
}