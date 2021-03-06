﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace ONWServices
{
    public class Program
    {
        public static void Main(string[] args)
        {
			//testing branching
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
