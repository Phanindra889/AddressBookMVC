
using Domain.Services;
using Repository.Context;
using Repository.RepositoryFiles;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using System.ComponentModel;

namespace AddressBookasp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            /* builder.Services.AddScoped<IDapperContext,DapperContext>();
             builder.Services.AddScoped<IAddressBookActions,AddressBookActions>();
             builder.Services.AddScoped<IServices,ServiceActions>();*/
             var container = new SimpleInjector.Container();
           
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            container.Register<IDapperContext, DapperContext>(Lifestyle.Scoped);
            container.Register<IAddressBookActions, AddressBookActions>(Lifestyle.Scoped);
            container.Register<IServices,ServiceActions>(Lifestyle.Scoped);

            builder.Services.AddSimpleInjector(container,options =>
            {
                options.AddAspNetCore()
                .AddControllerActivation()
                .AddViewComponentActivation();
            }
            );
            
            var app = builder.Build();
            
            app.Services.UseSimpleInjector(container);
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
                pattern: "{controller=AddressBook}/{action=Index}/{id?}");

            app.Run();
        }
    }
}