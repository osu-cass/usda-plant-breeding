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
    
    public partial class Map
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Map()
        {
            this.MapComponents = new HashSet<MapComponent>();
            this.Questions = new HashSet<Question>();
            this.Years = new HashSet<Years>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string PlantingYear { get; set; }
        public int GenusId { get; set; }
        public bool IsSeedlingMap { get; set; }
        public string PicklistPrefix { get; set; }
        public bool Retired { get; set; }
        public int DefaultPlantsInRep { get; set; }
        public Nullable<int> LocationId { get; set; }
        public Nullable<int> ExternalId { get; set; }
        public string StartCorner { get; set; }
        public string LocationSuffix { get; set; }
        public Nullable<int> CrossTypeId { get; set; }
        public string Note { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MapComponent> MapComponents { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Question> Questions { get; set; }
        public virtual Genus Genus { get; set; }
        public virtual Location Location { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Years> Years { get; set; }
        public virtual CrossType CrossType { get; set; }
    }
}
