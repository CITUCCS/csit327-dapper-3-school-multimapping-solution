using Dapper;
using DapperExer2.Context;
using DapperExer3.Models;

namespace DapperExer3.Repositories
{
    internal class SchoolRepository : ISchoolRepository
    {
        private readonly DapperContext _context;

        public SchoolRepository()
        {
            _context = new DapperContext("Data Source=JCA-PC;Initial Catalog=Exer3Db;Integrated Security=True;" +
                "Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
        public async Task<IEnumerable<School>> GetAllSchools()
        {
            var sql = "SELECT * FROM SCHOOL s "
                + "INNER JOIN COLLEGE c ON s.Id = c.SchoolId "
                + "INNER JOIN DEPARTMENT d ON c.Id = d.CollegeId;";

            var schoolMap = new Dictionary<int, School>();
            var collegeMap = new Dictionary<int, College>();
            
            using(var connection = _context.CreateConnection())
            {
                await connection.QueryAsync<School, College, Department, School>(
                    sql,
                    (school, college, department) => 
                    {
                        // Get/Add school in map
                        if (schoolMap.TryGetValue(school.Id, out School? storedSchool))
                        {
                            school = storedSchool;

                        } else
                        {
                            schoolMap.Add(school.Id, school);
                        }

                        // Get/Add college in map
                        if (collegeMap.TryGetValue(college.Id, out College? storedCollege))
                        {
                            college = storedCollege;

                        }
                        else
                        {
                            collegeMap.Add(college.Id, college);
                        }

                        college.Departments.Add(department);

                        if (!school.Colleges.Exists(c => c.Id == college.Id))
                        {
                            school.Colleges.Add(college);
                        }

                        return school;
                    });

                return schoolMap.Values;
            }
        }

        public async Task<School> GetSchool(int id)
        {
            var sql = "SELECT * FROM SCHOOL s "
                + "INNER JOIN COLLEGE c ON s.Id = c.SchoolId "
                + "INNER JOIN DEPARTMENT d ON c.Id = d.CollegeId "
                + "WHERE s.Id = @Id;";

            var collegeMap = new Dictionary<int, College>();

            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync<School, College, Department, School>(
                    sql,
                    (school, college, department) =>
                    {
                        // Get/Add college in map
                        if (collegeMap.TryGetValue(college.Id, out College? storedCollege))
                        {
                            college = storedCollege;

                        }
                        else
                        {
                            collegeMap.Add(college.Id, college);
                        }

                        college.Departments.Add(department);

                        if (!school.Colleges.Exists(c => c.Id == college.Id))
                        {
                            school.Colleges.Add(college);
                        }

                        return school;
                    }, new { id });
                return result.First();
            }
        }
    }
}
