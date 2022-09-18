using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Azure.Cosmos;
using PSAZDemo1.NetCoreWebApplication.Model;

namespace PSAZDemo1.NetCoreWebApplication.Pages
{
    public class MediaDetail : PageModel
    {
        private readonly ILogger<LocationModel> _logger;
        private IConfiguration _config;
        public MediaDetail(IConfiguration config)
        {
            _config = config;
        }

        [BindProperty]
        public ImageUploadDetail ImageUploadDetail { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var imageFileName = "pm_workflow.png";
            var model = new ImageUploadDetail();

            var containerClient = new BlobContainerClient(_config["BlobCNN"], "psazdemo1storagecontainer");

            var blob = containerClient.GetBlobClient(imageFileName);

            var builder = new BlobSasBuilder
            {
                BlobContainerName = containerClient.Name,
                BlobName = blob.Name,
                ExpiresOn = DateTime.UtcNow.AddMinutes(2),
                Protocol = SasProtocol.Https
            };

            builder.SetPermissions(BlobAccountSasPermissions.Read);

            var uBuilder = new UriBuilder(blob.Uri);
            uBuilder.Query = builder.ToSasQueryParameters(new Azure.Storage.StorageSharedKeyCredential(containerClient.AccountName, _config["BlobKey"])).ToString();


            model.Url = uBuilder.Uri.ToString();
            model.ImageFileName = imageFileName;

            ImageUploadDetail = model;

            return Page();
        }
    }
}