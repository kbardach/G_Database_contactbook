using ContactBook_Database.Models.Entities;
using Kontaktbok_Databas.Contexts;

namespace ContactBook_Database.Repositories;

internal class SocialMediaPlatformRepository : Repo<SocialMediaPlatformEntity>
{
    private readonly DataContext _context;

    public SocialMediaPlatformRepository(DataContext context) : base(context)
    {
        _context = context;
    }
}
