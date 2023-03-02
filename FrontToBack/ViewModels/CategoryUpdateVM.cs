﻿using System.ComponentModel.DataAnnotations;

namespace FrontToBack.ViewModels
{
    public class CategoryUpdateVM
    {
        [Required, MaxLength(50)]
        public string Name { get; set; }

        [Required, MinLength(5)]
        public string Description { get; set; }
    }
}
