using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Usda.PlantBreeding.Data.Util;

namespace Usda.PlantBreeding.Data.Models
{
    [MetadataType(typeof(GrowerMetadata))]
    [DisplayColumn("Value"), DisplayName("Contact")]
    public partial class Grower
    {
        public string MobilePhoneFormatted
        {
            get
            {
                return this.MobilePhone.FormatPhoneNumber();
            }
        }

        public string WorkPhoneFormatted
        {
            get
            {
                return this.WorkPhone.FormatPhoneNumber();
            }
        }
    }
    public class GrowerMetadata
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Mailing Name")]
        public string MailingName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Mobile Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^$|^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public string MobilePhone { get; set; }


        [Display(Name = "Work Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^$|^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public string WorkPhone { get; set; }


    }


}
