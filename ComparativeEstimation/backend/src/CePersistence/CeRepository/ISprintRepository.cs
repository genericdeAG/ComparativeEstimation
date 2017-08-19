using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CeRepository
{
    public interface ISprintRepository
    {
        ISprint Create(string[] userStories);
        void Store(ISprint sprint);

        ISprint Load(string sprintId);

        void Delete(string sprintId);
    }
}
