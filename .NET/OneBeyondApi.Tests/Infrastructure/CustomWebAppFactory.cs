using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OneBeyondApi.DataAccess;
using OneBeyondApi.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneBeyondApi.Tests.Infrastructure
{
    internal class CustomWebAppFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);

            var testProjectRootPath = Path.GetDirectoryName(GetType().Assembly.Location);
            builder.UseContentRoot(testProjectRootPath);
            builder.UseWebRoot(testProjectRootPath);

            builder.UseEnvironment(ApplicationEnvironment.Test);

            builder.ConfigureAppConfiguration(configurationBuilder =>
            {

            });

            builder.ConfigureServices(serviceCollection =>
            {

            });

            builder.ConfigureTestServices(async serviceCollection =>
            {
                using var serviceProvider = serviceCollection.BuildServiceProvider();
                using var scope = serviceProvider.CreateScope();

                serviceCollection.AddDbContext<LibraryContext>(options =>
                    options.UseInMemoryDatabase(databaseName: nameof(LibraryContext)));

                var context = scope.ServiceProvider.GetRequiredService<LibraryContext>();
                await context.Database.EnsureDeletedAsync();
                await context.Database.EnsureCreatedAsync();
            });
        }
    }
}
