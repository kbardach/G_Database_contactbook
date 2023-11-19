using ContactBook_Database.Models.Entities;
using System.Diagnostics;

namespace ContactBook_Database.Services.MenuService;

internal class SocialMediaMenuService
{
    private readonly SocialMediaService _socialMediaService;

    public SocialMediaMenuService(SocialMediaService socialMediaService)
    {
        _socialMediaService = socialMediaService;
    }

    internal async Task SocialMediaMenuAsync()
    {
        var exit = false;

        do
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" --- Hantera Sociala Medier Information --- ");
            Console.WriteLine("--------------------------------------------");
            Console.ResetColor();
            Console.WriteLine("1. Hämta alla användarnamn");
            Console.WriteLine("2. Hämta alla platformar");
            Console.WriteLine("0. Gå tillbaka");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Välj ett av ovanstående alternativ. (0-2): ");
            Console.WriteLine();
            Console.ResetColor();

            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    await GetAllUsernamesMenuAsync();
                    break;
                case "2":
                    await GetAllPlatformsMenuAsync();
                    break;

                case "0":
                    exit = true;
                    break;

                default:
                    break;

            }

        } while (exit == false);
    }

    internal async Task GetAllUsernamesMenuAsync()
    {
        try
        {
            var userNames = await _socialMediaService.GetAllUserNamesAsync();

            if (userNames != null)
            {
                Console.Clear();
                foreach (var userName in userNames)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(" -- Hämta alla användarnamn -- ");
                    Console.WriteLine("-------------------------------");
                    Console.ResetColor();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"ID: {userName.ContactId}");
                    Console.WriteLine($"användarnamn: {userName.Username}");
                    Console.WriteLine("---------------------------------------");
                    Console.WriteLine();
                    Console.ResetColor();
                }
            }
            else
            {
                Console.WriteLine("Inga användarnamn hittades.");
            }
            Console.WriteLine("Tryck på en valfri tangent för att återgå till menyn");
            Console.ReadKey();
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
    }

    internal async Task GetAllPlatformsMenuAsync()
    {
        try
        {
            var platforms = await _socialMediaService.GetAllPlatformsAsync();

            if (platforms != null)
            {
                Console.Clear();
                foreach (var platform in platforms)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(" -- Hämta alla användarnamn -- ");
                    Console.WriteLine("-------------------------------");
                    Console.ResetColor();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"ID: {platform.Id}");
                    Console.WriteLine($"Platform: {platform.Platform}");
                    Console.WriteLine("---------------------------------------");
                    Console.WriteLine();
                    Console.ResetColor();
                }
            }
            else
            {
                Console.WriteLine("Ingen platform hittades.");
            }
            Console.WriteLine("Tryck på en valfri tangent för att återgå till menyn");
            Console.ReadKey();
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
    }
}
