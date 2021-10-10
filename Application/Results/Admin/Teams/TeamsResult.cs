using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Identity;

namespace Application.Results.Admin.Teams
{
    public class GetAllTeamsDTO
    {
        public int Id { get; set; }
        public string TeamName { get; set; }
        public string Location { get; set; }
        public string AddedAt { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int SubCategoryID { get; set; }
        public string SubCategoryName { get; set; }
    }
    public class AllTeamsResult
    {
        public List<GetAllTeamsDTO> Teams { get; set; }

        public AllTeamsResult()
        {
            Teams = new List<GetAllTeamsDTO>();
        }
    }
}