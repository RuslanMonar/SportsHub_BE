using System.Collections.Generic;
using Domain;

namespace Application.Admin.Users
{
    public class SearchUsersDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Image { get; set; }
    }

    public class SearchResult : Result
    {
        public IEnumerable<SearchUsersDto> Users { get; set; }
    }
}