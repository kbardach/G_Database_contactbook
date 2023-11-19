using ContactBook_Database.Models.Entities;
using ContactBook_Database.Repositories;
using System.Diagnostics;

namespace ContactBook_Database.Services;

internal class ContactInformationService
{
    private readonly Repo<ContactInformationEntity> _contactInfoRepo;

    public ContactInformationService(Repo<ContactInformationEntity> contactInfoRepo)
    {
        _contactInfoRepo = contactInfoRepo;
    }

    public async Task<ContactInformationEntity> CreateContactInfoAsync(ContactInformationEntity contactInfo)
    {
        try
        {
            if (!await _contactInfoRepo.ExistsAsync(x => x.Id == contactInfo.Id))
            {
                await _contactInfoRepo.CreateAsync(contactInfo);
                return contactInfo;
            }
            return null!;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public async Task<ContactInformationEntity> GetOneContactInfoAsync(ContactInformationEntity contactInfo)
    {
        try
        {
            if (await _contactInfoRepo.ExistsAsync(x => x.Id == contactInfo.Id))
            {
                return await _contactInfoRepo.ReadOneAsync(x => x.Id == contactInfo.Id);
            }
            return null!;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public async Task<IEnumerable<ContactInformationEntity>> GetAllContactInfoAsync()
    {
        try
        {
            if (await _contactInfoRepo.ExistsAsync(x => true))
            {
                return await _contactInfoRepo.ReadAllAsync();
            }
            return null!;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public async Task<ContactInformationEntity> UpdateContactInfoAsync(ContactInformationEntity updateContactInfo)
    {
        try
        {
            var contactInfoUpdate = await _contactInfoRepo.ReadOneAsync(x => x.Id == updateContactInfo.Id);
            if (contactInfoUpdate != null)
            {
                contactInfoUpdate.Type = updateContactInfo.Type;
                contactInfoUpdate.Value = updateContactInfo.Value;

                await _contactInfoRepo.UpdateAsync(contactInfoUpdate);
                return contactInfoUpdate;
            }
            return null!;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public async Task<bool> DeleteContactInformationAsync(int contactInfoId)
    {
        try
        {
            var contact = await _contactInfoRepo.ExistsAsync(x => x.Id == contactInfoId);
            if (contact)
            {
                return await _contactInfoRepo.DeleteAsync(x => x.Id == contactInfoId);
            }
            return false;

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;
    }
}
