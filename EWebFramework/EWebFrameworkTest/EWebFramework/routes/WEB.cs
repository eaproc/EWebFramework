using EWebFramework.Vendor;
using EWebFramework.Vendor.api.routes;
using EWebFramework.Vendor.api.middlewares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EWebFramework.routes
{
    public class WEB: IRoutingList
    {

        List<IRouteDefinition> IRoutingList.RoutingDefinitions => new List<IRouteDefinition>(new IRouteDefinition[] {


                 new RouteGroup(
                                     new Target("/auth/login",new string[]{"GET"}, controllers.auth.Login.Index),
                                        new DynamicTarget<controllers.Home>("/home",new string[]{"GET"}, "Index")
                                )
                            {
                               middleware = new IMiddlewareCheckConstraint[]{ new Vendor.api.middlewares.auth.IsGuestUser() }
                            },


                 new RouteGroup(


                                    new Target("/auth/logout",new string[]{"GET"}, controllers.auth.Logout.Index)

                                 
                                )
                            {
                                middleware = new Vendor.api.middlewares.IMiddlewareCheckConstraint[]{
                                    new Vendor.api.middlewares.auth.IsAuthenticatedUser()
                                }
                            },



                  new RouteGroup(
                                      new Target("/errors/404",new string[]{"GET"}, controllers.errors.Error404.Index),
                                      new Target("/errors/401",new string[]{"GET"}, controllers.errors.Error401.Index),
                                      new Target(
                                          MaintenanceMode.URL,new string[]{"GET"}, EWebFramework.Vendor.controllers.system.MaintenanceMode.Index, new Vendor.api.middlewares.IsMaintenanceMode()
                                                                                                                ),
                                    new DynamicTarget<controllers.Test>("/test",new string[]{"GET","POST"}, "Index")
                                )
                            {
                               // Anyone Can access
                            }





              });


    }
}