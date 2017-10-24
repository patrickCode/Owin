using System;
using Microsoft.Owin;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace KatanaConsole
{
    using AppFunc = Func<IDictionary<string, object>, Task>;

    public class MiddlewareGreetingComponent
    {
        private AppFunc _next;
        private string _message;
        public MiddlewareGreetingComponent(AppFunc next, string message)
        {
            _next = next;
            _message = message;
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            IOwinContext context = new OwinContext(environment);
            await context.Response.WriteAsync(_message);
            await _next.Invoke(environment);
        }
    }
}