using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendistaProject.Domain.Dto.Models.Interfaces;

namespace VendistaProject.Domain.Dto.Models
{
    public class Terminal : IBaseModel
    {
        public int page_number { get; set; }
        public int items_per_page { get; set; }
        public int itmes_count { get; set; }
        public BodyTerminal[] items { get; set; }
    }
    public class MultyCommand: IBaseCommandModel 
    {
        public int command_id { get; set; }
        public int? parameter1 { get; set; } = 0;
        public int? parameter2 { get; set; } = 0;
        public int? parameter3 { get; set; } = 0;
        public int? parameter4 { get; set; } = 0;
        public string str_parameter1 { get; set; } = "string";
        public string str_parameter2 { get; set; } = "string";
    }
    public class BodyCommand:IBaseCommandModel 
    {
        public int id { get; set; }
        public string name { get; set; }
        public string parameter_name1 { get; set; }
        public string parameter_name2 { get; set; }
        public string parameter_name3 { get; set; }
        public string parameter_name4 { get; set; }
        public string str_parameter_name1 { get; set; }
        public string str_parameter_name2 { get; set; }
        public int? parameter_default_value1 { get; set; }
        public int? parameter_default_value2 { get; set; }
        public int? parameter_default_value3 { get; set; }
        public int? parameter_default_value4 { get; set; }
        public string str_parameter_default_value1 { get; set; }
        public string str_parameter_default_value2 { get; set; }
        public bool visible { get; set; }
    }
    public class Command: IBaseCommandModel 
    {
        public int pageNumber { get; set; }
        public int itemsPerPage { get; set; }
        public int itemsCount { get; set; }
        public BodyCommand[] items { get; set; }
    }
}
