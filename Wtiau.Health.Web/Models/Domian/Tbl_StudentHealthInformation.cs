//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Wtiau.Health.Web.Models.Domian
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tbl_StudentHealthInformation
    {
        public int SHI_ID { get; set; }
        public System.Guid SHI_Guid { get; set; }
        public int SHI_StudentID { get; set; }
        public double SHI_Height { get; set; }
        public double SHI_Weight { get; set; }
        public double SHI_BloodSuger { get; set; }
        public double SHI_BloodPressure { get; set; }
        public double SHI_BMI { get; set; }
        public bool SHI_IsDelete { get; set; }
    
        public virtual Tbl_Student Tbl_Student { get; set; }
    }
}
