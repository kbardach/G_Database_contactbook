namespace ContactBook_Database.Models.Entities;

internal class SocialMediaPlatformEntity
{
    public int Id { get; set; }
    public string Platform { get; set; } = null!;

    public ICollection<SocialMediaUserNameEntity> SocialMediaInformationTypes { get; set; } = new HashSet<SocialMediaUserNameEntity>();
}
