namespace ContactBook_Database.Models.Entities;

internal class ContactInformationEntity
{
    public int Id { get; set; }
    public string Type { get; set; } = null!;
    public string Value { get; set; } = null!;

    public int ContactId { get; set; } 
    public ContactEntity Contact { get; set; } = null!;
}
