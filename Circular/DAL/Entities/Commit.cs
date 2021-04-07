using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DAL.Identity
{
    [Table("Commit")]
    class Commit
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int project_id { get; set; }

        [Required]
        public string hash_code { get; set; }

        public string description { get; set; }

        public List<Comment> Comment { get; set; }
    }
}
