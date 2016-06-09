using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PersonService.Models
{
    public class Person
    {
        public Person()
        {
            this.ID = 0;
            this.Name = null;
            this.LastName = null;
        }

        public Person(int id, string name, string lastName)
        {
            this.ID = id;
            this.Name = name;
            this.LastName = lastName;
        }

        public int ID { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "LastName is required")]
        public string LastName { get; set; }
    }
}