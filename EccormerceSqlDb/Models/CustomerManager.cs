using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EccormerceSqlDb.Models
{
    public class CustomerManager
    {
        EcommerceDbEntities db = new EcommerceDbEntities();

        public IList<Customer>GetCustomersInCity(string city)
        {
            var result = db.Customers.Where(c => c.AddressCountry==city).ToList();
            return result;
        }
    }
}