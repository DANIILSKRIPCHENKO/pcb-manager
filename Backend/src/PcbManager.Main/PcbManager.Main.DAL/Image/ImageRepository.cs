using PcbManager.Main.App.Image;
using PcbManager.Main.DAL.Abstractions;
using PcbManager.Main.Domain.ImageNS.ValueObjects;

namespace PcbManager.Main.DAL.Image
{
    public class ImageRepository : RepositoryBase<Domain.ImageNS.Image, ImageId>, IImageRepository
    {
        public ImageRepository(PcbManagerDbContext context)
            : base(context) { }
    }
}
