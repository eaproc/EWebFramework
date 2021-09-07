using EWebFramework.Vendor;
using EWebFramework.Vendor.api.routes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace EWebFramework.routes
{
    public class API: IRoutingList
    {

        List<IRouteDefinition> IRoutingList.RoutingDefinitions => new List<IRouteDefinition>(new IRouteDefinition[] {
                            new RouteGroup(
                                    new Target("/auth/login",new string[]{"POST"}, api.controllers.auth.Login.Index)
                                )
                            {
                               middleware = new Vendor.api.middlewares.IMiddlewareCheckConstraint[]{ new Vendor.api.middlewares.auth.IsGuestUser() }
                            },


                            new RouteGroup(
                                    new Target("/auth/logout",new string[]{"GET","POST"}, api.controllers.auth.Logout.Index)
                                )
                            {
                                middleware = new Vendor.api.middlewares.IMiddlewareCheckConstraint[]{
                                    new Vendor.api.middlewares.auth.IsAuthenticatedUser()
                                }
                            },



                            new RouteGroup(

                                     new DynamicTarget<api.controllers.Test>("/test",new string[]{ "GET","POST" }, "Index"),
                                     new Target(MaintenanceMode.URL,new string[]{"GET", "POST"}, EWebFramework.Vendor.api.controllers.system.MaintenanceMode.Index, new Vendor.api.middlewares.IsMaintenanceMode() )

                                )
                            {
                               // Anyone Can access
                            }




              });


    }
}