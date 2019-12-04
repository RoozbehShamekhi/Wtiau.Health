using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Wtiau.Health.Web.Models.ViewModels
{
    public class Model_StudentHealthInformation
    {
        public int ID { get; set; }

        [Display(Name = "قد (سانتی متر)")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "لطفا مقداری را وارد نمایید")]
        public float Height { get; set; }

        [Display(Name = "وزن (کیلوگرم)")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "لطفا مقداری را وارد نمایید")]
        public float Weight { get; set; }

        [Display(Name = "قند خون")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "لطفا مقداری را وارد نمایید")]
        public float BloodSuger { get; set; }

        [Display(Name = "فشار خون")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "لطفا مقداری را وارد نمایید")]
        public float BloodPressure { get; set; }
    }
}