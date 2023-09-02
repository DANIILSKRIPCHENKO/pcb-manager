using PcbManager.App.Abstractions;
using PcbManager.Domain.ImageNS.ValueObjects;

namespace PcbManager.App.Image
{
    public interface IImageRepository : IRepositoryBase<Domain.ImageNS.Image, ImageId>
    {

    }
}
