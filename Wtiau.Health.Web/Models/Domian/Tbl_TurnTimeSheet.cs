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
    
    public partial class Tbl_TurnTimeSheet
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tbl_TurnTimeSheet()
        {
            this.Tbl_TakeTurn = new HashSet<Tbl_TakeTurn>();
        }
    
        public int TTS_ID { get; set; }
        public System.Guid TTS_Guid { get; set; }
        public int TTS_TurnID { get; set; }
        public string TTS_Name { get; set; }
        public int TTS_MaxSize { get; set; }
        public bool TTS_IsActive { get; set; }
        public bool TTS_IsDelete { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_TakeTurn> Tbl_TakeTurn { get; set; }
        public virtual Tbl_Turn Tbl_Turn { get; set; }
    }
}