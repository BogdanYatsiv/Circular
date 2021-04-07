using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DAL.Identity
{
    [Table("Comment")]
    class Comment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int user_id { get; set; }
        
        [Required]
        public int commit_id { get; set; }

        [Required]
        public string description { get; set; }
    }
}
