using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendistaProject.Dto.Models.Interfaces;

namespace VendistaProject.Infrastructure.Models
{
    public class HistoryModel:IHistoryModel
    {
        public int id { get; set; }
        public DateTime dataTime { get; set; }
        public string command { get; set; }
        public string param1 { get; set; }
        public string param2 { get; set; }
        public string param3 { get; set; }
        public string status { get; set; }
    }
}
