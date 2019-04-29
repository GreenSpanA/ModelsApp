using ModelsApp.Models;
using System.Collections.Generic;

namespace ModelsApp.Repository
{
    public interface FRepository<F> 
    {        
        IEnumerable<F> FindAll();
        IEnumerable<int> FindMax();
        void Update(int id);
    }
}