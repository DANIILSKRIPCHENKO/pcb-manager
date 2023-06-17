using CSharpExtensions.Maybe;
using CSharpExtensions.Result;
using Microsoft.EntityFrameworkCore;
using PcbManager.App;
using PcbManager.Domain.ImageNS.ValueObjects;

namespace PcbManager.DAL.Image
{
    public class ImageRepository : IImageRepository
    {
        private readonly PcbManagerDbContext _context;

        public ImageRepository(PcbManagerDbContext context)
        {
            _context = context;
        }

        public async Task<Result<Domain.ImageNS.Image>> CreateAsync(Domain.ImageNS.Image image)
        {
            var entityEntry = await _context.Images.AddAsync(image);

            await _context.SaveChangesAsync();

            return Result<Domain.ImageNS.Image>.Success(entityEntry.Entity);
        }

        public async Task<Maybe<Domain.ImageNS.Image>> GetByIdAsync(ImageId imageId)
        {
            var image =  await _context.Images.FirstOrDefaultAsync<Domain.ImageNS.Image>(x => x.Id == imageId);

            return Maybe<Domain.ImageNS.Image>.From<Domain.ImageNS.Image>(image);
        }

        public async Task<Maybe<List<Domain.ImageNS.Image>>> GetAllAsync()
        {
            return Maybe<List<Domain.ImageNS.Image>>.From(await _context.Images.ToListAsync<Domain.ImageNS.Image>());
        }
    }
}
