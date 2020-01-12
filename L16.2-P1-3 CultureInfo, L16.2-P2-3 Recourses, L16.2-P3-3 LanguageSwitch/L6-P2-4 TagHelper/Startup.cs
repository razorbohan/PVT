using System.Text;
using System.Globalization;
using FluentValidation;
using FluentValidation.AspNetCore;
using L6_P2_4_TagHelper.DAL.Models;
using L6_P2_4_TagHelper.DAL;
using L6_P2_4_TagHelper.Infrastructure;
using L6_P2_4_TagHelper.Logic;
using L6_P2_4_TagHelper.Middleware;
using L6_P2_4_TagHelper.ViewModel;
using L6_P2_4_TagHelper.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Localization;

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
                 .AddJwtBearer(options =>
                 {
                     options.TokenValidationParameters = new TokenValidationParameters
                     {
                         //ValidateIssuer = true,
                         //ValidateAudience = true,
                         ValidateLifetime = true,
                         ValidateIssuerSigningKey = true,

                         ValidIssuer = Configuration["JwtAuthentication:ValidIssuer"],
                         ValidAudience = Configuration["JwtAuthentication:ValidAudience"],
                         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtAuthentication:SecurityKey"]))
                     };
                 });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Plus18", policy => policy.RequireClaim("Plus18"));
                options.AddPolicy("Female", policy => policy.RequireClaim("Female"));
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

            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddMvc()
                    //.AddFluentValidation(fv => { fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false; })
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                    .AddDataAnnotationsLocalization()
                    .AddViewLocalization();

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en"),
                    new CultureInfo("ru")
                };

                options.DefaultRequestCulture = new RequestCulture("ru");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            //services.AddTransient<IValidator<VoteViewModel>, VoteViewValidator>();

            services.AddDefaultIdentity<User>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 1;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<UserContext>();

            services.Configure<JwtAuthentication>(Configuration.GetSection("JwtAuthentication"));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
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

            var supportedCultures = new[]
            {
                new CultureInfo("en-US"),
                new CultureInfo("en-GB"),
                new CultureInfo("en"),
                new CultureInfo("ru-RU"),
                new CultureInfo("ru")
            };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("ru-RU"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

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

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}
