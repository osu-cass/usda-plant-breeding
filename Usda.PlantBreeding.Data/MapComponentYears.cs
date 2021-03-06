//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Usda.PlantBreeding.Data.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class MapComponentYears
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MapComponentYears()
        {
            this.Fates = new HashSet<Fate>();
            this.Answers = new HashSet<Answer>();
        }
    
        public int Id { get; set; }
        public string Comments { get; set; }
        public int MapComponentId { get; set; }
        public int YearsId { get; set; }
    
        public virtual MapComponent MapComponent { get; set; }
        public virtual Years Year { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Fate> Fates { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Answer> Answers { get; set; }
    }
}
