using ContactBook_Database.Models.Entities;
using Kontaktbok_Databas.Contexts;

namespace ContactBook_Database.Repositories;

internal class AddressRepsository : Repo<AddressEntity>
{
    private readonly DataContext _context;

    public AddressRepsository(DataContext context) : base(context)
    {
        _context = context;
    }
}
