using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TKDprogress_BLL.Interfaces;
using TKDprogress_BLL.Services;
using TKDprogress_DAL.Data;
using TKDprogress_DAL.Repositories;
using TKDprogress_SL.Interfaces;

namespace TKDprogress
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(
                    connectionString,
                    new MySqlServerVersion(new Version(10, 4, 22)),
                    mySqlOptions => mySqlOptions.MigrationsAssembly("TKDprogress")
                ));

            builder.Services.AddScoped<ApplicationDbContext>();

            builder.Services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>() // Specify the role type explicitly
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

            builder.Services.AddScoped<ITerminologyService, TerminologyService>();
            builder.Services.AddScoped<ITerminologyRepository, TerminologyRepository>();

            builder.Services.AddScoped<IUserCategoryService, UserCategoryService>();
            builder.Services.AddScoped<IUserCategoryRepository, UserCategoryRepository>();

            builder.Services.AddScoped<ICategoryTerminologyService, CategoryTerminologyService>();
            builder.Services.AddScoped<ICategoryTerminologyRepository, CategoryTerminologyRepository>();

            builder.Services.AddScoped<IMovementService, MovementService>();
            builder.Services.AddScoped<IMovementRepository, MovementRepository>();

            builder.Services.AddScoped<ITulService, TulService>();
            builder.Services.AddScoped<ITulRepository, TulRepository>();

            builder.Services.AddScoped<ITulMovementService, TulMovementService>();
            builder.Services.AddScoped<ITulMovementRepository, TulMovementRepository>();

            builder.Services.AddScoped<IUserTulService, UserTulService>();
            builder.Services.AddScoped<IUserTulRepository, UserTulRepository>();

            builder.Services.AddScoped<IStatusRepository, StatusRepository>();
            builder.Services.AddScoped<IStatusService, StatusService>();

            builder.Services.AddScoped<IPercentageCalculationService, PercentageCalculationService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
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

            app.UseAuthorization();

            app.MapAreaControllerRoute(
                name: "MyAreaAdmin",
                areaName: "Admin",
                pattern: "Admin/{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}