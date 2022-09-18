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
            
            using(var connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync<School, College, Department, School>(
                    sql,
                    (school, college, department) => 
                    {
                        college.Departments.Add(department);
                        school.Colleges.Add(college);
                        return school;
                    });

                return result.GroupBy(s => s.Id).Select(schoolGroup => 
                {
                    var firstSchool = schoolGroup.First();
                    var colleges = schoolGroup.SelectMany(school => school.Colleges);
                    firstSchool.Colleges = colleges.GroupBy(c => c.Id).Select(collegeGroup =>
                    {
                        var firstCollege = collegeGroup.First();
                        firstCollege.Departments = collegeGroup.SelectMany(college => college.Departments).ToList();

                        return firstCollege;
                    }).ToList();

                    return firstSchool;
                });
            }
        }

        public async Task<School> GetSchool(int id)
        {
            var sql = "SELECT * FROM SCHOOL s "
                + "INNER JOIN COLLEGE c ON s.Id = c.SchoolId "
                + "INNER JOIN DEPARTMENT d ON c.Id = d.CollegeId "
                + "WHERE s.Id = @Id;";

            using (var connection = _context.CreateConnection())
            {

                var result = await connection.QueryAsync<School, College, Department, School>(
                    sql,
                    (school, college, department) =>
                    {
                        college.Departments.Add(department);
                        school.Colleges.Add(college);
                        return school;
                    }, 
                    new { id });

                return result.GroupBy(s => s.Id).Select(schoolGroup =>
                {
                    var firstSchool = schoolGroup.First();
                    var colleges = schoolGroup.SelectMany(school => school.Colleges);
                    
                    firstSchool.Colleges = colleges.GroupBy(c => c.Id).Select(collegeGroup =>
                    {
                        var firstCollege = collegeGroup.First();
                        firstCollege.Departments = collegeGroup.SelectMany(college => college.Departments).ToList();

                        return firstCollege;
                    }).ToList();

                    return firstSchool;
                }).Single();
            }
        }
    }
}
