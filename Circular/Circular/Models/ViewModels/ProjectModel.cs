using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Circular.Models.ViewModels
{
    public class ProjectViewModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string GithubLink { get; set; }

        public DateTime DateTime { get; set; }
    }

    public class CreateProjectModel
    {
        //public string Name { get; set; }

        //public string Description { get; set; }

        public string GithubLink { get; set; }
    }
}
