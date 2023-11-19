using ContactBook_Database.Models.Entities;
using Kontaktbok_Databas.Contexts;

namespace ContactBook_Database.Repositories;

internal class SocialMediaUserNameRepository : Repo<SocialMediaUserNameEntity>
{
    private readonly DataContext _context;

    public SocialMediaUserNameRepository(DataContext context) : base(context)
    {
        _context = context;
    }
}
