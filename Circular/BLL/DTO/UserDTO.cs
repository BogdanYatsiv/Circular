﻿using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    class UserDTO
    {   
        public string Id { get; set; }
        public List<Project> Projects { get; set; }
    }
}
