using System;
using Microsoft.Owin;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace KatanaConsole
{
    using AppFunc = Func<IDictionary<string, object>, Task>;

    public class MiddlewareComponent
    {
        private AppFunc _next;
        public MiddlewareComponent(AppFunc next)
        {
            _next = next;
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            IOwinContext context = new OwinContext(environment);
            await context.Response.WriteAsync("<h1>Middleware 3 triggered</h1>");
            await _next.Invoke(environment);
        }
    }
}