using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace task.Models
{
    public class Person
    {
        public int id { get; set; }
        
        [StringLength(250)]
        public string userName { get; set; }
        public string firstName { get; set; }
        public string fatherName { get; set; }
        public string familyName { get; set; }
        public string password { get; set; }
        public string address { get; set; }
        public string birthdate { get; set; }
        

    }

}