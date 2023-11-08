using System;
using System.Collections.Generic;
using System.Linq;
using DataSearch.Models;
using Microsoft.Extensions.Logging;

namespace DataSearch.Services
{
    /// <summary>
    /// Service for searching user data based on a provided search term.
    /// </summary>
    public class SearchService
    {
        private readonly List<Users> _userData;
        private readonly ILogger<SearchService> _logger;

        /// <summary>
        /// Initializes a new instance of the SearchService class.
        /// </summary>
        /// <param name="userData">The list of users to search within.</param>
        /// <param name="logger">The logger for recording events and errors.</param>
        public SearchService(List<Users> userData, ILogger<SearchService> logger)
        {
            _userData = userData ?? throw new ArgumentNullException(nameof(userData));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Searches for users based on a provided search term.
        /// </summary>
        /// <param name="searchTerm">The term to search for in user names and emails.</param>
        /// <returns>A list of users matching the search criteria.</returns>
        public List<Users> Search(string searchTerm)
        {
            try
            {
                // Handle an empty search term
                if (string.IsNullOrEmpty(searchTerm))
                {
                    // Log a warning and return an empty list
                    _logger.LogWarning("Empty search term provided");
                    return new List<Users>();
                }

                // Convert search term to lowercase for case-insensitive search
                searchTerm = searchTerm.ToLower();

                // Perform a case-insensitive search in first and last names, and email
                return _userData
                    .Where(user =>
                        $"{user.FirstName} {user.LastName}".ToLower().Contains(searchTerm) ||
                        user.Email.ToLower().Contains(searchTerm))
                    .ToList();
            }
            catch (Exception ex)
            {
                // Log the exception and return an empty list
                _logger.LogError(ex, "An error occurred during search");
                return new List<Users>();
            }
        }
    }
}
