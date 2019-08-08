using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usda.PlantBreeding.Data.Models;

namespace Usda.PlantBreeding.Data.DataAccess
{
    /// <summary>
    /// Data Context for the repository.
    /// </summary>
    public class PlantBreedingContext : DbContext, IPlantBreedingContext
    {
        public IDbSet<Answer> Answers { get; set; }
        public IDbSet<Family> Families { get; set; }
        public IDbSet<Genotype> Genotypes { get; set; }
        public IDbSet<CrossType> CrossTypes { get; set; }
        public IDbSet<Genus> Genus { get; set; }
        public IDbSet<Origin> Origins { get; set; }
        public IDbSet<Ploidy> Ploidies { get; set; }
        public IDbSet<Question> Questions { get; set; }
        public IDbSet<Candidate> Candidates { get; set; }
        public IDbSet<Map> Maps { get; set; }
        public IDbSet<MapComponent> MapComponents { get; set; }
        public IDbSet<CrossPlan> CrossPlans { get; set; }
        public IDbSet<Location> Locations { get; set; }
        public IDbSet<Fate> Fates { get; set; }
        public IDbSet<Note> Notes { get; set; }
        public IDbSet<FlatNote> FlatNotes { get; set; }
        public IDbSet<FlatType> FlatType { get; set; }
        public IDbSet<Years> Years { get; set; }
        public IDbSet<MapComponentYears> MapComponentYears { get; set; }
        public IDbSet<UserPreference> UserPreferences { get; set; }
        public IDbSet<Purpose> Purposes { get; set; }
        public IDbSet<InputType> InputTypes { get; set; }
        public IDbSet<Order> Orders { get; set; }
        public IDbSet<OrderProduct> OrderProducts {get; set; }
        public IDbSet<Grower> Growers { get; set; }
        public IDbSet<Material> Materials { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();


        }

    }
}
