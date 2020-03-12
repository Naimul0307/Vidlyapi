 using AutoMapper;
using DocumentFormat.OpenXml.Office2010.Excel;
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
        private ApplicationDbContext db;
        public CustomersController()
        {
            db = new ApplicationDbContext();
        }
        //gets /api/customers
        public IEnumerable<CustomerDto> GetCustomers()
        {
            return db.Customers.ToList().Select(Mapper.Map<Customer,CustomerDto>);
        }

        //gets /api/customers/1
         public CustomerDto GetCustomer(int id)
        {
            var customer = db.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return Mapper.Map<Customer,CustomerDto>(customer);
        }
        
        //post /api/customers
        [HttpPost]
        public CustomerDto CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            db.Customers.Add(customer);
            db.SaveChanges();
            customerDto.Id = customer.Id;

            return customerDto;

        }

        //put /api/customers/1
        [HttpPut]
        public void UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            var customerInDb = db.Customers.SingleOrDefault(c => c.Id == id);

            if(customerInDb==null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            Mapper.Map(customerDto, customerInDb);
            customerInDb.Name = customerDto.Name;
            customerInDb.BirthDate = customerDto.BirthDate;
            customerInDb.IsSubscribedToNewsletter = customerDto.IsSubscribedToNewsletter;
            customerInDb.MemberShipTypeId = customerDto.MemberShipTypeId;
            db.SaveChanges();
        }

        //Delete /api/customer/1
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            var customrInDb = db.Customers.SingleOrDefault(c => c.Id == id);

            if (customrInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            db.Customers.Remove(customrInDb);
            db.SaveChanges();


        }
    }
}
