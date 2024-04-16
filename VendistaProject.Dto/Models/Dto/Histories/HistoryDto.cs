using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendistaProject.Dto.Models.Dto.Histories
{
    public class HistoryDto
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string Command { get; set; }
        public string Param1 { get; set; }
        public string Param2 { get; set; }
        public string Param3 { get; set; }
        public string Status { get; set; }
    }
}
