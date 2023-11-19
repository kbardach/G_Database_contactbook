using ContactBook_Database.Models.Entities;
using System.Diagnostics;

namespace ContactBook_Database.Services.MenuService;

internal class ContactInformationMenuService
{
    private readonly ContactInformationService _contactInformationService;

    public ContactInformationMenuService(ContactInformationService contactInformationService)
    {
        _contactInformationService = contactInformationService;
    }

    internal async Task ContactInformationMenuAsync()
    {
        var exit = false;

        do
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" --- Hantera kontaktinformation --- ");
            Console.WriteLine("------------------------------------");
            Console.ResetColor();
            Console.WriteLine("1. Hämta en uppgift om kontaktinformation");
            Console.WriteLine("2. Hämta alla uppgifter om kontaktinformation");
            Console.WriteLine("3. Uppdatera kontaktinformation");
            Console.WriteLine("4. Radera kontaktinformation");
            Console.WriteLine("0. Gå tillbaka");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Välj ett av ovanstående alternativ. (0-4): ");
            Console.ResetColor();

            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    await GetOneContactInfoMenuAsync();
                    break;

                case "2":
                    await GetAllContactInfoMenuAsync();
                    break;

                case "3":
                    await UpdateContactInfoMenuAsync();
                    break;

                case "4":
                    await DeleteContactInfoMenuAsync();
                    break;

                case "0":
                    exit = true;
                    break;

                default:
                    break;

            }

        } while (exit == false);
    }

    internal async Task GetOneContactInfoMenuAsync()
    {

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(" -- Hämta en kontaktinformation -- ");
        Console.WriteLine("-----------------------------------");
        Console.ResetColor();
        Console.Write("Ange Kontatktinformationens ID: ");
        if (int.TryParse(Console.ReadLine(), out int Id))
        {
            try
            {
                var contactInfo = await _contactInformationService.GetOneContactInfoAsync(new ContactInformationEntity { Id = Id });
                if (contactInfo != null)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Clear();
                    Console.WriteLine($"Kontakt typ: {contactInfo.Type}");
                    Console.WriteLine($"Telefonnummer: {contactInfo.Value}");
                    Console.ResetColor();
                }
                else
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Ingen kontaktinformation hittades med det ID:et.");
                    Console.ResetColor();

                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
        }
        Console.ReadKey();
    }

    internal async Task GetAllContactInfoMenuAsync()
    {
        try
        {
            var contact = await _contactInformationService.GetAllContactInfoAsync();

            if (contact != null)
            {
                Console.Clear();
                foreach (var contacts in contact)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(" -- All kontaktinformation -- ");
                    Console.WriteLine("------------------------------");
                    Console.ResetColor();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"ID: {contacts.Id}");
                    Console.WriteLine($"Kontatkt typ: {contacts.Type}");
                    Console.WriteLine($"Telefonnummer: {contacts.Value}");
                    Console.WriteLine("------------------------------------------------");
                    Console.WriteLine();
                    Console.ResetColor();
                }
            }
            else
            {
                Console.WriteLine("Inga adresser hittades.");
            }
            Console.WriteLine("Tryck på en valfri tangent för att återgå till menyn");
            Console.ReadKey();
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

    }

    internal async Task<bool> UpdateContactInfoMenuAsync()
    {
        try
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" -- Uppdatera kontaktinformation-- ");
            Console.WriteLine("-----------------------------------");
            Console.ResetColor();
            Console.Write("Ange ID för kontaktinformationen som ska uppdateras: ");
            if (!int.TryParse(Console.ReadLine(), out int Id))
            {
                Console.WriteLine("Ogiltigt ID. Försök igen.");
                return false;
            }

            var updateContactInfo = await _contactInformationService.GetOneContactInfoAsync(new ContactInformationEntity { Id = Id });

            if (updateContactInfo != null)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Här är kontaktinformationen du vill uppdatera");
                Console.WriteLine("---------------------------------------------");
                Console.ResetColor();
                Console.WriteLine($"kontakt typ: {updateContactInfo.Type}");
                Console.WriteLine($"Telefonnummer: {updateContactInfo.Value}");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ange nya uppgifter där ändringar önskas. Lämna övriga fält tomma.");
                Console.ResetColor();
                Console.WriteLine();

                Console.Write("Ny kontakt typ: ");
                var updatedType = Console.ReadLine();
                if (!string.IsNullOrEmpty(updatedType)) updateContactInfo.Type = updatedType;

                Console.Write("Nytt telefonnummer: ");
                var updatedValue = Console.ReadLine();
                if (!string.IsNullOrEmpty(updatedValue)) updateContactInfo.Value = updatedValue;

                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Om ändringar har gjorts, så har kontaktinformationen uppdaterats!");
                Console.ResetColor();

                _contactInformationService.UpdateContactInfoAsync(updateContactInfo).Wait();
            }
            else
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine();
                Console.WriteLine("Kunde tyvärr inte hitta någon adress med det ID:et.");
                Console.ResetColor();
            }

            Console.ReadKey();
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return true;
    }

    internal async Task DeleteContactInfoMenuAsync()
    {
        try
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" -- Ta bort kontaktinformation -- ");
            Console.WriteLine("----------------------------------");
            Console.ResetColor();
            Console.Write("Ange ID för kontaktinformationen som ska tas bort: ");
            if (!int.TryParse(Console.ReadLine(), out int Id))
            {
                Console.WriteLine("Ogiltigt ID. Försök igen.");
                return;
            }

            var contactInfoToDelete = await _contactInformationService.GetOneContactInfoAsync(new ContactInformationEntity { Id = Id });

            if (contactInfoToDelete != null)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Gatunamn: {contactInfoToDelete.Type}");
                Console.WriteLine($"Gatunummer: {contactInfoToDelete.Value}");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Vill du ta bort denna kontaktinformation? (j/n): ");
                Console.ResetColor();
                var option = Console.ReadLine();

                if (option == "j")
                {
                    var delete = await _contactInformationService.DeleteContactInformationAsync(Id);
                    if (delete)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Kontaktinformationen har tagits bort, tryck på valfri tangent för att komma tillbaka till menyn..");
                    }
                    else
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Kunde inte ta bort kontaktinformationen PGA. relationer i tabellen.");
                        Console.WriteLine("tryck på valfri tangent för att komma tillbaka till menyn.");
                    }
                    Console.ResetColor();
                }
            }
            else
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Kunde inte hitta någon kontaktinformation med det ID:et.");
                Console.ResetColor();
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Ett fel inträffade vid försök att ta bort kontaktinformationen, kolla eventuella beroenden.");
            Console.ResetColor();
        }

        Console.ReadKey();
    }
}
