using Mens_Fashion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Net.Mail;

namespace Mens_Fashion.Controllers
{


    public class HomeController : Controller
    {
        Model3 db = new Model3();
        public ActionResult IndexCustomer(int? id)
        {
            ShopModel s = new ShopModel();

            s.Pro = db.Products.OrderByDescending(x => x.PRODUCT_ID).Take(8).ToList(); 
            return View(s);
        }
        public ActionResult IndexAdmin()
        {
            return View();
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Admin admin)
        {
            Admin a = db.Admins.Where(x => x.ADMIN_EMAIL == admin.ADMIN_EMAIL && x.ADMIN_PASSWORD == admin.ADMIN_PASSWORD).FirstOrDefault();
            if (a != null)
            {
                Session["Admin"] = a;
                return RedirectToAction("IndexAdmin", "Home");
            }
            else
            {
                ViewBag.Message = "Invalid Email or Password";
                return View();
            }
        }
        public ActionResult Logout()
        {
            Session["Admin"] = null;
            return RedirectToAction("Login");
        }

        public ActionResult Cart()
        {

            return View();
        }

        public ActionResult DisplayProduct(int? id , string SortOrder )
        {

            ShopModel s = new ShopModel();
            s.Cat = db.Categories.ToList();
            if (id == null)
            {
                s.Pro = db.Products.OrderByDescending(x => x.PRODUCT_ID).ToList();

            }
            else
            {
                s.Pro = db.Products.Where(p => p.CATEGORY_FID == id).OrderByDescending(x => x.PRODUCT_ID).ToList();
            }
            return View(s);

            
            //switch (SortOrder)
            //{
            //    case "Asc":
            //        {
            //            s.Pro = db.Products.OrderBy(x => x.PRODUCT_SALEPRICE).ToList();
            //            break;
            //        }
            //    case "Dsc":
            //        {
            //            s.Pro = db.Products.OrderByDescending(x => x.PRODUCT_SALEPRICE).ToList();
            //            break;
            //        }
            //    case "Def":
            //        {
            //            s.Pro = db.Products.OrderBy(x => x.PRODUCT_SALEPRICE).ToList();
            //            break;
            //        }
            //    default:
            //        {
            //            s.Pro = db.Products.OrderByDescending(x => x.PRODUCT_ID).ToList();
            //            break;
            //        }
            //}




            //ShopModel s = new ShopModel();
            //s.Cat = db.Categories.ToList();
            //if (id == null)
            //{

            //    if(SortOrder=="Asc")
            //    {
            //        s.Pro = db.Products.OrderBy(x => x.PRODUCT_SALEPRICE).Take(12).ToList();
            //    }
            //    else if (SortOrder == "Dsc")
            //    {
            //        s.Pro = db.Products.OrderByDescending(x => x.PRODUCT_SALEPRICE).Take(12).ToList();
            //    }
            //    else
            //    {
            //        s.Pro = db.Products.OrderByDescending(x => x.PRODUCT_ID).Take(12).ToList();
            //    }
            //}
            //else
            //{
            //    s.Pro = db.Products.Where(p => p.CATEGORY_FID == id).OrderByDescending(x => x.PRODUCT_ID).Take(12).ToList();
            //}
            //return View(s);








        }


        public ActionResult Feedback()
        {
            var p = db.ContactUs.OrderByDescending(x => x.FEEDBACK_ID).ToList();
            return View(p);
        }


        public ActionResult SignIn()
        {

            return View();
        }

        public ActionResult AddToCart(int id)
        {
            List<Product> list;
            if (Session["myCart"] == null)
            { list = new List<Product>(); }
            else
            {
                list = (List<Product>)Session["myCart"];
            }

            Boolean isProductExist = false;
            foreach (var item in list)
            {
                if (id == item.PRODUCT_ID)
                {
                    isProductExist = true;
                    item.PRO_QUANTITY++;
                }
            }
            if (isProductExist == false)
            {
                list.Add(db.Products.Where(p => p.PRODUCT_ID == id).FirstOrDefault());
                list[list.Count - 1].PRO_QUANTITY = 1;
            }

            Session["myCart"] = list;
            return RedirectToAction("Cart");
        }

        public ActionResult MinusFromCart(int RowNo)
        {
            List<Product> list = (List<Product>)Session["myCart"];
            list[RowNo].PRO_QUANTITY--;
            if (list[RowNo].PRO_QUANTITY == 0)
                list.RemoveAt(RowNo);
            Session["myCart"] = list;
            return RedirectToAction("Cart");
        }
        public ActionResult PlusToCart(int RowNo)
        {

            List<Product> list = (List<Product>)Session["myCart"];
            int P_ID = list[RowNo].PRODUCT_ID;
            int? available = db.OrderDetails.Where(x => x.PRODUCT_FID == P_ID).Sum(x => x.QUANTITY);
            if (available > list[RowNo].PRO_QUANTITY)
                list[RowNo].PRO_QUANTITY++;
            Session["myCart"] = list;
            return RedirectToAction("Cart");
        }
        public ActionResult RemoveFromCart(int RowNo)
        {

            List<Product> list = (List<Product>)Session["myCart"];
            list.RemoveAt(RowNo);
            Session["myCart"] = list;
            return RedirectToAction("Cart");
        }
        public ActionResult Checkout()
        {

            return View();
        }
        public ActionResult PayNow(Order o)
        {
            o.ORDER_DATE = System.DateTime.Now;

            o.ORDER_TYPE = "Sale";
            o.STATUS = "Active";
            if(Session["Customer"]!=null)
            {
                Customer c = (Customer)Session["Customer"];
                o.CUSTOMER_FID = c.CUSTOMER_ID;

            }
            Session["Order"] = o;
            if (o.ORDER_STATUS == "Cash on Delivery")
            {
                return RedirectToAction("OrderBooked");

            }
            else
            {
                return RedirectToAction("OrderBooked");

                //return Redirect("https://www.paypal.com/cgi-bin/webscr?cmd=_xclick&business=bcsf17m83b@gmail.com&item_name=Mens_fashionProducts&return=http://localhost:60694/Home/OrderBooked&amount=" + double.Parse(Session["totalAmount"].ToString()));

            }
        }

        public ActionResult FBack(ContactU contactU)
        {


            if (Session["Customer"] != null)
            {
                Customer c = (Customer)Session["Customer"];
                contactU.CUSTOMER_FID = c.CUSTOMER_ID;

            }

            // ContactU f = (ContactU)Session["ContactU"];

            //ConatctUTableAdapter.Insert(fb.FEEDBACK_NAME,fb.FEEDBACK_EMAIL,fb.FEEDBACK_DETAIL);
            //db.ContactUs.Add(contactU);
            //  db.SaveChanges();
            Session["contact"] = contactU;
            return RedirectToAction("FB");
        }

        public ActionResult FB()
        {
            ContactU con = (ContactU)Session["contact"];
            db.ContactUs.Add(con);
            db.SaveChanges();
            return RedirectToAction("Contact");
        }
        public ActionResult OrderBooked()
        {
            Order o = (Order)Session["Order"];
            //Email Alert
            //MailMessage mail = new MailMessage();

            //mail.From = new MailAddress("bcsf17m83b@gmail.com");
            //mail.To.Add(o.ORDER_EMIAL);
            //mail.Subject = "Order Confirmation";
            //mail.Body = "<b>The Mens fashion!</b></br> Thanks for choosing us. We will deliver your order as per company Terms and Conditions.";
            //mail.IsBodyHtml = true;
            //SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            //SmtpServer.Port = 587;
            //SmtpServer.Credentials = new System.Net.NetworkCredential("bcsf17m83b@gmail.com", "123STAR123");
            //SmtpServer.EnableSsl = true;
            //SmtpServer.Send(mail);

            //Phone Alert not Added.video 8
            //db data save

            db.Orders.Add(o);
            db.SaveChanges();
            List<Product> p = (List<Product>)Session["myCart"];
            for (int i = 0; i < p.Count; i++)
            {
                OrderDetail od = new OrderDetail();
                int orderID = db.Orders.Max(x => x.ORDER_ID);
                od.ORDER_FID = orderID;
                od.PRODUCT_FID = p[i].PRODUCT_ID;
                od.QUANTITY = p[i].PRO_QUANTITY * -1;
                od.PURCHASE_PRICE = p[i].PRODUCT_PURCHASEPRICE;
                od.SALE_PRICE = p[i].PRODUCT_SALEPRICE;

                db.OrderDetails.Add(od);
                db.SaveChanges();
            }

            return View();
        }
        public ActionResult CloseOrder()
        {
            Session["Order"] = null;
            Session["myCart"] = null;
            return RedirectToAction("IndexCustomer");

        }
        public ActionResult PaymentMethode()
        {
            return View();
        }
        public ActionResult Return()
        {
            return View();
        }
        public ActionResult Delivery()
        {
            return View();
        }


    }
}