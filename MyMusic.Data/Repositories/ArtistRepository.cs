using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyMusic.Core.Models;
using MyMusic.Core.Repositories;

namespace MyMusic.Data.Repositories
{
  public class ArtistRepository : Repository<Artist>, IArtistRepository
  {
    public ArtistRepository(MyMusicDbContext context) : base(context) { }

    public async Task<IEnumerable<Artist>> GetAllWithMusicsAsync()
    {
      return await MyMusicDbContext.Artists.Include(a => a.Musics).ToListAsync();
    }

    public async Task<Artist> GetWithMusicsByIdAsync(int id)
    {
      return await MyMusicDbContext
      .Artists
      .Include(a => a.Musics)
      .SingleOrDefaultAsync(a => a.Id == id);
    }

    private MyMusicDbContext MyMusicDbContext
    {
      get { return _context as MyMusicDbContext; }
    }
  }
}