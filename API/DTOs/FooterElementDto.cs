using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class FooterElementDto
    {
        public IEnumerable<FooterElement> Elements { get; set; }
    }
}
