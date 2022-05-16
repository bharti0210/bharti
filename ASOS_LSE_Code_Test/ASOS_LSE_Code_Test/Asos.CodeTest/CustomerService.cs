using System;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Asos.CodeTest
{
    using System.Threading.Tasks;

    public class CustomerService
    {
        public async Task<Customer> GetCustomer(int customerId, bool isCustomerArchived)
        {

            Customer archivedCustomer = null;

            if (isCustomerArchived)
            {
                var archivedDataService = new ArchivedDataService();
                archivedCustomer = archivedDataService.GetArchivedCustomer(customerId);

                return archivedCustomer;
            }
            else
            {

                var failoverRespository = new FailoverRepository();
                var failoverEntries = failoverRespository.GetFailOverEntries();


                var failedRequests = 0;

                foreach (var failoverEntry in failoverEntries)
                {
                    if (failoverEntry.DateTime > DateTime.Now.AddMinutes(-10))
                    {
                        failedRequests++;
                    }
                }

                CustomerResponse customerResponse = null;
                Customer customer = null;

                if (failedRequests > 100 && (ConfigurationManager.AppSettings["IsFailoverModeEnabled"] == "true" || ConfigurationManager.AppSettings["IsFailoverModeEnabled"] == "True"))
                {
                    customerResponse = await FailoverCustomerDataAccess.GetCustomerById(customerId);
                }
                else
                {
                    var dataAccess = new CustomerDataAccess();
                    customerResponse = await dataAccess.LoadCustomerAsync(customerId);                    
                }

                if (customerResponse.IsArchived)
                {
                    var archivedDataService = new ArchivedDataService();
                    customer = archivedDataService.GetArchivedCustomer(customerId);
                }
                else
                {
                    customer = customerResponse.Customer;
                }


                return customer;
            }
        }
    }
}
