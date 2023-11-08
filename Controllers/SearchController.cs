using Microsoft.AspNetCore.Mvc;
using DataSearch.Services;

namespace DataSearch.Controllers
{
    /// <summary>
    /// Controller responsible for handling search requests.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly SearchService _searchService;

        /// <summary>
        /// Initializes a new instance of the SearchController class.
        /// </summary>
        /// <param name="searchService">The search service used to perform search operations.</param>
        public SearchController(SearchService searchService)
        {
            _searchService = searchService;
        }

        /// <summary>
        /// Handles HTTP GET requests to search for users based on a search term.
        /// </summary>
        /// <param name="searchTerm">The term to search for in user data.</param>
        /// <returns>An IActionResult representing the HTTP response.</returns>
        [HttpGet]
        public IActionResult Search(string searchTerm)
        {
            // Perform the search operation using the injected SearchService
            var results = _searchService.Search(searchTerm);

            // Return the results as Ok (HTTP 200) response
            return Ok(results);
        }
    }
}
