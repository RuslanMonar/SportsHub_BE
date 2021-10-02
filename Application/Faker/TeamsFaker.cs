using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Bogus;
using Bogus.DataSets;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Data;

namespace Application.Faker
{
    public class TeamsFaker
    {
        public static async Task GenerateTeams(DataContext dataContext)
        {
            if (dataContext.Teams.Count() < 5 && dataContext.Categories.Count() < 2)
            {
                var nhl = new Category
                {
                    Name = "NHL"
                };
                var nba = new Category
                {
                    Name = "NBA"
                };
                var soccer = new Category
                {
                    Name = "Soccer"
                };


                var categories = new List<Category>
                { 
                    nhl, nba, soccer
                };

                var westernConferenceNHL = new SubCategory
                {
                    Name = "Western Conference",
                    Category = nhl
                };

                var easternConferenceNHL = new SubCategory
                {
                    Name = "Eastern Conference",
                    Category = nhl
                };

                var westernConferenceNBA = new SubCategory
                {
                    Name = "Western Conference",
                    Category = nba
                };

                var easternConferenceNBA = new SubCategory
                {
                    Name = "Eastern Conference",
                    Category = nba
                };

                var LaLiga = new SubCategory
                {
                    Name = "La Liga Santander",
                    Category = soccer
                };


                var subcategories = new List<SubCategory>
                {
                    westernConferenceNBA,
                    easternConferenceNBA,
                    westernConferenceNHL,
                    easternConferenceNHL,
                    LaLiga
                };

                var teams = new List<Team>
                {
                    new Team
                    {
                        Name = "Avalanche",
                        Location = "Colorado",
                        Category = nhl,
                        SubCategory = westernConferenceNHL,
                        AddedAt = DateTime.Now,
                        ImageUrl = "~/API/resources/img/teams/" + $"{"Avalanche"}-{"Colorado"}.png"
                    },
                    new Team
                    {
                        Name = "Bruins",
                        Location = "Boston",
                        Category = nhl,
                        SubCategory = easternConferenceNHL,
                        AddedAt = DateTime.Now,
                        ImageUrl = "~/API/resources/img/teams/" + $"{"Bruins"}-{"Boston"}.png"
                    },
                    new Team
                    {
                        Name = "Canadiens",
                        Location = "Montreal",
                        Category = nhl,
                        SubCategory = easternConferenceNHL,
                        AddedAt = DateTime.Now,
                        ImageUrl = "~/API/resources/img/teams/" + $"{"Canadiens"}-{"Montreal"}.png"
                    },
                    new Team
                    {
                        Name = "Oilers",
                        Location = "Edmonton",
                        Category = nhl,
                        SubCategory = westernConferenceNHL,
                        AddedAt = DateTime.Now,
                        ImageUrl = "~/API/resources/img/teams/" + $"{"Oilers"}-{"Edmonton"}.png"
                    },
                    new Team
                    {
                        Name = "Bulls",
                        Location = "Chickago",
                        Category = nba,
                        SubCategory = easternConferenceNBA,
                        AddedAt = DateTime.Now,
                        ImageUrl = "~/API/resources/img/teams/" + $"{"Bulls"}-{"Chickago"}.png"
                    },
                    new Team
                    {
                        Name = "Warriors",
                        Location = "Golden State",
                        Category = nba,
                        SubCategory = westernConferenceNBA,
                        AddedAt = DateTime.Now,
                        ImageUrl = "~/API/resources/img/teams/" + $"{"Warriors"}-{"Golden State"}.png"
                    },
                    new Team
                    {
                        Name = "Lakers",
                        Location = "Los Angeles",
                        Category = nba,
                        SubCategory = westernConferenceNBA,
                        AddedAt = DateTime.Now,
                        ImageUrl = "~/API/resources/img/teams/" + $"{"Lakers"}-{"Los Angeles"}.png"
                    },
                    new Team
                    {
                        Name = "FC Barcelona",
                        Location = "Barcelona",
                        Category = soccer,
                        SubCategory = LaLiga,
                        AddedAt = DateTime.Now,
                        ImageUrl = "~/API/resources/img/teams/" + $"{"FC Barcelona"}-{"Barcelona"}.png"
                    },
                    new Team
                    {
                        Name = "FC Barcelona",
                        Location = "Barcelona",
                        Category = soccer,
                        SubCategory = LaLiga,
                        AddedAt = DateTime.Now,
                        ImageUrl = "~/API/resources/img/teams/" + $"{"FC Barcelona"}-{"Barcelona"}.png"
                    },
                    new Team
                    {
                        Name = "Real",
                        Location = "Madrid",
                        Category = soccer,
                        SubCategory = LaLiga,
                        AddedAt = DateTime.Now,
                        ImageUrl = "~/API/resources/img/teams/" + $"{"Real"}-{"Madrid"}.png"
                    },
                    new Team
                    {
                        Name = "Atletico",
                        Location = "Madrid",
                        Category = soccer,
                        SubCategory = LaLiga,
                        AddedAt = DateTime.Now,
                        ImageUrl = "~/API/resources/img/teams/" + $"{"Atletico"}-{"Madrid"}.png"
                    },
                    new Team
                    {
                        Name = "Valencia CF",
                        Location = "Valencia",
                        Category = soccer,
                        SubCategory = LaLiga,
                        AddedAt = DateTime.Now,
                        ImageUrl = "~/API/resources/img/teams/" + $"{"Valencia CF"}-{"Valencia"}.png"
                    },


                };

                foreach (var team in teams)
                {
                    await dataContext.Teams.AddAsync(team);
                    await dataContext.SaveChangesAsync();
                }
            }
        }
    }
}