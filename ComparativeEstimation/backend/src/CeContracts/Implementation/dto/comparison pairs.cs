namespace CeContracts.dto
{
    public class ComparisonPairsDto {
        public string SprintId { get; set; }
        public ComparisonPairDto[] Pairs { get; set; }
    }

    public class ComparisonPairDto
    {
        public string Id { get; set; }

        public string A { get; set; }
        public string B { get; set; }
    }
}
