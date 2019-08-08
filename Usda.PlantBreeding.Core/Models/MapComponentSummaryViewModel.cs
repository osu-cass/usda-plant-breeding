using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Usda.PlantBreeding.Data.Models;

namespace Usda.PlantBreeding.Core.Models
{
    public class MapComponentSummaryViewModel
    {
        private string m_GenotypeFamilyFemaleGenotypeName = null;

        private string m_GenotypeFamilyMaleGenotypeName = null;
        public int Id { get; set; }
        public int MapComponentId { get; set; }

        [Display(Name = "Genotype")]

        public int MapComponentGenotypeId { get; set; }
        [Display(Name = "Picking")]

        public string MapComponentPickingNumber { get; set; }
        [Display(Name = "Plot")]

        public int MapComponentPlantNum { get; set; }
        [Display(Name = "Location")]

        public int MapComponentLocation { get; set; }
        [Display(Name = "Crosstype")]
        public string MapComponentGenotypeFamilyCrossTypeName { get; set; }


        public string MapComponentGenotypeName { get; set; }
        [Display(Name = "P2")]

        public string MapComponentGenotypeFamilyMaleGenotypeName
        {
            get
            {
                string result = "N/A";

                if (!string.IsNullOrWhiteSpace(m_GenotypeFamilyMaleGenotypeName))
                {
                    result = m_GenotypeFamilyMaleGenotypeName;
                }

                return result;
            }
            set
            {
                m_GenotypeFamilyMaleGenotypeName = value;
            }
        }

        [Display(Name = "P1")]

        public string MapComponentGenotypeFamilyFemaleGenotypeName
        {
            get
            {
                string result = "N/A";

                if (!string.IsNullOrWhiteSpace(m_GenotypeFamilyFemaleGenotypeName))
                {
                    result = m_GenotypeFamilyFemaleGenotypeName;
                }

                return result;
            }
            set
            {
                m_GenotypeFamilyFemaleGenotypeName = value;
            }
        }

        [Display(Name = "FemaleGP1")]
        public string MapComponentGenotypeFamilyFemaleGenotypeFamilyFemaleGenotypeName { get; set; }


        [Display(Name = "FemaleGP2")]
        public string MapComponentGenotypeFamilyFemaleGenotypeFamilyMaleGenotypeName { get; set; }

        [Display(Name = "MaleGP1")]
        public string MapComponentGenotypeFamilyMaleGenotypeFamilyFemaleGenotypeName { get; set; }


        [Display(Name = "MaleGP2")]
        public string MapComponentGenotypeFamilyMaleGenotypeFamilyMaleGenotypeName { get; set; }
       
        [Display(Name = "Row")]

        public int MapComponentRow { get; set; }
        [Display(Name = "Comments")]

        public string Comments { get; set; }
        [Display(Name = "Year")]

        public int YearId { get; set; }

        [Display(Name = "Year")]

        public string YearYear { get; set; }

        [Display(Name="Rep")]
        public string MapComponentRep { get; set; }


        [Display(Name = "Selected")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yy}", ApplyFormatInEditMode = true)]
        public DateTime? MapComponentGenotypeCreatedDate { get; set; }

        public IEnumerable<Fate> Fates { get; set; }

        public IEnumerable<SelectListItem> PossibleFates { get; set; }

        public IEnumerable<int> QuestionOrder { get; set; }

        public IDictionary<int, Answer> Answers { get; set; }
    }

}