using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Circular.Models.ViewModels
{
    public class SubprojectViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Language { get; set; }

        public string GithubLink { get; set; }

        public int ProjectId { get; set; }

        //public DateTime DateTime { get; set; }
    }

    public class CreateSubprojectModel
    {
        [Required]
        public string GithubLink { get; set; }

        public int ProjectId { get; set; }
    }
}
