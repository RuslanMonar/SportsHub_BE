using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Identity;

namespace Application.Admin.Users
{
    public class SearchUsersDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Image { get; set; }
        public bool IsBlocked { get; set; }
        public IList<string> Role { get; set; }
        
        public SearchUsersDto()
        {
        }

        public SearchUsersDto(ref AppUser user)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Image = user.Image;
            IsBlocked = user.IsBlocked;
        }
        public SearchUsersDto(ref AppUser user ,  IList<string> role)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Image = user.Image;
            IsBlocked = user.IsBlocked;
        }
    }

    public class SearchResult : Result
    {
        public IEnumerable<SearchUsersDto> Users { get; set; }
    }
    public class AllUsersResult : Result
    {
        public IEnumerable<Task<SearchUsersDto>> Users { get; set; }
    }
}