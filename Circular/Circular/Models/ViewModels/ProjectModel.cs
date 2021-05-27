﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Circular.Models.ViewModels
{
    public class ProjectViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Language { get; set; }

        public string GithubLink { get; set; }

        //public DateTime DateTime { get; set; }
    }

    public class CreateProjectModel
    {
        [Required]
        public string GithubLink { get; set; }
    }
}
