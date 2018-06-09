using Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Storage
{
    public static class FavoritesStorage
    {
        private static Dictionary<string, List<Repository>> data = new Dictionary<string, List<Repository>>();

        public static bool AddRepository(string clientId, Repository repository)
        {
            bool result = false;

            if (!data.ContainsKey(clientId))
            {
                data.Add(clientId, new List<Repository>() { repository });

                result = true;
            }
            else
            {
                var clientFavorites = data[clientId];

                if (!clientFavorites.Exists(f => f.Key == repository.Key))
                {
                    clientFavorites.Add(repository);

                    result = true;
                }
            }

            return result;
        }

        public static bool RemoveRepository(string clientId, string repositoryKey)
        {
            bool result = false;

            if (data.ContainsKey(clientId))
            {
                var clientFavorites = data[clientId];

                var repository = clientFavorites.FirstOrDefault(f => f.Key == repositoryKey);

                if(repository!=null)
                {
                    clientFavorites.Remove(repository);

                    result = true;
                }
            }

            return result;
        }


        public static List<Repository> GetRepositories(string clientId)
        {
            List<Repository> result = new List<Repository>();

            if (data.ContainsKey(clientId))
            {
                result = data[clientId];
            }

            return result;
        }
    }
}