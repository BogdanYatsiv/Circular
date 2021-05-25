using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    class ProjectDTO
    {
        public int Id { get; set; }
        public string githubLink { get; set; }
        public User user { get; set; }
    }
}
