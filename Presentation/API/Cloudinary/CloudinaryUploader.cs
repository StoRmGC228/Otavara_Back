using CloudinaryDotNet.Actions;
using CloudinaryDotNet;

namespace API.Cloudinary
{
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
    }

}
