using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MasterCRUDOperation;
using MasterCRUDOperation.Models;
using PagedList;

namespace MasterCRUDOperation.Controllers
{

    public class HomeController : Controller
    {
        ProductContext db = new ProductContext();   

        // GET: Home
        public ActionResult Index(String sortOrder, string SortBy,int PageNumber = 1)
        {
            ViewBag.SortOrder = sortOrder;
            ViewBag.SortBy = SortBy;    
            var data = db.Products.ToList();

            ViewBag.TotalPages = Math.Ceiling(data.Count() / 10.0);
            ViewBag.PageNumber = PageNumber;
            //data = data.Take(10).ToList();
           data = data.Skip((PageNumber -1 ) *10).Take(10).ToList();
            return View(data);
        }
        public ActionResult Create()
        {
           return View();
        }
        [HttpPost]
        public ActionResult Create(Product s)
        {
            if (ModelState.IsValid == true)
            {
                db.Products.Add(s);
                int a = db.SaveChanges();
                if (a > 0)
                {
                    //ViewBag.InsertMessage = "<script>alert('Data Inserted !!')</script>";
                    TempData["InsertMessage"] = "<script>alert('Data Inserted !!')</script>";
                    return RedirectToAction("Index");
                    //ModelState.Clear();
                }
                else
                {
                    ViewBag.InsertMessage = "<script>alert('Data Not Inserted !')</script>";

                }
            }
                return View();
            }
        public ActionResult Edit (int id)
        {
            var row = db.Products.Where(model => model.ProductId== id) .FirstOrDefault();
            return View(row);
        }
        [HttpPost]
        public ActionResult Edit(Product s)
        {
            if(ModelState.IsValid == true)
            {
                db.Entry(s).State = System.Data.Entity.EntityState.Modified;
                int a = db.SaveChanges();
                if (a > 0)
                {
                    // ViewBag.UpdateMessage = "<script>alert('Data Updated Successfully !!')</script>";
                    TempData["UpdateMessage"] = "<script>alert('Data Updated Successfully !!')</script>";
                    return RedirectToAction("Index");
                    //ModelState.Clear();
                }
                else
                {
                   ViewBag.UpdateMessage = "<script>alert('Data Not Updated !!')</script>";
                }
            }
            return View();
        }
        public ActionResult Delete(int id)
        {
            if(id>0)
            {
                var ProductIdRow = db.Products.Where(model => model.ProductId == id).FirstOrDefault();
                if(ProductIdRow != null)
                {
                    db.Entry((Product)ProductIdRow).State = System.Data.Entity.EntityState.Deleted;
                    int a= db.SaveChanges();
                    if (a>0)
                    {
                        TempData["DeleteMessage"] ="<script>alert('Data Deleted Successfully !!')</script>";
                    }
                    else
                    {
                        TempData["UpdateMessage"] = "<script>alert('Data Not Deleted')</script>";
                    }
                }
            }  
            return RedirectToAction("Index");   
            
        }
    }
}