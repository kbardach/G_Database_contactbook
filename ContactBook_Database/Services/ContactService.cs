using ContactBook_Database.Models.Entities;
using ContactBook_Database.Repositories;
using System.Diagnostics;

namespace ContactBook_Database.Services;

internal class ContactService
{
    private readonly Repo<ContactEntity> _contactRepo;

    public ContactService(Repo<ContactEntity> contactRepo)
    {
        _contactRepo = contactRepo;
    }

    public async Task<ContactEntity> CreateContactAsync(ContactEntity contact)
    {
        try
        {
            if (!await _contactRepo.ExistsAsync(x => x.Email == contact.Email))
            {
                await _contactRepo.CreateAsync(contact);
                return contact;
            }
            return null!;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public async Task<ContactEntity> GetOneContactAsync(ContactEntity contact)
    {
        try
        {
            if (await _contactRepo.ExistsAsync(x => x.Email == contact.Email))
            {
                return await _contactRepo.ReadOneAsync(x => x.Email == contact.Email);
            }
            return null!;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }       // lägg till i GetÒneContact att man ska .include alla underkategorier så man slipper GetOne/GetAll i alla entiteter

    public async Task<IEnumerable<ContactEntity>> GetAllContactsAsync()
    {
        try
        {
            if (await _contactRepo.ExistsAsync(x => true))
            {
                return await _contactRepo.ReadAllAsync();
            }
            return null!;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public async Task<ContactEntity> UpdateContactAsync(ContactEntity updateContact)
    {
        try
        {
            var contactUpdate = await _contactRepo.ReadOneAsync(x => x.Email == updateContact.Email);
            if (contactUpdate != null)
            {
                contactUpdate.FirstName = updateContact.FirstName;
                contactUpdate.LastName = updateContact.LastName;
                contactUpdate.Email = updateContact.Email;
                contactUpdate.Title = updateContact.Title;

                await _contactRepo.UpdateAsync(contactUpdate);
                return contactUpdate;
            }
            return null!;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public async Task<bool> DeleteContactAsync(string email)
    {
        try
        {
            var contact = await _contactRepo.ExistsAsync(x => x.Email == email);
            if (contact)
            {
                return await _contactRepo.DeleteAsync(x => x.Email == email);
            }
            return false;

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;
    }


}




