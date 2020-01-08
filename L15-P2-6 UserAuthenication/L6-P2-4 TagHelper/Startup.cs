using L6_P2_4_TagHelper.Models;
using FluentValidation;
using FluentValidation.AspNetCore;
using L6_P2_4_TagHelper.DAL;
using L6_P2_4_TagHelper.Infrastructure;
using L6_P2_4_TagHelper.Logic;
using L6_P2_4_TagHelper.Middleware;
using L6_P2_4_TagHelper.ViewModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace L6_P2_4_TagHelper
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PartyContext>(options => options.UseSqlServer(Configuration.GetConnectionString("PartyDB")));
            services.AddDbContext<UserContext>(options => options.UseSqlServer(Configuration.GetConnectionString("PartyDB")));

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new PathString("/Account/Login");
                });

            services.AddTransient<IParticipantRepository, ParticipantRepository>();
            services.AddTransient<IPartyRepository, PartyRepository>();
            services.AddTransient<IPartyService, PartyService>();
            services.AddTransient<ILogger, Logger>();

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddSession();

            services
                .AddMvc()
                .AddFluentValidation(fv => { fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false; })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddRazorPagesOptions(options =>
                {
                    options.AllowAreas = true;
                    options.Conventions.AuthorizeAreaFolder("Identity", "/Account/Manage");
                    options.Conventions.AuthorizeAreaPage("Identity", "/Account/Logout");
                });
            services.AddTransient<IValidator<VoteViewModel>, VoteViewValidator>();

            services.AddDefaultIdentity<User>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 1;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<UserContext>();
            //services.AddIdentity<User, Microsoft.AspNetCore.Identity.IdentityRole>()
            //        .AddEntityFrameworkStores<UserContext>();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/Identity/Account/Login";
                options.LogoutPath = $"/Identity/Account/Logout";
                options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            //app.UseTraceMiddleware();

            app.UseExportMiddleware();

            app.UseSession();

            app.UseAuthentication();

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
