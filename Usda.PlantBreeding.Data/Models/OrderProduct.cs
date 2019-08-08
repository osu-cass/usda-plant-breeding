using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usda.PlantBreeding.Data.Models
{
    [MetadataType(typeof(OrderProductMetadata))]
    public partial class OrderProduct
    {
    }

    public class OrderProductMetadata
    {
        [JsonIgnore]
        public virtual Genotype Genotype { get; set; }
        [JsonIgnore]
        public virtual Material Material { get; set; }
        [JsonIgnore]
        public virtual Order Order { get; set; }
    }
}
