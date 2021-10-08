using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Application.Results.Admin.Users
{
    public class FooterElementResultDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public bool IsVisible { get; set; }
        public bool IsDeletable { get; set; }
        public string SourceLink { get; set; }

        public FooterElementResultDto()
        {

        }
        public FooterElementResultDto(ref FooterElement footerElement)
        {
            Id = footerElement.Id;
            Name = footerElement.Name;
            Category = footerElement.Category;
            IsVisible = footerElement.IsVisible;
            IsDeletable = footerElement.IsDeletable;
            SourceLink = footerElement.SourceLink;

        }
        public class FooterElementResult: Result
        {
            public IEnumerable<FooterElementResultDto> Elements { get; set; }
        }
            

    }
}
