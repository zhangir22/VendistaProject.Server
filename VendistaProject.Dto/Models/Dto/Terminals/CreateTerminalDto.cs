﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendistaProject.Dto.Models.Dto.Terminals
{
    public class CreateTerminalDto:TerminalDto,ICreateDto
    {
        public DateTime DateCreated { get; set; }
    }
}