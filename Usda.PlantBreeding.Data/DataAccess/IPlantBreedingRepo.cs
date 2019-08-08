using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Usda.PlantBreeding.Data.Models;
using Usda.PlantBreeding.Data.Util;

namespace Usda.PlantBreeding.Data.DataAccess
{
    /// <summary>
    /// Interface representation of our data access layer.
    /// </summary>
    public interface IPlantBreedingRepo : IDisposable
    {
        #region Access

        /// <summary>
        /// Gets all Fruit Families from the database.
        /// </summary>
        IEnumerable<Family> GetFamilies();
        /// <summary>
        /// Gets a Family by specified id
        /// </summary>
        Family GetFamily(int id);

        /// <summary>
        /// Gets all families in a genus
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IEnumerable<Family> GetFamilies(int id);

        IEnumerable<Family> GetFamilies(string term, int genusId, int recordsToReturn);

        IQueryable<Family> GetQueryableFamilies(string term, int genusId);


        IQueryable<Family> GetQueryableFamilies(Expression<Func<Family, bool>> predicate);

        /// <summary>
        /// Retrieves the families by predicate
        /// </summary>
        IEnumerable<Family> GetFamilies(Expression<Func<Family, bool>> predicate);

        /// <summary>
        /// Gets all Fruit Genotypes from the database.
        /// </summary>
        IEnumerable<Genotype> GetGenotypes();

        /// <summary>
        /// Gets a list of genotypes by predicate
        /// </summary>
        /// <param name="predicate"></param>
        IEnumerable<Genotype> GetGenotypes(Expression<Func<Genotype, bool>> predicate);

        /// <summary>
        /// Gets all genotypes which match the given search term.
        /// </summary>
        IQueryable<Genotype> GetQueryableGenotypes(string term, int genusId);
        IEnumerable<Genotype> GetGenotypes(string term, int genusId, int recordsToReturn);
        IEnumerable<Genotype> GetGenotypes(string term, int genusId, int recordsToReturn, int mapId);
        IEnumerable<Genotype> GetGenotypes(string term, int genusId, int recordsToReturn, int mapId, bool mapOnly);
        IQueryable<Genotype> GetQueryableGenotypes(Expression<Func<Genotype, bool>> predicate);


        /// <summary>
        /// Gets the genotype by specified id
        /// </summary>
        Genotype GetGenotype(int id);

        /// <summary>
        /// Gets all Fruit Crosstypes from the database.
        /// </summary>
        IEnumerable<CrossType> GetCrossTypes();

        /// <summary>
        /// Gets a CrossType with the specified Id.
        /// </summary>
        CrossType GetCrossType(int id);
        IEnumerable<CrossType> GetGenusCrossTypes(int genusId);

        /// <summary>
        /// Gets all Fruit Genus from the database.
        /// </summary>
        IEnumerable<Genus> GetAllGenera();

        /// <summary>
        /// Gets the Fruit Genus by the specified Id.
        /// </summary>
        Genus GetGenus(int id);

        /// <summary>
        /// Gets all Fruit Origins from the database.
        /// </summary>
        IEnumerable<Origin> GetOrigins();

        IEnumerable<Origin> GetOrigins(string term, int recordsToReturn);


        /// <summary>
        /// Gets the Fruit Origin by Id.
        /// </summary>
        Origin GetOrigin(int id);
        string GetNextCross(int originId, int genusId);


        /// <summary>
        /// Gets all Fruit PLoidies from the database.
        /// </summary>
        IEnumerable<Ploidy> GetPloidies();

        /// <summary>
        /// Gets a Fruit Ploidy by id.
        /// </summary>
        Ploidy GetPloidyById(int id);

        /// <summary>
        /// Gets all Questions from the database.
        /// </summary>
        IEnumerable<Question> GetQuestions();

        /// <summary>
        /// Gets list of questions based on predicate func
        /// </summary>

        IEnumerable<Question> GetQuestions(Expression<Func<Question, bool>> predicate);


        IEnumerable<InputType> InputTypes();
        /// <summary>
        /// Gets a list of fates
        /// </summary>
        IEnumerable<Fate> GetFates();

        void UpdateMapComponentFates(IEnumerable<Fate> selected, int mapComponentYear);

        /// <summary>
        /// Gets a fate by its Id
        /// </summary>
        Fate GetFate(int id);

        /// <summary>
        /// Saves a fate
        /// </summary>
        void SaveFate(Fate fate);

        /// <summary>
        /// Gets a Question by id.
        /// </summary>
        Question GetQuestion(int id);

        /// <summary>
        /// Gets all Candidates from the database
        /// </summary>
        IEnumerable<Candidate> GetCandidates();

        /// <summary>
        /// Retrieves list of Candidates based on predicate func
        /// </summary>
        IEnumerable<Candidate> GetCandidates(Expression<Func<Candidate, bool>> predicate);

        /// <summary>
        /// Get Candidate by ID
        /// </summary>
        Candidate GetCandidate(int id);

        /// <summary>
        /// Get Candidate by GenotypeID
        /// </summary>
        Candidate GetCandidate(int id, int year);


        Years GetYear(int id);

        /// <summary>
        /// Gets all Maps from the database.
        /// </summary>
        IEnumerable<Map> GetMaps();

        /// <summary>
        /// Retrieves list of Maps based on predicate func
        /// </summary>
        IEnumerable<Map> GetMaps(Expression<Func<Map, bool>> predicate);
        IQueryable<Map> GetQueryableMaps(Expression<Func<Map, bool>> predicate);

        /// <summary>
        /// Gets map by id.
        /// </summary>
        Map GetMap(int id);

        /// <summary>
        /// Gets all map compononts from the database.
        /// </summary>
        IEnumerable<MapComponent> GetMapComponents();

        /// <summary>
        /// Gets the Map component by id.
        /// </summary>
        MapComponent GetMapComponent(int id);

        /// <summary>
        /// Gets the Map component by MapId.
        /// </summary>
        IEnumerable<MapComponent> GetMapComponents(int id);

        IQueryable<MapComponent> GetQueryableMapComponents(Expression<Func<MapComponent, bool>> predicate);

        IQueryable<MapComponentYears> GetQueryableMapComponentYears(Expression<Func<MapComponentYears, bool>> predicate);


        /// <summary>
        /// Retrieves all of a maps questions by genus id, filtered by required bool param
        /// </summary>
        IEnumerable<Question> GetMapQuestions(int genusId, bool required);


        /// <summary>
        /// Gets genotype's parents. 
        /// </summary>
        /// <remarks>
        /// First item is always the male.
        /// </remarks>
        Tuple<Genotype, Genotype> GetGenotypeParents(Genotype genotype);

        /// <summary>
        /// Get all cross plans
        /// </summary>
        IEnumerable<CrossPlan> GetCrossPlans();
        IEnumerable<CrossPlan> GetCrossPlans(Expression<Func<CrossPlan, bool>> predicate);
        /// <summary>
        /// Get a cross plan by specified Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        CrossPlan GetCrossPlan(int id);

        IQueryable<CrossPlan> GetQueryableCrossPlans(Expression<Func<CrossPlan, bool>> predicate);


        /// <summary>
        /// Get all locations
        /// </summary>
        IEnumerable<Location> GetLocations();
        IEnumerable<Location> GetLocations(string term, int recordsToReturn, bool mapFlag);



        ///<summary>
        /// Get a location by Id
        /// </summary>
        Location GetLocation(int id);
        IEnumerable<FlatNote> GetFlatNotes(Expression<Func<FlatNote, bool>> predicate);
        FlatNote GetFlatNote(int id);
        IEnumerable<Note> GetNotes(Expression<Func<Note, bool>> predicate);

        Note GetNote(int id);

        FlatType GetFlatType(int id);
        IEnumerable<FlatType> GetFlatTypes(Expression<Func<FlatType, bool>> predicate);
        IEnumerable<FlatType> GetFlatTypes();

        Origin GetDefaultOrigin();

        IEnumerable<UserPreference> GetUserPreferences();
        UserPreference GetUserPreference(string userId);
        void SaveUserPreference(UserPreference userpreference);

        Answer GetAnswer(Expression<Func<Answer, bool>> predicate);

        void SaveMaterial(Material material);
        IEnumerable<Material> GetMaterials();
        Material GetMaterial(int id);
        void DeleteMaterial(Material material);

        void SaveGrower(Grower grower);
        IEnumerable<Grower> GetGrowers();
        Grower GetGrower(int id);
        IEnumerable<Grower> GetGrowers(string term, int recordsToReturn);

        IEnumerable<Order> GetOrders();
        IEnumerable<Order> GetOrders(Expression<Func<Order, bool>> predicate);
        Order GetOrder(Expression<Func<Order, bool>> predicate);
        void SaveOrder(Order order);
        IEnumerable<OrderProduct> GetOrderProducts();
        IEnumerable<OrderProduct> GetOrderProducts(Expression<Func<OrderProduct, bool>> predicate);
        OrderProduct GetOrderProduct(Expression<Func<OrderProduct, bool>> predicate);

        void SaveOrderProduct(OrderProduct SaveOrderProduct);
        void DeleteOrderProduct(OrderProduct orderProduct);

        #endregion

        #region Modifiers

        /// <summary>
        /// Saves a question to the database.
        /// </summary>
        void SaveAnswer(Answer answer);
        void DoSaveAnswer(Answer answer);



        /// <summary>
        /// Saves a given family.
        /// </summary>
        void SaveFamily(Family family);

        void DoSaveFamily(Family family);

        /// <summary>
        /// Saves a given genotype.
        /// </summary>
        void SaveGenotype(Genotype genotype);

        void UpdateGeotypesAlias(int familyId, string alias);

        
        /// <summary>
        /// Updates or inserts a genotype in the context without the copyto method
        /// </summary>
        /// <param name="genotype"></param>
       void DoSaveGenotype(Genotype genotype);

        void SaveGenotypeCreatedDate(int genotypeId, DateTime created);



        /// <summary>
        /// Saves a given crosstype.
        /// </summary>
        void SaveCrossType(CrossType crossType);

        /// <Summary>
        /// Saves a given cross plan
        /// </Summary>
        void SaveCrossPlan(CrossPlan crossPlan);
        void SaveCrossPlan(IEnumerable<CrossPlan> crossPlans);


        /// <summary>
        /// Deletes a given Crosstype.
        /// </summary>
        void DeleteCrossType(CrossType crossType);

        /// <summary>
        /// Deletes a specified family from the db
        /// </summary>
        void DeleteFamily(Family family);

        /// <summary>
        /// Deletes a genotype from the db
        /// </summary>
        void DeleteGenotype(Genotype genotype);

        void DeleteGenotypeWithRelated(Genotype genotype);


        /// <summary>
        /// Saves a given Genus.
        /// </summary>
        void SaveGenus(Genus genus);

        void SaveNote(Note note);

        void SaveFlatNote(FlatNote note);

        /// <summary>
        /// Deletes a Genus from the DB
        /// </summary>

        void DeleteGenus(Genus genus);

        /// <summary>
        /// Saves a given Origin.
        /// </summary>
        void SaveOrigin(Origin origin);

        void DeleteOrigin(Origin origin);

        /// <summary>
        /// Deletes a given cross plan
        /// </summary>
        /// <param name="crossPlan"></param>
        void DeleteCrossPlan(CrossPlan crossPlan);

        /// <summary>
        /// Saves a given Ploidy.
        /// </summary>
        void SavePloidy(Ploidy ploidy);

        /// <summary>
        /// Deletes a given Ploidy.
        /// </summary>
        void DeletePloidy(Ploidy ploidy);

        /// <summary>
        /// Saves the supplied Question.
        /// </summary>
        void SaveQuestion(Question question);

        /// <summary>
        /// Deletes the supplied Question.
        /// </summary>
        void DeleteQuestion(Question question);

        /// <summary>
        /// Retires a question which matches the specified Id.
        /// </summary>
        /// <param name="id"></param>
        void RetireQuestion(int id);

        /// <summary>
        /// Un-retires a question which matches the specified id.
        /// </summary>
        void UnRetireQuestion(int id);

        /// <summary>
        /// Saves a given Cadidate.
        /// </summary>
        void SaveCandidate(Candidate candidate);

        /// <summary>
        /// Deletes a given Candidate.
        /// </summary>
        void DeleteCandidate(Candidate candidate);

        /// <summary>
        /// Saves a given Map.
        /// </summary>
        void SaveMap(Map map);

        /// <summary>
        /// Deletes a given map.
        /// </summary>
        void DeleteMap(Map map);
        void SaveYear(Years years);

        void DeleteMapComponentYear(MapComponentYears mapcomponentyears);

        /// <summary>
        /// Updates mapcomponent without doing a copyto method.
        /// </summary>
        /// <param name="mapComponent"></param>
        void UpdateMapComponent(MapComponent mapComponent);
        void SaveMapComponentYear(MapComponentYears mapComponent);
        void DoSaveMapComponentYear(MapComponentYears mapComponentyears);


        void SaveMapComponentYearComment(MapComponentYears mapComponentYears);

        MapComponentYears GetMapComponentYear(int id);



        /// <summary>
        /// Saves a given MapComponent.
        /// </summary>
        void SaveMapComponent(MapComponent mapComponent);

        void SaveMapComponentGenotype(int mapComponentId, int? genotypeId);



        /// <summary>
        /// Deletes a given MapComponent.
        /// </summary>

        /// <summary>
        /// Saves a given Location 
        /// </summary>
        void SaveLocation(Location location);

        /// <summary>
        /// Deletes a Location
        /// </summary>
        void DeleteLocation(Location location);

        void SaveFlatType(FlatType flatType);

        void SavePurpose(Purpose purpose);
        IEnumerable<Purpose> GetPurposes();
        Purpose GetPurpose(int id);

        #endregion
    }
}
