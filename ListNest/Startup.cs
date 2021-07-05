using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PotatoServer;
using PotatoServer.Filters.HandleException;
using PotatoServer.Filters.HandleExceptionHub;
using ListNest.Database;
using ListNest.Hubs;
using ListNest.Services.Implementations;
using ListNest.Services.Interfaces;
using System;
using Microsoft.AspNetCore.Identity;
using PotatoServer.Database;
using ListNest.Database.Models;

namespace ListNest
{
    public class Startup : BaseStartup
    {
        public override IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) : base(configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public override void ConfigureServices(IServiceCollection services)
        {
            services.SetupCors("http://localhost:3000");
            services.AddControllers();
            services.AddControllersWithViews(options =>
            {
                options.Filters.Add(typeof(HandleExceptionFilterAttribute));
            });
            services.AddAutoMapper(typeof(Startup));
            services.AddSignalR(hubOptions =>
            {
                hubOptions.AddFilter<HandleExceptionHubFilter>();
                hubOptions.EnableDetailedErrors = IsDevelopement;
            });
            services.SetupIdentity<ListNestUser, ListNestDbContext>(Configuration);
            services.SetupAuthentication(Configuration);
            //services.AddSingleton<IUserIdProvider, EmailBasedUserIdProvider>();
            services.AddDbContext<ListNestDbContext>(options =>
                options.EnableSensitiveDataLogging(IsDevelopement)
                 .UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                builder => builder.MigrationsAssembly("ListNest")));

            services.AddTransient<IListService, ListService>();
            services.AddTransient<IListItemService, ListItemService>();
            base.ConfigureServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ListNestHub>("/hubs/lists");
            });

            var userManager = serviceProvider.GetService<UserManager<ListNestUser>>();
            DatabaseSeeder.AddAdminAsync(userManager, "admin@admin.pl", "Admin", "Admin").GetAwaiter().GetResult();

            base.Configure(app, env, serviceProvider);
        }
    }
}
