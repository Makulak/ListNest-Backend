using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PotatoServer;
using PotatoServer.Database.Models;
using PotatoServer.Filters.HandleException;
using ShoppingListApp.Database;
using ShoppingListApp.Hubs;
using ShoppingListApp.Services.Implementations;
using ShoppingListApp.Services.Interfaces;

namespace ShoppingListApp
{
    public class Startup : BaseStartup
    {
        public Startup(IConfiguration configuration) : base(configuration)
        {
            Configuration = configuration;
        }

        public override IConfiguration Configuration { get; }

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
            services.AddSignalR();
            services.SetupIdentity<User, ShoppingListDbContext>(Configuration);
            services.SetupAuthentication(Configuration);
            services.AddDbContext<ShoppingListDbContext>(o => o.EnableSensitiveDataLogging(IsDevelopement)
            .UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("ShoppingList")));

            services.AddTransient<IShoppingListService, ShoppingListService>();
            base.ConfigureServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
                endpoints.MapHub<ShoppingListHub>("/hubs/shopping-list");
            });
            base.Configure(app, env);
        }
    }
}
