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
    
    public partial class Question
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Question()
        {
            this.Answers = new HashSet<Answer>();
            this.Maps = new HashSet<Map>();
        }
    
        public int Id { get; set; }
        public string Text { get; set; }
        public int GenusId { get; set; }
        public bool Required { get; set; }
        public Nullable<bool> Retired { get; set; }
        public string Label { get; set; }
        public int Order { get; set; }
        public Nullable<int> ExternalId { get; set; }
        public Nullable<int> InputTypeId { get; set; }
    
        public virtual Genus Genus { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Answer> Answers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Map> Maps { get; set; }
        public virtual InputType InputType { get; set; }
    }
}
