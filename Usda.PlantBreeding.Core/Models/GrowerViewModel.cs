using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usda.PlantBreeding.Data.Models;

namespace Usda.PlantBreeding.Core.Models
{
    public class GrowerViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string WorkPhone { get; set; }
        public string MailingName { get; set; }
        public string MobilePhone { get; set; }
        public DateTime? CreatedDate { get; set; }

        public string FullName { get; set; }

        public static GrowerViewModel Create(Grower grower)
        {
            if (grower == null) return null;
            return new GrowerViewModel
            {
                Id = grower.Id,
                FirstName = grower.FirstName,
                LastName = grower.LastName,
                Email = grower.Email,
                WorkPhone = grower.WorkPhoneFormatted,
                MailingName = grower.MailingName,
                MobilePhone = grower.MobilePhoneFormatted,
                CreatedDate = grower.CreatedDate,
                FullName = $"{grower.FirstName} {grower.LastName}"
            };

        }

    }
}
