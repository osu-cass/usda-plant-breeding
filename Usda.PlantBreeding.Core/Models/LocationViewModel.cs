using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usda.PlantBreeding.Data.Models;

namespace Usda.PlantBreeding.Core.Models
{
    public class LocationViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Retired { get; set; }
        public string Description { get; set; }
        public bool MapFlag { get; set; }
        public int? PrimaryContactId { get; set; }
        public string Address { get; set; }
        public string PrimaryContactName { get; set; }
        public virtual GrowerViewModel Grower {get;set;}

        public static LocationViewModel Create(Location location)
        {
            if (location == null) return null;
            var growerVM = GrowerViewModel.Create(location.Grower);
            return new LocationViewModel
            {
                Id = location.Id,
                Name = location.Name,
                MapFlag = location.MapFlag,
                Description = location.Description,
                PrimaryContactId = location.PrimaryContactId,
                Retired = location.Retired,
                Grower = growerVM,
                PrimaryContactName = growerVM?.FullName,
                Address = location.Address
            };
        }

        public static Location ToLocation(LocationViewModel locationVM )
        {
            if (locationVM == null) throw new ArgumentNullException();
            return new Location
            {
                Id = locationVM.Id,
                Name = locationVM.Name,
                MapFlag = locationVM.MapFlag,
                Description = locationVM.Description,
                PrimaryContactId = locationVM.PrimaryContactId,
                Retired = locationVM.Retired,
                Address = locationVM.Address
            };
        }


    }
}
