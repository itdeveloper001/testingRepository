using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ThemeMVC.Models;


namespace ThemeMVC.Controllers
{
    public class AccountController : Controller
    {

        dbShoppingEntities db = new dbShoppingEntities();

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email,string password)
        {
            var query = db.tblUsers.FirstOrDefault(m => m.Email == email && m.Password == password);

            if (query != null)
            {
                if(query.UserType == 1){

                FormsAuthentication.SetAuthCookie(query.Email, false);
                Session["Email"] = query.Email;
                return RedirectToAction("Index", "Home");

              }
                else if(query.UserType == 2)
                {
                    FormsAuthentication.SetAuthCookie(query.Email, false);
                    Session["Email"] = query.Email;
                    Session["UserId"] = query.UserId;
                    return RedirectToAction("Index", "Home");

                }

            }
            else
            {
                TempData["msg"] = "Invalid Username or Password";
            }
            return View();
        }


        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            //Session.Abandon();
            return RedirectToAction("Login", "Account");
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(string email,string password,int usertype )
        {
            tblUser u = new tblUser();
            u.Email = email;
            u.Password = password;
            u.UserType = usertype;
            db.tblUsers.Add(u);
            db.SaveChanges();
            TempData["msg"] = "User Registered";
            return View();
        }
    }

}