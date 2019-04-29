using ModelsApp.Models;
using System.Collections.Generic;

namespace ModelsApp.Repository
{
    public interface FRepository<F> where F : BaseEntity
    {        
        IEnumerable<F> FindAll();

        IEnumerable<int> FindMax();
    }
}