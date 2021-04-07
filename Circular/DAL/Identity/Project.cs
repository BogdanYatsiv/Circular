using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Identity
{
    [Table("Project")]
    class Project
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string name { get; set; }

        public string description { get; set; }

        [Required]
        public string githubLink { get; set; }

        [Required]
        public DateTime dateTime { get; set; }

        public List<User> User { get; set; }
    }
}
