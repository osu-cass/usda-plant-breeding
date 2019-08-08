using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usda.PlantBreeding.Data.DataAccess;
using Usda.PlantBreeding.Data.Models;
using Usda.PlantBreeding.Site.Models;
using Usda.PlantBreeding.Site.Models.Translators;

namespace Usda.PlantBreeding.Site.Tests.Translators
{
    [TestClass]
    public class ViewModelTranslatorTest
    {
        #region Helpers

        private KeyValuePair<KeyValuePair<int, int>, Answer> BuildDictionaryEntry(Answer ans)
        {
            //this is not working properly.
            return new KeyValuePair<KeyValuePair<int, int>, Answer>(new KeyValuePair<int, int>(ans.MapComponentYearsId, ans.QuestionId), ans);
        }

        #endregion


        #region PhenotypeEntryViewModel

        //[TestMethod]
        //public void ToPhenotypeEntryViewModelTestGetAllAnswers()
        //{
        //    var answer1 = new Answer { Id = 1, QuestionId = 1, MapComponentId = 1, GenotypeId = 5, Text = "Answer 1 MC1" };
        //    var answer2 = new Answer { Id = 2, QuestionId = 2, MapComponentId = 1, GenotypeId = 5, Text = "Answer 2 MC1" };
        //    var answer3 = new Answer { Id = 3, QuestionId = 3, MapComponentId = 1, GenotypeId = 5, Text = "Answer 3 MC1" };
        //    var answer4 = new Answer { Id = 4, QuestionId = 1, MapComponentId = 3, GenotypeId = 5, Text = "Answer 1 MC3" };
        //    var answer5 = new Answer { Id = 5, QuestionId = 2, MapComponentId = 3, GenotypeId = 5, Text = "Answer 2 MC3" };
        //    var answer6 = new Answer { Id = 6, QuestionId = 3, MapComponentId = 3, GenotypeId = 5, Text = "Answer 3 MC3" };

        //    var mcSummary1 = new MapComponentSummaryViewModel()
        //    {
        //        Id = 1,
        //        GenotypeId = 1,
        //        GenotypeName = "TestGenoName"
        //    };

        //    var mcSummary3 = new MapComponentSummaryViewModel()
        //    {
        //        Id = 3,
        //        GenotypeId = 3,
        //        GenotypeName = "TestGenoName"
        //    };

        //    var expectedGenotype = new Genotype()
        //    {
        //        GivenName = "TestGenoName"
        //    };

        //    var expected = new PhenotypeEntryViewModel()
        //    {
        //        Keys = new List<KeyValuePair<int, int>> 
        //        { 
        //            new KeyValuePair<int, int>(1, 1),
        //            new KeyValuePair<int, int>(1, 2),
        //            new KeyValuePair<int, int>(1, 3),
        //            new KeyValuePair<int, int>(3, 1),
        //            new KeyValuePair<int, int>(3, 2),
        //            new KeyValuePair<int, int>(3, 3)
        //        },

        //        QuestionHeaders = new Dictionary<int, string>()
        //        {
        //            {1, "Question 1"},
        //            {2, "Question 2"},
        //            {3, "Question 3"}
        //        },

        //        MapComponentRows = new Dictionary<int, MapComponentSummaryViewModel>()
        //        {
        //            {1, mcSummary1},
        //            {3, mcSummary3}
        //        },

        //        Rows = new Dictionary<KeyValuePair<int, int>, Answer>()
        //        {
        //            {this.BuildDictionaryEntry(answer1).Key,this.BuildDictionaryEntry(answer1).Value},
        //            {this.BuildDictionaryEntry(answer2).Key,this.BuildDictionaryEntry(answer2).Value},
        //            {this.BuildDictionaryEntry(answer3).Key,this.BuildDictionaryEntry(answer3).Value},
        //            {this.BuildDictionaryEntry(answer4).Key,this.BuildDictionaryEntry(answer4).Value},
        //            {this.BuildDictionaryEntry(answer5).Key,this.BuildDictionaryEntry(answer5).Value},
        //            {this.BuildDictionaryEntry(answer6).Key,this.BuildDictionaryEntry(answer6).Value}
        //        }
        //    };

        //    var input = new Map()
        //    {
        //        Id = 1,
        //        Questions = new List<Question>
        //        {
        //            new Question {Id=1, Text="Question 1"},
        //            new Question {Id=2, Text="Question 2"},
        //            new Question {Id=3, Text="Question 3"}
        //        },
        //        MapComponents = new List<MapComponent>
        //        {
        //            new MapComponent {Id=1, GenotypeId=5, MapId=1, 
        //                Answers= new List<Answer> 
        //                {
        //                    answer1, answer2, answer3
        //                },
        //                Genotype = expectedGenotype
        //            },
        //            new MapComponent {Id=3, GenotypeId=5, MapId=1, 
        //                Answers= new List<Answer> 
        //                {
        //                    answer4, answer5, answer6                          
        //                },
        //                Genotype = expectedGenotype
        //            }
        //        }
        //    };

        //    var actual = input.ToPhenotypeEntryViewModel();

        //    Assert.IsTrue(expected.Equals(expected, actual));
        //}

        //[TestMethod]
        //public void ToPhenotypeEntryViewModelTestGetAllAnswersAndNullCoalesce()
        //{
        //    var answer1 = new Answer { Id = 1, QuestionId = 1, MapComponentId = 1, GenotypeId = 5, Text = "Answer 1 MC1" };
        //    var answer2 = new Answer { Id = 2, QuestionId = 2, MapComponentId = 1, GenotypeId = 5, Text = "Answer 2 MC1" };
        //    var answer3 = new Answer { Id = 3, QuestionId = 3, MapComponentId = 1, GenotypeId = 5, Text = "Answer 3 MC1" };
        //    var answer4 = new Answer { Id = 4, QuestionId = 1, MapComponentId = 3, GenotypeId = 5, Text = "Answer 1 MC3" };
        //    var answer5 = new Answer { Id = 5, QuestionId = 2, MapComponentId = 3, GenotypeId = 5, Text = "Answer 2 MC3" };
        //    var nullAnswer = new Answer { QuestionId = 3, MapComponentId = 3 };

        //    var mcSummary1 = new MapComponentSummaryViewModel()
        //    {
        //        Id = 1,
        //        GenotypeId = 1,
        //        GenotypeName = "TestGenoName"
        //    };

        //    var mcSummary3 = new MapComponentSummaryViewModel()
        //    {
        //        Id = 3,
        //        GenotypeId = 3,
        //        GenotypeName = "TestGenoName"
        //    };

        //    var expectedGenotype = new Genotype()
        //    {
        //        GivenName = "TestGenoName"
        //    };

        //    var expected = new PhenotypeEntryViewModel()
        //    {
        //        Keys = new List<KeyValuePair<int, int>> 
        //        { 
        //            new KeyValuePair<int, int>(1, 1),
        //            new KeyValuePair<int, int>(1, 2),
        //            new KeyValuePair<int, int>(1, 3),
        //            new KeyValuePair<int, int>(3, 1),
        //            new KeyValuePair<int, int>(3, 2),
        //            new KeyValuePair<int, int>(3, 3)
        //        },

        //        QuestionHeaders = new Dictionary<int, string>()
        //        {
        //            {1, "Question 1"},
        //            {2, "Question 2"},
        //            {3, "Question 3"}
        //        },

        //        MapComponentRows = new Dictionary<int, MapComponentSummaryViewModel>()
        //        {
        //            {1, mcSummary1},
        //            {3, mcSummary3}
        //        },

        //        Rows = new Dictionary<KeyValuePair<int, int>, Answer>()
        //        {
        //            {this.BuildDictionaryEntry(answer1).Key,this.BuildDictionaryEntry(answer1).Value},
        //            {this.BuildDictionaryEntry(answer2).Key,this.BuildDictionaryEntry(answer2).Value},
        //            {this.BuildDictionaryEntry(answer3).Key,this.BuildDictionaryEntry(answer3).Value},
        //            {this.BuildDictionaryEntry(answer4).Key,this.BuildDictionaryEntry(answer4).Value},
        //            {this.BuildDictionaryEntry(answer5).Key,this.BuildDictionaryEntry(answer5).Value},
        //            {this.BuildDictionaryEntry(nullAnswer).Key,this.BuildDictionaryEntry(nullAnswer).Value}
        //        }
        //    };

        //    var input = new Map()
        //    {
        //        Id = 1,
        //        Questions = new List<Question>
        //        {
        //            new Question {Id=1, Text="Question 1"},
        //            new Question {Id=2, Text="Question 2"},
        //            new Question {Id=3, Text="Question 3"}
        //        },
        //        MapComponents = new List<MapComponent>
        //        {
        //            new MapComponent {Id=1, GenotypeId=5, MapId=1, 
        //                Answers= new List<Answer> 
        //                {
        //                    answer1, answer2, answer3
        //                },
        //                Genotype = expectedGenotype
        //            },
        //            new MapComponent {Id=3, GenotypeId=5, MapId=1, 
        //                Answers= new List<Answer> 
        //                {
        //                    answer4, answer5                        
        //                },
        //                Genotype = expectedGenotype
        //            }
        //        }
        //    };

        //    var actual = input.ToPhenotypeEntryViewModel();

        //    Assert.IsTrue(expected.Equals(expected, actual));
        //}

        //[TestMethod]
        //[ExpectedException(typeof(ArgumentNullException), "map")]
        //public void ToPhenotypeEntryViewModelNullArgument()
        //{
        //    Map map = null;

        //    var actual = map.ToPhenotypeEntryViewModel();
        //}

        #endregion

        #region MapQuestionListViewModel

        //[TestMethod]
        //public void ToMapQuestionListViewModelHappyCase()
        //{
        //    int targetGenus = 1;
        //    var question1 = new Question() { GenusId = 1, Text = "Question1" };
        //    var question2 = new Question() { GenusId = 1, Text = "Question2" };
        //    var question3 = new Question() { GenusId = 1, Text = "Question3" };

        //    var questionList = new List<Question>() { question1, question2, question3 };

        //    var expected = new MapQuestionListViewModel()
        //    {
        //        Map = new Map() { GenusId = targetGenus },
        //        Questions = questionList
        //    };

        //    var actual = questionList.ToMapQuestionListViewModel(targetGenus);

        //    Assert.IsTrue(expected.Equals(expected, actual));
        //}

        //[TestMethod]
        //[ExpectedException(typeof(ArgumentNullException), "questions")]
        //public void ToMapQuestionListViewModelNullArgument()
        //{
        //    int targetGenus = 1;
        //    List<Question> questionList = null;

        //    var actual = questionList.ToMapQuestionListViewModel(targetGenus);
        //}

        #endregion

        #region SelectionSummaryViewModel

        /// <summary>
        // need deep object comparer and make sure that the fixed models are actually correct
        // I'm not sure I setup the dummy data correctly
        // don't trust this test
        /// </summary>
        //[TestMethod]
        //public void ToSelectionSummaryViewModelHappyCase()
        //{
        //    IEnumerable<FamilyUseViewModel> familyUseList = new List<FamilyUseViewModel>();
        //    PhenotypeEntryViewModel observationViewModel = new PhenotypeEntryViewModel();

        //    var ploidy = new Ploidy()
        //    {
        //        Name = "Ploidy A"
        //    };

        //    var male = new Genotype()
        //    {
        //        GivenName = "Male"
        //    };

        //    var female = new Genotype()
        //    {
        //        GivenName = "Female"
        //    };

        //    var family = new Family()
        //    {
        //        Ploidy = ploidy,
        //        MaleGenotype = male,
        //        FemaleGenotype = female
        //    };

        //    var genotype = new Genotype()
        //    {
        //        Id = 1,
        //        GivenName = "Genotype 1",
        //        Family = family
        //    };

        //    SelectionSummaryViewModel expected = new SelectionSummaryViewModel()
        //    {
        //        GenotypeId = 1,
        //        GenotypeName = "Genotype 1",
        //        PloidyName = "Ploidy A",
        //        MaleParentName = male.Name,
        //        FemaleParentName = female.Name,
        //        ObservationViewModel = observationViewModel,
        //        FamilyUseList = familyUseList,
        //        SeedlingViewModel = null,
        //        ObservationList = null
        //    };

        //    var actual = genotype.ToSelectionSummaryViewModel(observationViewModel, familyUseList);

        //    Assert.IsTrue(expected.Equals(expected, actual));
        //}

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "genotype")]
        public void ToSelectionSummaryViewModelNullGenotype()
        {
            //Genotype genotype = null;

            //IEnumerable<FamilyUseViewModel> familyUseList = new List<FamilyUseViewModel>();
            //PhenotypeEntryViewModel observationViewModel = new PhenotypeEntryViewModel();

            //genotype.ToSelectionSummaryViewModel(observationViewModel, familyUseList);

            //this unit test is not working properly.
            Assert.IsTrue(false);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "observationViewModel")]
        public void ToSelectionSummaryViewModelNullobservationViewModel()
        {
            //PhenotypeEntryViewModel observationViewModel = null;

            //Genotype genotype = new Genotype();
            //IEnumerable<FamilyUseViewModel> familyUseList = new List<FamilyUseViewModel>();

            //genotype.ToSelectionSummaryViewModel(observationViewModel, familyUseList);

            //this unit test is not working properly.
            Assert.IsTrue(false);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "familyUseList")]
        public void ToSelectionSummaryViewModelNullfamilyUseList()
        {
            //IEnumerable<FamilyUseViewModel> familyUseList = null;

            //Genotype genotype = new Genotype();
            //PhenotypeDisplayViewModel observationViewModel = new PhenotypeDisplayViewModel();

            //genotype.ToSelectionSummaryViewModel(observationViewModel, familyUseList);

            //this unit test is not working properly.
            Assert.IsTrue(false);
        }

        #endregion

        #region FamilyUseViewModel
        
        /// <summary>
        // need deep object comparer and make sure that the fixed models are actually correct
        // I'm not sure I setup the dummy data correctly
        // don't trust this test
        /// </summary>
        //[TestMethod]
        //public void ToFamilyUseViewModelHappyCase()
        //{
        //    var origin = new Origin() { Name = "Origin", Id = 1 };
        //    var purpose = new Purpose() { Name = "Purpose" };
        //    var female = new Genotype() { GivenName = "Female" };
        //    var male = new Genotype() { GivenName = "Male" };

        //    var femaleFamily = new Family() 
        //        {
        //            GivenName = "Female", 
        //            Purpose = purpose, 
        //            Year = "3030", 
        //            FemaleGenotype = female, 
        //            MaleGenotype = male
        //        };

        //    var maleFamily = new Family()
        //        {
        //            GivenName = "Male",
        //            Purpose = purpose,
        //            Year = "3030",
        //            FemaleGenotype = female,
        //            MaleGenotype = male
        //        };

        //    IEnumerable<Family> families = new List<Family>() { femaleFamily, maleFamily };

        //    IEnumerable<FamilyUseViewModel> expected = new List<FamilyUseViewModel>()
        //    {
        //        new FamilyUseViewModel()
        //        {
        //            OriginName = femaleFamily.OriginalName,
        //            GivenName = femaleFamily.GivenName,
        //            PurposeName = purpose.Name,
        //            Year = femaleFamily.Year,
        //            CrossWithName = male.GivenName
        //        }, 
        //        new FamilyUseViewModel()
        //        {
        //            OriginName = maleFamily.OriginalName,
        //            GivenName = maleFamily.GivenName,
        //            PurposeName = purpose.Name,
        //            Year = maleFamily.Year,
        //            CrossWithName = female.GivenName
        //        }
        //    };
        //    IEnumerable<FamilyUseViewModel> actual = families.ToFamilyUseViewModel(female).ToList();
        //    Assert.IsTrue(expected.Equals(actual));
        //}

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "genotype")]
        public void ToFamilyUseViewModelNullGenotype()
        {
            //Genotype genotype = null;

            //IEnumerable<Family> families = new List<Family>();

            //families.ToFamilyUseViewModel(genotype);

            //this unit test is not working properly.
            Assert.IsTrue(false);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "families")]
        public void ToFamilyUseViewModelNullFamilies()
        {
            //IEnumerable<Family> families = null;

            //Genotype genotype = new Genotype();

            //families.ToFamilyUseViewModel(genotype);

            //this unit test is not working properly.
            Assert.IsTrue(false);
        }

        #endregion
    }



}
