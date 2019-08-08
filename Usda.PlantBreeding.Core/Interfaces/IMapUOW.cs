using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usda.PlantBreeding.Core.Models;
using Usda.PlantBreeding.Data.Models;

namespace Usda.PlantBreeding.Core.Interfaces
{
    public interface IMapUOW
    {
        IEnumerable<DataListViewModel> SearchAccessions(string term, int genusId, int? mapId, bool mapOnly, int recordsToReturn);
        IEnumerable<DataListViewModel> SearchAccessions(string term, int genusId, int recordsToReturn);
        void Save(MapComponentViewModel mapcomponentvm);
        void DeleteMapComponentForYear(int mapcomponentId, int yearId);
        IEnumerable<DataListViewModel> SearchLocations(string term, int recordsToReturn);

        void CreateMap(MapQuestionListViewModel model);
        Years GetNewMapComponentYears(int mapId, int newyear);
        IEnumerable<MapComponentViewModel> GetMapComponents(Map map, int row, int yearId);

        MapBuilderViewModel GetMapBuilderViewModel(int mapId);

        MapComponentViewModel GetDefaultMapComponent(int mapId);
        bool IsDuplicateRow(int mapcomponentId, int mapId, int row, int plantnum, int yearId);

        void UpdateMapComponentGenotype(int mapComponentId, int? genotypeId);




    }
}
