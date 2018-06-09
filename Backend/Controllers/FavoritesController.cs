using Backend.Models;
using Backend.Storage;
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
    [RoutePrefix("api/v1")]
    [ApiExplorerSettings(IgnoreApi = false)]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class FavoritesController : ApiController
    {
        /// <summary>
        /// Add repository to client favorites
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Clients/{clientId}/Favorites")]
        [SwaggerOperation(Tags = new[] { "Favorites" })]
        [SwaggerResponse(HttpStatusCode.Created)]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [SwaggerResponse(HttpStatusCode.Conflict)]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(string))]
        public IHttpActionResult AddRepositoryToFavorites(string clientId, Repository repository)
        {
            bool result = false;

            try
            {
                if (string.IsNullOrWhiteSpace(clientId) || repository == null)
                {
                    return BadRequest();
                }

                result = FavoritesStorage.AddRepository(clientId, repository);

                if (result)
                {
                    return Created(string.Empty, result);
                }
                else
                {
                    return Conflict();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Delete repository to client favorites
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("Clients/{clientId}/Favorites/{repositoryKey}")]
        [SwaggerOperation(Tags = new[] { "Favorites" })]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.Conflict)]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(string))]
        public IHttpActionResult RemoveRepositoryFromFavorites(string clientId, string repositoryKey)
        {
            bool result = false;

            try
            {
                if (string.IsNullOrWhiteSpace(clientId) || string.IsNullOrWhiteSpace(repositoryKey))
                {
                    return BadRequest();
                }

                result = FavoritesStorage.RemoveRepository(clientId, repositoryKey);

                if (result)
                {
                    return Ok();
                }
                else
                {
                    return Conflict();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Get client favorite repositories
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Clients/{clientId}/Favorites")]
        [SwaggerOperation(Tags = new[] { "Favorites" })]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(List<Repository>))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(string))]
        public IHttpActionResult GetClientFavorites(string clientId)
        {
            try
            {
                return Ok(FavoritesStorage.GetRepositories(clientId));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}