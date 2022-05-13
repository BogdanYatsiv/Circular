using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

 namespace DAL.Entities
    {
        [Table("Project")]
        public class Project
        {
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }

            public string name { get; set; }


            public string UserId { get; set; }

            public User User { get; set; }


            public List<Subproject> Subproject { get; set; }

        }
    }
