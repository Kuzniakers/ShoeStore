using Microsoft.EntityFrameworkCore;
using ShoeStore.Data;
using ShoeStore;


namespace ShoeStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            var builder = WebApplication.CreateBuilder(args);
            
            builder.Services.AddRazorPages();
            builder.Services.AddDbContext<ShoeStoreContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();
            var host = CreateHostBuilder(args).Build();
            host.Run();
         
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");     
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.MapRazorPages();
            app.Run();
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
        });

    }
}
