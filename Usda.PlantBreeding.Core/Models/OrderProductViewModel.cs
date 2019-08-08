using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usda.PlantBreeding.Data.Models;
using Usda.PlantBreeding.Core.Translations;

namespace Usda.PlantBreeding.Core.Models
{
    public class OrderProductViewModel
    {
        public int Id { get; set; }
        public int GenotypeId { get; set; }
        public string GenotypeName { get; set; }
        public int? Quantity { get; set; }
        public int MaterialId { get; set; }
        public int OrderId { get; set; }
        public string Note { get; set; }
        public DateTime? VirusTested { get; set; }
        public GenotypeViewModel GenotypeVM { get; set; }
        public DateTime? DateSent { get; set; }
        public static OrderProductViewModel Create(OrderProduct op)
        {
            OrderProductViewModel vm = new OrderProductViewModel
            {
                Id = op.Id,
                GenotypeId = op.GenotypeId,
                MaterialId = op.MaterialId,
                OrderId = op.OrderId,
                Quantity = op.Quantity,
                Note = op.Note,
                VirusTested = op.VirusTested,
                GenotypeName = op.Genotype.Name,
                GenotypeVM = op.Genotype.ToGenotypeVM(),
                DateSent = op.DateSent
            };

            return vm;
        }

        public static OrderProduct ToOrderProduct(OrderProductViewModel orderProduct)
        {
            OrderProduct vm = new OrderProduct
            {
                Id = orderProduct.Id,
                GenotypeId = orderProduct.GenotypeId,
                MaterialId = orderProduct.MaterialId,
                OrderId = orderProduct.OrderId,
                Quantity = orderProduct.Quantity,
                Note = orderProduct.Note,
                VirusTested = orderProduct.VirusTested,
                DateSent = orderProduct.DateSent
            };
            return vm;
        }
    }

}
