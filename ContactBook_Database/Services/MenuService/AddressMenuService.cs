using ContactBook_Database.Models.Entities;
using System.Diagnostics;

namespace ContactBook_Database.Services.MenuService;

internal class AddressMenuService
{
    private readonly AddressService _addressService;

    public AddressMenuService(AddressService addressService)
    {
        _addressService = addressService;
    }

    internal async Task AddressMenuAsync()
    {
        var exit = false;

        do
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" --- Hantera adresser --- ");
            Console.WriteLine("--------------------------");
            Console.ResetColor();
            Console.WriteLine("1. Hämta en adress");
            Console.WriteLine("2. Hämta alla adresser");
            Console.WriteLine("3. Uppdatera adress");
            Console.WriteLine("4. Radera adress");
            Console.WriteLine("0. Gå tillbaka");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Välj ett av ovanstående alternativ. (0-4): ");
            Console.ResetColor();

            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    await GetOneAddressMenuAsync();
                    break;

                case "2":
                    await GetAllAddressesMenuAsync();
                    break;

                case "3":
                    await UpdateAddressMenuAsync();
                    break;

                case "4":
                    await DeleteAddressMenuAsync();
                    break;

                case "0":
                    exit = true;
                    break;

                default:
                    break;

            }

        } while (exit == false);
    }

    internal async Task GetOneAddressMenuAsync()
    {
        try
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" -- Hämta en adress -- ");
            Console.WriteLine("-----------------------");
            Console.ResetColor();
            Console.Write("Ange adressens ID:");
            if (int.TryParse(Console.ReadLine(), out int addressId))
            {
                var address = await _addressService.GetOneAddressAsync(new AddressEntity { Id = addressId });
                if (address != null)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Clear();
                    Console.WriteLine($"Gatunamn: {address.StreetName}");
                    Console.WriteLine($"Gatunummer: {address.StreetNumber}");
                    Console.WriteLine($"Postnummer: {address.PostalCode}");
                    Console.WriteLine($"Stad: {address.City}");
                    Console.ResetColor();
                }
                else
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Ingen adress hittades med det ID:et.");
                    Console.ResetColor();
                }
            }
            Console.ReadKey();
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
    }

    internal async Task GetAllAddressesMenuAsync()
    {
        try
        {
            var addresses = await _addressService.GetAllAddressesAsync();

            if (addresses != null)
            {
                Console.Clear();
                foreach (var address in addresses)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(" -- Hämta alla kontakter -- ");
                    Console.WriteLine("------------------------");
                    Console.ResetColor();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"ID: {address.Id}");
                    Console.WriteLine($"Gatunamn: {address.StreetName}");
                    Console.WriteLine($"Gatunummer: {address.StreetNumber}");
                    Console.WriteLine($"Postnummer: {address.PostalCode}");
                    Console.WriteLine($"Stad: {address.City}");
                    Console.WriteLine("---------------------------------------");
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

    internal async Task<bool> UpdateAddressMenuAsync()
    {
        try
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" -- Uppdatera en adress -- ");
            Console.WriteLine("--------------------------");
            Console.ResetColor();
            Console.Write("Ange ID för adressen som ska uppdateras: ");
            if (!int.TryParse(Console.ReadLine(), out int addressId))
            {
                Console.WriteLine("Ogiltigt ID. Försök igen.");
                return false;
            }

            var updateAddress = await _addressService.GetOneAddressAsync(new AddressEntity { Id = addressId });

            if (updateAddress != null)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Här är adressen du vill uppdatera");
                Console.WriteLine("---------------------------------");
                Console.ResetColor();
                Console.WriteLine($"Gatunamn: {updateAddress.StreetName}");
                Console.WriteLine($"Gatunummer: {updateAddress.StreetNumber}");
                Console.WriteLine($"Postnummer: {updateAddress.PostalCode}");
                Console.WriteLine($"Stad: {updateAddress.City}");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ange nya uppgifter där ändringar önskas. Lämna övriga fält tomma.");
                Console.ResetColor();
                Console.WriteLine();

                Console.Write("Nytt gatunamn: ");
                var updatedStreetName = Console.ReadLine();
                if (!string.IsNullOrEmpty(updatedStreetName)) updateAddress.StreetName = updatedStreetName;

                Console.Write("Nytt gatunummer: ");
                var updatedStreetNumber = Console.ReadLine();
                if (!string.IsNullOrEmpty(updatedStreetNumber)) updateAddress.StreetNumber = updatedStreetNumber;

                Console.Write("Nytt postnummer: ");
                var updatedPostalCode = Console.ReadLine();
                if (!string.IsNullOrEmpty(updatedPostalCode)) updateAddress.PostalCode = updatedPostalCode;

                Console.Write("Ny stad: ");
                var updatedCity = Console.ReadLine();
                if (!string.IsNullOrEmpty(updatedCity)) updateAddress.City = updatedCity;

                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Om ändringar har gjorts, så har adressen uppdaterats!");
                Console.ResetColor();

                _addressService.UpdateAddressAsync(updateAddress).Wait();
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

    internal async Task DeleteAddressMenuAsync()
    {
        try
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" -- Ta bort en adress -- ");
            Console.WriteLine("------------------------");
            Console.ResetColor();
            Console.Write("Ange ID för adressen som ska tas bort: ");
            if (!int.TryParse(Console.ReadLine(), out int addressId))
            {
                Console.WriteLine("Ogiltigt ID. Försök igen.");
                return;
            }

            var addressToDelete = await _addressService.GetOneAddressAsync(new AddressEntity { Id = addressId });

            if (addressToDelete != null)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Gatunamn: {addressToDelete.StreetName}");
                Console.WriteLine($"Gatunummer: {addressToDelete.StreetNumber}");
                Console.WriteLine($"Postnummer: {addressToDelete.PostalCode}");
                Console.WriteLine($"Stad: {addressToDelete.City}");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Vill du ta bort denna adress? (j/n): ");
                Console.ResetColor();
                var option = Console.ReadLine();

                if (option == "j")
                {
                    var delete = await _addressService.DeleteAddressAsync(addressId);
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
