using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RGAttendanceService_V00.DAL;
using RGAttendanceService_V00.DAL.CRUD;
using RGAttendanceService_V00.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace RGAttendanceService_V00
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

            services.AddMvc().AddSessionStateTempDataProvider();
            services.AddSession();

            //Authentication
            services.AddAuthentication("CookieAuthentication")
                .AddCookie("CookieAuthentication", config =>
                {
                    config.Cookie.HttpOnly = true;
                    config.Cookie.SecurePolicy = Microsoft.AspNetCore.Http.CookieSecurePolicy.None;
                    config.Cookie.Name = "UserLoginCookie";
                    config.LoginPath = "/Login/RGLogin";
                    config.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict;
                });

            //Entity Framework
            services.AddDbContext<ParentContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("RGAttendanceService"));
            });

            services.AddRazorPages(options => {
                options.Conventions.AuthorizeFolder("/ParticipantManage");
                options.Conventions.AuthorizeFolder("/GroupManage");
                options.Conventions.AuthorizeFolder("/CoachManage");
                options.Conventions.AuthorizePage("/AttendanceCheck");
                options.Conventions.AuthorizePage("/AttendanceCheckUp");
            });
            
            //ADO net interfaces
            services.Add(new ServiceDescriptor(typeof(IParticipant), new ParticipantSQLDB(Configuration)));
            
            services.Add(new ServiceDescriptor(typeof(IGroup), new GroupSQLDB(Configuration)));
            services.Add(new ServiceDescriptor(typeof(ICoach), new CoachSQLDB(Configuration)));
            services.Add(new ServiceDescriptor(typeof(IUser), new UserSQLDB(Configuration)));
            services.Add(new ServiceDescriptor(typeof(IAttendance), new AttendanceSQLDB(Configuration)));
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
