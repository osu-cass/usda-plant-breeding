using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usda.PlantBreeding.Core.Models;
using Usda.PlantBreeding.Data.Models;
using BSGUtils;
namespace Usda.PlantBreeding.Core.Translations
{
    public static class DataListViewModelTranslator
    {

        public static IEnumerable<DataListViewModel> ToDataListViewModel(this IEnumerable<Genotype> genotypes)
        {
            var genotypeGivenNames = genotypes.Where(t => !t.GivenName.IsNullOrWhiteSpace())
                                                             .Select(t => new DataListViewModel
                                                                    {
                                                                        key = t.Id.ToString(),
                                                                        value = t.GivenName
                                                                    });

            var genotypesListOriginal = genotypes.Select(t => new DataListViewModel
                                                                {
                                                                    key = t.Id.ToString(),
                                                                    value = t.OriginalName
                                                                });

            var genotypesAlias1 = genotypes.Where(t => !t.Alias1.IsNullOrWhiteSpace())
                                                            .Select(t => new DataListViewModel
                                                                    {
                                                                        key = t.Id.ToString(),
                                                                        value = t.Alias1
                                                                    });
            var genotypesAlias2 = genotypes.Where(t => !t.Alias2.IsNullOrWhiteSpace())
                                                            .Select(t => new DataListViewModel
                                                            {
                                                                key = t.Id.ToString(),
                                                                value = t.Alias2
                                                            });


            return genotypeGivenNames.Union(genotypesListOriginal, new DataListViewModelComparer())
                .Union(genotypesAlias1, new DataListViewModelComparer())
                .Union(genotypesAlias2, new DataListViewModelComparer());

        }
        public static IEnumerable<DataListViewModel> ToDataListViewModel(this IEnumerable<AccessionSearch> accessions)
        {
            var genotypeGivenNames = accessions.Where(t => !t.GivenName.IsNullOrWhiteSpace())
                                                             .Select(t => new DataListViewModel
                                                             {
                                                                 key = t.GenotypeId.ToString(),
                                                                 value = t.GivenName
                                                             });

            var genotypesListOriginal = accessions.Select(t => new DataListViewModel
            {
                key = t.GenotypeId.ToString(),
                value = t.OriginalName
            });

            var genotypesAlias1 = accessions.Where(t => !t.Alias1.IsNullOrWhiteSpace())
                                                            .Select(t => new DataListViewModel
                                                            {
                                                                key = t.GenotypeId.ToString(),
                                                                value = t.Alias1
                                                            });
            var genotypesAlias2 = accessions.Where(t => !t.Alias2.IsNullOrWhiteSpace())
                                                            .Select(t => new DataListViewModel
                                                            {
                                                                key = t.GenotypeId.ToString(),
                                                                value = t.Alias2
                                                            });


            return genotypeGivenNames.Union(genotypesListOriginal, new DataListViewModelComparer())
                .Union(genotypesAlias1, new DataListViewModelComparer())
                .Union(genotypesAlias2, new DataListViewModelComparer());

        }


        public static IEnumerable<DataListViewModel> ToDataListViewModel(this IEnumerable<Location> locations)
        {
            return locations.Select(t => new DataListViewModel
                                                {
                                                    key = t.Id.ToString(),
                                                    value = t.Name
                                                });
        }
    }
}
