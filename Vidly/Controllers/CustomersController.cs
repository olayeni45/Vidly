using System.Collections.Generic;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        //Customers data for data persistence
        static readonly List<Customer> customers = new List<Customer>()
            {
                new Customer{Id=1 ,Name="John Smith"},
                new Customer{Id=2, Name="Mary Williams"}
            };

        // GET: /customers
        public ActionResult Index()
        {
            CustomersViewModel viewModel = new CustomersViewModel()
            {
                Customers = customers
            };

            return View(viewModel);
        }

        //GET: /customers/details/{id}
        [Route("customers/details/{id}")]
        public ActionResult Details(int id)
        {
            Customer customer = new Customer();

            customer = customers.Find(cst => cst.Id == id);

            if (customer == null)
            {
                return HttpNotFound();
            }

            return View(customer);

        }
    }
}