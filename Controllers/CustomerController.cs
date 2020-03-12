using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using VidlyApi.Models;
using VidlyApi.ViewModel;
using Eco.ViewModel.Runtime;
using XLabs.Forms.Mvvm;

namespace VidlyApi.Controllers
{
    public class CustomerController : Controller
    {
        private ApplicationDbContext dbcontext;
        public CustomerController()
        {
            dbcontext = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            dbcontext.Dispose();
        }

        public ActionResult New()
        {
            var MembershipTypes = dbcontext.MemberShipTypes.ToList();
            var ViewModel = new NewCustomerViewModel
            {
                MemberShipTypes = MembershipTypes
            };
            return View(ViewModel);
        }

        [HttpPost]
        public ActionResult Save(Customer customer)
        {
            if (customer.Id == 0)
            {
                dbcontext.Customers.Add(customer);
            }
            
            else
            {
                var customerInDb = dbcontext.Customers.Single(c => c.Id == customer.Id);
                customerInDb.Name = customer.Name;
                customerInDb.BirthDate = customer.BirthDate;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
                customerInDb.MemberShipTypeId = customer.MemberShipTypeId;
              
            }
            dbcontext.SaveChanges();
            return RedirectToAction("Index", "Customer");
        }
        // GET: Customer
        public ViewResult Index()
        {
            var customer = dbcontext.Customers.Include(c => c.MemberShipType).ToList();
            return View(customer);
        }

        //public ActionResult Details(int id)
        //{
        //    var customer = dbcontext.Customers.SingleOrDefault(c => c.Id == id);

        //    if (customer == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(customer);
        //}
        public ActionResult Edit(int id)
        {
            var customer = dbcontext.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
            {
                return HttpNotFound();
            }
            var ViewModel = new NewCustomerViewModel
            {
                customer = customer,
                MemberShipTypes = dbcontext.MemberShipTypes.ToList()
            };
            return View("New", ViewModel);
        }
    }
}