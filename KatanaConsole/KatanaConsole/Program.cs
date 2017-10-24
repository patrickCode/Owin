using System;
using Microsoft.Owin.Hosting;

namespace KatanaConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            WebApp.Start<Startup>("http://localhost:8080");
            Console.WriteLine("Server Started. Press any key to quit");
            Console.ReadLine();
        }
    }
}