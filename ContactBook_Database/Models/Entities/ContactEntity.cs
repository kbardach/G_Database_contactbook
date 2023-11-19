namespace ContactBook_Database.Models.Entities;

internal class ContactEntity
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Title {  get; set; }

    public int AddressId { get; set; }
    public AddressEntity? Address { get; set; }
    public ICollection<ContactInformationEntity> ContactInformations { get; set; } = new HashSet<ContactInformationEntity>();                      
    public ICollection<SocialMediaUserNameEntity> SocialMediaInformationTypes { get; set; } = new HashSet<SocialMediaUserNameEntity>();
}
