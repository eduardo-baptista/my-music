using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyMusic.Core.Models;
using MyMusic.Core.Repositories;

namespace MyMusic.Data.Repositories
{
  public class MusicRepository : Repository<Music>, IMusicRepository
  {
    public MusicRepository(MyMusicDbContext context) : base(context) { }

    public async Task<IEnumerable<Music>> GetAllWithArtistAsync()
    {
      return await MyMusicDbContext.Musics.Include(m => m.Artist).ToListAsync();
    }

    public async Task<Music> GetWithArtistByIdAsync(int id)
    {
      return await MyMusicDbContext.Musics.Include(m => m.Artist)
        .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<IEnumerable<Music>> GetAllWithArtistByArtistIdAsync(int artistId)
    {
      return await MyMusicDbContext.Musics
      .Include(m => m.Artist)
      .Where(m => m.ArtistId == artistId)
      .ToListAsync();
    }

    private MyMusicDbContext MyMusicDbContext
    {
      get { return _context as MyMusicDbContext; }
    }
  }
}