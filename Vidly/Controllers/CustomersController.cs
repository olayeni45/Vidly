using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        //GET: /customers
        public ActionResult Index()
        {
            List<Customer> cst = _context.Customers
                .Include(c => c.MembershipType)
                .ToList();
            return View(cst);
        }

        //GET: /customers/details/{id}
        [Route("customers/details/{id}")]
        public ActionResult Details(int id)
        {
            Customer cst = _context.Customers
                .Include(c => c.MembershipType)
                .SingleOrDefault(c => c.Id == id);

            if (cst == null)
            {
                return HttpNotFound();
            }

            return View(cst);

        }

        //GET /customers/new
        public ActionResult New()
        {
            List<MembershipType> membershipTypes = _context
                .MembershipTypes
                .ToList();
            CustomerFormViewModel vm = new CustomerFormViewModel()
            {
                Customer = new Customer(),
                MembershipTypes = membershipTypes
            };
            return View("CustomerForm", vm);
        }

        //Create a new customer
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                CustomerFormViewModel model = new CustomerFormViewModel()
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };
                return View("CustomerForm", model);
            }
            if (customer.Id == 0)
            {
                _context.Customers.Add(customer);
            }
            else
            {
                Customer dbCustomer = _context.Customers.Single(c => c.Id == customer.Id);

                dbCustomer.Name = customer.Name;
                dbCustomer.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
                dbCustomer.Birthdate = customer.Birthdate;
                dbCustomer.MembershipTypeId = customer.MembershipTypeId;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Customers");
        }

        //Edit a customer
        public ActionResult Edit(int id)
        {
            Customer cst = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (cst == null)
                return HttpNotFound();

            CustomerFormViewModel vm = new CustomerFormViewModel()
            {
                Customer = cst,
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            return View("CustomerForm", vm);
        }

        /*
          
         //Customers data for data persistence
        static readonly List<Customer> customers = new List<Customer>()
            {
                new Customer{Id=1 ,Name="John Smith"},
                new Customer{Id=2, Name="Mary Williams"}
            };

        public ActionResult Details(int id)
        {
            Customer cst = GetCustomers().SingleOrDefault(c => c.Id == id);

            if (cst == null)
                return HttpNotFound();

            return View(cst);
        }

        private IEnumerable<Customer> GetCustomers()
        {
            return new List<Customer>()
            {
                new Customer{Id=1 ,Name="John Smith"},
                new Customer{Id=2, Name="Mary Williams"}
            };
        }
        */
    }
}