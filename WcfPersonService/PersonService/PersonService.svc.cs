using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using PersonService.Models;
using System.Xml.Linq;
using System.Web;

namespace PersonService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SaleService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SaleService.svc or SaleService.svc.cs at the Solution Explorer and start debugging.
    public class PersonService : IPersonService
    {
        private List<Person> allPersons;
        private XDocument personData;

        // constructor
        public PersonService()
        {
            allPersons = new List<Person>();

            personData = XDocument.Load(HttpContext.Current.Server.MapPath("~/App_Data/Persons.xml"));
            var persons = from person in personData.Descendants("item")
                           select new Person((int)person.Element("id"), person.Element("name").Value, person.Element("lastName").Value);
            allPersons.AddRange(persons.ToList<Person>());
        }

        // returna list of all persons
        public IEnumerable<Person> Get()
        {
            return allPersons;
        }

        public Person GetByID(int id)
        {
            return allPersons.Find(item => item.ID == id);
        }

        // Insert Record
        public void Insert(Person person)
        {
            person.ID = (int)(from b in personData.Descendants("item") orderby (int)b.Element("id") descending select (int)b.Element("id")).FirstOrDefault() + 1;

            personData.Root.Add(new XElement("item", new XElement("id", person.ID), new XElement("name", person.Name), new XElement("lastName", person.LastName)));

            personData.Save(HttpContext.Current.Server.MapPath("~/App_Data/Persons.xml"));
        }

        // Delete Record
        public void Delete(int id)
        {
            personData.Root.Elements("item").Where(i => (int)i.Element("id") == id).Remove();

            personData.Save(HttpContext.Current.Server.MapPath("~/App_Data/Persons.xml"));
        }

        // Edit Record
        public void Edit(Person person)
        {
            XElement node = personData.Root.Elements("item").Where(i => (int)i.Element("id") == person.ID).FirstOrDefault();

            node.SetElementValue("name", person.Name);
            node.SetElementValue("lastName", person.LastName);

            personData.Save(HttpContext.Current.Server.MapPath("~/App_Data/Persons.xml"));
        }
    }
}
