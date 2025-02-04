using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarGo.Model;

namespace CarGo.Service.Common
{
    public interface ILocationService
    {
        Task<List<Location>> GetAsync();

        Task<Location> GetByIdAsync(Guid id);

        Task<bool> PostAsync(Location entity, Guid id);
        //Task<bool> Put(Guid id);

        Task<bool> DeleteAsync(Guid id);
    }
}
