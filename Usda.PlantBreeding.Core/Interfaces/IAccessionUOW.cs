using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usda.PlantBreeding.Core.Models;
using Usda.PlantBreeding.Data.Models;

namespace Usda.PlantBreeding.Core.Interfaces
{
    public interface IAccessionUOW
    {
        AccessionViewModel GetAccessionViewModel(int id);

        IPagedList<AccessionIndexViewModel> GetPageInIndex(int pageSize, int? genotypeId, string filter, int page, int genusId);

        IPagedList<AccessionIndexViewModel> GetPageInIndex(int pageSize, int page, int genusId);

        /// <summary>
        /// Gets the next accession for a new population.
        /// </summary>
        /// <param name="id">
        /// The genus ID of the family.
        /// </param>
        /// <returns></returns>
        AccessionViewModel GetNextAccession(int id);

        /// <summary>
        /// Gets the next available accession with origin and next available cross number
        /// </summary>
        /// <param name="id">genus id of the accession</param>
        /// <param name="originId">origin id to get the next available number</param>
        /// <returns></returns>
        AccessionViewModel GetNextAccession(int id, int originId);

        // TODO: comment
        void SaveAccessionViewModel(AccessionViewModel accessionViewModel);

        /// <summary>
        /// Returns true if the requested page number is within the pages.
        /// </summary>
        /// <param name="page">
        /// Requested page number.
        /// </param>
        /// <param name="total">
        /// Total number of items.
        /// </param>
        /// <param name="pageSize">
        /// Number of items per page.
        /// </param>
        /// <returns></returns>
        bool isReasonableRequest(int page, int total, int pageSize);

        IEnumerable<DataListViewModel> Search(string term, int genusId, int recordsToReturn);

        IEnumerable<DataListViewModel> Search(string term, int genusId, int? mapId, bool mapOnly, int recordsToReturn);

        string GetNextCross(int originId, int genusId);

        /// <summary>
        /// Returns the next selection number in the given family.
        /// </summary>
        /// <param name="familyId"></param>
        /// <returns></returns>
        int GetNextSelection(int familyId);

        /// <summary>
        /// Returns true if some other genotype in the same family as the given genotype has the given selection number, otherwise returns false.
        /// </summary>
        /// <param name="genotypeId"></param>
        /// <param name="selection"></param>
        /// <returns></returns>
        bool IsDuplicateSelection(int genotypeId, int selection, int familyId);

        /// <summary>
        /// Returns true if some other base genotype in the same genus as the given family has the given origin and cross number, otherwise returns false.
        /// </summary>
        /// <param name="familyId"></param>
        /// <param name="originId"></param>
        /// <param name="cross"></param>
        /// <returns></returns>
        bool IsDuplicatePopulation(int familyId, int originId, string cross, int genusId);

        /// <summary>
        /// Restores properties from repository that are missing from given accession.
        /// </summary>
        /// <param name="accession"></param>
        void RestoreProperties(AccessionViewModel accession);
        Genotype CreateGenotypeSelection(int genotypeId);

        void UpdateGenotypePloidy(int genotypeId, string ploidy);
        void UpdateGenotypeNote(int genotypeId, string note);
        void CreatePopulationFromGenotype(int genotypeId);
        void DeleteSelection(int genotypeId);
        Genotype GetGenotype(int id);
        void UpdateGenotypeIsPopulation(int id, bool isPopulation);
    }
}
