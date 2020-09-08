using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PatientLibrary;
using PatientRepository;
using WebApplication1.Controllers;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WebApplication1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public static string ConnectionString
        {
            get;
            private set;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors(o => o.AddPolicy("MyCorsPolicy",builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyMethod();
                builder.AllowAnyHeader();

            }
            ));

            services.AddControllersWithViews();



            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>

            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "Guru",
                    ValidAudience = "Guru",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Guru@99986867Guru@9999999"))

            };

            }
            );



            services.AddDistributedMemoryCache();
            services.AddAutoMapper(typeof(Startup));
            // services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IPatient, PatientAddress>();
            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddSingleton<IConfiguration>(Configuration);
            
            //services.AddScoped<IPatient>((ctx) =>
            //{

            //    IHttpContextAccessor con = ctx.GetService<IHttpContextAccessor>();
            //    string _address = con.HttpContext.Request.Form["txtPatientAddress"].ToString();

            //    if (_address == "")
            //    {
            //        return new Patient();
            //    }
            //    else
            //    {
            //        return new PatientAddress();
            //    }


            //});

            var conn = Configuration.GetConnectionString("myDb");
            services.AddDbContext<PatientDBContext>(options => options.UseSqlServer(conn,b=>b.MigrationsAssembly("PatientRepository")));
            
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(20);//You can set Time   
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors("MyCorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
