using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usda.PlantBreeding.Data.Models;
using Usda.PlantBreeding.Data.Util;
using BSGUtils;
using System.Linq.Expressions;
using AutoMapper;

namespace Usda.PlantBreeding.Data.DataAccess
{
    /// <summary>
    /// Data access layer conforming to the Repository Pattern.
    /// </summary>
    public sealed class PlantBreedingRepo : IPlantBreedingRepo
    {
        private IPlantBreedingContext m_context;
        private bool m_disposed;

        public PlantBreedingRepo() : this(new PlantBreedingContext()) { }

        public PlantBreedingRepo(IPlantBreedingContext context)
        {
            m_context = context;
        }


        #region Access

        /// <summary>
        /// Retrieves all answers from the database
        /// </summary>
        public IEnumerable<Answer> GetAnswers()
        {
            return m_context.Answers;
        }
        /// <summary>
        /// Retrieves all answers from the database
        /// </summary>
        public IQueryable<Answer> GetAnswers(Expression<Func<Answer, bool>> predicate)
        {
            return m_context.Answers.Where(predicate);
        }

        public Answer GetAnswer(Expression<Func<Answer, bool>> predicate)
        {
            return m_context.Answers.SingleOrDefault(predicate);
        }




        /// <summary>
        /// Retrieves an answer by specified id
        /// </summary>
        public Answer GetAnswer(int id)
        {
            return m_context.Answers.SingleOrDefault(a => a.Id == id);
        }

        /// <summary>
        /// Retrieves all the Families from the database.
        /// </summary>
        public IEnumerable<Family> GetFamilies()
        {
            return GetQueryableFamilies(t => true);
        }
        public IQueryable<Family> GetQueryableFamilies(Expression<Func<Family, bool>> predicate)
        {
            return m_context.Families.Where(predicate).OrderBy(f => f.Origin.Name).ThenBy(f => f.CrossNum.Length).ThenBy(f => f.CrossNum);
        }


        /// <summary>
        /// Retrieves all families with a given genus id
        /// </summary>
        public IEnumerable<Family> GetFamilies(int genusId)
        {
            return GetQueryableFamilies(f => f.GenusId == genusId);

        }
        public IQueryable<Family> GetQueryableFamilies(string term, int genusId)
        {
            term = term.TrimAndRemoveDoubleSpaces().ToLower();
            if (term.IsNullOrWhiteSpace())
            {
                return null;
            }

            IQueryable<Family> families = GetQueryableFamilies(t => t.GenusId == genusId && (t.Origin.Name + " " + t.CrossNum).ToLower().StartsWith(term));
            return families;
        }

        public IEnumerable<Family> GetFamilies(string term, int genusId, int recordsToReturn)
        {
            if (term.IsNullOrWhiteSpace())
            {
                return null;
            }


            return GetQueryableFamilies(term, genusId).Take(recordsToReturn);
        }

        /// <summary>
        /// Retrieves the families by predicate
        /// </summary>
        public IEnumerable<Family> GetFamilies(Expression<Func<Family, bool>> predicate)
        {
            return GetQueryableFamilies(predicate);
        }

        /// <summary>
        /// Retrieves a family by specified id
        /// </summary>
        public Family GetFamily(int id)
        {
            return m_context.Families.SingleOrDefault(f => f.Id == id);
        }


        /// <summary>
        /// Retrieves the genotype by specified id
        /// </summary>
        public Genotype GetGenotype(int id)
        {
            return m_context.Genotypes.SingleOrDefault(p => p.Id == id);
        }

        /// <summary>
        /// Retrieves the genotypes by predicate
        /// </summary>
        public IEnumerable<Genotype> GetGenotypes(Expression<Func<Genotype, bool>> predicate)
        {
            return GetQueryableGenotypes(predicate);
        }

        public IQueryable<Genotype> GetQueryableGenotypes(Expression<Func<Genotype, bool>> predicate)
        {
            return m_context.Genotypes.Where(predicate)
                                        .OrderBy(g => g.Family.Origin.Name)
                                        .ThenBy(g => g.Family.CrossNum.Length)
                                        .ThenBy(g => g.Family.CrossNum)
                                        .ThenBy(g => g.Family.ReciprocalString != "N")
                                        .ThenBy(g => g.SelectionNum);
        }

        /// <summary>
        /// Retrieves all Genotypes from the database.
        /// </summary>
        public IEnumerable<Genotype> GetGenotypes()
        {
            return GetQueryableGenotypes(t => true);
        }

        /// <summary>
        /// Retrieves all genotypes that match the given search term.
        /// </summary>
        public IQueryable<Genotype> GetQueryableGenotypes(string term, int genusId)
        {
            term = term.TrimAndRemoveDoubleSpaces().ToLower();
            if (term.IsNullOrWhiteSpace())
            {
                return null;
            }

            IQueryable<Genotype> genotypes = GetQueryableGenotypes(g => g.Family.GenusId == genusId
                                                                        &&
                                                                        (
                                                                            g.GivenName.ToLower().StartsWith(term)
                                                                            ||
                                                                            (g.Family.Origin.Name + " " + g.Family.CrossNum + (
                                                                                    (g.Family.ReciprocalString != string.Empty && !g.Family.ReciprocalString.ToLower().Equals("n")) ? g.Family.ReciprocalString : "")
                                                                                    + "-" + g.SelectionNum.ToString())
                                                                                .ToLower().StartsWith(term)
                                                                            ||
                                                                            g.Alias1.ToLower().StartsWith(term)
                                                                            ||
                                                                            g.Alias2.ToLower().StartsWith(term)
                                                                        )
                                                                    );

            return genotypes;
        }
        public IEnumerable<Genotype> GetGenotypes(string term, int genusId, int recordsToReturn)
        {
            return GetQueryableGenotypes(term, genusId).Take(recordsToReturn);
        }
        public IQueryable<Genotype> GetQueryableGenotypes(string term, int genusId, int recordsToReturn)
        {
            return GetQueryableGenotypes(term, genusId).Take(recordsToReturn);
        }

        public IEnumerable<Genotype> GetGenotypes(string term, int genusId, int recordsToReturn, int mapId)
        {
            Map map = GetMap(mapId);
            IEnumerable<Genotype> genotypes;

            if (map != null && map.IsSeedlingMap)
            {
                genotypes = GetQueryableGenotypes(term, genusId).Where(t => t.IsRoot == true || t.SelectionNum == 0).Take(recordsToReturn);

            }
            else
            {
                genotypes = GetGenotypes(term, genusId, recordsToReturn);
            }

            return genotypes;
        }
        public IEnumerable<Genotype> GetGenotypes(string term, int genusId, int recordsToReturn, int mapId, bool mapOnly)
        {
            Map map = GetMap(mapId);
            IEnumerable<Genotype> genotypes;

            if (map != null && map.IsSeedlingMap && mapOnly)
            {
                genotypes = GetQueryableGenotypes(term, genusId).Where(t => t.MapComponents.Any(m => m.MapId == mapId && m.isSeedling == true) && t.IsRoot == true).Take(recordsToReturn);
            }
            else
            {
                genotypes = GetGenotypes(term, genusId, recordsToReturn, mapId);
            }

            return genotypes;
        }



        /// <summary>
        /// Retrieves all Crosstypes from the database.
        /// </summary>
        public IEnumerable<CrossType> GetCrossTypes()
        {
            return m_context.CrossTypes.OrderBy(t => t.Name);
        }

        /// <summary>
        /// Retrieves the Cross Type by the specified Id.
        /// </summary>
        public CrossType GetCrossType(int id)
        {
            return m_context.CrossTypes.SingleOrDefault(ct => ct.Id == id);
        }

        public IEnumerable<CrossType> GetGenusCrossTypes(int genusId)
        {
            return m_context.CrossTypes.Where(t => t.GenusId == genusId).OrderBy(t => t.Name);
        }

        /// <summary>
        /// Retrieves all Genera from the database.
        /// </summary>
        public IEnumerable<Genus> GetAllGenera()
        {
            return m_context.Genus;
        }

        /// <summary>
        /// Retrieves the Genus by the specified Id.
        /// </summary>
        public Genus GetGenus(int id)
        {
            return m_context.Genus.SingleOrDefault(g => g.Id == id);
        }

        /// <summary>
        /// Retrieves all Origins from the database.
        /// </summary>
        public IEnumerable<Origin> GetOrigins()
        {
            return GetQueryableOrigins(t => true).OrderBy(t => t.Name);
        }

        /// <summary>
        /// Retrieves the Origin by Id.
        /// </summary>
        public Origin GetOrigin(int id)
        {
            return m_context.Origins.SingleOrDefault(o => o.Id == id);
        }
        private IQueryable<Origin> GetQueryableOrigins(Expression<Func<Origin, bool>> predicate)
        {
            return m_context.Origins.Where(predicate).OrderBy(t => t.Name);
        }


        public IEnumerable<Origin> GetOrigins(string term, int recordsToReturn)
        {
            term = term.TrimAndRemoveDoubleSpaces().ToLower();
            if (term.IsNullOrWhiteSpace())
            {
                return null;
            }
            IQueryable<Origin> origins = GetQueryableOrigins(t => t.Name.ToLower().Contains(term)).Take(recordsToReturn);

            return origins;
        }



        /// <summary>
        /// Retrieves all Ploidies from the database.
        /// </summary>
        public IEnumerable<Ploidy> GetPloidies()
        {
            return m_context.Ploidies;
        }

        /// <summary>
        /// Retrieves a Ploidy by id.
        /// </summary>
        public Ploidy GetPloidyById(int id)
        {
            return m_context.Ploidies.SingleOrDefault(p => p.Id == id);
        }

        /// <summary>
        /// Retrieves all Fates from the database.
        /// </summary>
        public IEnumerable<Fate> GetFates()
        {
            return m_context.Fates.OrderBy(t => t.Order);
        }

        /// <summary>
        /// Retrieves a fate by Id.
        /// </summary>
        public Fate GetFate(int id)
        {
            return m_context.Fates.SingleOrDefault(f => f.Id == id);
        }

        public IEnumerable<FlatNote> GetFlatNotes(Expression<Func<FlatNote, bool>> predicate)
        {
            return m_context.FlatNotes.Where(predicate).OrderBy(t => t.Date);
        }

        public FlatNote GetFlatNote(int id)
        {
            return m_context.FlatNotes.SingleOrDefault(t => t.Id == id);
        }

        public IEnumerable<Note> GetNotes(Expression<Func<Note, bool>> predicate)
        {
            return m_context.Notes.Where(predicate).OrderBy(t => t.Date);
        }

        public Note GetNote(int id)
        {
            return m_context.Notes.SingleOrDefault(t => t.Id == id);
        }

        public IEnumerable<FlatType> GetFlatTypes()
        {
            return m_context.FlatType;
        }

        public FlatType GetFlatType(int id)
        {
            return m_context.FlatType.SingleOrDefault(f => f.Id == id);
        }

        public IEnumerable<FlatType> GetFlatTypes(Expression<Func<FlatType, bool>> predicate)
        {
            return m_context.FlatType.Where(predicate).OrderBy(t => t.Name);
        }


        /// <summary>
        /// Retrieves all Questions from the database.
        /// </summary>
        public IEnumerable<Question> GetQuestions()
        {
            return m_context.Questions;
        }

        /// <summary>
        /// Retrieves list of questions based on predicate func
        /// </summary>
        public IEnumerable<Question> GetQuestions(Expression<Func<Question, bool>> predicate)
        {
            return m_context.Questions.Where(predicate).OrderBy(t => t.Order);
        }

        /// <summary>
        /// Retrieves a Question by id.
        /// </summary>
        public Question GetQuestion(int id)
        {
            return m_context.Questions.SingleOrDefault(q => q.Id == id);
        }

        public IEnumerable<InputType> InputTypes()
        {
            return m_context.InputTypes;
        }


        /// <summary>
        /// Retrieves all candidates from the database
        /// </summary>
        public IEnumerable<Candidate> GetCandidates()
        {
            return m_context.Candidates;
        }

        /// <summary>
        /// Retrieves list of Candidates based on predicate func
        /// </summary>
        public IEnumerable<Candidate> GetCandidates(Expression<Func<Candidate, bool>> predicate)
        {
            return m_context.Candidates.Where(predicate);
        }

        /// <summary>
        /// Get a candidate by id
        /// </summary>
        public Candidate GetCandidate(int id)
        {
            return m_context.Candidates.SingleOrDefault(c => c.Id == id);
        }

        /// <summary>
        /// Get a candidate by GenotypeID
        /// </summary>
        public Candidate GetCandidate(int genotypeId, int year)
        {
            return m_context.Candidates.SingleOrDefault(c => c.GenotypeId == genotypeId && c.Year == year);
        }

        public Years GetYear(int id)
        {
            return m_context.Years.SingleOrDefault(t => t.Id == id);
        }

        /// <summary>
        /// Retrieves all Maps from the database.
        /// </summary>
        public IEnumerable<Map> GetMaps()
        {
            return m_context.Maps;
        }
        public IQueryable<Map> GetQueryableMaps(Expression<Func<Map, bool>> predicate)
        {
            return m_context.Maps.Where(predicate);
        }

        /// <summary>
        /// Retrieves list of Maps based on predicate func
        /// </summary>
        public IEnumerable<Map> GetMaps(Expression<Func<Map, bool>> predicate)
        {
            return m_context.Maps.Where(predicate).OrderBy(t => t.PlantingYear);
        }

        /// <summary>
        /// Retrieves map by id.
        /// </summary>
        public Map GetMap(int id)
        {
            return m_context.Maps.SingleOrDefault(m => m.Id == id);
        }

        /// <summary>
        /// Retrieves all map compononts from the database.
        /// </summary>
        public IEnumerable<MapComponent> GetMapComponents()
        {
            return m_context.MapComponents;
        }

        public IQueryable<MapComponent> GetQueryableMapComponents(Expression<Func<MapComponent, bool>> predicate)
        {
            return m_context.MapComponents.Where(predicate);
        }


        /// <summary>
        /// Retrieves all Map components by MapId.
        /// </summary>
        public IEnumerable<MapComponent> GetMapComponents(int mapId)
        {
            return m_context.MapComponents.Where(mc => mc.MapId == mapId);
        }

        /// <summary>
        /// Retrieves the Map component by id.
        /// </summary>
        public MapComponent GetMapComponent(int id)
        {
            return m_context.MapComponents.SingleOrDefault(mc => mc.Id == id);
        }

        /// <summary>
        /// Retrieves the Map component by id.
        /// </summary>
        public MapComponentYears GetMapComponentYear(int id)
        {
            return m_context.MapComponentYears.SingleOrDefault(mc => mc.Id == id);
        }

        /// <summary>
        /// Retrieves the Map component by id.
        /// </summary>
        public IQueryable<MapComponentYears> GetQueryableMapComponentYears(Expression<Func<MapComponentYears, bool>> predicate)
        {
            return m_context.MapComponentYears.Where(predicate);
        }


        /// <summary>
        /// Retrieves genotype's male parent by genotype id
        /// </summary>
        private Genotype GetGenotypeMaleParent(Genotype genotype)
        {
            Genotype ret = null;

            if (genotype.Family.MaleParent.HasValue)
            {
                ret = GetGenotype(genotype.Family.MaleParent.Value);
            }

            return ret;
        }

        /// <summary>
        /// Retrieves genotype's female parent by genotype id.
        /// </summary>
        private Genotype GetGenotypeFemaleParent(Genotype genotype)
        {
            Genotype ret = null;

            if (genotype.Family.FemaleParent.HasValue)
            {
                ret = GetGenotype(genotype.Family.FemaleParent.Value);
            }

            return ret;
        }

        /// <summary>
        /// Retrieves genotype's parents. 
        /// </summary>
        /// <remarks>
        /// First item is always the female parent.
        /// </remarks>
        public Tuple<Genotype, Genotype> GetGenotypeParents(Genotype genotype)
        {
            return Tuple.Create(GetGenotypeFemaleParent(genotype), GetGenotypeMaleParent(genotype));
        }

        /// <summary>
        /// Retrieves all of a maps questions by genus id, filtered by "required" bool
        /// </summary>
        public IEnumerable<Question> GetMapQuestions(int genusId, bool required)
        {
            return m_context.Questions.Where(q => q.GenusId == genusId && q.Required == required);
        }

        public IQueryable<CrossPlan> GetQueryableCrossPlans(Expression<Func<CrossPlan, bool>> predicate)
        {

            return m_context.CrossPlans.Where(predicate);

        }
        /// <summary>
        /// Get all cross plans
        /// </summary>
        public IEnumerable<CrossPlan> GetCrossPlans()
        {
            return GetQueryableCrossPlans(t => true);
        }
        public IEnumerable<CrossPlan> GetCrossPlans(Expression<Func<CrossPlan, bool>> predicate)
        {
            return GetQueryableCrossPlans(predicate);
        }

        /// <summary>
        /// Get cross plan by Id
        /// </summary>
        public CrossPlan GetCrossPlan(int id)
        {
            return GetQueryableCrossPlans(cp => cp.Id == id).SingleOrDefault();
        }

        /// <summary>
        /// Get all locations
        /// </summary>
        public IEnumerable<Location> GetLocations()
        {
            return GetQueryableLocations(t => true).OrderBy(t => t.Name);
        }

        /// <summary>
        /// Get locations by id
        /// </summary>
        public Location GetLocation(int id)
        {
            return m_context.Locations.Where(l => l.Id == id).SingleOrDefault();
        }

        private IQueryable<Location> GetQueryableLocations(Expression<Func<Location, bool>> predicate)
        {
            return m_context.Locations.Where(predicate).OrderBy(t => t.Name);
        }


        public IEnumerable<Location> GetLocations(string term, int recordsToReturn, bool mapFlag)
        {
            term = term.TrimAndRemoveDoubleSpaces().ToLower();
            if (term.IsNullOrWhiteSpace())
            {
                return null;
            }
            IQueryable<Location> locations = GetQueryableLocations(t => t.Name.ToLower().Contains(term))
                                                    .Where(t => t.Retired == false && t.MapFlag == mapFlag).Take(recordsToReturn);

            return locations;
        }

        public Origin GetDefaultOrigin()
        {
            return m_context.Origins.Where(o => o.IsDefault).FirstOrDefault();
        }

        public string GetNextCross(int originId, int genusId)
        {
            string result = "";
            int crossVal = 0;
            string crossnum = GetQueryableFamilies(t => t.OriginId == originId && t.GenusId == genusId)
                                            .OrderByDescending(f => f.CrossNum.Length)
                                            .ThenByDescending(f => f.CrossNum)
                                            .FirstOrDefault()
                                            .CrossNum;
            int.TryParse(crossnum, out crossVal);

            if (crossVal != 0)
            {
                crossVal++;
                result = crossVal.ToString();
            }
            return result;

        }

        public IEnumerable<UserPreference> GetUserPreferences()
        {
            return m_context.UserPreferences;
        }
        public UserPreference GetUserPreference(string userId)
        {
            return m_context.UserPreferences.SingleOrDefault(t => t.UserId == userId);
        }

        public void SaveUserPreference(UserPreference userpreference)
        {
            UserPreference old = GetUserPreference(userpreference.UserId);

            if (userpreference == null)
            {
                throw new ArgumentNullException(userpreference.GetType().Name);
            }

            if (old == null)
            {
                m_context.UserPreferences.Add(userpreference);
            }
            else
            {
                userpreference.CopyTo(old);
            }

            m_context.SaveChanges();
        }
        #region materials
        public IEnumerable<Material> GetMaterials()
        {
            return m_context.Materials;
        }

        /// <summary>
        /// Get materials by id
        /// </summary>
        public Material GetMaterial(int id)
        {
            return m_context.Materials.Where(m => m.Id == id).SingleOrDefault();
        }

        public void DeleteMaterial(Material material)
        {
            if (material == null || material.Id <= 0)
            {
                throw new Exception($"material does not exist for {material.ToString()}");
            }
            //entity framework fix
            var op = m_context.Materials.SingleOrDefault(o => o.Id == material.Id);

            m_context.Materials.Remove(op);
            m_context.SaveChanges();
        }

        public void SaveMaterial(Material material)
        {
            Material old = null;

            if (material == null)
            {
                throw new ArgumentNullException(material.GetType().Name);
            }


            if (material.Id <= 0)
            {
                m_context.Materials.Add(material);
            }
            else
            {
                old = m_context.Materials.SingleOrDefault(t => t.Id == material.Id);
                material.CopyTo(old);
            }
            DoSaveUpdate();
        }
        #endregion

        #region growers
        public IEnumerable<Grower> GetGrowers()
        {
            return m_context.Growers;
        }

        public void SaveGrower(Grower grower)
        {
            Grower old = null;

            if (grower == null)
            {
                throw new ArgumentNullException(grower.GetType().Name);
            }

            grower.MobilePhone = grower.MobilePhone.ParsePhoneNumber();
            grower.WorkPhone = grower.WorkPhone.ParsePhoneNumber();

            if (grower.Id <= 0)
            {
                grower.CreatedDate = DateTime.Now;
                m_context.Growers.Add(grower);
            }
            else
            {
                old = m_context.Growers.SingleOrDefault(t => t.Id == grower.Id);
                grower.CreatedDate = old.CreatedDate;
                grower.CopyTo(old);
            }
            DoSaveUpdate();
        }


        public Grower GetGrower(int id)
        {
            return m_context.Growers.Where(g => g.Id == id).SingleOrDefault();
        }

        private IQueryable<Grower> GetQueryableGrowers(Expression<Func<Grower, bool>> predicate)
        {
            return m_context.Growers.Where(predicate).OrderBy(t => t.FirstName).ThenBy(t => t.LastName);
        }


        public IEnumerable<Grower> GetGrowers(string term, int recordsToReturn)
        {
            term = term.TrimAndRemoveDoubleSpaces().ToLower();
            if (term.IsNullOrWhiteSpace())
            {
                return null;
            }
            IQueryable<Grower> growers = GetQueryableGrowers(t => (t.FirstName + t.LastName).ToLower().Contains(term))
                                                    .Take(recordsToReturn);

            return growers;
        }

        public IEnumerable<Order> GetOrders()
        {
            return m_context.Orders;
        }

        public Order GetOrder(Expression<Func<Order, bool>> predicate)
        {
            return m_context.Orders.SingleOrDefault(predicate);
        }

        public IEnumerable<Order> GetOrders(Expression<Func<Order, bool>> predicate)
        {
            return m_context.Orders.Where(predicate);
        }

        public void SaveOrder(Order order)
        {
            Order old = null;

            if (order == null)
            {
                throw new ArgumentNullException(order.GetType().Name);
            }


            if (order.Id <= 0)
            {
                m_context.Orders.Add(order);
            }
            else
            {
                old = m_context.Orders.SingleOrDefault(t => t.Id == order.Id);
                order.OrderProducts = old.OrderProducts;//TODO: remove this from ef designer
                order.CopyTo(old);
            }
            DoSaveUpdate();
        }

        public IEnumerable<OrderProduct> GetOrderProducts()
        {
            return m_context.OrderProducts;
        }

        public IEnumerable<OrderProduct> GetOrderProducts(Expression<Func<OrderProduct, bool>> predicate)
        {
            return m_context.OrderProducts.Where(predicate);
        }

        public OrderProduct GetOrderProduct(Expression<Func<OrderProduct, bool>> predicate)
        {
            return m_context.OrderProducts.SingleOrDefault(predicate);
        }

        public void SaveOrderProduct(OrderProduct orderProduct)
        {
            OrderProduct old = null;

            if (orderProduct == null)
            {
                throw new ArgumentNullException(orderProduct.GetType().Name);
            }


            if (orderProduct.Id <= 0)
            {
                orderProduct.CreatedDate = DateTime.Now;
                m_context.OrderProducts.Add(orderProduct);
            }
            else
            {
                old = m_context.OrderProducts.SingleOrDefault(t => t.Id == orderProduct.Id);
                orderProduct.CreatedDate = old.CreatedDate;
                orderProduct.CopyTo(old);
            }
            DoSaveUpdate();
        }

        public void DeleteOrderProduct(OrderProduct orderProduct)
        {
            if (orderProduct == null || orderProduct.Id <= 0)
            {
                throw new Exception($"order product does not exist for {orderProduct.ToString()}");
            }
            //entity framework fix
            var op = m_context.OrderProducts.SingleOrDefault(o => o.Id == orderProduct.Id);

            m_context.OrderProducts.Remove(op);
            m_context.SaveChanges();
        }

        #endregion

        #endregion

        #region Modifiers

        /// <summary>
        /// Saves a fate
        /// </summary>
        public void SaveFate(Fate fate)
        {
            if (fate.Id > 0)
            {
                var oldFate = GetFate(fate.Id);
                if (oldFate != null)
                {

                    if (oldFate.Order != fate.Order && fate.Order.HasValue && GetFates().HasAny(t => t.Order == fate.Order))
                    {
                        int oldOrder = oldFate.Order.GetValueOrDefault();
                        if (!oldFate.Order.HasValue)
                        {
                            oldOrder = GetFates().Max(t => t.Order.GetValueOrDefault()) + 1;
                        }

                        UpdateFateOrderList(fate, oldOrder, fate.Order.Value);
                    }
                }

            }

            DoSave(m_context.Fates, fate, fate.Id, f => f.Id == fate.Id);
        }

        private void UpdateFateOrder(int fateId, int order)
        {
            var fate = GetFate(fateId);
            if (fate == null)
            {
                throw new Exception("Invalid fate");
            }
            fate.Order = order;
            DoSaveUpdate();

        }

        private void UpdateFateOrderList(Fate fate, int oldOrder, int newOrder)
        {

            if (oldOrder < newOrder)
            {
                // shift questions down; note that questions must be a list
                List<Fate> fates = GetFates().Where(t => t.Order.HasValue && t.Retired == fate.Retired && t.Order > oldOrder && t.Order <= newOrder).ToList();
                foreach (var f in fates)
                {
                    f.Order--;
                    UpdateFateOrder(f.Id, f.Order.GetValueOrDefault());
                }
            }
            else if (oldOrder > newOrder)
            {
                // shift questions up; note that questions must be a list
                List<Fate> fates = GetFates().Where(t => t.Order.HasValue && t.Retired == fate.Retired && t.Order >= newOrder && t.Order < oldOrder).ToList();
                foreach (var f in fates)
                {
                    f.Order++;
                    UpdateFateOrder(f.Id, f.Order.GetValueOrDefault());
                }
            }
        }


        /// <summary>
        /// Save changes to the fates for a map component that are selected by a user
        /// </summary>
        public void UpdateMapComponentFates(IEnumerable<Fate> selected, int mapComponentYear)
        {
            MapComponentYears target = GetMapComponentYear(mapComponentYear);

            if (selected.IsNullOrEmpty())
            {
                target.Fates.Clear();
            }

            else
            {
                HashSet<int> selectedFates = new HashSet<int>(selected.Select(f => f.Id));
                HashSet<int> currentFates = new HashSet<int>(target.Fates.Select(f => f.Id));

                if (target.MapComponent.Map.IsSeedlingMap && target.MapComponent.GenotypeId.HasValue)
                {
                    Genotype genotype = GetGenotype(target.MapComponent.GenotypeId.Value);
                    StringBuilder builtString = new StringBuilder();

                    foreach (Fate fate in GetFates())
                    {
                        if (selectedFates.Contains(fate.Id))
                        {
                            if (!string.IsNullOrEmpty(fate.Name))
                            {
                                builtString.AppendLine(fate.Name);
                            }
                        }
                    }

                    genotype.Fate = builtString.ToString();

                    DoSaveGenotype(genotype);
                }



                foreach (Fate fate in GetFates())
                {
                    if (selectedFates.Contains(fate.Id) && !currentFates.Contains(fate.Id))
                    {
                        target.Fates.Add(fate);
                    }
                    else if (!selectedFates.Contains(fate.Id) && currentFates.Contains(fate.Id))
                    {
                        target.Fates.Remove(fate);
                    }
                }
            }
            DoSaveMapComponentYear(target);

        }

        private void Remove<T>(IDbSet<T> dbSet, IEnumerable<T> dbSetRemove) where T : class
        {
            foreach (T entity in dbSetRemove)
            {
                dbSet.Remove(entity);
            }
        }

        private void DoSaveUpdate()
        {
            try
            {
                m_context.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting
                        // the current instance as InnerException
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }

        }


        /// <summary>
        /// Abstracts the saving process for an item in the Db Context.
        /// </summary>
        private void DoSave<T>(IDbSet<T> dbSet, T entity, int entityId, Func<T, bool> predicate) where T : class
        {
            T old = null;

            if (entity == null)
            {
                throw new ArgumentNullException(entity.GetType().Name);
            }


            if (entityId <= 0)
            {
                dbSet.Add(entity);
            }
            else
            {
                old = dbSet.SingleOrDefault(predicate);
                entity.CopyTo(old);

            }
            DoSaveUpdate();


        }

        /// <summary>
        /// Abstracts the saving process for an item in the Db Context.
        /// </summary>
        private void ContextUpdate<T>(IDbSet<T> dbSet, T entity, int entityId, Func<T, bool> predicate) where T : class
        {
            T old = null;

            if (entity == null)
            {
                throw new ArgumentNullException(entity.GetType().Name);
            }


            if (entityId <= 0)
            {
                dbSet.Add(entity);
            }
            else
            {
                old = dbSet.SingleOrDefault(predicate);
                entity.CopyTo(old);

            }

        }


        /// <summary>
        /// Saves a question to the database.
        /// </summary>
        /// <remarks>
        /// This is more complicated than others because it allows for the removal of all text in a cell.
        /// </remarks>
        public void SaveAnswer(Answer answer)
        {

            if (answer.Id <= 0)
            {
                Answer old = GetAnswer(t => t.GenotypeId == answer.GenotypeId && t.MapComponentYearsId == answer.MapComponentYearsId && t.QuestionId == answer.QuestionId);
                if (old != null && old.Id > 0)
                {
                    old.Text = answer.Text;
                    DoSave(m_context.Answers, old, old.Id, a => a.Id == old.Id);
                }
                else
                {
                    DoSave(m_context.Answers, answer, answer.Id, a => a.Id == answer.Id);
                }
            }
            else
            {
                Answer temp = GetAnswer(answer.Id);
                temp.Text = answer.Text;
                DoSave(m_context.Answers, temp, temp.Id, a => a.Id == temp.Id);
            }
        }


        /// <summary>
        /// Saves an answer update/insert without copyto method
        /// </summary>
        /// <param name="answer"></param>
        public void DoSaveAnswer(Answer answer)
        {

            if (answer == null) throw new ArgumentNullException(answer.GetType().Name);
            if (answer.GenotypeId < 0) throw new ArgumentNullException("answer genotypeid needs a value");
            if (answer.QuestionId < 0) throw new ArgumentNullException("answer questionid needs a value");
            if (answer.MapComponentYearsId < 0) throw new ArgumentNullException("answer mapcomponentYearsId needs a value");

            if (answer.Id <= 0)
            {
                var old = m_context.Answers.FirstOrDefault(a => a.MapComponentYearsId == answer.MapComponentYearsId && a.QuestionId == answer.QuestionId);
                if (old == null)
                {
                    m_context.Answers.Add(answer);
                }
                else
                {
                    old.Text = answer.Text;
                }
            }

            DoSaveUpdate();

        }


        /// <summary>
        /// Saves a given Family.
        /// </summary>
        public void SaveFamily(Family family)
        {
            Family old = null;

            if (family == null)
            {
                throw new ArgumentNullException(family.GetType().Name);
            }


            if (family.Id <= 0)
            {
                m_context.Families.Add(family);
            }
            else
            {
                old = m_context.Families.SingleOrDefault(t => t.Id == family.Id);
                family.Genotypes = old.Genotypes;

                family.CopyTo(old);


            }

            DoSaveUpdate();
        }

        /// <summary>
        /// Updates or inserts a genotype in the context without the copyto method
        /// </summary>
        /// <param name="genotype"></param>
        public void DoSaveFamily(Family family)
        {

            if (family == null) { throw new ArgumentNullException(family.GetType().Name); }

            if (family.Id <= 0)
            {
                m_context.Families.Add(family);
            }

            DoSaveUpdate();
        }


        public void UpdateGeotypesAlias(int familyId, string alias)
        {
            Family family = m_context.Families.SingleOrDefault(t => t.Id == familyId);

            List<Genotype> genotypes = family.Genotypes.ToList();

            foreach (Genotype gen in genotypes)
            {
                gen.Alias2 = gen.Alias1;
                gen.Alias1 = alias;
            }
            DoSaveUpdate();

        }

        /// <summary>
        /// Saves a given Genotype.
        /// </summary>
        public void SaveGenotype(Genotype genoType)
        {
            Genotype old = null;

            if (genoType == null)
            {
                throw new ArgumentNullException(genoType.GetType().Name);
            }


            if (genoType.Id <= 0)
            {
                m_context.Genotypes.Add(genoType);
            }
            else
            {
                old = m_context.Genotypes.SingleOrDefault(t => t.Id == genoType.Id);
                genoType.CreatedDate = old.CreatedDate;
                genoType.Notes = old.Notes;
                genoType.FlatNotes = old.FlatNotes;
                genoType.Family = old.Family;
                genoType.MapComponents = old.MapComponents;
                genoType.CrossPlanId = old.CrossPlanId;
                genoType.CopyTo(old);
            }
            DoSaveUpdate();

        }
        /// <summary>
        /// Updates or inserts a genotype in the context without the copyto method
        /// </summary>
        /// <param name="genotype"></param>
        public void DoSaveGenotype(Genotype genotype)
        {

            if (genotype == null)
            {
                throw new ArgumentNullException(genotype.GetType().Name);
            }

            if (genotype.Id <= 0)
            {
                m_context.Genotypes.Add(genotype);
            }

            DoSaveUpdate();

        }

        public void SaveNote(Note note)
        {
            DoSave(m_context.Notes, note, note.Id, n => n.Id == note.Id);
        }

        public void SaveFlatNote(FlatNote note)
        {
            DoSave(m_context.FlatNotes, note, note.Id, n => n.Id == note.Id);
        }

        /// <summary>
        /// Saves a given Crosstype.
        /// </summary>
        public void SaveCrossType(CrossType crossType)
        {
            if (crossType.Id < 1)
            {
                crossType.Retired = false;
            }
            DoSave(m_context.CrossTypes, crossType, crossType.Id, ct => ct.Id == crossType.Id);
        }

        /// <summary>
        /// Deletes a specified Cross Type from the database.
        /// </summary>
        /// <param name="crossType"></param>
        public void DeleteCrossType(CrossType crossType)
        {
            if (crossType == null || crossType.Id <= 0)
            {
                return;
            }

            m_context.CrossTypes.Remove(crossType);
            m_context.SaveChanges();
        }

        /// <summary>
        /// Deletes a specified Family from the db
        /// </summary>
        public void DeleteFamily(Family family)
        {
            if (family == null || family.Id <= 0)
            {
                return;
            }

            m_context.Families.Remove(family);
            m_context.SaveChanges();
        }

        /// <summary>
        /// Deletes a Genotype from the db
        /// </summary>
        public void DeleteGenotype(Genotype genoType)
        {
            if (genoType == null || genoType.Id <= 0)
            {
                return;
            }

            m_context.Genotypes.Remove(genoType);
            m_context.SaveChanges();
        }

        /// <summary>
        /// Deletes a Genotype from the db
        /// </summary>
        public void DeleteGenotypeWithRelated(Genotype genotype)
        {
            if (genotype == null || genotype.Id <= 0)
            {
                return;
            }
            var mapComponents = m_context.MapComponents.Where(mc => mc.GenotypeId == genotype.Id);
            var mapComponentsYears = m_context.MapComponentYears.Where(mcy => mcy.MapComponent.GenotypeId == genotype.Id);
            var answers = m_context.Answers.Where(a => a.GenotypeId == genotype.Id);

            if (mapComponents.Any())
            {
                Remove(m_context.MapComponents, mapComponents);
            }

            if (mapComponentsYears.Any())
            {
                Remove(m_context.MapComponentYears, mapComponentsYears);
            }

            if (answers.Any())
            {
                Remove(m_context.Answers, answers);
            }

            m_context.Genotypes.Remove(genotype);

            m_context.SaveChanges();
        }


        /// <summary>
        /// Saves a given Genus.
        /// </summary>
        public void SaveGenus(Genus genus)
        {
            if (genus.Id < 1)
            {
                genus.Retired = false;
            }
            DoSave(m_context.Genus, genus, genus.Id, g => g.Id == genus.Id);
        }

        /// <summary>
        /// Saves a given Cross Plan
        /// </summary>
        public void SaveCrossPlan(CrossPlan crossPlan)
        {
            DoSave(m_context.CrossPlans, crossPlan, crossPlan.Id, c => c.Id == crossPlan.Id);

        }
        public void SaveCrossPlan(IEnumerable<CrossPlan> crossPlans)
        {
            foreach (CrossPlan cp in crossPlans)
            {

                ContextUpdate(m_context.CrossPlans, cp, cp.Id, c => c.Id == cp.Id);
            }

            try
            {
                m_context.SaveChanges();

            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting
                        // the current instance as InnerException
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }


        }

        /// <summary>
        /// Delete a given cross plan
        /// </summary>
        public void DeleteCrossPlan(CrossPlan crossPlan)
        {
            if (crossPlan == null || crossPlan.Id <= 0)
            {
                return;
            }

            m_context.CrossPlans.Remove(crossPlan);
            m_context.SaveChanges();
        }

        /// <summary>
        /// Deletes a Genus from the DB
        /// </summary>
        public void DeleteGenus(Genus genus)
        {
            if (genus == null || genus.Id <= 0)
            {
                return;
            }

            m_context.Genus.Remove(genus);
            m_context.SaveChanges();
        }

        /// <summary>
        /// Saves a given Origin.
        /// </summary>
        public void SaveOrigin(Origin origin)
        {
            if (origin.Id < 1)
            {
                origin.Retired = false;
            }
            DoSave(m_context.Origins, origin, origin.Id, o => o.Id == origin.Id);
        }

        /// <summary>
        /// Delets the specified Origin from the database.
        /// </summary>
        public void DeleteOrigin(Origin origin)
        {
            if (origin == null || origin.Id <= 0)
            {
                return;
            }

            m_context.Origins.Remove(origin);
            m_context.SaveChanges();
        }

        /// <summary>
        /// Saves a given Ploidy.
        /// </summary>
        public void SavePloidy(Ploidy ploidy)
        {
            if (ploidy.Id < 1)
            {
                ploidy.Retired = false;
            }
            DoSave(m_context.Ploidies, ploidy, ploidy.Id, p => p.Id == ploidy.Id);
        }

        /// <summary>
        /// Deletes a Ploidy from the database.
        /// </summary>
        public void DeletePloidy(Ploidy ploidy)
        {
            if (ploidy == null || ploidy.Id <= 0)
            {
                return;
            }

            m_context.Ploidies.Remove(ploidy);
            m_context.SaveChanges();
        }

        /// <summary>
        /// Deletes a Candidate from the database.
        /// </summary>
        public void DeleteCandidate(Candidate candidate)
        {
            if (candidate == null || candidate.Id <= 0)
            {
                return;
            }

            m_context.Candidates.Remove(candidate);
            m_context.SaveChanges();
        }

        /// <summary>
        /// Saves a candidate to the database.
        /// </summary>
        public void SaveCandidate(Candidate candidate)
        {
            DoSave(m_context.Candidates, candidate, candidate.Id, c => c.Id == candidate.Id);
        }

        /// <summary>
        /// Saves a given Question.
        /// </summary>
        public void SaveQuestion(Question question)
        {
            if (question.Id > 0)
            {
                var oldQuestion = GetQuestion(question.Id);
                UpdateQuestionOrderList(question, oldQuestion.Order, question.Order);
                question.Genus = oldQuestion.Genus;
                question.GenusId = oldQuestion.GenusId;
                if (question.InputTypeId.HasValue)
                {
                    question.InputType = m_context.InputTypes.SingleOrDefault(i => i.Id == question.InputTypeId.Value);
                }
            }
            DoSave(m_context.Questions, question, question.Id, q => q.Id == question.Id);
        }
        public void UpdateQuestionOrder(int questionId, int order)
        {
            var question = GetQuestion(questionId);
            if (question == null)
            {
                throw new Exception("Invalid question");
            }
            question.Order = order;
            DoSaveUpdate();
        }

        public void UpdateQuestionOrderList(Question question, int oldOrder, int newOrder)
        {
            if (oldOrder < newOrder)
            {
                // shift questions down; note that questions must be a list
                List<Question> questions = GetQuestions(q => q.Retired == question.Retired && q.GenusId == question.GenusId && q.Order > oldOrder && q.Order <= newOrder).ToList();
                foreach (var q in questions)
                {
                    q.Order--;
                    UpdateQuestionOrder(q.Id, q.Order);
                }
            }

            else if (oldOrder > newOrder)
            {
                // shift questions up; note that questions must be a list
                List<Question> questions = GetQuestions(q => q.Retired == question.Retired && q.GenusId == question.GenusId && q.Order >= newOrder && q.Order < oldOrder).ToList();
                foreach (var q in questions)
                {
                    q.Order++;
                    UpdateQuestionOrder(q.Id, q.Order);
                }
            }

        }

        /// <summary>
        /// Deletes a given Question.
        /// </summary>
        public void DeleteQuestion(Question question)
        {
            if (question == null || question.Id <= 0)
            {
                return;
            }

            m_context.Questions.Remove(question);
            m_context.SaveChanges();
        }

        /// <summary>
        /// Retires a question specified by id.
        /// </summary>
        public void RetireQuestion(int id)
        {
            var question = GetQuestion(id);
            if (question == null)
            {
                return;
            }

            question.Retired = true;
            SaveQuestion(question);
        }

        /// <summary>
        /// Un-retires a question specified by id.
        /// </summary>
        public void UnRetireQuestion(int id)
        {
            var question = GetQuestion(id);
            if (question == null)
            {
                return;
            }

            question.Retired = false;
            SaveQuestion(question);
        }

        /// <summary>
        /// Saves a given Map.
        /// </summary>
        public void SaveMap(Map map)
        {
            //If the map is new, Id == 0. New maps have list of required questions added to them.
            if (map.Id == 0)
            {
                foreach (var question in GetMapQuestions(map.GenusId, true))
                {
                    map.Questions.Add(question);
                }
                //  map.PicklistPrefix = null;
            }
            else
            {
                //   map.PicklistPrefix = GetMap(map.Id).PicklistPrefix;

            }

            DoSave(m_context.Maps, map, map.Id, q => q.Id == map.Id);
        }

        /// <summary>
        /// Deletes a given map.
        /// </summary>
        public void DeleteMap(Map map)
        {
            if (map == null || map.Id <= 0)
            {
                return;
            }

            m_context.Maps.Remove(map);
            m_context.SaveChanges();
        }

        public void SaveYear(Years years)
        {
            DoSave(m_context.Years, years, years.Id, year => year.Id == year.Id);
        }

        public void DeleteMapComponentYear(MapComponentYears mapcomponentyears)
        {
            m_context.MapComponentYears.Remove(mapcomponentyears);
            m_context.SaveChanges();
        }


        /// <summary>
        /// Saves a given MapComponent.
        /// If the id is <= 0, generates a new map component with the PlantNum value appropriately set.
        /// </summary>
        public void SaveMapComponent(MapComponent mapComponent)
        {
            if (mapComponent.Id <= 0 && !mapComponent.PlantNum.HasValue)
            {

                mapComponent.PlantNum = GetNextPlantNumber(mapComponent);

            }

            DoSave(m_context.MapComponents, mapComponent, mapComponent.Id, mc => mc.Id == mapComponent.Id);
        }

        private int GetNextPlantNumber(MapComponent mapComponent)
        {
            return GetMapComponents(mapComponent.MapId).Where(mc => mc.Row == mapComponent.Row && mc.isSeedling == mapComponent.isSeedling)
                                                        .Max(mc => mc.PlantNum)
                                                        .GetValueOrDefault() + 1;
        }

        public void UpdateMapComponent(MapComponent mapComponent)
        {
            if (!mapComponent.PlantNum.HasValue)
            {

                mapComponent.PlantNum = GetMapComponents(mapComponent.MapId)
                                                         .Where(mc => mc.Row == mapComponent.Row && mc.isSeedling == mapComponent.isSeedling)
                                                         .Max(mc => mc.PlantNum)
                                                         .GetValueOrDefault() + 1;

            }

            DoSaveUpdate();

        }


        public void SaveMapComponentGenotype(int mapComponentId, int? genotypeId)
        {
            var old = GetMapComponent(mapComponentId);
            if (old == null)
            {
                throw new Exception();
            }

            var genotype = GetGenotype(genotypeId.GetValueOrDefault());
            old.GenotypeId = genotypeId;
            old.Genotype = genotype;
            DoSaveUpdate();

        }

        /// <summary>
        /// Saves a given MapComponent.
        /// If the id is <= 0, generates a new map component with the PlantNum value appropriately set.
        /// </summary>
        public void SaveMapComponentYear(MapComponentYears mapComponent)
        {


            DoSave(m_context.MapComponentYears, mapComponent, mapComponent.Id, mc => mc.Id == mapComponent.Id);
        }

        public void DoSaveMapComponentYear(MapComponentYears mapComponentyears)
        {

            if (mapComponentyears == null) throw new ArgumentNullException(mapComponentyears.GetType().Name);

            if (mapComponentyears.Id <= 0)
            {
                m_context.MapComponentYears.Add(mapComponentyears);
            }

            DoSaveUpdate();


        }


        /// <summary>
        /// Saves a given MapComponent.
        /// If the id is <= 0, generates a new map component with the PlantNum value appropriately set.
        /// </summary>
        public void SaveMapComponentYearComment(MapComponentYears mapComponentYears)
        {
            var old = GetMapComponentYear(mapComponentYears.Id);
            if (old == null)
            {
                throw new Exception();
            }
            old.Comments = mapComponentYears.Comments;
            DoSaveUpdate();


        }

        public void SaveGenotypeCreatedDate(int genotypeId, DateTime created)
        {
            var old = GetGenotype(genotypeId);
            if (old == null)
            {
                throw new Exception("No Genotype");
            }
            if (old == null)
            {
                throw new Exception();
            }

            old.CreatedDate = created;
            DoSaveUpdate();
        }



        /// <summary>
        /// Deletes a given MapComponent.
        /// </summary>
        public void DeleteMapComponent(MapComponent mapComponent)
        {
            if (mapComponent == null || mapComponent.Id <= 0)
            {
                return;
            }

            m_context.MapComponents.Remove(mapComponent);
            m_context.SaveChanges();
        }



        public void SaveLocation(Location location)
        {
            if (location.Id < 1)
            {
                location.Retired = false;
            }
            else if (location.PrimaryContactId.HasValue)
            {
                location.Grower = GetGrower(location.PrimaryContactId.Value);
            }
            DoSave(m_context.Locations, location, location.Id, l => l.Id == location.Id);
        }

        public void DeleteLocation(Location location)
        {
            if (location.Id == 0)
            {
                return;
            }
            m_context.Locations.Remove(location);
            m_context.SaveChanges();
        }

        public void SaveFlatType(FlatType flatType)
        {
            if (flatType.Id < 1)
            {
                flatType.Retired = false;
            }
            DoSave(m_context.FlatType, flatType, flatType.Id, f => f.Id == flatType.Id);
        }


        public void SavePurpose(Purpose purpose)
        {
            DoSave(m_context.Purposes, purpose, purpose.Id, t => t.Id == purpose.Id);
        }
        public IEnumerable<Purpose> GetPurposes()
        {
            return m_context.Purposes;
        }
        public Purpose GetPurpose(int id)
        {
            return m_context.Purposes.SingleOrDefault(t => t.Id == id);
        }

        #endregion

        #region Disposal

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="isDisposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031",
            Justification = "Swallows general exceptions, to prevent the service from being disabled.")]
        private void Dispose(bool isDisposing)
        {
            if (!m_disposed)
            {
                try
                {
                    // Called from the IDisposable Method
                    // Nothing happens when isDisposing is false, since we don't have unmanaged resources
                    if (isDisposing)
                    {
                        try
                        {
                            if (m_context != null)
                            {
                                m_context.Dispose();
                            }
                        }
                        catch (Exception)
                        {
                            // This is intended to swallow up any exceptions to prevent the service from crashing.
                        }
                    }
                }
                finally
                {
                    m_disposed = true;
                }
            }
        }
        #endregion
    }
}
