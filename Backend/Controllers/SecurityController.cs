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
    public class SecurityController : ApiController
    {
        /// <summary>
        /// Authenticate client and get ClientId
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Authenticate")]
        [SwaggerOperation(Tags = new[] { "Security" })]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(string))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(string))]
        public IHttpActionResult Authenticate()
        {
            return Ok(Guid.NewGuid().ToString());
        }
    }
}