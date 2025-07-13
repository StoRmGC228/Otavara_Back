namespace API.Cloudinary;

using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Cloudinary = CloudinaryDotNet.Cloudinary;

public class CloudinaryUploader : IImageUploader
{
    private readonly Cloudinary _cloudinary;

    public CloudinaryUploader(IConfiguration config)
    {
        var account = new Account(
            config["Cloudinary:CloudName"],
            config["Cloudinary:ApiKey"],
            config["Cloudinary:ApiSecret"]);
        _cloudinary = new Cloudinary(account);
    }

    public async Task<string> UploadImageAsync(IFormFile file)
    {
        await using var stream = file.OpenReadStream();
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(file.FileName, stream),
            Folder = "events"
        };
        var result = await _cloudinary.UploadAsync(uploadParams);
        return result.SecureUrl.AbsoluteUri;
    }

    public async Task<List<string>> GetPublicIdOfAllImagesAsync()
    {
        var result = await _cloudinary.ListResourceByAssetFolderAsync("events", false, false, false);
        return result.Resources.Select(p => p.PublicId).ToList();
    }

    public Dictionary<string, string> GenerateUploadEventSignature(string folder = "events")
    {
        var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        var parameters = new SortedDictionary<string, object>
        {
            { "folder", folder },
            { "timestamp", timestamp },
        };

        var signature = _cloudinary.Api.SignParameters(parameters);

        return new Dictionary<string, string>
        {
            { "signature", signature },
            { "timestamp", timestamp.ToString() },
            { "folder", folder },
            { "api_key", _cloudinary.Api.Account.ApiKey },
        };

        
    }
    public Dictionary<string, string> GenerateUploadGoodsSignature(string folder = "goods")
    {
        var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        var parameters = new SortedDictionary<string, object>
        {
            { "folder", folder },
            { "timestamp", timestamp },
        };

        var signature = _cloudinary.Api.SignParameters(parameters);

        return new Dictionary<string, string>
        {
            { "signature", signature },
            { "timestamp", timestamp.ToString() },
            { "folder", folder },
            { "api_key", _cloudinary.Api.Account.ApiKey },
        };
    }
}