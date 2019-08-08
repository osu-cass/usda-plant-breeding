using System.Collections.Generic;
using System.Linq;
using Usda.PlantBreeding.Core.Interfaces;
using Usda.PlantBreeding.Core.Models;
using Usda.PlantBreeding.Data.DataAccess;
using Usda.PlantBreeding.Data.Models;
using Usda.PlantBreeding.Core.Translations;
using System.Web.Mvc;
using Usda.PlantBreeding.Core.Extensions;
using BSGUtils;
using System;

namespace Usda.PlantBreeding.Core.Infrastructure
{
    public class PhenotypeEntryUOW : IPhenotypeEntryUOW
    {
        private IPlantBreedingRepo u_repo;
        private IAccessionUOW a_repo;
        const string seedlingName = "seedling";
        const string observationName = "observation";

        public PhenotypeEntryUOW() : this(new PlantBreedingRepo()) { }

        public PhenotypeEntryUOW(IPlantBreedingRepo repo)
        {
            u_repo = repo;
            a_repo = new AccessionUOW(repo);
        }

        #region Access


        public PhenotypeEntryViewModel GetPhenotypeEntryVM(int id, string year)
        {
            Map map = u_repo.GetMap(id);
            List<Fate> possibleFates = u_repo.GetFates().OrderBy(t => t.Order).ToList();
            Years mapYear;
            if (year.IsNullOrWhiteSpace())
            {
                mapYear = map.Years.OrderByDescending(t => t.Year).FirstOrDefault();
            }
            else
            {
                mapYear = map.Years.SingleOrDefault(t => t.Year == year);
            }
            if (mapYear == null || mapYear.Id <= 0)
            {
                throw new PhenotypeException(Properties.Settings.Default.ExceptionPhenotypeInvalidYear);
            }

            PhenotypeEntryViewModel vm = GetPhenotypeEntryVM(map, possibleFates, mapYear.Id);
            return vm;
        }


        public PhenotypeRowsViewModel GetPhenotypeMapRows(int id, string year)
        {
            Map map = u_repo.GetMap(id);
            Years mapYear;
            if (year.IsNullOrWhiteSpace())
            {
                mapYear = map.Years.OrderByDescending(t => t.Year).FirstOrDefault();
            }
            else
            {
                mapYear = map.Years.SingleOrDefault(t => t.Year == year);
            }

            if (mapYear == null || mapYear.Id <= 0)
            {
                throw new PhenotypeException(Properties.Settings.Default.ExceptionPhenotypeInvalidYear);
            }

            var mapComponentYears = GetMapComponentsYears(map, mapYear.Id);
            return GetPhenotypeMapRows(map, mapComponentYears, mapYear.Id);
        }

        public PhenotypeEntryViewModel GetPhenotypeSummary(int genotypeId)
        {
            Genotype genotype = u_repo.GetGenotype(genotypeId);
            List<Fate> possibleFates = u_repo.GetFates().OrderBy(t => t.Order).ToList();
            PhenotypeEntryViewModel vm = GenotypePhenotypeEntry(genotype, possibleFates);
            return vm;
        }

        public PhenotypeRowsViewModel GetGenotypeRows(int genotypeId)
        {
            Genotype genotype = u_repo.GetGenotype(genotypeId);
        
            return GetGenotypeRows(genotype);
        }

        private IEnumerable<MapComponentYears> GetMapComponentsYears(Map map, int yearId)
        {
            IEnumerable<MapComponentYears> mapComponents = map
              .MapComponents.Where(mc => mc.isSeedling == false).SelectMany(t => t.MapComponentYears).Where(t => t.YearsId == yearId);
            return mapComponents;
        }

        private IEnumerable<MapComponentYears> GetPreviousMapComponentsYears(Map map, int year)
        {
            var previousYear = map.Years.Where(y => int.Parse(y.Year) < year).OrderByDescending(t => int.Parse(t.Year)).FirstOrDefault();

            if (previousYear == null)
            {
                return new List<MapComponentYears>();
            }

            var components = u_repo.GetQueryableMapComponentYears(mcy => mcy.MapComponent.MapId == map.Id
                                                                && mcy.MapComponent.isSeedling == false
                                                                && mcy.YearsId == previousYear.Id)
                                                                .ToList();
            return components;
        }

        private Dictionary<int, MapComponentYears> GetPreviousMapComponentsYearsDict(Map map, int year)
        {
            var components = GetPreviousMapComponentsYears(map, year).ToDictionary(mcy => mcy.MapComponentId, mcy => mcy);
            return components;
        }


        public IEnumerable<DataListViewModel> SearchAccessions(string term, int genusId, int? mapId, bool mapOnly, int recordsToReturn)
        {
            return a_repo.Search(term, genusId, mapId, mapOnly, recordsToReturn);
        }

        #endregion

        #region Modifiers


        private MapComponentYears SaveSeedling(SeedlingViewModel seedlingViewModel, Map map)
        {

            Genotype seedlingGen = u_repo.GetGenotype(seedlingViewModel.GenotypeId);
            if (seedlingGen == null)
            {
                throw new PhenotypeException("No Accession specified");
            }

            var year = u_repo.GetYear(seedlingViewModel.MapYearId);
            Genotype selection = CreateGenotypeSelection(seedlingViewModel.GenotypeId, year.Year, seedlingViewModel.CreatedDate);
            MapComponentYears newSeedling = CreateMapComponent(seedlingViewModel, selection, map);
            return newSeedling;
        }


        private Genotype CreateGenotypeSelection(int genotypeId, string year, DateTime? createdDate)
        {
            Genotype genotype = u_repo.GetGenotype(genotypeId);
            int SelectionNum = genotype.Family.Genotypes.Max(x => x.SelectionNum).GetValueOrDefault() + 1;
            if (!createdDate.HasValue)
            {
                createdDate = DateTime.Now;
            }

            Genotype newSelection = new Genotype()
            {
                Family = genotype.Family,
                FamilyId = genotype.FamilyId,
                Year = year,
                SelectionNum = SelectionNum,
                IsRoot = false,
                CreatedDate = createdDate
            };
            u_repo.SaveGenotype(newSelection);

            return newSelection;
        }

        public MapComponentSummaryVM CreateSeedlingSelection(SeedlingViewModel seedlingViewModel)
        {
            var map = u_repo.GetMap(seedlingViewModel.MapId);

            MapComponentYears mapcomp = SaveSeedling(seedlingViewModel, map);
            MapComponentSummaryViewModel mapSummaryVM = mapcomp.ToMapComponentSummaryViewModel();
            mapSummaryVM.PossibleFates = u_repo.GetFates().ToSelectList(t => t.Id.ToString(), t => t.Name, t => false);

            return PhenotypeEntryRowFull(mapcomp, map.Questions.ToList());
        }


        private MapComponentYears CreateMapComponent(SeedlingViewModel seedlingViewModel, Genotype genotype, Map map)
        {
            MapComponent newSeedling = new MapComponent()
            {
                GenotypeId = genotype.Id,
                Genotype = genotype,
                MapId = map.Id,
                Map = map,
                Row = seedlingViewModel.RowNum,
                PlantNum = seedlingViewModel.PlantNum,
                isSeedling = false
            };
            u_repo.SaveMapComponent(newSeedling);

            MapComponentYears mapComponentYear = new MapComponentYears()
            {
                Answers = new List<Answer>(),
                YearsId = seedlingViewModel.MapYearId,
                MapComponentId = newSeedling.Id,
                MapComponent = newSeedling
            };
            u_repo.SaveMapComponentYear(mapComponentYear);
            return mapComponentYear;
        }

        #endregion

        private List<MapComponentSummaryVM> PhenotypeEntryRowsFull(List<MapComponentYears> mcYears, List<Question> mapQuestions, Dictionary<int, MapComponentYears> previousYearComps = null)
        {
            List<MapComponentSummaryVM> rows = new List<MapComponentSummaryVM>();
            foreach (MapComponentYears mcy in mcYears)
            {
                rows.Add(PhenotypeEntryRowFull(mcy, mapQuestions, previousYearComps));
            }

            rows = rows.OrderBy(mc => mc.Row).ThenBy(mc => mc.PlantNum).ToList();
            return rows;
        }

        private MapComponentSummaryVM PhenotypeEntryRowFull(MapComponentYears mapcomponentYear, List<Question> mapQuestions, Dictionary<int, MapComponentYears> previousYearComps = null)
        {
            MapComponentYears previous = null;
            if (previousYearComps != null)
            {
                previousYearComps.TryGetValue(mapcomponentYear.MapComponentId, out previous);
            }
            MapComponentSummaryVM row = mapcomponentYear.ToMapComponentSummaryVM(previous);
            IEnumerable<Question> unanswered = mapQuestions
                                                 .Where(q => !row.QuestionToAnswer.Any(a => a.Key == q.Id.ToString()))
                                                 .ToList();

            row.QuestionToAnswer = PhenotypeEntryViewModelTranslator.GenerateRowsFullAnswerVMs(row.Id, row.Genotype.Id, row.Id, unanswered, row.QuestionToAnswer);
            return row;
        }

        /// <summary>
        ///  Gets a map phenotype entry viewmodel with possible fates based on the year
        /// </summary>
        /// <returns></returns>
        private PhenotypeEntryViewModel GetPhenotypeEntryVM(Map map, IEnumerable<Fate> possibleFates, int yearId)
        {
            string type = (map.IsSeedlingMap) ? seedlingName : observationName;
            Years year = u_repo.GetYear(yearId);

            var questions = map.Questions;
            var mapVM = map.ToMapVM();
            mapVM.EvaluationYear = year.Year;
            var fates = possibleFates.Select(pf => pf.ToFateVM()).ToList();

            return GetPhenotypeEntryVM(
                questions: questions,
                fates: fates,
                genusId: map.GenusId,
                type: type,
                map: mapVM,
                yearId: yearId,
                yearName: year.Year);
        }

        /// <summary>
        ///  Gets a map phenotype entry viewmodel with possible fates based on the year
        /// </summary>
        /// <returns></returns>
        private PhenotypeRowsViewModel GetPhenotypeMapRows(Map map, IEnumerable<MapComponentYears> mapComponentsYears, int yearId)
        {
            Years year = u_repo.GetYear(yearId);
            Dictionary<int, MapComponentYears> previousYearComps = GetPreviousMapComponentsYearsDict(map, int.Parse(year.Year));

            var questions = map.Questions;

            return GetPhenotypeRows(mapComponentsYears, questions, previousYearComps);
        }

        /// <summary>
        ///  Gets a map phenotype entry viewmodel with possible fates based on the year
        /// </summary>
        /// <returns></returns>
        private PhenotypeRowsViewModel GetGenotypeRows(Genotype genotype)
        {
            var mapComponentYears = u_repo.GetQueryableMapComponentYears(
                mcy => mcy.MapComponent.GenotypeId == genotype.Id
                && mcy.MapComponent.isSeedling == false).ToList();


            IEnumerable<Question> questions = mapComponentYears.SelectMany(t => t.Answers.Select(a => a.Question)).Distinct().ToList();

            return GetPhenotypeRows(mapComponentYears, questions, null);
        }

        /// <summary>
        ///  Gets a genotype phenotype entry viewmodel with possible fates
        /// </summary>
        /// <returns></returns>
        public PhenotypeEntryViewModel GenotypePhenotypeEntry(Genotype genotype, IEnumerable<Fate> possibleFates)
        {
            //genotype.ThrowIfNull("genotype");
            var mapComponentYears = u_repo.GetQueryableMapComponentYears(
                mcy => mcy.MapComponent.GenotypeId == genotype.Id
                && mcy.MapComponent.isSeedling == false).ToList();


            IEnumerable<Question> totalQuestions = mapComponentYears.SelectMany(t => t.Answers.Select(a => a.Question)).Distinct().ToList();

            var fates = possibleFates.Select(pf => pf.ToFateVM()).ToList();
            var genotypeVM = genotype.ToGenotypeVM();

            return GetPhenotypeEntryVM(
                questions: totalQuestions,
                fates: fates,
                genusId: genotype.Family.GenusId,
                type: "summary",
                genotype: genotypeVM);

        }

        /// <summary>
        ///  Gets a phenotype entry viewmodel for a type with optional arguments
        /// </summary>
        /// <returns></returns>
        private PhenotypeEntryViewModel GetPhenotypeEntryVM(
            IEnumerable<Question> questions,
            List<FateViewModel> fates,
            int genusId,
            string type,
            Dictionary<int, MapComponentYears> previousYearComps = null,
            MapViewModel map = null,
            int yearId = -1,
            string yearName = "",
            GenotypeViewModel genotype = null)
        {
            Dictionary<string, QuestionViewModel> questionHeaders = questions.ToDictionary(q => q.Id.ToString(), q => q.ToQuestionVM());

            List<string> questionOrder = questionHeaders
                                        .OrderBy(t => t.Value.Order)
                                        .Select(q => q.Key)
                                        .ToList();


            return new PhenotypeEntryViewModel()
            {
                Map = map,
                Type = type,
                QuestionToQuestion = questionHeaders,
                QuestionOrder = questionOrder,
                YearId = yearId,
                GenusId = genusId,
                YearName = yearName,
                Fates = fates,
                Genotype = genotype
            };
        }

        /// <summary>
        ///  Gets a phenotype entry viewmodel for a type with optional arguments
        /// </summary>
        /// <returns></returns>
        private PhenotypeRowsViewModel GetPhenotypeRows(
            IEnumerable<MapComponentYears> mapComponentsYears,
            IEnumerable<Question> questions,
            Dictionary<int, MapComponentYears> previousYearComps = null)
        {
            var filteredMapComponentYears = mapComponentsYears.Where(t => t.MapComponent.GenotypeId != null).ToList();
            var rows = PhenotypeEntryRowsFull(filteredMapComponentYears, questions.ToList(), previousYearComps);

            List<string> pageListIds = rows.Where(t => t.Row.HasValue)
                                       .Select(r => r.Row.ToString())
                                       .Distinct()
                                       .OrderBy(r => r)
                                       .ToList();

            return new PhenotypeRowsViewModel()
            {
                RowIdList = pageListIds,
                MapComponentRows = rows,
            };
        }


        public void SaveAnswer(AnswerViewModel answervm)
        {
            Answer answer = null;
            if (answervm.Id > 0)
            {
                answer = u_repo.GetAnswer(a => a.Id == answervm.Id);
            }
            else if (answervm.MapComponentYearsId > 0 && answervm.QuestionId > 0)
            {
                answer = u_repo.GetAnswer(a => a.MapComponentYearsId == answervm.MapComponentYearsId
                                        && a.QuestionId == answervm.QuestionId);
            }

            if (answer == null)
            {
                answer = answervm.ToAnswer();
            }

            try
            {
                answer.Text = answervm.Text;
                u_repo.DoSaveAnswer(answer);

            }
            catch (ArgumentNullException)
            {
                throw new PhenotypeException("Invalid answer to save");
            }

        }

        public void SaveComments(MapComponentSummaryVM summary)
        {
            if (summary == null) throw new PhenotypeException("Invalid viewModel");
            if (summary.Id < 0) throw new PhenotypeException("Invalid viewModel");

            MapComponentYears mapcompyears = u_repo.GetMapComponentYear(summary.Id);
            mapcompyears.Comments = summary.Comments;
            try
            {
                u_repo.DoSaveMapComponentYear(mapcompyears);

            }
            catch (ArgumentNullException)
            {
                throw new PhenotypeException("Invalid mapcompyears to save");
            }

        }

        public void SavePlantNum(MapComponentSummaryVM summary)
        {
            if (summary == null) throw new PhenotypeException("Invalid viewModel");
            if (summary.Id < 0) throw new PhenotypeException("Invalid viewModel");

            MapComponentYears mapcompyears = u_repo.GetMapComponentYear(summary.Id);
            mapcompyears.MapComponent.PlantNum = summary.PlantNum;

            try
            {
                u_repo.DoSaveMapComponentYear(mapcompyears);

            }
            catch (ArgumentNullException)
            {
                throw new PhenotypeException("Invalid mapcompyears to save");
            }

        }


        public void SaveRowNum(MapComponentSummaryVM summary)
        {
            if (summary == null) throw new PhenotypeException("Invalid viewModel");
            if (summary.Id < 0) throw new PhenotypeException("Invalid viewModel");

            MapComponentYears mapcompyears = u_repo.GetMapComponentYear(summary.Id);
            mapcompyears.MapComponent.Row = summary.Row;

            try
            {
                u_repo.DoSaveMapComponentYear(mapcompyears);

            }
            catch (ArgumentNullException)
            {
                throw new PhenotypeException("Invalid mapcompyears to save");
            }

        }
        public void SaveCreatedDate(MapComponentSummaryVM summary)
        {
            if (summary == null) throw new PhenotypeException("Invalid viewModel");
            if (summary.Id < 0) throw new PhenotypeException("Invalid viewModel");
            if (summary.Genotype == null) throw new PhenotypeException("Invalid viewModel");

            try
            {
                u_repo.SaveGenotypeCreatedDate(summary.Genotype.Id, summary.Genotype.CreatedDate);
            }
            catch (ArgumentNullException)
            {
                throw new PhenotypeException("Invalid mapcompyears to save");
            }

        }



    }
}
