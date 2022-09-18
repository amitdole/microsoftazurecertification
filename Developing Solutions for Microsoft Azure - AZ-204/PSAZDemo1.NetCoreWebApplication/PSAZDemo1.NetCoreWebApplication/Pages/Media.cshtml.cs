using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Azure.Cosmos;
using PSAZDemo1.NetCoreWebApplication.Model;

namespace PSAZDemo1.NetCoreWebApplication.Pages
{
    public class MediaModel : PageModel
    {
        private readonly ILogger<LocationModel> _logger;
        private IConfiguration _config;

        [BindProperty]
        public IFormFile ImageFile { get; set; }

        public MediaModel(IConfiguration config)
        {
            _config = config;
        }

        [BindProperty]
        public List<Location> Locations { get; set; }

        public async Task<IActionResult> OnGet()
        {
            
            return Page();
        }

        public async Task<RedirectToPageResult> OnPostAsync(ImageUpload model)
        {
            return RedirectToPage("Media");
        }
            public async Task<IActionResult> OnPostSubmit(ImageUpload model)
        {

            var containerClient = new BlobContainerClient(_config["BlobCNN"], "psazdemo1storagecontainer");

            var blobClient = containerClient.GetBlobClient(model.ImageFile.FileName);

            var result = await blobClient.UploadAsync(model.ImageFile.OpenReadStream(), new BlobHttpHeaders
            {
                ContentType = model.ImageFile.ContentType,
                CacheControl = "public"
            },
            new Dictionary<string, string> { { "customName", model.Name } }
            );

            return RedirectToPage("Media");
        }
    }
}