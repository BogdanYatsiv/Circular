using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Identity
{
    [Table("User")]
    class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        public string password { get; set; }

        [Required]
        [Index(IsUnique = true)]
        public string email { get; set; }

        [Required]
        public bool isAdmin { get; set; }

        public List<Project> Project { get; set; }
        public List<Comment> Comment { get; set; }
    }
}
