using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VidlyApi.Dtos;
using VidlyApi.Models;

namespace VidlyApi.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext dbContext;

        public CustomersController()
        {
            dbContext = new ApplicationDbContext();
        }
        //Get /api/customer
        public IEnumerable<CustomerDto> GetCustomers()
        {
            return dbContext.Customers.ToList().Select(Mapper.Map<Customer, CustomerDto>);
        }

        //Get /api/customer/1

        public CustomerDto GetCustomer(int id)
        {
            var customer = dbContext.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return Mapper.Map<Customer , CustomerDto>(customer);
        }

        //Post /api/customer
        [HttpPost]
        public CustomerDto CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            dbContext.Customers.Add(customer);
            dbContext.SaveChanges();

            customerDto.Id = customer.Id;
            return customerDto;
        }

        //Put /api/customer/1
        [HttpPut]
        public void UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            var customerInDb = dbContext.Customers.SingleOrDefault(c => c.Id == id);
            if (customerInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            Mapper.Map(customerDto, customerInDb);

            //customerInDb.Name = customer.Name;
            //customerInDb.BirthDate = customer.BirthDate;
            //customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            //customerInDb.MemberShipTypeId = customer.MemberShipTypeId;

            dbContext.SaveChanges();
        }

        //Delete /api/customer/1
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            var customerInDb = dbContext.Customers.SingleOrDefault(c => c.Id == id);
            if (customerInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            dbContext.Customers.Remove(customerInDb);
            dbContext.SaveChanges();
        }
    }
}
