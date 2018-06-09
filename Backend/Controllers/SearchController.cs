using Backend.Models;
using GitHubSearchLib.Classes;
using GitHubSearchLib.Managers;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace Backend.Controllers
{
    [RoutePrefix("api/v1/Search")]
    [ApiExplorerSettings(IgnoreApi = false)]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class SearchController : ApiController
    {
        /// <summary>
        /// Seach GitHub repositories
        /// </summary>
        /// <returns>GitHub search result</returns>
        [HttpGet]
        [Route("")]
        [SwaggerOperation(Tags = new[] { "Search" })]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(SearchResult))]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(string))]
        public IHttpActionResult Search(string clientId, string searchString)
        {
            SearchResult result = null;

            try
            {
                var searchResponse = GitHubSearchManager.Search(searchString);

                if (searchResponse != null && searchResponse.total_count > 0)
                {
                    return Ok(new SearchResult(clientId, searchResponse));
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}