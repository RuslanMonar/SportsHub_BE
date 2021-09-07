﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Admin.Users;
using Domain;

namespace Application.Services.Admin.Interfaces
{
    public interface IUsersService
    {
        Task<SearchResult>  SearchUserAsync(string Username);

        Task<SearchResult> SortUsersAsync(string type);
        Task<Result> SwitchRolesAsync(string id);

        Task<Result> ChangeStatus(string id);

        Task<Result> DeleteUserAsync(string id);
    }
}