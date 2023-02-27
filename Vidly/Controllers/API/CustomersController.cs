using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using Vidly.DTOs;
using Vidly.Models;

namespace Vidly.Controllers.API
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        //GET /api/customers
        public IEnumerable<CustomerDTO> GetCustomers()
        {
            return _context.Customers
                .ToList()
                .Select(Mapper.Map<Customer, CustomerDTO>);
        }

        //GET /api/customers/{id}
        public CustomerDTO GetCustomer(int id)
        {
            Customer cst = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (cst == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return Mapper.Map<Customer, CustomerDTO>(cst);
        }

        //POST /api/customers
        [HttpPost]
        public CustomerDTO CreateCustomer(CustomerDTO cstDTO)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            Customer cst = Mapper.Map<CustomerDTO, Customer>(cstDTO);
            _context.Customers.Add(cst);
            _context.SaveChanges();

            cstDTO.Id = cst.Id;

            return cstDTO;
        }

        //PUT /api/customers/{id}
        [HttpPut]
        public void UpdateCustomer(int id, CustomerDTO cstDTO)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            Customer dbCustomer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (dbCustomer == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(cstDTO, dbCustomer);

            _context.SaveChanges();
        }

        //DELETE /api/customers/{id}
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            Customer dbCustomer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (dbCustomer == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Customers.Remove(dbCustomer);
            _context.SaveChanges();
        }
    }
}
