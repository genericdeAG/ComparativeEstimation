using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CeRepository
{
    public class SprintBlobRepository: ISprintRepository
    {
        //TODO: zweck mock nur in memory --> daraus wird ein blob speicher in azure

        Dictionary<string, ISprint> _sprints = new Dictionary<string, ISprint>();

        public SprintBlobRepository(SprintBlobRepositoryConfig configuration)
        {
            
        }

        public ISprint Create(string[] userStories)
        {
            var sprintBlob = new SprintBlob(Guid.NewGuid().ToString(), userStories);
            _sprints.Add(sprintBlob.Id, sprintBlob);
            return sprintBlob;
        }

        public void Store(ISprint sprint)
        {
            if (_sprints.ContainsKey(sprint.Id))
            {
                _sprints[sprint.Id] = sprint;
                return;
            }

            _sprints.Add(sprint.Id,sprint);
           
        }

        public ISprint Load(string sprintId)
        {
            if (_sprints.ContainsKey(sprintId))
            {
                return _sprints[sprintId];
            }

            return null;
        }

        public void Delete(string sprintId)
        {
            _sprints.Remove(sprintId);
        }
    }
}
