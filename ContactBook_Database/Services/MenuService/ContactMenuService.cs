using ContactBook_Database.Models;
using ContactBook_Database.Models.Entities;
using System.Diagnostics;

namespace ContactBook_Database.Services.MenuService;

internal class ContactMenuService
{
    private readonly AddressService _addressService;
    private readonly ContactInformationService _contactInformationService;
    private readonly ContactService _contactService;
    private readonly SocialMediaService _socialMediaService;

    public ContactMenuService(AddressService addressService, ContactInformationService contactInformationService, ContactService contactService, SocialMediaService socialMediaService)
    {
        _addressService = addressService;
        _contactInformationService = contactInformationService;
        _contactService = contactService;
        _socialMediaService = socialMediaService;
    }

    internal async Task ContactMenuAsync()
    {
        try
        {

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        var exit = false;

        do
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" --- Lägg till kontakt --- ");
            Console.WriteLine("---------------------------");
            Console.ResetColor();
            Console.WriteLine("1. Lägg till en kontakt");
            Console.WriteLine();
            Console.WriteLine("2. Hämta en kontakt");
            Console.WriteLine("3. Hämta alla kontakter");
            Console.WriteLine("4. Uppdatera en kontakt");
            Console.WriteLine("5. Radera en kontakt");
            Console.WriteLine("0. Gå tillbaka");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Välj ett av ovanstående alternativ. (0-5): ");
            Console.WriteLine();
            Console.ResetColor();

            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    await CreateContactAsync();
                    break;

                case "2":
                    await GetOneContactMenuAsync();
                    break;

                case "3":
                    await GetAllContactsMenuAsync();
                    break;

                case "4":
                    await UpdateContactMenuAsync();
                    break;

                case "5":
                    await DeleteContactMenuAsync();
                    break;

                case "0":
                    exit = true;
                    break;

                default:
                    break;

            }

        } while (exit == false);
    }

    private async Task CreateContactAsync()
    {
        try
        {
            var form = new ContactForm();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" -- Lägg till en kontakt -- ");
            Console.WriteLine("----------------------------");
            Console.ResetColor();
            Console.Clear();
            // Här läggs kontakt in 
            Console.WriteLine("Ange förnamn:");
            form.FirstName = Console.ReadLine()!;

            Console.WriteLine("Ange efternamn:");
            form.LastName = Console.ReadLine()!;

            Console.WriteLine("Ange e-post:");
            form.Email = Console.ReadLine()!;


            // Här läggs Address in
            Console.WriteLine("Ange gatuadress:");
            form.StreetName = Console.ReadLine()!;

            Console.WriteLine("Ange husnummer:");
            form.StreetNumber = Console.ReadLine()!;

            Console.WriteLine("Ange postnummer:");
            form.PostalCode = Console.ReadLine()!;

            Console.WriteLine("Ange stad:");
            form.City = Console.ReadLine()!;


            //Här läggs kontaktInfo in
            Console.WriteLine("Ange mobiltelefon / arbets telefon:");
            form.ContactType = Console.ReadLine()!;

            Console.WriteLine("Ange telefonnummer:");
            form.PhoneNumber = Console.ReadLine()!;


            //Här läggs platform in
            Console.WriteLine("Ange social media plattform:");
            form.PlatformName = Console.ReadLine()!;


            //Här läggs Användarnamnet in
            Console.WriteLine("Ange användarnamn på social media:");
            form.PlatformUserName = Console.ReadLine()!;


            var addressEntity = await _addressService.CreateAddressAsync(new AddressEntity
            {
                StreetName = form.StreetName!,
                StreetNumber = form.StreetNumber!,
                PostalCode = form.PostalCode!,
                City = form.City!
            });

            if (addressEntity == null)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Kunde inte skapa en Kontakt och den tillhörande information, tryck på valfri tangent och försök igen..");
                Console.ResetColor();
                Console.ReadKey();
                return;
            }


            // Lägg till Contact
            var contactEntity = await _contactService.CreateContactAsync(new ContactEntity
            {
                FirstName = form.FirstName!,
                LastName = form.LastName!,
                Email = form.Email!,
                AddressId = addressEntity.Id 
            });
            if (contactEntity == null)
            {
                Console.WriteLine("Kunde inte skapa kontakt.");
                Console.ReadKey();
                return;
            }


            // Lägg till ContactInformation
            var contactInformationEntity = await _contactInformationService.CreateContactInfoAsync(new ContactInformationEntity
            {
                Type = form.ContactType!,
                Value = form.PhoneNumber!,
                ContactId = contactEntity!.Id
            });

            if (contactInformationEntity == null)
            {
                Console.WriteLine("Kunde inte skapa kontaktinformation.");
                Console.ReadKey();
                return;
            }



            // Lägger till en SocialMediaPlatform
            var socialMediaPlatformEntity = await _socialMediaService.CreateSocialMediaPlatformAsync(new SocialMediaPlatformEntity
            {
                Platform = form.PlatformName!
            });

            if (socialMediaPlatformEntity == null)
            {
                Console.WriteLine("Plattformen kunde inte skapas.");
                Console.ReadKey();
                return;
            }



            // Lägger till SocialMediaUserName        

            var socialMediaUserNameEntity = await _socialMediaService.CreateSocialMediaUserName(new SocialMediaUserNameEntity
            {
                Username = form.PlatformUserName!,
                ContactId = contactEntity.Id,
                SocialMediaPlatformId = socialMediaPlatformEntity!.Id
            });


            if (socialMediaUserNameEntity == null)
            {
                Console.WriteLine("Användarnamnet kunde inte skapas.");
                Console.ReadKey();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Clear();
            Console.WriteLine("Kontakten skapad, tryck på valfri tangent för att återgå till menyn");
            Console.ResetColor();

            Console.ReadKey();
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
    }

    internal async Task GetOneContactMenuAsync()
    {
        try
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" -- Hämta en kontakt -- ");
            Console.WriteLine("------------------------");
            Console.ResetColor();
            Console.Write("Ange kontaktens e-postadress: ");
            var email = Console.ReadLine();

            if (!string.IsNullOrEmpty(email))
            {
                var contact = await _contactService.GetOneContactAsync(new ContactEntity { Email = email });
                if (contact != null)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Clear();
                    Console.WriteLine($"ID: {contact.Id}");
                    Console.WriteLine($"Förnamn: {contact.FirstName}");
                    Console.WriteLine($"Efternamn: {contact.LastName}");
                    Console.WriteLine($"E-post: {contact.Email}");
                    Console.ResetColor();
                }
                else
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Ingen kontakt hittades med den e-postadressen.");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.WriteLine("Du måste fylla i en E-post.");
            }

            Console.ReadKey();
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

    }

    internal async Task GetAllContactsMenuAsync()
    {
        try
        {
            var contacts = await _contactService.GetAllContactsAsync();

            if (contacts != null)
            {
                Console.Clear();
                foreach (var contact in contacts)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(" -- Alla kontakter -- ");
                    Console.WriteLine("----------------------");
                    Console.ResetColor();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"ID: {contact.Id}");
                    Console.WriteLine($"Gatunamn: {contact.FirstName}");
                    Console.WriteLine($"Gatunummer: {contact.LastName}");
                    Console.WriteLine($"Postnummer: {contact.Email}");
                    Console.WriteLine("---------------------------------------");
                    Console.WriteLine();
                    Console.ResetColor();
                }
            }
            else
            {
                Console.WriteLine("Inga kontakter hittades.");
            }
            Console.WriteLine("Tryck på en valfri tangent för att återgå till menyn");
            Console.ReadKey();
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
    }

    internal async Task<bool> UpdateContactMenuAsync()
    {
        try
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" -- Uppdatera en kontakt -- ");
            Console.WriteLine("----------------------------");
            Console.ResetColor();
            Console.Write("Ange kontaktens e-postadress: ");
            var email = Console.ReadLine();

            if (string.IsNullOrEmpty(email))
            {
                Console.WriteLine("Ogiltig e-postadress. Försök igen.");
                return false;
            }

            var updateContact = await _contactService.GetOneContactAsync(new ContactEntity { Email = email });

            if (updateContact != null)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Här är kontakten du vill uppdatera");
                Console.WriteLine("----------------------------------");
                Console.ResetColor();
                Console.WriteLine($"Förnamn: {updateContact.FirstName}");
                Console.WriteLine($"Efternamn: {updateContact.LastName}");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ange nya uppgifter där ändringar önskas. Lämna övriga fält tomma.");
                Console.ResetColor();
                Console.WriteLine();

                Console.Write("Nytt förnamn: ");
                var updatedFirstName = Console.ReadLine();
                if (!string.IsNullOrEmpty(updatedFirstName)) updateContact.FirstName = updatedFirstName;

                Console.Write("Nytt efternamn: ");
                var updatedLastName = Console.ReadLine();
                if (!string.IsNullOrEmpty(updatedLastName)) updateContact.LastName = updatedLastName;

                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Om ändringar har gjorts, så har kontakten uppdaterats!");
                Console.ResetColor();

                await _contactService.UpdateContactAsync(updateContact);
            }
            else
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine();
                Console.WriteLine("Kunde tyvärr inte hitta någon kontakt med den e-postadressen.");
                Console.ResetColor();
            }

            Console.ReadKey();
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return true;

    }

    internal async Task DeleteContactMenuAsync()
    {
        try
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" -- Ta bort en kontakt -- ");
            Console.WriteLine("--------------------------");
            Console.ResetColor();
            Console.Write("Ange kontaktens e-postadress som ska tas bort: ");
            var email = Console.ReadLine();

            if (string.IsNullOrEmpty(email))
            {
                Console.WriteLine("Ogiltig e-postadress. Försök igen.");
            }

            var contactToDelete = await _contactService.GetOneContactAsync(new ContactEntity { Email = email! });

            if (contactToDelete != null)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(" -- Kontakten -- ");
                Console.WriteLine("-----------------");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Förnamn: {contactToDelete.FirstName}");
                Console.WriteLine($"Efternamn: {contactToDelete.LastName}");
                Console.WriteLine($"Email: {contactToDelete.Email}");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Vill du ta bort denna kontakt? (j/n): ");
                Console.ResetColor();
                var option = Console.ReadLine();

                if (option == "j")
                {
                    var delete = await _contactService.DeleteContactAsync(email!);
                    if (delete)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Adressen har tagits bort, tryck på valfri tangent för att komma tillbaka till menyn..");
                    }
                    else
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Kunde inte ta bort adressen PGA. relationer i tabellen.");
                        Console.WriteLine("tryck på valfri tangent för att komma tillbaka till menyn.");
                    }
                    Console.ResetColor();
                }
            }
            else
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Kunde inte hitta någon adress med det ID:et.");
                Console.ResetColor();
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Ett fel inträffade vid försök att ta bort adressen, kolla eventuella beroenden.");
            Console.ResetColor();
        }
        Console.ReadKey();
    }
}
