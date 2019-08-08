using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usda.PlantBreeding.Data.Models;

namespace Usda.PlantBreeding.Core.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public int GenusId { get; set; }
        public GrowerViewModel Grower { get; set; }
        [DisplayName("Contact Id")]
        public int GrowerId { get; set; }
        public string GrowerName { get; set; }
        public int? LocationId { get; set; }
        public string LocationName { get; set; }
        public string LocationAddress { get; set; }
        public string Note { get; set; }
        public bool MTARequestedProp { get; set; }
        public bool MTARequestedTest { get; set; }
        public DateTime? MTAComplete { get; set; }

        public OrderViewModel() { }
        public OrderViewModel(
            int id,
            string name,
            int year,
            int genusId,
            int growerId,
            GrowerViewModel grower,
            string growerName,
            int? locationId,
            string locationName,
            string locationAddress,
            string note,
            bool mtaRequestedProp,
            bool mtaRequestedTest,
            DateTime? mtaComplete
            )
        {
            Id = id;
            Name = name;
            Year = year;
            GenusId = genusId;
            Grower = grower;
            GrowerId = growerId;
            GrowerName = growerName;
            LocationId = locationId;
            LocationName = locationName;
            LocationAddress = locationAddress;
            Note = note; 
            MTARequestedProp = mtaRequestedProp; 
            MTARequestedTest = mtaRequestedTest;
            MTAComplete = mtaComplete;
        }
        public static OrderViewModel Create(Order order)
        {
            if (order == null) throw new Exception("Order cannot be null");
            var grower = GrowerViewModel.Create(order.Grower);
            if (grower == null) throw new Exception("Grower cannot be null");
            OrderViewModel vm = new OrderViewModel
            (
                id: order.Id,
                name: order.Name,
                year: order.Year,
                grower: grower,
                genusId: order.GenusId,
                growerId: order.GrowerId,
                growerName: grower.FullName,
                locationId: order.LocationId,
                locationName: order.Location?.Name,
                locationAddress: order.Location?.Address,
                note: order.Note,
                mtaRequestedProp: order.MTARequestedProp,
                mtaRequestedTest: order.MTARequestedTest,
                mtaComplete: order.MTAComplete
            );

            return vm;
        }

        public static Order ToOrder(OrderViewModel order)
        {
            Order vm = new Order
            {
                Id = order.Id,
                GenusId = order.GenusId,
                Name = order.Name,
                GrowerId = order.GrowerId,
                Year = order.Year,
                LocationId = order.LocationId,
                Note = order.Note,
                MTARequestedProp = order.MTARequestedProp,
                MTARequestedTest = order.MTARequestedTest,
                MTAComplete = order.MTAComplete
            };

            return vm;
        }

    }
}
