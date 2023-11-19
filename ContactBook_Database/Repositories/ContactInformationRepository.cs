using ContactBook_Database.Models.Entities;
using Kontaktbok_Databas.Contexts;

namespace ContactBook_Database.Repositories;

internal class ContactInformationRepository : Repo<ContactInformationEntity>
{
    private readonly DataContext _context;

    public ContactInformationRepository(DataContext context) : base(context)
    {
        _context = context;
    }
}
