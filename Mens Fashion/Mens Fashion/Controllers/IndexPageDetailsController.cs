using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Mens_Fashion.Models
{
    public class IndexPageDetailsController : Controller
    {
        private Model3 db = new Model3();

        // GET: IndexPageDetails
        public ActionResult Index()
        {
            return View(db.IndexPageDetails.ToList());
        }

        // GET: IndexPageDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IndexPageDetail indexPageDetail = db.IndexPageDetails.Find(id);
            if (indexPageDetail == null)
            {
                return HttpNotFound();
            }
            return View(indexPageDetail);
        }

        // GET: IndexPageDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IndexPageDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IndexPageDetail indexPageDetail)
        {
            if (ModelState.IsValid)
            {
                indexPageDetail.S1_PIC.SaveAs(Server.MapPath("~/IndexPagePicture/" + indexPageDetail.S1_PIC.FileName));
                indexPageDetail.SIDER1_PIC = "~/IndexPagePicture/" + indexPageDetail.S1_PIC.FileName;

                indexPageDetail.S2_PIC.SaveAs(Server.MapPath("~/IndexPagePicture/" + indexPageDetail.S2_PIC.FileName));
                indexPageDetail.SIDER2_PIC = "~/IndexPagePicture/" + indexPageDetail.S2_PIC.FileName;

                indexPageDetail.B1_PIC.SaveAs(Server.MapPath("~/IndexPagePicture/" + indexPageDetail.B1_PIC.FileName));
                indexPageDetail.BANNER1_PIC = "~/IndexPagePicture/" + indexPageDetail.B1_PIC.FileName;

                indexPageDetail.B2_PIC.SaveAs(Server.MapPath("~/IndexPagePicture/" + indexPageDetail.B2_PIC.FileName));
                indexPageDetail.BANNER2_PIC = "~/IndexPagePicture/" + indexPageDetail.B2_PIC.FileName;

                indexPageDetail.B3_PIC.SaveAs(Server.MapPath("~/IndexPagePicture/" + indexPageDetail.B3_PIC.FileName));
                indexPageDetail.BANNER3_PIC = "~/IndexPagePicture/" + indexPageDetail.B3_PIC.FileName;


                db.IndexPageDetails.Add(indexPageDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(indexPageDetail);
        }

        // GET: IndexPageDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IndexPageDetail indexPageDetail = db.IndexPageDetails.Find(id);
            if (indexPageDetail == null)
            {
                return HttpNotFound();
            }
            return View(indexPageDetail);
        }

        // POST: IndexPageDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( IndexPageDetail indexPageDetail)
        {
            if (ModelState.IsValid)
            {
                if (indexPageDetail.S1_PIC != null)
                {
                    indexPageDetail.S1_PIC.SaveAs(Server.MapPath("~/IndexPagePicture/" + indexPageDetail.S1_PIC.FileName));
                    indexPageDetail.SIDER1_PIC = "~/IndexPagePicture/" + indexPageDetail.S1_PIC.FileName;
                }
                if (indexPageDetail.S2_PIC != null)
                {
                    indexPageDetail.S2_PIC.SaveAs(Server.MapPath("~/IndexPagePicture/" + indexPageDetail.S2_PIC.FileName));
                    indexPageDetail.SIDER2_PIC = "~/IndexPagePicture/" + indexPageDetail.S2_PIC.FileName;
                }
                if (indexPageDetail.B1_PIC != null)
                {
                    indexPageDetail.B1_PIC.SaveAs(Server.MapPath("~/IndexPagePicture/" + indexPageDetail.B1_PIC.FileName));
                    indexPageDetail.BANNER1_PIC = "~/IndexPagePicture/" + indexPageDetail.B1_PIC.FileName;
                }
                if (indexPageDetail.B2_PIC != null)
                {
                    indexPageDetail.B2_PIC.SaveAs(Server.MapPath("~/IndexPagePicture/" + indexPageDetail.B2_PIC.FileName));
                    indexPageDetail.BANNER2_PIC = "~/IndexPagePicture/" + indexPageDetail.B2_PIC.FileName;
                }
                if (indexPageDetail.B3_PIC != null)
                {
                    indexPageDetail.B3_PIC.SaveAs(Server.MapPath("~/IndexPagePicture/" + indexPageDetail.B3_PIC.FileName));
                    indexPageDetail.BANNER3_PIC = "~/IndexPagePicture/" + indexPageDetail.B3_PIC.FileName;
                }
                db.Entry(indexPageDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(indexPageDetail);
        }

        // GET: IndexPageDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IndexPageDetail indexPageDetail = db.IndexPageDetails.Find(id);
            if (indexPageDetail == null)
            {
                return HttpNotFound();
            }
            return View(indexPageDetail);
        }

        // POST: IndexPageDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IndexPageDetail indexPageDetail = db.IndexPageDetails.Find(id);
            db.IndexPageDetails.Remove(indexPageDetail);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
