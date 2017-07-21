using System;
using System.Collections.Generic;

using CeContracts.dto;
using CeContracts.data;

namespace CeContracts
{
    //TODO: DTOs sind hier nicht bekannt!
    public interface IWeighting
    {
        void Compute_Estimator_Weighting(IEnumerable<WeightedComparisonPairDto> voting, IEnumerable<ComparisonPair> comparisonPairs, Action<TotalWeighting> ok, Action exception);

        TotalWeighting Get_Total_Weighting_From(IEnumerable<TotalWeighting> estimatorWeightings);
    }
}
