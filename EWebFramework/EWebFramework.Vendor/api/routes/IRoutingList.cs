using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EWebFramework.Vendor.api.routes
{
    public interface  IRoutingList
    {

         List<IRouteDefinition> RoutingDefinitions { get; }

    }



}