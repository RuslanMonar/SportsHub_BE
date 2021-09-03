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
        public bool IsBlocked { get; set; }
        
        public SearchUsersDto()
        {
        }

        public SearchUsersDto(ref AppUser user)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Image = null;
            IsBlocked = user.IsBlocked;
        }
    }

    public class SearchResult : Result
    {
        public IEnumerable<SearchUsersDto> Users { get; set; }
    }
}