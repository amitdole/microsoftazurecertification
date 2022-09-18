using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Azure.Cosmos;
using PSAZDemo1.NetCoreWebApplication.Model;

namespace PSAZDemo1.NetCoreWebApplication.Pages
{
    public class LocationModel : PageModel
    {
        private readonly ILogger<LocationModel> _logger;
        private CosmosClient _cosmosClient;
        private Container _container;

        public LocationModel(CosmosClient cosmosClient)
        {
            _cosmosClient = cosmosClient;
            _container = cosmosClient.GetContainer("PSAZDemo1CosmosDB", "psazdemo1cosmosdbcontainer");
        }

        [BindProperty]
        public List<Location> Locations { get; set; }

        public async Task<IActionResult> OnGet(string Country)
        {
            //get countries for search list
            var countries = new List<string>();
            var iteratorCountries = _container.GetItemQueryIterator<string>
                ("SELECT DISTINCT VALUE c.country FROM psazdemo1cosmosdbcontainer c");

            while (iteratorCountries.HasMoreResults)
            {
                var pageCountries = await iteratorCountries.ReadNextAsync();
                countries.AddRange(pageCountries);
            }

            ViewData["CountriesList"] = countries;

            var locations = new List<Location>();

            var iterator = _container.GetItemQueryIterator<Location>(
                requestOptions: new QueryRequestOptions
                {
                    PartitionKey = new PartitionKey(Country)
                });

            while(iterator.HasMoreResults)
            {
                var pageOfLocations = await iterator.ReadNextAsync();
                locations.AddRange(pageOfLocations.ToList());
            }

            Locations = locations;

            return Page();
        }

        public async Task<IActionResult> OnPostSubmit(Location model)
        {
            //save new location to the cosmos
            model.Id = Guid.NewGuid().ToString();

            await _container.CreateItemAsync(model, new PartitionKey(model.Country));

            return RedirectToPage("Location", new { Country = model.Country });

        }
    }
}