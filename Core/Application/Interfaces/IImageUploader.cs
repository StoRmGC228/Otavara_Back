using Microsoft.AspNetCore.Http;

public interface IImageUploader
{
    Task<List<string>> GetPublicIdOfAllImagesAsync();
}