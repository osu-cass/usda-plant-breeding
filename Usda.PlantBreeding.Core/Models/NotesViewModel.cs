using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Usda.PlantBreeding.Data.Models;


namespace Usda.PlantBreeding.Core.Models
{
    public class NotesViewModel
    {
        public IEnumerable<Note> Notes { get; set; }
        public Note NewNote { get; set; }
    }
}
