using System;
using System.Collections.Generic;

namespace CeContracts
{
    public interface IComparativeEstimation
    {
        void Register_Product_Owner_Client(string id);

        void Register_Estimator_Client(string id);

        void Register_Estimator_Weighting(IEnumerable<WeightedComparisonPairDto> voting, Action ok, Action exception);

        TotalWeightingDto TotalWeighting { get; }

        IEnumerable<ComparisonPairDto> ComparisonPairs { get; }

        void Create_Sprint(IEnumerable<string> stories);

        void Delete_Sprint();      
    }
}
