using Owin;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Owin;

namespace KatanaConsole
{
    //use an Alias for OWIN App Func
    using AppFunc = Func<IDictionary<string, object>, Task>;
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var middleware1 = new Func<AppFunc, AppFunc>(MyMiddleware_1);
            var middleware2 = new Func<AppFunc, AppFunc>(MyMiddleware_2);
            
            app.Use(middleware1);
            app.Use(middleware2);
            app.Use<MiddlewareComponent>();
            app.UseGreetingsMiddleware("<h1>Hello from custom middleware<h1>");
        }

        private AppFunc MyMiddleware_1(AppFunc next)
        {
            AppFunc appFunc = async (IDictionary<string, object> environment) =>
            {
                //Incoming Request
                var response = environment["owin.ResponseBody"] as Stream;
                using (var writer = new StreamWriter(response))
                {
                    await writer.WriteAsync("<h1>Middleware 1 triggered</h1>");
                }

                //Call next middleware in chain
                await next.Invoke(environment);
            };

            return appFunc;
        }

        private AppFunc MyMiddleware_2(AppFunc next)
        {
            AppFunc appFunc = async (IDictionary<string, object> environment) =>
            {
                //Using OwinContext
                IOwinContext context = new OwinContext(environment);
                await context.Response.WriteAsync("<h1>Middleware 2 triggered</h1>");
                //Call next middleware in chain
                await next.Invoke(environment);
            };

            return appFunc;
        }
    }
}