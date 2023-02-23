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
            NewCustomerViewModel vm = new NewCustomerViewModel()
            {
                MembershipTypes = membershipTypes
            };
            return View(vm);
        }

        //Create a new customer
        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return RedirectToAction("Index", "Customers");
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