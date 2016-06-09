using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using PersonService.Models;

namespace PersonService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISaleService" in both code and config file together.
    [ServiceContract]
    public interface IPersonService
    {
        [OperationContract]
        IEnumerable<Person> Get();

        [OperationContract]
        Person GetByID(int id);

        [OperationContract]
        void Insert(Person person);

        [OperationContract]
        void Delete(int id);

        [OperationContract]
        void Edit(Person person);
    }
}
