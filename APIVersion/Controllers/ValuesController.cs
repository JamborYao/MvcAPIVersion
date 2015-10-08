using Core.Webs.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIVersion.Controllers
{
    [Authorize]
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }

    public class Values_V1Controller : ApiController
    {
        [VersionedRoute("api/values", 1)]

        [Route("api/v1/values")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1 (V1)", "value2 (V1)" };
        }
        [VersionedRoute("api/values", 1)]

        [Route("api/v1/values/{id:int}")]
        public string Get(int id)
        {
            return String.Format("value {0} (V1)", id);
        }
    }

    public class Values_V2Controller : ApiController
    {
        [VersionedRoute("api/values", 2)]

        [Route("api/v2/values")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1 (V2)", "value2 (V2)" };
        }
        [VersionedRoute("api/values", 2)]
        [Route("api/v2/values/{id:int}")]
        public string Get(int id)
        {
            return String.Format("value {0} (V2)", id);
        }
    }

}
