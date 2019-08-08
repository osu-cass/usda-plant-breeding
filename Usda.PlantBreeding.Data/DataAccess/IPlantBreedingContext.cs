using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usda.PlantBreeding.Data.Models;

namespace Usda.PlantBreeding.Data.DataAccess
{
    /// <summary>
    /// Data Context interface (for testing purposes) for the USDA Repo.
    /// </summary>
    public interface IPlantBreedingContext : IDisposable
    {
        IDbSet<Answer> Answers { get; set; }
        IDbSet<Family> Families { get; set; }
        IDbSet<Genotype> Genotypes { get; set; }
        IDbSet<CrossType> CrossTypes { get; set; }
        IDbSet<Genus> Genus { get; set; }
        IDbSet<Origin> Origins { get; set; }
        IDbSet<Ploidy> Ploidies { get; set; }
        IDbSet<Question> Questions { get; set; }
        IDbSet<Candidate> Candidates { get; set; }
        IDbSet<Map> Maps { get; set; }
        IDbSet<MapComponent> MapComponents { get; set; }
        IDbSet<CrossPlan> CrossPlans { get; set; }
        IDbSet<Fate> Fates { get; set; }
        IDbSet<Location> Locations { get; set; }
        IDbSet<Note> Notes { get; set; }
        IDbSet<FlatNote> FlatNotes { get; set; }
        IDbSet<FlatType> FlatType { get; set; }
        IDbSet<Years> Years { get; set; }
        IDbSet<MapComponentYears> MapComponentYears { get; set; }
        IDbSet<UserPreference> UserPreferences { get; set; }
        IDbSet<Purpose> Purposes { get; set; }
        IDbSet<InputType> InputTypes { get; set; }
        IDbSet<Order> Orders { get; set; }
        IDbSet<OrderProduct> OrderProducts { get; set; }
        IDbSet<Grower> Growers { get; set; }
        IDbSet<Material> Materials { get; set; }



        int SaveChanges();
        Task<int> SaveChangesAsync();
        DbEntityEntry Entry(object entity);

    }
}
