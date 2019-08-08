using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usda.PlantBreeding.Core.Interfaces;
using Usda.PlantBreeding.Core.Models;
using Usda.PlantBreeding.Data.DataAccess;
using Usda.PlantBreeding.Data.Models;
using Usda.PlantBreeding.Core.Translations;
using System.Web.Mvc;
using Usda.PlantBreeding.Core.Extensions;
using BSGUtils;
namespace Usda.PlantBreeding.Core.Infrastructure
{
    public class MapUow : IMapUOW
    {
        private IPlantBreedingRepo u_repo;
        private IAccessionUOW a_repo;

        public MapUow() : this(new PlantBreedingRepo()) { }

        public MapUow(IPlantBreedingRepo repo)
        {
            u_repo = repo;
            a_repo = new AccessionUOW(repo);
        }


        public IEnumerable<DataListViewModel> SearchAccessions(string term, int genusId, int recordsToReturn)
        {
            return a_repo.Search(term, genusId, recordsToReturn);
        }

        public IEnumerable<DataListViewModel> SearchAccessions(string term, int genusId, int? mapId, bool mapOnly, int recordsToReturn)
        {
            return a_repo.Search(term, genusId, mapId, mapOnly, recordsToReturn);
        }
        public IEnumerable<DataListViewModel> SearchLocations(string term, int recordsToReturn)
        {
            IEnumerable<Location> locations = u_repo.GetLocations(term, recordsToReturn, true);
            return locations.ToDataListViewModel();
        }


        private MapComponent GetMapComponentFromViewModel(MapComponentViewModel mapcomponentvm)
        {
            MapComponent mapcomponent = null;
            if (mapcomponentvm.Id > 0)
            {
                ValidateMapComponentGenotype(mapcomponentvm.Id, mapcomponentvm.GenotypeId);
                mapcomponent = u_repo.GetMapComponent(mapcomponentvm.Id);
                mapcomponentvm.ToMapComponent(mapcomponent);

            }
            else
            {
                mapcomponent = mapcomponentvm.ToMapComponent();
                mapcomponent.isRemoved = false;
            }


            if (mapcomponent.GenotypeId.HasValue )
            {
                mapcomponent.Genotype = u_repo.GetGenotype(mapcomponent.GenotypeId.Value);
            }

            if (mapcomponent.Map == null)
            {
                mapcomponent.Map = u_repo.GetMap(mapcomponent.MapId);
            }
     

            return mapcomponent;

        }



        public void Save(MapComponentViewModel mapcomponentvm)
        {
            Years year = u_repo.GetYear(mapcomponentvm.YearsId);
            
            if (year == null || mapcomponentvm.YearsId <= 0)
            {
                throw new MapException(Properties.Settings.Default.ExceptionMapComponentInvalid);
            }


            MapComponent mapcomponent = GetMapComponentFromViewModel(mapcomponentvm);


            if (mapcomponent.Id > 0)
            {
                UpdateMapComponent(mapcomponent, mapcomponentvm.YearsId);
            }
            else
            {
             
                CreateMapComponent(mapcomponent, mapcomponentvm.YearsId);
            }


            MapComponentViewModel newMapComponent = u_repo.GetMapComponent(mapcomponent.Id).ToMapComponentViewModel();
            newMapComponent.CopyTo(mapcomponentvm);
        }

        public MapComponentViewModel GetDefaultMapComponent(int mapId)
        {
            Map map = u_repo.GetMap(mapId);
            return new MapComponentViewModel()
                            {
                                MapId = map.Id,
                                PlantsInRep = map.DefaultPlantsInRep
                            };

        }
        private void CreateMapComponent(MapComponent mapcomponent, int yearId)
        {

            Map map = u_repo.GetMap(mapcomponent.MapId);
            Years y = map.Years.SingleOrDefault(t => t.Id == yearId);

            mapcomponent.Map = map;
            mapcomponent.CreatedYear = y.Year;
            u_repo.SaveMapComponent(mapcomponent);
            MapComponentYears mapcompyear = new MapComponentYears()
                                            {
                                                MapComponentId = mapcomponent.Id,
                                                Year = y,
                                                YearsId = y.Id
                                            };
            u_repo.SaveMapComponentYear(mapcompyear);
            mapcomponent.MapComponentYears.Add(mapcompyear);

        }


        public void UpdateMapComponentGenotype(int mapComponentId, int? genotypeId)
        {

            ValidateMapComponentGenotype(mapComponentId, genotypeId);
            u_repo.SaveMapComponentGenotype(mapComponentId, genotypeId);
        }

        private void UpdateMapComponent(MapComponent mapcomponent, int yearId)
        {
            if (mapcomponent.Id <= 0 )
            {
                throw new MapException(Properties.Settings.Default.ExceptionMapComponentInvalid);

            }

            u_repo.UpdateMapComponent(mapcomponent);
            UpdateMapComponentYears(mapcomponent, yearId);
        }

     

        private void ValidateMapComponentGenotype(int mapComponentId, int? newGenotypeId)
        {
            MapComponent old = u_repo.GetMapComponent(mapComponentId);
            if (old == null)
            {
                throw new MapException(Properties.Settings.Default.ExceptionMapComponentInvalid);

            }

            Genotype newGenotype = null;
            if (newGenotypeId.HasValue)
            {
                newGenotype = u_repo.GetGenotype(newGenotypeId.Value);
            }

            //Genotype is empty and the old has a value

            if (newGenotype == null && old.GenotypeId.HasValue)
            {
                if (old.MapComponentYears.HasAny() && old.MapComponentYears.SelectMany(t => t.Answers).HasAny())
                {
                    throw new MapException(Properties.Settings.Default.ExceptionMapComponentYear);
                }
            }


        }
        /// <summary>
        /// Adds the map component year if it doesn't exist 
        /// </summary>
        /// <param name="mapcomponent"></param>
        /// <param name="yearId"></param>
        private void UpdateMapComponentYears(MapComponent mapcomponent, int yearId)
        {
            MapComponent old = u_repo.GetMapComponent(mapcomponent.Id);
            MapComponentYears current = old.MapComponentYears.SingleOrDefault(t => t.YearsId == yearId);
            if (current.Id <= 0)
            {
                Years y = mapcomponent.Map.Years.SingleOrDefault(t => t.Id == yearId);
                current.MapComponentId = mapcomponent.Id;
                current.MapComponent = mapcomponent;
                current.Year = y;
                current.YearsId = y.Id;
                u_repo.SaveMapComponentYear(current);
            }
        }


        public void DeleteMapComponentForYear(int mapcomponentId, int yearId)
        {
            MapComponent mapcomponent = u_repo.GetMapComponent(mapcomponentId);
            MapComponentYears mapcompyear = mapcomponent.MapComponentYears.SingleOrDefault(t => t.YearsId == yearId);
            if (mapcomponent == null)
            {
                throw new MapException(Properties.Settings.Default.ExceptionMapComponentInvalid);
            }

            if (mapcompyear.Answers.HasAny() || mapcompyear.Fates.HasAny() || !mapcompyear.Comments.IsNullOrWhiteSpace())
            {
                throw new MapException(Properties.Settings.Default.ExceptionMapComponentYear);
            }

            u_repo.DeleteMapComponentYear(mapcompyear);
        }


        public void CreateMap(MapQuestionListViewModel model)
        {
            Map map = model.Map;
            if (map == null)
            {
                throw new MapException("Map is empty");
            }
            IEnumerable<Question> selectedQuestions = model.Questions.Where(q => q.isChecked).ToQuestion();
            IEnumerable<Question> mapQuestions = u_repo.GetQuestions(t => t.GenusId == model.Map.GenusId);


            map.Questions = mapQuestions.Intersect(selectedQuestions, new QuestionComparer()).ToList();
            map.Genus = u_repo.GetGenus(map.GenusId);
            map.Retired = false;
            u_repo.SaveMap(map);
            Years year = new Years()
                                {
                                    Map = map,
                                    MapId = map.Id,
                                    Year = map.PlantingYear
                                };
            u_repo.SaveYear(year);
            map.Years.Add(year);
            u_repo.SaveMap(map);

        }

        public Years GetNewMapComponentYears(int mapId, int newyear)
        {
            Map map = u_repo.GetMap(mapId);
            if (map == null)
            {
                throw new MapException("Map is empty");
            }
            bool exists = map.Years.HasAny(t => t.Year == newyear.ToString());
            if (exists)
            {
                throw new MapException("Map year exists");

            }
            Years closest = map.Years.Where(t => int.Parse(t.Year) < newyear).OrderByDescending(t => int.Parse(t.Year)).FirstOrDefault();
            if (closest == null)
            {
                throw new MapException("Problem Creating Year");
            }

            Years year = new Years()
                            {
                                Map = map,
                                MapId = mapId,
                                Year = newyear.ToString()
                            };
            IEnumerable<MapComponentYears> mapcomponentsyears = map.MapComponents.SelectMany(t => t.MapComponentYears).Where(t => t.YearsId == closest.Id);
            if (mapcomponentsyears.HasAny())
            {
                IEnumerable<MapComponent> mapcomps = mapcomponentsyears.Select(t => t.MapComponent);
                CreateMapComponentYears(mapcomps, year);
            }

            return year;
        }

        public MapBuilderViewModel GetMapBuilderViewModel(int mapId)
        {
            Map map = u_repo.GetMap(mapId);
            IEnumerable<Years> years = map.Years.OrderByDescending(t => int.Parse(t.Year));
            Years latest = map.Years.OrderByDescending(t => t.Year).FirstOrDefault();
            Years earliest = map.Years.OrderBy(t => t.Year).FirstOrDefault();

            int? maxRow = map.MapComponents.Select(t => t.Row).Max();

            //gets maxRow and creates a list of nullable int accordingly
            List<int?> rows = new List<int?>();

            if (maxRow.HasValue)
            {
                List<int> tempRow = Enumerable.Range(1, maxRow.Value).ToList();
                foreach (var i in tempRow)
                    rows.Add(i);
            }
            else
            {
                rows.Add(1);
            }

            int row = rows.Min().Value;

            var yearItems = years.ToSelectList(t => t.Id.ToString(), t => t.Year, t => t.Id == latest.Id);
            List<MapComponentViewModel> mapcomps = GetMapComponents(map, row, latest.Id).ToList();

            int currentYear = DateTime.Now.Year - 2;
            var yearRange = Enumerable.Range(currentYear, 5).Where(t => t > int.Parse(earliest.Year)).Select(t => t.ToString()).ToList();
            var newYears = yearRange.OrderBy(t => t).ToSelectList(t => t.ToString(), t => t.ToString());

            return new MapBuilderViewModel()
                                    {
                                        Map = map,
                                        MapComponentsRow = mapcomps,
                                        Years = yearItems,
                                        Rows = rows,
                                        NewMapComponent = GetDefaultMapComponent(map.Id),
                                        NewYears = newYears

                                    };

        }
        public IEnumerable<MapComponentViewModel> GetMapComponents(Map map, int row, int yearId)
        {
            IEnumerable<MapComponent> mapCompYears;
            if (map.IsSeedlingMap)
            {
                mapCompYears = map.MapComponents.Where(t => t.isSeedling == true && t.Row == row && t.MapComponentYears.Any(y => y.YearsId == yearId)).ToList();
            }
            else
            {
                mapCompYears = map.MapComponents.Where(x => x.Row == row && x.MapComponentYears.Any(y => y.YearsId == yearId)).ToList();
            }
            return mapCompYears.ToMapComponentViewModels().OrderBy(t => t.Row).ThenBy(t => t.PlantNum);
        }
        private void CreateMapComponentYears(IEnumerable<MapComponent> mapcomponents, Years year)
        {
            foreach (MapComponent mc in mapcomponents.ToList())
            {
                MapComponentYears mcy = new MapComponentYears()
                                                {
                                                    Year = year,
                                                    YearsId = year.Id,
                                                    MapComponent = mc,
                                                    MapComponentId = mc.Id

                                                };
                u_repo.SaveMapComponentYear(mcy);
                mc.MapComponentYears.Add(mcy);
            }
        }

        public bool IsDuplicateRow(int mapcomponentId, int mapId, int row, int plantnum, int yearId)
        {

            Map map = u_repo.GetMap(mapId);
            return map.MapComponents.Where(t => t.Id != mapcomponentId).SelectMany(t => t.MapComponentYears).Where(t => t.YearsId == yearId).HasAny(t => t.MapComponent.Row == row && t.MapComponent.PlantNum == plantnum);

        }

    }
}
