using ContactBook_Database.Models.Entities;
using ContactBook_Database.Repositories;
using ContactBook_Database.Services;
using ContactBook_Database.Services.MenuService;
using Kontaktbok_Databas.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
/*
    hur gör man om man vill radera/VISA en hel kontakt inkl. alla beroenden?
    SVAR: gör en .Include med en QUERY metod

    I DELETE DELARNA LÄGG TILL ETT MEDDELANDE ATT DET INTE GÅR PGA BEROENDEN


    Frågor:
    -------
    Behövs det try catch i ContactMenuService
    kolla igenom contactMenuService
    hämtar adresser på id?
*/

namespace ContactBook_Database;

class Program
{
    static async Task Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices(services =>
            {
                services.AddDbContext<DataContext>(x => x.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\kimba\OneDrive\Desktop\Skola\Nackademin-Webbutvecklare.NET\DatabasTeknik\01_ExaminerandeUppgift\ContactBook_Database\ContactBook_Database\ContactBook_Database\Contexts\assignment_database.mdf;Integrated Security=True;Connect Timeout=30"));

                //dom här kan behövas ändras till -----> services.AddScoped<AddressRepsository>();

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

        // HÄR STARTAR MAN SIN MENY!!!!!!!


        // ---TEST---
        //await TestCreateEntitiesAsync(host.Services);       

        var serviceScope = host.Services.CreateScope();
        var services = serviceScope.ServiceProvider;
        var menuService = services.GetRequiredService<MainMenuService>();

        await menuService.MainMenuAsync();

        await host.RunAsync();          // TA INTE BORT DENNA
    }



    //------------------------------------------------------------------------------------------------------------------------------------
    //                  TEST FÖR ATT KOLLA OM INFO LÄGGS TILL I TABELLERNA
    //------------------------------------------------------------------------------------------------------------------------------------


    //private static async Task TestCreateEntitiesAsync(IServiceProvider serviceProvider)
    //{
    //    using var serviceScope = serviceProvider.CreateScope();
    //    var services = serviceScope.ServiceProvider;

    //    var addressService = services.GetRequiredService<AddressService>();
    //    var contactService = services.GetRequiredService<ContactService>();
    //    var contactInfoService = services.GetRequiredService<ContactInformationService>();
    //    var socialMediaService = services.GetRequiredService<SocialMediaService>();


    //    // Skapa en adress
    //    var address = new AddressEntity { StreetName = "Karlbergsvägen", StreetNumber = "40", PostalCode = "11327", City = "Stockholm" };
    //    var createdAddress = await addressService.CreateAddressAsync(address);

    //    // Skapa en kontakt
    //    var contact = new ContactEntity { FirstName = "Kim", LastName = "Bardach", Email = "kimbardach@live.se", AddressId = createdAddress.Id };
    //    var createdContact = await contactService.CreateContactAsync(contact);

    //    // Skapa kontaktinformation
    //    var contactInfo = new ContactInformationEntity { Type = "Mobil", Value = "0733912231", ContactId = createdContact.Id };
    //    var createdContactInfo = await contactInfoService.CreateContactInfoAsync(contactInfo);

    //    // Skapa en social media plattform
    //    var platform = new SocialMediaPlatformEntity { Platform = "Instagram" };
    //    var createdPlatform = await socialMediaService.CreateSocialMediaPlatformAsync(platform);

    //    // Skapa ett social media användarnamn
    //    var userName = new SocialMediaUserNameEntity { ContactId = createdContact.Id, SocialMediaPlatformId = createdPlatform.Id, Username = "baardach" };
    //    var createdUserName = await socialMediaService.CreateSocialMediaUserName(userName);

    //    // resultat
    //    Console.WriteLine($"Adress skapad: {createdAddress.StreetName}, Kontakt skapad: {createdContact.FirstName}, Kontaktinfo skapad: {createdContactInfo.Type}, Plattform skapad: {createdPlatform.Platform}, Användarnamn skapad: {createdUserName.Username}");
    //}

}