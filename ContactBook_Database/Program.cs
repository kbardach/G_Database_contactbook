using ContactBook_Database.Models.Entities;
using ContactBook_Database.Repositories;
using ContactBook_Database.Services;
using ContactBook_Database.Services.MenuService;
using Kontaktbok_Databas.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace ContactBook_Database;

class Program
{
    static async Task Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices(services =>
            {
                services.AddDbContext<DataContext>(x => x.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\kimba\OneDrive\Desktop\Skola\Nackademin-Webbutvecklare.NET\DatabasTeknik\01_ExaminerandeUppgift\ContactBook_Database\ContactBook_Database\ContactBook_Database\Contexts\assignment_database.mdf;Integrated Security=True;Connect Timeout=30"));


                services.AddScoped<Repo<AddressEntity>, AddressRepsository>();
                services.AddScoped<Repo<ContactInformationEntity>, ContactInformationRepository>();
                services.AddScoped<Repo<ContactEntity>, ContactRepository>();
                services.AddScoped<Repo<SocialMediaPlatformEntity>, SocialMediaPlatformRepository>();
                services.AddScoped<Repo<SocialMediaUserNameEntity>, SocialMediaUserNameRepository>();

                services.AddScoped<AddressService>();
                services.AddScoped<ContactInformationService>();
                services.AddScoped<ContactService>();
                services.AddScoped<SocialMediaService>();

                services.AddScoped<AddressMenuService>();
                services.AddScoped<ContactInformationMenuService>();
                services.AddScoped<ContactMenuService>();
                services.AddScoped<MainMenuService>();
                services.AddScoped<SocialMediaMenuService>();
            })
            .Build();


        var serviceScope = host.Services.CreateScope();
        var services = serviceScope.ServiceProvider;
        var menuService = services.GetRequiredService<MainMenuService>();

        await menuService.MainMenuAsync();

        await host.RunAsync();       
    }

}