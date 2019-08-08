using AutoMapper;
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
    public static class MapViewModelTranslator
    {
        public static MapComponentViewModel ToMapComponentViewModel(this MapComponent mapcomponent)
        {
            return Mapper.Map<MapComponent, MapComponentViewModel>(mapcomponent);
        }

        /// <summary>
        /// Returns a mapcomponent from a mapcomponentviewmodel
        /// </summary>
        /// <param name="mapcomponent"></param>
        /// <returns></returns>
        public static MapComponent ToMapComponent(this MapComponentViewModel mapcomponentvm)
        {
            MapComponent mapcomponent = new MapComponent();
            mapcomponentvm.CopyTo(mapcomponent);
            return mapcomponent;
        }

        /// <summary>
        /// Returns a mapcomponent from a mapcomponent viewmodel and copied from mapcomponent
        /// </summary>
        /// <param name="mapcomponent"></param>
        /// <param name="old"></param>
        /// <returns></returns>
        public static MapComponent ToMapComponent(this MapComponentViewModel mapcomponentvm, MapComponent old)
        {
         
            mapcomponentvm.isSeedling = old.isSeedling;
            mapcomponentvm.ExternalId = old.ExternalId;
            mapcomponentvm.PickingNumber = old.PickingNumber;

            mapcomponentvm.CopyTo(old);

            return old;
        }


        public static IEnumerable<MapComponentViewModel> ToMapComponentViewModels(this IEnumerable<MapComponent> mapcomponents)
        {
            return Mapper.Map<IEnumerable<MapComponent>, IEnumerable<MapComponentViewModel>>(mapcomponents);
        }

        public static MapViewModel ToMapVM(this Map m)
        {
            return new MapViewModel
            {
                Id = m.Id,
                Name = m.Name,
                EvaluationYear = m.EvaluationYear,
                CrossTypeName = m.CrossType?.Name,
                DefaultPlant = m.DefaultPlantsInRep.ToString(),
                LocationName = m.Location?.Name,
                LocationAddress = m.Location?.Address,
                LocationSuffix = m.LocationSuffix,
                Note = m.Note,
                PicklistPrefix = m.PicklistPrefix,
                StartCorner = m.StartCorner,
                PlantingYear = m.PlantingYear
            };
        }

    }
}
