using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usda.PlantBreeding.Data.Models
{
    [MetadataType(typeof(OrderMetadata))]
    public partial class Order
    {
        public override string ToString()
        {
            return $"{this.Grower.LastName}, {this.Grower.FirstName}";
        }
    }

    public class OrderMetadata
    {
        [JsonIgnore]
        public virtual Genus Genus { get; set; }
        [JsonIgnore]
        public virtual Grower Grower { get; set; }
        [JsonIgnore]
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }

    }
}
