using BusinessLayer.Abstract;
using BusinessLayer.Concrete;

using DataAcessLayer.Abstract;
using DataAcessLayer.Concrete;
using DataAcessLayer.Concrete.EfCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PresentationLayer.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using Microsoft.AspNetCore.Identity.UI.Services;
namespace PresentationLayer
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
            services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>()//datalarý burda sakla
                .AddDefaultTokenProviders(); //parola restte benzersýz sayý gonder

            services.Configure<IdentityOptions>(options =>
            {//identýty ýle ýlgýlý ekstra ýslemler

                //password

                options.Password.RequireDigit = true;  //mutlaka sayý
                options.Password.RequiredLength = 6;   //min 6 hane
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;  //bharf zorunlu degýl

                options.Lockout.MaxFailedAccessAttempts = 5; //kere yanlýþ gýrme hakký var
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); //5dk kullanýcý gýrýs yapamýcak
                options.User.RequireUniqueEmail =true; //önceden gýrýs yaptýysa ayný maýlle giriþ yapmz
              
                options.SignIn.RequireConfirmedEmail = false; //lognicn onay olmal

            });
            //cookie logýn gerceklesýnce serverde sessýon tanýr
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.AccessDeniedPath = "/Account/Accessdenied"; //gýrýs hakký yoksa
                options.ExpireTimeSpan = TimeSpan.FromHours(1);   // boyunca tarayýcýda saklanaýr cookie
                options.SlidingExpiration = true; //giriþden býr sure sonra coocký býter
                options.Cookie = new CookieBuilder
                {
                    HttpOnly = true,   // tarauýcý scriptelr coockýe okuyamz
                    Name = ".ShopApp.Security.Cookie",
                    SameSite = SameSiteMode.Strict   //cross-site ataklarını engeller(baska býr kullanýcý býzým cooký alýp servera gonderemz)

                };

            });

            services.AddControllersWithViews();
            services.AddScoped<IProductDal, EfCoreProductDal>();
            services.AddScoped<ICategoryDal, EfCoreCategoryDal>();
            services.AddScoped<ICartDal, EfCoreCartDal>();
            services.AddScoped<IProductService, ProductManager>();
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<ICartService, CartManager>();

  

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,UserManager<ApplicationUser> userManager ,RoleManager<IdentityRole> roleManager)
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
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseAuthentication();


            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
              name: "products",
              pattern: "products/{category?}",
              defaults: new { controller = "Shop", Action = "List" }
                  );


                endpoints.MapControllerRoute(
                name: "adminProducts",
                pattern: "Admin/products",
                defaults: new { controller = "Admin", Action = "ProductList" }  
                    );
                endpoints.MapControllerRoute(
              name: "adminProducts",
              pattern: "admin/products/{id?}",
              defaults: new { controller = "Admin", Action = "EditProduct" }
                  );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });



            SeedIdentity.Seed(userManager, roleManager, Configuration).Wait();
        }
    }
}
