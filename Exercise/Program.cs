using DapperExer3.Repositories;

internal class Program
{
    private static async Task Main(string[] args)
    {
        ISchoolRepository repository = new SchoolRepository();

        Console.WriteLine(await repository.GetSchool(2));
        Console.WriteLine("---------------------------------------");
        foreach (var school in await repository.GetAllSchools())
        {
            Console.WriteLine(school);
        }
    }
}