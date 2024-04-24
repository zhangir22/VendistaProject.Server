using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendistaProject.Domain.Dto
{
    public class ResponseDto 
    {
        public int Status { get; set; }
        public List<string> Errors { get; } = new();
        public bool HasErrors => Errors.Count > 0;
     
 
    }
}
