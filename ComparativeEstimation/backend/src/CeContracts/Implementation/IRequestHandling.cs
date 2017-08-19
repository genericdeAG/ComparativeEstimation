using System;
using System.Collections.Generic;

using CeContracts.dto;

namespace CeContracts
{
    public interface IRequestHandling
    {
        string Create_Sprint(IEnumerable<string> stories);

        ComparisonPairsDto ComparisonPairs(string id);
        void Submit_voting(string sprintId, VotingDto voting, Action onOk, Action<InconsistentVotingDto> onInconsistency);

        TotalWeightingDto Get_total_weighting_for_sprint(string id);
        void Delete_Sprint(string id);
    }
}