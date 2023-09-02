using PcbManager.Domain.ImageNS;

namespace PcbManager.WebApi.Dtos;

public class ImageDto
{
    private ImageDto(Image image)
    {
        Id = image.Id.Value;
        ImageName = image.ImageName.Value;
        ImagePath = image.ImagePath.Value;
    }

    public Guid Id { get; }

    public string ImageName { get; }

    public string ImagePath { get; }

    public static ImageDto From(Image image) => new(image);
}