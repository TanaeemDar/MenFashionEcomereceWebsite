using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Mens_Fashion.Models;

namespace Mens_Fashion.Controllers
{
    public class CustomersController : Controller
    {
        private Model3 db = new Model3();

        // GET: Customers
        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {

            return View() ;
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CUSTOMER_ID,CUSTOMER_NAME,CUSTOMER_EMAIL,CUSTOMER_PASSWORD,CUSTOMER_ADDRESS,CUSTOMER_CONTACT")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CUSTOMER_ID,CUSTOMER_NAME,CUSTOMER_EMAIL,CUSTOMER_PASSWORD,CUSTOMER_ADDRESS,CUSTOMER_CONTACT")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {   
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
         
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
            db.SaveChanges();
            Session["Customer"] = null;
            return RedirectToAction("IndexCustomer","Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Customer customer)
        {
            Customer c = db.Customers.Where(x => x.CUSTOMER_EMAIL == customer.CUSTOMER_EMAIL && x.CUSTOMER_PASSWORD == customer.CUSTOMER_PASSWORD).FirstOrDefault();
            if (c != null)
            {
                Session["Customer"] = c;
                return RedirectToAction("DisplayProduct","Home");

            }
            else
            {
                ViewBag.Message = "Invalid Email or Password";
                return View();
            }

        }
        public ActionResult Logout()
        {
            Session["Customer"] = null;
            return RedirectToAction("IndexCustomer", "Home");
        }
        public ActionResult CustomerHistory()
        {

            return View();

        }
        public ActionResult Invoice(int id)
        {
            var o = db.Orders.Where(x => x.ORDER_ID == id).ToList();
            return View(o);
        }
        
        public ActionResult OrderCancellation(int id)
        {
            Order o = db.Orders.Where(x => x.ORDER_ID == id).FirstOrDefault();
            o.STATUS = "Cancelled";
            db.Entry(o).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("CustomerHistory");
        }
        public ActionResult OrderActivate(int id)
        {
            Order o = db.Orders.Where(x => x.ORDER_ID == id).FirstOrDefault();
            o.STATUS = "Active";
            db.Entry(o).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("CustomerHistory");
        }
        public ActionResult CancelledOrder()
        {
            return View();
        }
    }
}
