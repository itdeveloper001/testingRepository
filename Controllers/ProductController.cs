using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThemeMVC.Models;

namespace ThemeMVC.Controllers
{
    public class ProductController : Controller
    {

        dbShoppingEntities db = new dbShoppingEntities();
        public ActionResult AddProduct()
        {
            
            return View();
        }


        


        [HttpPost]
        public ActionResult AddProduct(string name,int price , string status , HttpPostedFileBase Image )
        {

          
            tblProduct t = new tblProduct();
            t.Name = name;
            t.Price = price;
            t.Status_ = status;
            t.Image = Image.FileName.ToString();
            var folder = Server.MapPath("~/images/");
            Image.SaveAs(Path.Combine(folder, Image.FileName.ToString()));
            db.tblProducts.Add(t);
            db.SaveChanges();
            TempData["msg"] = "Product Add";
            return View();
        }

        public ActionResult ViewProduct()
        {
            var query = db.tblProducts.ToList();

            var products = db.tblProducts.ToList();

            SelectList list = new SelectList(products, "ProId", "Name");
            ViewBag.pronames = list;

            return View(query);
        }

        public ActionResult SingleProduct(int id)
        {

            var query = db.tblProducts.SingleOrDefault(m => m.ProId == id);
            

            return View(query);
        }

        public ActionResult OrderBook()
        {
            return View();
        }

        [HttpPost]
        public ActionResult OrderBook(int proid, int userid, string contact, string email, string address, int price, int qty, int total, string paymethod, string date)
        {
            tblOrder t = new tblOrder();
            t.ProId = proid;
            t.UserId = userid.ToString();
            t.Contact = contact;
            t.Email = email;
            t.Address = address;
            t.Price = price;
            t.Qty = qty;
            t.Total = total;
            t.PayMethod = paymethod;
            t.Date = DateTime.Now;
            db.tblOrders.Add(t);
            db.SaveChanges();
            TempData["book"] = "Order Book Successfully!!";

            return View();
        }

        public ActionResult ViewOrder()
        {
            var query = db.tblOrders.ToList();
            return View(query);

        }

        public ActionResult UserOrderDetail(string userid)
        {


            var query = db.tblOrders.Where(m => m.UserId == userid).ToList();

            return View(query);

        }
    }


}