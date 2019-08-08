using System.Collections.Generic;
using System.Linq;
using Usda.PlantBreeding.Core.Interfaces;
using Usda.PlantBreeding.Core.Models;
using Usda.PlantBreeding.Data.DataAccess;
using Usda.PlantBreeding.Data.Models;
using Usda.PlantBreeding.Core.Translations;
using System.Web.Mvc;
using Usda.PlantBreeding.Core.Extensions;
using Usda.PlantBreeding.Site.Models;

namespace Usda.PlantBreeding.Core.Infrastructure
{
    public class SelectionSummaryUOW : ISelectionSummaryUOW
    {
        private IPlantBreedingRepo u_repo;
        private IAccessionUOW a_repo;
        private IPhenotypeEntryUOW p_repo;
   

        public SelectionSummaryUOW() : this(new PlantBreedingRepo()) { }

        public SelectionSummaryUOW(IPlantBreedingRepo repo)
        {
            u_repo = repo;
            a_repo = new AccessionUOW(repo);
            p_repo = new PhenotypeEntryUOW(repo);
        }

        private PhenotypeEntryViewModel GetPhenotypeEntry(int id)
        {
            return p_repo.GetPhenotypeSummary(id);
        }

        public IEnumerable<GenotypeUseSummaryViewModel> GetCrossSelections(int id)
        {
            var family = u_repo.GetFamily(id);
            if (family == null)
            {
                return null;
            }
            return family.Genotypes.Where(t => t.IsRoot == false).ToGenotypeUseSummaryViewModel().OrderBy(t => t.OriginalName);

        }

        public SelectionSummaryViewModel GetSelectionSummary(int id)
        {
            Genotype genotype = u_repo.GetGenotype(id);
            IEnumerable<Family> families = u_repo.GetQueryableFamilies(t => t.MaleParent == genotype.Id || t.FemaleParent == genotype.Id).ToList();
            IEnumerable<FamilyUseViewModel> fuvms = families.ToFamilyUseViewModel(genotype);
            PhenotypeEntryViewModel pdvm = GetPhenotypeEntry(id);
            SelectionSummaryViewModel ssvm = genotype.ToSelectionSummaryViewModel(pdvm, fuvms);
            return ssvm;
        }

        public void UpdateGenotypePloidy(int genotypeId, string ploidy)
        {
            a_repo.UpdateGenotypePloidy(genotypeId, ploidy);

        }
        public void UpdateGenotypeNote(int genotypeId, string note)
        {
            a_repo.UpdateGenotypeNote(genotypeId, note);
        }
        

    }
}
