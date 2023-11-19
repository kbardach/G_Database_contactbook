using ContactBook_Database.Models.Entities;
using ContactBook_Database.Repositories;
using System.Diagnostics;

namespace ContactBook_Database.Services;

internal class AddressService
{
    private readonly Repo<AddressEntity> _addressRepo;

    public AddressService(Repo<AddressEntity> addressRepo)
    {
        _addressRepo = addressRepo;
    }

    public async Task<AddressEntity> CreateAddressAsync(AddressEntity address)
    {
        try
        {
            if (!await _addressRepo.ExistsAsync(x => x.StreetName == address.StreetName && 
            x.StreetNumber == address.StreetNumber &&
            x.PostalCode == address.PostalCode &&
            x.City == address.City))
            {
                await _addressRepo.CreateAsync(address);
                return address;
            }
            return null!;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public async Task<AddressEntity> GetOneAddressAsync(AddressEntity address)
    {
        try
        {
            if (await _addressRepo.ExistsAsync(x => x.Id == address.Id))
            {
                return await _addressRepo.ReadOneAsync(x => x.Id == address.Id);
            }
            return null!;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public async Task<IEnumerable<AddressEntity>> GetAllAddressesAsync()
    {
        try
        {
            if (await _addressRepo.ExistsAsync(x => true))
            {
                return await _addressRepo.ReadAllAsync();
            }
            return null!;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public async Task<AddressEntity> UpdateAddressAsync(AddressEntity updateAddress)
    {
        try
        {
            var addressUpdate = await _addressRepo.ReadOneAsync(x => x.Id == updateAddress.Id);
            if (addressUpdate != null)
            {
                addressUpdate.StreetName = updateAddress.StreetName;
                addressUpdate.StreetNumber = updateAddress.StreetNumber;
                addressUpdate.PostalCode = updateAddress.PostalCode;
                addressUpdate.City = updateAddress.City;

                await _addressRepo.UpdateAsync(addressUpdate);
                return addressUpdate;
            }
            return null!;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }


    public async Task<bool> DeleteAddressAsync(int addressId)
    {
        try
        {
            var contact = await _addressRepo.ExistsAsync(x => x.Id == addressId);
            if (contact)
            {
                return await _addressRepo.DeleteAsync(x => x.Id == addressId);
            }
            return false;

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;
    }
}
