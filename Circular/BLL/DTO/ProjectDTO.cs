using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace BLL.DTO
{
    public class ProjectDTO
    {
        public int Id { get; set; }
        public string name { get; set; }
        public User user { get; set; }

        public List<Subproject> Subprojects { get; set; }
    }
}
