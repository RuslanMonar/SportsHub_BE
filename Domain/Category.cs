using System.Collections.Generic;
using System.ComponentModel;

namespace Domain
{
    public class Category
    {
        public int Id { get; set; }

        
        public string Name { get; set; }
        
        public virtual List<SubCategory> SubCategories { get; set; }
    }
}
