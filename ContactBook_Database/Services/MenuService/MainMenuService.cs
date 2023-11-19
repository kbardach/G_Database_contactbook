namespace ContactBook_Database.Services.MenuService;

internal class MainMenuService
{
    private readonly AddressMenuService _addressMenuService;
    private readonly ContactInformationMenuService _contactInformationMenuService;
    private readonly ContactMenuService _contactMenuService;
    private readonly SocialMediaMenuService _socialMediaMenuService;


    public MainMenuService(AddressMenuService addressMenuService, ContactInformationMenuService contactInformationMenuService, ContactMenuService contactMenuService, SocialMediaMenuService socialMediaMenuService)
    {
        _addressMenuService = addressMenuService;
        _contactInformationMenuService = contactInformationMenuService;
        _contactMenuService = contactMenuService;
        _socialMediaMenuService = socialMediaMenuService;
    }

    internal async Task MainMenuAsync()
    {
            var exit = false;

            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(" --- Kontaktbok --- ");
                Console.WriteLine("--------------------");
                Console.ResetColor();
                Console.WriteLine("1. Lägg till kontakt & hantera kontakt");
                Console.WriteLine();
                Console.WriteLine("2. Hantera Adresser");
                Console.WriteLine("3. Hantera Kontaktinformation");
                Console.WriteLine("4. Hantera Sociala Medier");
                Console.WriteLine("0. Avsluta");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Välj ett av ovanstående alternativ. (0-4): ");
                Console.ResetColor();

                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                    await _contactMenuService.ContactMenuAsync(); 
                        break;

                    case "2":
                    await _addressMenuService.AddressMenuAsync();
                    break;

                    case "3":
                    await _contactInformationMenuService.ContactInformationMenuAsync();
                    break;

                    case "4":
                    await _socialMediaMenuService.SocialMediaMenuAsync();
                    break;

                    case "0":
                        exit = true;
                        break;

                    default:
                        break;

                }

            } while (exit == false);
    }

}
