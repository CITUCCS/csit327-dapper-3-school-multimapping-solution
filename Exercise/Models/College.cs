using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperExer3.Models
{
    internal class College
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Dean { get; set; }
        public string? Theme { get; set; }
        public List<Department> Departments { get; set; } = new List<Department>();

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
