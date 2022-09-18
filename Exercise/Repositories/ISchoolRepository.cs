using DapperExer3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperExer3.Repositories
{
    /// <summary>
    /// Interface for school repository
    /// </summary>
    internal interface ISchoolRepository
    {
        Task<School> GetSchool(int id);
        Task<IEnumerable<School>> GetAllSchools();
    }
}
