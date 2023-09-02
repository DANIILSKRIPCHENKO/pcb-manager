using PcbManager.Main.App.Abstractions;
using PcbManager.Main.Domain.ImageNS.ValueObjects;

namespace PcbManager.Main.App.Image
{
    public interface IImageRepository : IRepositoryBase<Domain.ImageNS.Image, ImageId>
    {

    }
}
