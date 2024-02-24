using LuftBornTest.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuftBornTest.Core.Interfaces
{
    public interface IRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync();

        Task<Employee> GetByIdAsync(int id);

        Task<Employee> AddAsync(Employee entity);

        Task<Employee> UpdateAsync(Employee entity);

        Task<bool> DeleteAsync(int id);
    }
}