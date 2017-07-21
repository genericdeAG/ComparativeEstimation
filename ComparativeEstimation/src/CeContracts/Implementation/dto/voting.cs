using System.Collections.Generic;

namespace CeContracts.dto
{
    public class VotingDto {
        public string VoterId { get; set; }
        public IEnumerable<WeightedComparisonPairDto> Weightings { get; set; }
    }

    public class WeightedComparisonPairDto
    {
        public string Id { get; set; }

        public Selection Selection { get; set; }
    }
}
