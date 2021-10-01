using System;

namespace Domain
{
    public class Team
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public virtual Category Category { get; set; }

        public virtual SubCategory SubCategory { get; set; }

        public string ImageUrl { get; set; }

        public DateTime AddedAt { get; set; }
    }
}
