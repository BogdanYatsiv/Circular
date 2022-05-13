using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    [Table("Subproject")]
    public class Subproject
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string name { get; set; }

        public string language { get; set; }

        [Required]
        public string githubLink { get; set; }

        public DateTime createDate { get; set; }



        public int ProjectId { get; set; }

        public Project Project { get; set; }

        List<Commit> Commits { get; set; }

    }
}
