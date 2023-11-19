namespace ContactBook_Database.Models.Entities;

internal class SocialMediaUserNameEntity
{
    public int ContactId { get; set; }
    public int SocialMediaPlatformId { get; set; }
    public string Username { get; set; } = null!;


    public ContactEntity Contact { get; set; } = null!;
    public SocialMediaPlatformEntity SocialMediaPlatform { get; set; } = null!;
}
