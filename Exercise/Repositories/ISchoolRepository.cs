using DapperExer2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperExer2.Repositories
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
