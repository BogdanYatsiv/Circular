using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    [Table("Commit")]
    public class Commit
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string ownerName { get; set; }

        public string description { get; set; }

        public DateTime createTime { get; set; }


        public Subproject Subproject { get; set; }

        public int SubProjectId { get; set; }

    }
}
