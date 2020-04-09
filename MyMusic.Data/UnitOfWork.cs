using System.Threading.Tasks;
using MyMusic.Core;
using MyMusic.Core.Repositories;
using MyMusic.Data.Repositories;

namespace MyMusic.Data
{
  public class UnitOfWork : IUnitOfWork
  {
    private readonly MyMusicDbContext _context;
    private MusicRepository _musicRepository { get; set; }
    private ArtistRepository _artistRepository { get; set; }

    public UnitOfWork(MyMusicDbContext context)
    {
      _context = context;
    }

    public IMusicRepository Musics => _musicRepository = _musicRepository ?? new MusicRepository(_context);
    public IArtistRepository Artists => _artistRepository = _artistRepository ?? new ArtistRepository(_context);

    public async Task<int> CommitAsync()
    {
      return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
      _context.Dispose();
    }
  }
}