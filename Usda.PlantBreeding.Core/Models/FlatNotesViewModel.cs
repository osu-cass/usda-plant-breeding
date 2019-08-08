using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Usda.PlantBreeding.Data.Models;

namespace Usda.PlantBreeding.Core.Models
{
    public class FlatNotesViewModel
    {
        public IEnumerable<FlatNote> Notes { get; set; }
        public FlatNote NewNote { get; set; }
    }
}