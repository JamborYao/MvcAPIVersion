using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Routing;

namespace Core.Webs.Routing
{
    public class VersionConstraint : IHttpRouteConstraint
    {
        private const Int32 DEFAULT_VERSION = 1;

        public VersionConstraint(Int32 allowedVersion)
        {
            AllowedVersion = allowedVersion;
        }

        public Int32 AllowedVersion
        {
            get;
            private set;
        }

        public Boolean Match(HttpRequestMessage request, IHttpRoute route, String parameterName, IDictionary<String, Object> values, HttpRouteDirection routeDirection)
        {
            if (routeDirection == HttpRouteDirection.UriResolution)
            {
                Int32 version = GetVersionHeader(request) ?? DEFAULT_VERSION;

                if (version == AllowedVersion)
                    return true;
            }

            return false;
        }

        private Int32? GetVersionHeader(HttpRequestMessage request)
        {
            String versionAsString = String.Empty;
            IEnumerable<String> headerValues;

            if (request.Headers.TryGetValues("api-version", out headerValues) && headerValues.Count() == 1)
                versionAsString = headerValues.First();

            Int32 version;

            if (!String.IsNullOrEmpty(versionAsString) && Int32.TryParse(versionAsString, out version))
                return version;

            return null;
        }
    }
    public class VersionedRoute : RouteFactoryAttribute
    {
        public VersionedRoute(String template, Int32 allowedVersion)
            : base(template)
        {
            AllowedVersion = allowedVersion;
        }

        public Int32 AllowedVersion
        {
            get;
            private set;
        }

        public override IDictionary<String, Object> Constraints
        {
            get
            {
                var constraints = new HttpRouteValueDictionary();
                constraints.Add("version", new VersionConstraint(AllowedVersion));
                return constraints;
            }
        }
    }

}
