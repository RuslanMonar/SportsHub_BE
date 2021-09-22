using System;

namespace Domain
{
    public class Team
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public string Category { get; set; }

        public string SubCategory { get; set; }

        public string ImageUrl { get; set; }

        public DateTime AddedAt { get; set; }
    }
}
