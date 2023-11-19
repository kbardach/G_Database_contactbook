using ContactBook_Database.Models.Entities;
using ContactBook_Database.Repositories;
using System;
using System.Diagnostics;
using System.Net;

namespace ContactBook_Database.Services;

internal class SocialMediaService
{
    private readonly Repo<SocialMediaPlatformEntity> _socialMediaPlatformRepo;
    private readonly Repo<SocialMediaUserNameEntity> _socialMediaUserNameRepo;

    public SocialMediaService(Repo<SocialMediaPlatformEntity> socialMediaPlatformRepo, Repo<SocialMediaUserNameEntity> socialMediaUserNameRepo)
    {
        _socialMediaPlatformRepo = socialMediaPlatformRepo;
        _socialMediaUserNameRepo = socialMediaUserNameRepo;
    }

    // CRUD för SocialMediaPlatformEntity
    public async Task<SocialMediaPlatformEntity> CreateSocialMediaPlatformAsync(SocialMediaPlatformEntity socialMediaPlatform)
    {
        try
        {
            if (await _socialMediaPlatformRepo.ExistsAsync(x => x.Platform == socialMediaPlatform.Platform))
            {
                return null!;
            }

            return await _socialMediaPlatformRepo.CreateAsync(socialMediaPlatform);
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public async Task<SocialMediaPlatformEntity> GetOnePlatformAsync(SocialMediaPlatformEntity socialMediaPlatform)
    {
        try
        {
            if (await _socialMediaPlatformRepo.ExistsAsync(x => x.Id == socialMediaPlatform.Id))
            {
                return await _socialMediaPlatformRepo.ReadOneAsync(x => x.Id == socialMediaPlatform.Id);
            }
            return null!;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public async Task<IEnumerable<SocialMediaPlatformEntity>> GetAllPlatformsAsync()
    {
        try
        {
            if (await _socialMediaPlatformRepo.ExistsAsync(x => true))
            {
                return await _socialMediaPlatformRepo.ReadAllAsync();
            }
            return null!;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public async Task<SocialMediaPlatformEntity> UpdatePlatformAsync(SocialMediaPlatformEntity updatePlatform)
    {
        try
        {
            var platformUpdate = await _socialMediaPlatformRepo.ReadOneAsync(x => x.Id == updatePlatform.Id);
            if (platformUpdate != null)
            {
                platformUpdate.Platform = updatePlatform.Platform;

                await _socialMediaPlatformRepo.UpdateAsync(platformUpdate);
                return platformUpdate;
            }
            return null!;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public async Task<bool> DeletePlatformAsync(int platformId)
    {
        try
        {
            var platform = await _socialMediaPlatformRepo.ExistsAsync(x => x.Id == platformId);
            if (platform)
            {
                return await _socialMediaPlatformRepo.DeleteAsync(x => x.Id == platformId);
            }
            return false;

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;
    }


    // CRUD för SocialMediaUserNameEntity
    public async Task<SocialMediaUserNameEntity> CreateSocialMediaUserName(SocialMediaUserNameEntity socialMediaUserName)
    {
        try
        {
            if (await _socialMediaUserNameRepo.ExistsAsync(x => x.Username == socialMediaUserName.Username))
            {
                return null!;
            }

            return await _socialMediaUserNameRepo.CreateAsync(socialMediaUserName);
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public async Task<SocialMediaUserNameEntity> GetOneSocialMediaUserNameAsync(string userName, int contactId)
    {
        try
        {
            if (await _socialMediaUserNameRepo.ExistsAsync(x => x.Username == userName && x.ContactId == contactId))
            {
                return await _socialMediaUserNameRepo.ReadOneAsync(x => x.Username == userName && x.ContactId == contactId);
            }
            return null!;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public async Task<IEnumerable<SocialMediaUserNameEntity>> GetAllUserNamesAsync()
    {
        try
        {
            if (await _socialMediaUserNameRepo.ExistsAsync(x => true))
            {
                return await _socialMediaUserNameRepo.ReadAllAsync();
            }
            return null!;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public async Task<SocialMediaUserNameEntity> UpdateUserNameAsync(SocialMediaUserNameEntity updateUserName)
    {
        try
        {
            var userNameUpdate = await _socialMediaUserNameRepo.ReadOneAsync(x => x.ContactId == updateUserName.ContactId && x.SocialMediaPlatformId == updateUserName.SocialMediaPlatformId);
            if (userNameUpdate != null)
            {
                userNameUpdate.Username = updateUserName.Username;

                await _socialMediaUserNameRepo.UpdateAsync(userNameUpdate);
                return userNameUpdate;
            }
            return null!;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public async Task<bool> DeleteUserNameAsync(int contactId, int platformId)
    {
        try
        {
            var userName = await _socialMediaUserNameRepo.ExistsAsync(x => x.ContactId == contactId && x.SocialMediaPlatformId == platformId);
            if (userName)
            {
                return await _socialMediaUserNameRepo.DeleteAsync(x => x.ContactId == contactId && x.SocialMediaPlatformId == platformId);
            }
            return false;

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;
    }

}
