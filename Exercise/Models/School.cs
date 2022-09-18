using Newtonsoft.Json;

namespace DapperExer3.Models
{
    internal class School
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Motto { get; set; }
        public double AverageTuition { get; set; }
        public List<College> Colleges { get; set; } = new List<College>();
        /// <summary>
        /// Prints this object as JSON
        /// </summary>
        /// <returns>json string representation of this object</returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
