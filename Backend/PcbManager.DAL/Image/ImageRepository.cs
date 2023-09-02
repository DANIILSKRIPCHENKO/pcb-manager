using PcbManager.App.Image;
using PcbManager.DAL.Abstractions;
using PcbManager.Domain.ImageNS.ValueObjects;

namespace PcbManager.DAL.Image
{
    public class ImageRepository : RepositoryBase<Domain.ImageNS.Image, ImageId>, IImageRepository
    {
        public ImageRepository(PcbManagerDbContext context) : base(context)
        {

        }
    }
}
