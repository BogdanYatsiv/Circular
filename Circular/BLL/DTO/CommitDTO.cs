using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace BLL.DTO
{
    public class CommitDTO
    {
        public int Id { get; set; }

        public string ownerName { get; set; }

        public string description { get; set; }

        public DateTime createTime { get; set; }

        public Subproject subproject { get; set; }
    }
}
