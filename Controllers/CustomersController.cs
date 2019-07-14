using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using vidly.Models;
using vidly.ViewModel;
using System.Data.Entity;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _Context;

        public CustomersController() 
        {
            _Context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _Context.Dispose();
        }

        public ViewResult Index()
        {
            return View();
        }
        public ActionResult newCustomer()
        {
            var MemberShipType = _Context.MemberShipType;
            var ViewModel = new NewCustomerViewModel
            {
                Customer = new Customer(),
                MemberShipType = MemberShipType
            };
            return View("newCustomer",ViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new NewCustomerViewModel
                {
                    Customer = customer,
                    MemberShipType = _Context.MemberShipType.ToList()
                };
                return View("newCustomer", viewModel);


            }
            if(customer.id == 0)
            _Context.Customers.Add(customer);
            else
            {
                var customerInDb = _Context.Customers.Single(c => c.id == customer.id);

                customerInDb.name = customer.name;
                customerInDb.birthdayDate = customer.birthdayDate;
                customerInDb.MemberShipTypeId = customer.MemberShipTypeId;
                customerInDb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
            }
            _Context.SaveChanges();
            return RedirectToAction("Index", ("Customers"));
        }
        public ActionResult Details(int id)
        {
            var customer = _Context.Customers.Include(c => c.MemberShipType).SingleOrDefault(c => c.id == id);

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }
        public ActionResult Edit(int id)
        {
            var customer = _Context.Customers.SingleOrDefault(c => c.id == id);
            var ViewModel = new NewCustomerViewModel
            {
                Customer = customer,
                MemberShipType = _Context.MemberShipType.ToList()
            };
            return View("newCustomer" , ViewModel);
        }

    }
}