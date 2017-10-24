using Owin;

namespace KatanaConsole
{
    public static class AppBuilderExtensions
    {
        public static void UseGreetingsMiddleware(this IAppBuilder app, string message)
        {
            app.Use<MiddlewareGreetingComponent>(message);
        }
    }
}