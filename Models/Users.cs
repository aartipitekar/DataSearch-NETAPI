using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace DataSearch.Models
{
    public class Users
    {
        [JsonProperty("id")]
        public int Id { get; }

        [JsonProperty("first_name")]
        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; }

        [JsonProperty("last_name")]
        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; }

        [JsonProperty("email")]
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; }

        [JsonProperty("gender")]
        [Required(ErrorMessage = "Gender is required.")]
        public string Gender { get; }

        [JsonConstructor]
        public Users(int id, string firstName, string lastName, string email, string gender)
        {
            Id = id;
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Gender = gender ?? throw new ArgumentNullException(nameof(gender));
        }
    }
}
