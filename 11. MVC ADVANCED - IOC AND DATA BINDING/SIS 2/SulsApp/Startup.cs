using SIS.Http;
using SIS.Http.Logging;
using SIS.MvcFramework;
using SulsApp.Controllers;
using SulsApp.Services;
using System;
using System.Collections.Generic;

namespace SulsApp
{
    public class Startup : IMvcApplication
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.Add<IUsersService, UsersService>();
            serviceCollection.Add<IProblemService, ProblemService>();
        }

        public void Configure(IList<Route> routeTable)
        {
            var db = new ApplicationDbContext();
            db.Database.EnsureCreated();
        }
    }
}
