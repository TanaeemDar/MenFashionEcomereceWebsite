using Mens_Fashion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Net.Mail;

namespace Mens_Fashion.Controllers
{
    

    public class PurchaseController : Controller
    {
        Model3 db = new Model3();
        

        public ActionResult Cart()
        {
            
            return View();
        }

        public ActionResult Purchase(int? id)
        {
            ShopModel s = new ShopModel();
            s.Cat = db.Categories.ToList();
            if (id == null)
            {
                s.Pro = db.Products.OrderByDescending(x => x.PRODUCT_ID).ToList();
            }
            else
            { s.Pro = db.Products.Where(p => p.CATEGORY_FID == id).OrderByDescending(x => x.PRODUCT_ID).ToList(); }
            return View(s);
        }

        
   

        public ActionResult AddToCart(int id)
        {
            List<Product> list ;
            if (Session["myCart"] == null)
            { list = new List<Product>(); }
            else
            {
                list = (List<Product>)Session["myCart"];
            }

                list.Add(db.Products.Where(p => p.PRODUCT_ID == id).FirstOrDefault());
            list[list.Count-1].PRO_QUANTITY = 1;
            Session["myCart"] = list;
            return RedirectToAction("Cart");
        }

        public ActionResult MinusFromCart(int RowNo)
        {
            List<Product> list = ( List<Product>)Session["myCart"];
             list[RowNo].PRO_QUANTITY -=2;
            Session["myCart"] = list;
            return RedirectToAction("Cart");
        }
        public ActionResult PlusToCart(int RowNo)
        {

            List<Product> list = (List<Product>)Session["myCart"];
            list[RowNo].PRO_QUANTITY +=3;
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
     
        public ActionResult PurchaseNow(Order o)
        {
            o.ORDER_DATE = System.DateTime.Now;
            o.ORDER_STATUS = "Paid";
            o.ORDER_TYPE = "Purchase";
            Session["Order"] = o;

            return RedirectToAction("OrderBooked");
        }
        public ActionResult OrderBooked()
        {
            Order o = (Order)Session["Order"];
           
            db.Orders.Add(o);
            db.SaveChanges();
            List<Product> p = (List<Product>)Session["myCart"];
            for (int i = 0 ; i<p.Count; i++)
            {
                OrderDetail od = new OrderDetail();
                int orderID = db.Orders.Max(x => x.ORDER_ID);
                od.ORDER_FID = orderID;
                od.PRODUCT_FID = p[i].PRODUCT_ID;
                od.QUANTITY = p[i].PRO_QUANTITY;
                od.PURCHASE_PRICE = p[i].PRODUCT_PURCHASEPRICE;
                od.SALE_PRICE = p[i].PRODUCT_SALEPRICE;
                
                db.OrderDetails.Add(od);
                db.SaveChanges();
            }

            return View();
        }


    }
}