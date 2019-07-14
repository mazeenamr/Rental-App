using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using vidly.Dtos;
using vidly.Models;

namespace vidly.Controllers.api
{

    public class CustomersController : ApiController
    {
        private ApplicationDbContext _Context;

        public CustomersController() 
        {
            _Context = new ApplicationDbContext();
        }
        public IEnumerable<customerDtos> GetCustomers()
        {
            return _Context.Customers
                .Include(c => c.MemberShipType)
                .ToList()
                .Select(Mapper.Map<Customer,customerDtos>);
        }

        public IHttpActionResult GetCustomer(int id)
        {
            var customer = _Context.Customers.SingleOrDefault(c => c.id == id);
            if (customer == null)
                return NotFound();
            return Ok(Mapper.Map<Customer, customerDtos>(customer));
        }
        [HttpPost]
        public IHttpActionResult CreateCustomer(customerDtos customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var customer = Mapper.Map<customerDtos, Customer>(customerDto);
            _Context.Customers.Add(customer);
            _Context.SaveChanges();

            customerDto.id = customer.id;
            return Created(new Uri(Request.RequestUri +"/"+customer.id),customerDto );

        }
        [HttpPut]
        public void UpdateCustomer(int id, customerDtos customerDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var customerInDb = _Context.Customers.SingleOrDefault(c => c.id == id);
            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(customerDto, customerInDb);
            
            _Context.SaveChanges();

        }
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            var customerInDb = _Context.Customers.SingleOrDefault(c => c.id == id);
            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            _Context.Customers.Remove(customerInDb);
            _Context.SaveChanges();

        }
    }
}
