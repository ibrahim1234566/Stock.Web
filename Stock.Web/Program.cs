using Microsoft.EntityFrameworkCore;
using Stock.Data.Context;
using Stock.Repository.Interfaces;
using Stock.Repository.Repositories;
using Stock.Service.Interfaces;
using Stock.Service.Services;

namespace Stock.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<StockDbContext>(op =>
op.UseSqlServer(builder.Configuration.GetConnectionString("myconn")));

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddScoped<IStoreService, StoreService>();
            builder.Services.AddScoped<IItemService, ItemService>();
            builder.Services.AddScoped<IStoreItemService, StoreItemService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Main}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
