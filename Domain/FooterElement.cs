using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class FooterElement
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public bool IsVisible { get; set; }
        public bool IsDeletable { get; set; }
        public string SourceLink { get; set; }

    }
}
