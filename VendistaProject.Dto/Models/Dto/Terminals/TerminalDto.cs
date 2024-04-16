using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendistaProject.Dto.Models.Dto.Terminals
{
    public class TerminalDto
    {
        public int page_number { get; set; }
        public int items_per_page { get; set; }
        public int itmes_count { get; set; }
        public BodyTerminalDto[] items { get; set; }
    }
}
