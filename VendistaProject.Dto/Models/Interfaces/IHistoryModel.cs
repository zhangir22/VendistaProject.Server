using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendistaProject.Dto.Models.Interfaces
{
    public interface IHistoryModel
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
