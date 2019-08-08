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
    
    public partial class Genus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Genus()
        {
            this.Questions = new HashSet<Question>();
            this.CrossTypes = new HashSet<CrossType>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public int DefaultPlantsInRep { get; set; }
        public string VirusLabel1 { get; set; }
        public string VirusLabel2 { get; set; }
        public string VirusLabel3 { get; set; }
        public bool Retired { get; set; }
        public Nullable<int> ExternalId { get; set; }
        public string VirusLabel4 { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Question> Questions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CrossType> CrossTypes { get; set; }
    }
}
