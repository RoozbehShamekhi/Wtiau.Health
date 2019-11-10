﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Wtiau.Health.Web.Models.ViewModels
{
    public class Modal_QuestionAdd
    {
        public int ID { get; set; }
        [Display(Name = "عنوان سوال")]
        [DataType(DataType.MultilineText)]
        public string Name { get; set; }
        [Display(Name = "نوع سوال")]
        public string type { get; set; }
        [Display(Name = "مرحله")]
        public string step { get; set; }
        [Display(Name = " ")]
        public bool Lie { get; set; }

    }
}