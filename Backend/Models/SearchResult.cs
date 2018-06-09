using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GitHubSearchLib.Classes;
using Backend.Storage;

namespace Backend.Models
{
    public class SearchResult
    {
        public int Count { get; set; }
        public List<Repository> Repositories { get; set; } = new List<Repository>();

        public SearchResult()
        {
        }

        public SearchResult(string clientId, GitHubSearchReponse v) : this()
        {
            if (v != null && v.total_count > 0)
            {
                this.Count = v.total_count;

                var favoriteRepositories = FavoritesStorage.GetRepositories(clientId);

                    foreach (var item in v.items)
                    {
                        var repository = new Repository(item);

                        repository.IsFavorite = favoriteRepositories.Exists(f => f.Key == repository.Key);

                        this.Repositories.Add(repository);
                    }

            }
        }
    }
}