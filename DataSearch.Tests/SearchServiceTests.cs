using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.IO;
using DataSearch.Models;
using DataSearch.Services;
using Xunit;

namespace DataSearch.Tests
{
    /// <summary>
    /// Unit tests for the SearchService class.
    /// </summary>
    public class SearchServiceTests
    {
        private readonly ILogger<SearchService> _logger;

        /// <summary>
        /// Constructor with logger parameter.
        /// </summary>
        public SearchServiceTests()
        {
            // Mocking ILogger for testing
            _logger = new Mock<ILogger<SearchService>>().Object;
        }

        /// <summary>
        /// Test case for searching with a specific first name.
        /// </summary>
        [Fact]
        public void SearchCase1_FirstName()
        {
            // Arrange
            var jsonContent = File.ReadAllText("dummydata.json");
            var userData = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Users>>(jsonContent);

            // Ensure userData is not null
            Assert.NotNull(userData);

            // Act
            var searchService = new SearchService(userData!, _logger);
            var results = searchService.Search("James");

            // Assert
            Assert.Collection(results,
                item => Assert.Equal("James Kubu", $"{item.FirstName} {item.LastName}"),
                item => Assert.Equal("James Pfeffer", $"{item.FirstName} {item.LastName}")
            );
        }

        /// <summary>
        /// Test case for searching with a term present in email addresses.
        /// </summary>
        [Fact]
        public void SearchCase2_EmailContainsTerm()
        {
            // Arrange
            var jsonContent = File.ReadAllText("dummydata.json");
            var userData = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Users>>(jsonContent);

            // Ensure userData is not null
            Assert.NotNull(userData);

            // Act
            var searchService = new SearchService(userData!, _logger);
            var results = searchService.Search("jam");

            // Assert
            Assert.Collection(results,
                item => Assert.Equal("James Kubu", $"{item.FirstName} {item.LastName}"),
                item => Assert.Equal("James Pfeffer", $"{item.FirstName} {item.LastName}"),
                item => Assert.Equal("Chalmers Longfut", $"{item.FirstName} {item.LastName}")
            );
        }

        /// <summary>
        /// Test case for searching with a specific full name.
        /// </summary>
        [Fact]
        public void SearchCase3_FullName()
        {
            // Arrange
            var jsonContent = File.ReadAllText("dummydata.json");
            var userData = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Users>>(jsonContent);

            // Ensure userData is not null
            Assert.NotNull(userData);

            // Act
            var searchService = new SearchService(userData!, _logger);
            var results = searchService.Search("Katey Soltan");

            // Assert
            Assert.Collection(results,
                item => Assert.Equal("Katey Soltan", $"{item.FirstName} {item.LastName}")
            );
        }

        /// <summary>
        /// Test case for searching with a term that should not return any results.
        /// </summary>
        [Fact]
        public void SearchCase4_NoResults()
        {
            // Arrange
            var jsonContent = File.ReadAllText("dummydata.json");
            var userData = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Users>>(jsonContent);

            // Ensure userData is not null
            Assert.NotNull(userData);

            // Act
            var searchService = new SearchService(userData!, _logger);
            var results = searchService.Search("Jasmine Duncan");

            // Assert
            Assert.Empty(results);
        }

        /// <summary>
        /// Test case for searching with an empty search term.
        /// </summary>
        [Fact]
        public void SearchCase5_EmptySearchTerm()
        {
            // Arrange
            var jsonContent = File.ReadAllText("dummydata.json");
            var userData = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Users>>(jsonContent);

            // Ensure userData is not null
            Assert.NotNull(userData);

            // Act
            var searchService = new SearchService(userData!, _logger);
            var results = searchService.Search("");

            // Assert
            // Verify that the results are empty
            Assert.Empty(results);
        }
    }
}
