namespace Harness
{
    using System;

    using Asos.CodeTest;

    public class RetrieveCustomerHarness
    {
        public static void ProveGetCustomer(string[] args)
        {
            /*
             * You MUST NOT change this code. This is an existing consumer of the service that must maintain 
             * backwards compatibility. 
             * 
             * Don't add any test code to this project.
             */

            var custService = new CustomerService();
            var customer = custService.GetCustomer(45, true).Result;

            Console.WriteLine(customer.Id + customer.Name);
        }
    }
}