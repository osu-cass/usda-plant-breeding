using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Usda.PlantBreeding.Data.DataAccess;
using Usda.PlantBreeding.Data.Models;
using Usda.PlantBreeding.Data.Util;
using BSGUtils;
using AutoMapper;
using Usda.PlantBreeding.Core.Models;
using Usda.PlantBreeding.Site.Models;

namespace Usda.PlantBreeding.Core.Translations
{
    /// <summary>
    /// Responsible for translation between data and view models
    /// </summary>
    public static class SelectionSummaryTranslator
    {


        /// <summary>
        /// Creates a SelectionSummaryViewModel from a targeted genotype
        /// </summary>
        /// <param name="genotype">The target Genotype</param>
        /// <returns>Loaded SelectionSummaryViewModel</returns>
        public static SelectionSummaryViewModel ToSelectionSummaryViewModel(this Genotype genotype, PhenotypeEntryViewModel phenotypeSummary, IEnumerable<FamilyUseViewModel> familyUseList)
        {
            genotype.ThrowIfNull("genotype");
            phenotypeSummary.ThrowIfNull("phenotypeSummary");
            familyUseList.ThrowIfNull("familyUseList");

            return new SelectionSummaryViewModel()
            {
                GenotypeNote = genotype.Note,
                GenotypeId = genotype.Id,
                GenotypeName = genotype.Name,
                PloidyName = genotype.PloidyName,
                MaleParentName = genotype.Family.MaleGenotype == null ? "Unknown" : genotype.Family.MaleGenotype.IfNotNull(g => g.Name),
                FemaleParentName = genotype.Family.FemaleGenotype == null ? "Unknown" : genotype.Family.FemaleGenotype.IfNotNull(g => g.Name),
                FamilyUseList = familyUseList,
                Notes = genotype.ToNotesViewModel(),
                FlatNotes = genotype.ToFlatNotesViewModel(),
                PhenotypeSummaryVM = phenotypeSummary,
                ObservationList = null
            };
        }

        /// <summary>
        /// Returns a IEnumerable<FamilyUseViewModel>
        /// </summary>
        /// <param name="families"></param>
        /// <param name="genotype"></param>
        /// <returns></returns>
        public static IEnumerable<FamilyUseViewModel> ToFamilyUseViewModel(this IEnumerable<Family> families, Genotype genotype)
        {
            genotype.ThrowIfNull("genotype");
            families.ThrowIfNull("families");

            return families.Select(f => new FamilyUseViewModel()
            {
                OriginName = f.OriginalName,
                PurposeName = f.Purpose,
                Year = f.BaseGenotype.Year,
                CrossWithName = f.GetOtherName(genotype),
                SelectionCount = f.Genotypes.Count() - 1,
                Id = f.Id,
                SeedNumber = f.SeedNum,
                FieldNumber = f.FieldPlantedNum,
                GivenName = f.BaseGenotype.GivenName,
                Name = f.BaseGenotype.Name
            });
        }

        public static IEnumerable<GenotypeUseSummaryViewModel> ToGenotypeUseSummaryViewModel(this IEnumerable<Genotype> genotypes)
        {
            return Mapper.Map<IEnumerable<Genotype>, IEnumerable<GenotypeUseSummaryViewModel>>(genotypes);
        }

        public static string GetOtherName(this Family family, Genotype parent)
        {
            string val = string.Empty;
            if (family.MaleParent.HasValue && family.MaleParent.Value != parent.Id)
            {
                val = family.MaleGenotype.Name;
            }
            else if (family.FemaleParent.HasValue && family.FemaleParent.Value != parent.Id)
            {
                val = family.FemaleGenotype.Name;
            }
            else
            {
                val = "n/a";
            }

            return val;
        }


        public static NotesViewModel ToNotesViewModel(this Genotype genotype)
        {
            return new NotesViewModel()
            {
                NewNote = new Note() { GenotypeId = genotype.Id, Date = DateTime.Now },
                Notes = genotype.Notes.OrderBy(t => t.Date).ThenByDescending(t => t.Id)
            };
        }

        public static FlatNotesViewModel ToFlatNotesViewModel(this Genotype genotype)
        {
            return new FlatNotesViewModel()
            {
                NewNote = new FlatNote() { GenotypeId = genotype.Id, Date = DateTime.Now },
                Notes = genotype.FlatNotes.OrderBy(t => t.Date).ThenByDescending(t => t.Id)
            };
        }
    }
}