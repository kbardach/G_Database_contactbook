using ContactBook_Database.Models.Entities;
using Kontaktbok_Databas.Contexts;

namespace ContactBook_Database.Repositories;

internal class ContactRepository : Repo<ContactEntity>
{
    private readonly DataContext _context;

    public ContactRepository(DataContext context) : base(context)
    {
        _context = context;
    }
}
