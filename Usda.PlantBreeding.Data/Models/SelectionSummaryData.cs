using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usda.PlantBreeding.Data.Models
{
    /// <summary>
    /// A container for information relevant to the Selection Summary.
    /// </summary>
    public class SelectionSummaryData
    {
        public string SelectionName { get; set; }
        public string MaleParentName { get; set; }
        public string FemaleParentName { get; set; }
        public string PloidyName { get; set; }
        public IEnumerable<Question> SeedlingQuestions { get; set; }
        public IEnumerable<Question> ObervationQuestions { get; set; }
        public IEnumerable<SeedlingObservationData> SeedlingObservationList { get; set; }
        public IEnumerable<ObservationSummaryData> ObservationSummaryList { get; set; }
        public IEnumerable<CrossUseData> CrossUseList { get; set; }
        public IEnumerable<ObservationData> ObservationList { get; set; }
    }

    public class SeedlingObservationData
    {
        public string Oyear { get; set; }
        public string Syear { get; set; }
        public IEnumerable<Answer> Answers { get; set; }
        public string Status { get; set; }
        public string Remark { get; set; }
    }

    public class ObservationSummaryData
    {
        public string Year { get; set; }
        public string Location { get; set; }
        public IEnumerable<Answer> Answers { get; set; }
        public string Status { get; set; }
        public string Remark { get; set; }
    }

    public class CrossUseData
    {
        public string OriginName { get; set; }
        public int CrossNum { get; set; }
        public int SNumber { get; set; }
        public string GivenName { get; set; }
        public string CrossWithThin { get; set; }
        public string PurposeName { get; set; }
        public string CrossYear { get; set; }
        public string PGSeedlingName { get; set; }
    }

    public class ObservationData
    {
        public DateTime ObservationDate { get; set; }
        public string ObservationNote { get; set; }
    }
}
