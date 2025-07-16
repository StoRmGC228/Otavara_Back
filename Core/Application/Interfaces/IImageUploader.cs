public interface IImageUploader
{
    Task<List<string>> GetPublicIdOfAllImagesAsync();
}