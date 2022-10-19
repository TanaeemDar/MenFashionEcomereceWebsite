using Mens_Fashion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mens_Fashion.Controllers
{
    public class ReportsController : Controller
    {
        // GET: Rports
        Model3 db = new Model3();
        public ActionResult PurchaseReport(FilterModel fm)
        {
            if (fm.DateFrom == null)
            {
                ViewBag.DateFrom = System.DateTime.Today.ToString();
                fm.DateFrom = System.DateTime.Today;
            }
            else
            {
                ViewBag.DateFrom = Convert.ToDateTime(fm.DateFrom);
            }
            if (fm.DateTo == null)
            {
                ViewBag.DateTo = System.DateTime.Now.ToString();
                fm.DateTo = System.DateTime.Now;
            }
            else
            {
                ViewBag.DateTo = Convert.ToDateTime(fm.DateTo);
            }

            ViewBag.Category = db.Categories.Select(x => new SelectListItem { Value = x.CATEGORY_ID.ToString(), Text = x.CATEGORY_NAME }).ToList();
            if (fm.Category == null)
            {
                ViewBag.Product = db.Products.Select(x => new SelectListItem { Value = x.PRODUCT_ID.ToString(), Text = x.PRODUCT_NAME + "  ( " + x.PRODUCT_DESCRIPTION + " )" }).ToList();
            }
            else
            {
                ViewBag.Product = db.Products.Where(x => x.CATEGORY_FID == fm.Category).Select(x => new SelectListItem { Value = x.PRODUCT_ID.ToString(), Text = x.PRODUCT_NAME + "  ( " + x.PRODUCT_DESCRIPTION + " )" }).ToList();

            }
            var od = db.OrderDetails.Select(x => x.ORDER_FID).ToList();

            if (fm.Category != null)
            {
                var p = db.Products.Where(x => x.CATEGORY_FID == fm.Category).Select(x => x.PRODUCT_ID).ToList();
                if (fm.Product != null)
                {
                    p = db.Products.Where(x => x.PRODUCT_ID == fm.Product).Select(x => x.PRODUCT_ID).ToList();

                }
                od = db.OrderDetails.Where(x => p.Contains(x.PRODUCT_FID)).Select(x => x.ORDER_FID).ToList();
            }

            var o = db.Orders.Where(x => x.ORDER_TYPE == "Purchase" & x.ORDER_DATE >= fm.DateFrom & x.ORDER_DATE <= fm.DateTo & od.Contains(x.ORDER_ID)).OrderByDescending(x => x.ORDER_ID).ToList();
            return View(o);
        }

        public ActionResult SaleReport(FilterModel fm)

        {
            if (fm.DateFrom == null)
            {
                ViewBag.DateFrom = System.DateTime.Today.ToString();
                fm.DateFrom = System.DateTime.Today;
            }
            else
            {
                ViewBag.DateFrom = Convert.ToDateTime(fm.DateFrom);
            }
            if (fm.DateTo == null)
            {
                ViewBag.DateTo = System.DateTime.Now.ToString();
                fm.DateTo = System.DateTime.Now;
            }
            else
            {
                ViewBag.DateTo = Convert.ToDateTime(fm.DateTo);
            }

            ViewBag.Category = db.Categories.Select(x => new SelectListItem { Value = x.CATEGORY_ID.ToString(), Text = x.CATEGORY_NAME }).ToList();
            if (fm.Category == null)
            {
                ViewBag.Product = db.Products.Select(x => new SelectListItem { Value = x.PRODUCT_ID.ToString(), Text = x.PRODUCT_NAME + "  ( " + x.PRODUCT_DESCRIPTION + " )" }).ToList();
            }
            else
            {
                ViewBag.Product = db.Products.Where(x => x.CATEGORY_FID == fm.Category).Select(x => new SelectListItem { Value = x.PRODUCT_ID.ToString(), Text = x.PRODUCT_NAME + "  ( " + x.PRODUCT_DESCRIPTION + " )" }).ToList();

            }
            var od = db.OrderDetails.Select(x => x.ORDER_FID).ToList();

            if (fm.Category != null)
            {
                var p = db.Products.Where(x => x.CATEGORY_FID == fm.Category).Select(x => x.PRODUCT_ID).ToList();
                if(fm.Product!=null)
                {
                    p = db.Products.Where(x => x.PRODUCT_ID == fm.Product).Select(x => x.PRODUCT_ID).ToList();

                }
                od = db.OrderDetails.Where(x => p.Contains(x.PRODUCT_FID)).Select(x => x.ORDER_FID).ToList();
            }

            var o = db.Orders.Where(x => x.ORDER_TYPE == "Sale" & x.ORDER_DATE >= fm.DateFrom & x.ORDER_DATE <= fm.DateTo & od.Contains(x.ORDER_ID)).OrderByDescending(x => x.ORDER_ID).ToList();
            return View(o);
        }
        public ActionResult Invoice(int id)
        {
            var o = db.Orders.Where(x => x.ORDER_ID == id).ToList();
            return View(o);
        }
        public ActionResult ProfitandLossReport(FilterModel fm)
        {
            if (fm.DateFrom == null)
            {
                ViewBag.DateFrom = System.DateTime.Today.ToString();
                fm.DateFrom = System.DateTime.Today;
            }
            else
            {
                ViewBag.DateFrom = Convert.ToDateTime(fm.DateFrom);
            }
            if (fm.DateTo == null)
            {
                ViewBag.DateTo = System.DateTime.Now.ToString();
                fm.DateTo = System.DateTime.Now;
            }
            else
            {
                ViewBag.DateTo = Convert.ToDateTime(fm.DateTo);
            }

            ViewBag.Category = db.Categories.Select(x => new SelectListItem { Value = x.CATEGORY_ID.ToString(), Text = x.CATEGORY_NAME }).ToList();
            if (fm.Category == null)
            {
                ViewBag.Product = db.Products.Select(x => new SelectListItem { Value = x.PRODUCT_ID.ToString(), Text = x.PRODUCT_NAME + "  ( " + x.PRODUCT_DESCRIPTION + " )" }).ToList();
            }
            else
            {
                ViewBag.Product = db.Products.Where(x => x.CATEGORY_FID == fm.Category).Select(x => new SelectListItem { Value = x.PRODUCT_ID.ToString(), Text = x.PRODUCT_NAME + "  ( " + x.PRODUCT_DESCRIPTION + " )" }).ToList();

            }
            var od = db.OrderDetails.Select(x => x.ORDER_FID).ToList();

            if (fm.Category != null)
            {
                var p = db.Products.Where(x => x.CATEGORY_FID == fm.Category).Select(x => x.PRODUCT_ID).ToList();
                if (fm.Product != null)
                {
                    p = db.Products.Where(x => x.PRODUCT_ID == fm.Product).Select(x => x.PRODUCT_ID).ToList();

                }
                od = db.OrderDetails.Where(x => p.Contains(x.PRODUCT_FID)).Select(x => x.ORDER_FID).ToList();
            }

            var o = db.Orders.Where(x => x.ORDER_TYPE == "Sale" & x.STATUS == "Active" & x.ORDER_DATE >= fm.DateFrom & x.ORDER_DATE <= fm.DateTo & od.Contains(x.ORDER_ID)).OrderByDescending(x => x.ORDER_ID).ToList();
            return View(o);
        }
        public ActionResult StockReport(FilterModel fm)
        {

            
            if (fm.DateTo == null)
            {
                ViewBag.DateTo = System.DateTime.Now.ToString();
                fm.DateTo = System.DateTime.Now;
            }
            else
            {
                ViewBag.DateTo = Convert.ToDateTime(fm.DateTo);
            }

            ViewBag.Category = db.Categories.Select(x => new SelectListItem { Value = x.CATEGORY_ID.ToString(), Text = x.CATEGORY_NAME }).ToList();
            if (fm.Category == null)
            {
                ViewBag.Product = db.Products.Select(x => new SelectListItem { Value = x.PRODUCT_ID.ToString(), Text = x.PRODUCT_NAME + "  ( " + x.PRODUCT_DESCRIPTION + " )" }).ToList();
            }
            else
            {
                ViewBag.Product = db.Products.Where(x => x.CATEGORY_FID == fm.Category).Select(x => new SelectListItem { Value = x.PRODUCT_ID.ToString(), Text = x.PRODUCT_NAME + "  ( " + x.PRODUCT_DESCRIPTION + " )" }).ToList();

            }

            var p = db.Products.ToList();

            if (fm.Category != null)
            {
                 p = db.Products.Where(x => x.CATEGORY_FID == fm.Category).ToList();
                if (fm.Product != null)
                {
                    p = db.Products.Where(x => x.PRODUCT_ID == fm.Product).ToList();

                }
            }

           
            return View(p);
        }
    }
}