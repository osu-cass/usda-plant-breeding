﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usda.PlantBreeding.Data.Models
{
    [MetadataType(typeof(OriginMetaData))]
    public partial class Origin
    {
        public override string ToString()
        {
            return Name;
        }
    }

    public class OriginMetaData
    {

        [Required]
        [StringLength(20)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Value")]        
        public string Value { get; set; }
    }
}
