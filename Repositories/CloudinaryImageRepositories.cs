using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace BloggieMvc.Repositories
{
    public class CloudinaryImageRepositories : IImageInterface
    {
        private readonly IConfiguration _configuration;
        private readonly Account account;
        public CloudinaryImageRepositories(IConfiguration configuration)
        {
            _configuration = configuration;

            account = new Account(
                _configuration.GetSection("Cloudinary")["CloudName"],
                _configuration.GetSection("Cloudinary")["ApiKey"],
                _configuration.GetSection("Cloudinary")["ApiSecret"]

                );
        }

        public async Task<string> UploadAsync(IFormFile file)
        {
            var client = new Cloudinary(account);

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                PublicId = file.FileName
            };
            var uploadResults = await client.UploadAsync(uploadParams);
            if (uploadResults != null && uploadResults.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return uploadResults.SecureUrl.ToString();
            }
            return null;
        }
    }
}
