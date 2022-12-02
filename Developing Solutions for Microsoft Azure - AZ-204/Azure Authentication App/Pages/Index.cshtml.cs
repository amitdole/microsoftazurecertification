using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Web;

namespace Azure_Authentication_App.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ITokenAcquisition _tokenAcquisition;
        public string accessToken;
        public string blobContent;

        public IndexModel(ILogger<IndexModel> logger, ITokenAcquisition tokenAcquisition)
        {
            _logger = logger;
            _tokenAcquisition = tokenAcquisition;
        }

        public async Task OnGet()
        {
            try
            {
                //string[] scope = new string[] { "https://storage.azure.com/user_impersonation" };
                //accessToken = await _tokenAcquisition.GetAccessTokenForUserAsync(scope);
                TokenAcquisitionTokenCredential credential = new TokenAcquisitionTokenCredential(_tokenAcquisition);

                var bloburl = new Uri("https://azure400storagegroup.blob.core.windows.net/data/DML.sql");
                var blobClient = new BlobClient(bloburl, credential);

                var memorystream = new MemoryStream();
                blobClient.DownloadTo(memorystream);

                memorystream.Position = 0;

                var sr = new StreamReader(memorystream);
                blobContent = sr.ReadToEnd();
            }
            catch (Exception e)
            {
                throw;
            }
            
        }
    }
}