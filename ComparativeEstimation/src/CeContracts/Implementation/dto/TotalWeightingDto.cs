namespace CeContracts.dto
{
    public class TotalWeightingDto
    {
        public string SprintId { get; set; }
        public string[] Stories { get; set; }
        public int EstimatorClientRegistrations { get; set; }
        public int EstimatorWeightings { get; set; }
    }
}
