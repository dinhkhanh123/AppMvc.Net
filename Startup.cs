using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using App.Services;
using DoAnLapTrinhWebNC.Data;
using DoAnLapTrinhWebNC.ExtendMethods;
using DoAnLapTrinhWebNC.Models;
using DoAnLapTrinhWebNC.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DoAnLapTrinhWebNC
{
    public class Startup
    {
        public static string ContentRootPath { set; get; }
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            ContentRootPath = env.ContentRootPath;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
          {
              string connectString = Configuration.GetConnectionString("AppMvcConnectionString");
              options.UseSqlServer(connectString);
          });
            // Dang ky Identity
            services.AddIdentity<AppUser, IdentityRole>()
                    .AddEntityFrameworkStores<AppDbContext>()
                    .AddDefaultTokenProviders();

            // Truy cập IdentityOptions
            services.Configure<IdentityOptions>(options =>
            {
                // Thiết lập về Password
                options.Password.RequireDigit = false; // Không bắt phải có số
                options.Password.RequireLowercase = false; // Không bắt phải có chữ thường
                options.Password.RequireNonAlphanumeric = false; // Không bắt ký tự đặc biệt
                options.Password.RequireUppercase = false; // Không bắt buộc chữ in
                options.Password.RequiredLength = 3; // Số ký tự tối thiểu của password
                options.Password.RequiredUniqueChars = 1; // Số ký tự riêng biệt
                // Cấu hình Lockout - khóa user
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // Khóa 5 phút
                options.Lockout.MaxFailedAccessAttempts = 3; // Thất bại 3 lầ thì khóa
                options.Lockout.AllowedForNewUsers = true;
                // Cấu hình về User.
                options.User.AllowedUserNameCharacters = // các ký tự đặt tên user
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;  // Email là duy nhất

                // Cấu hình đăng nhập.
                options.SignIn.RequireConfirmedEmail = true;            // Cấu hình xác thực địa chỉ email (email phải tồn tại)
                options.SignIn.RequireConfirmedPhoneNumber = false;     // Xác thực số điện thoại
                options.SignIn.RequireConfirmedAccount = true;

            });

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/login/";
                options.LogoutPath = "/logout/";
                options.AccessDeniedPath = "/khongduoctruycap.html";
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("ViewManageMenu", builder =>
                {
                    builder.RequireAuthenticatedUser();
                    builder.RequireRole(RoleName.Administrator);
                });
            });

            services.AddAuthentication()
                    .AddGoogle(options =>
                    {
                        var gconfig = Configuration.GetSection("Authentication:Google");
                        options.ClientId = gconfig["ClientId"];
                        options.ClientSecret = gconfig["ClientSecret"];
                        // https://localhost:5001/signin-google
                        options.CallbackPath = "/dang-nhap-tu-google";
                    })
                    .AddFacebook(options =>
                    {
                        var fconfig = Configuration.GetSection("Authentication:Facebook");
                        options.AppId = fconfig["AppId"];
                        options.AppSecret = fconfig["AppSecret"];
                        options.CallbackPath = "/dang-nhap-tu-facebook";
                    })
                    // .AddTwitter()
                    // .AddMicrosoftAccount()
                    ;

            services.AddOptions();
            var mailsetting = Configuration.GetSection("MailSettings");
            services.Configure<MailSettings>(mailsetting);
            services.AddSingleton<IEmailSender, SendMailService>();

            services.AddSingleton<IdentityErrorDescriber, AppIdentityErrorDescriber>();

            services.AddControllersWithViews();

            services.AddRazorPages();
            // services.AddTransient(typeof (ILogger<>),typeof (Logger<>));
            services.Configure<RazorViewEngineOptions>(options =>
            {
                // /MyView/Controller/Action.cshtml
                // {0} -> Action
                // {1} -> Controller
                // {2} -> Area

                options.ViewLocationFormats.Add("/MyView/{1}/{0}.cshtml");
            });
            //services.AddSingleton<ProductService>();
            //services.AddSingleton<ProductService,ProductService>();
            // services.AddSingleton(typeof(ProductService));
            services.AddSingleton(typeof(ProductService), typeof(ProductService));
            services.AddSingleton<PlanetService>();

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
            app.UseStaticFiles(); //truy cap file tinh
            app.AddStatusCodePage();  // Tuy bien respose loi 400-599

            app.UseRouting();

            app.UseAuthentication(); // xac dinh danh tinh
            app.UseAuthorization();// xac thuc quyen truy cap

            app.UseEndpoints(endpoints =>
            {
                // endpoints.MapControllers()
                // endpoints.MapControllerRoute()
                // endpoints.MapAreaControllerRoute()
                // endpoints.MapDefaultControllerRoute()

                // [Route]
                // endpoints.MapControllers();

                endpoints.MapAreaControllerRoute(
                    name: "product",
                    pattern: "/{controller}/{action=Index}/{id?}",
                    areaName: "ProductManage"
                );

                // endpoints.MapControllerRoute(
                //     name: "firstroute",
                //     pattern: "{controller=Home}/{action=Index}/{id?}"
                // );

                // /xemsanpham/2
                // endpoints.MapControllerRoute(
                //    name: "first",
                //    pattern: "{url:regex(^((xemsanpham)|(viewproduct))$)}/{id:range(1,4)}",
                //    defaults: new
                //    {
                //        controller = "First",
                //        action = "ViewProduct"
                //    },
                //    constraints: new
                //    {
                //        //    url = "xemsanpham"
                //        //    url = new RegexRouteConstraint(@"^((xemsanpham)|(viewproduct))$")
                //        //    id = new RangeRouteConstraint(1,4)
                //    }

                // );

                //controller khong co area
                endpoints.MapControllerRoute(
                   name: "default",
                   pattern: "{controller=Home}/{action=Index}/{id?}");



                // /sayhi
                // endpoints.MapGet("/sayhi", async content =>
                // {
                //     await content.Response.WriteAsync($"Hello bay gio la {DateTime.Now}");
                // });

                // Pages Razor page
                //endpoints.MapRazorPages();
            });


        }
    }
}
