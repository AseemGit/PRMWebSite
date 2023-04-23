using PRMWebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PRMWebSite.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Authorize(PRMWebSite.Models.PropertyOwner userModel ) 
        {
            using (PRMDatabaseEntities db = new PRMDatabaseEntities())
            {
                var userDetails = db.PropertyOwners.Where(x => x.Email == userModel.Email && x.Password == userModel.Password).FirstOrDefault();
                if(userDetails == null)
                {
                    userModel.LoginErrorMessage = "Wrong Email or Password!!";
                    return View("Index", userModel);
                }
                else
                {
                    Session["OwnerId"] = userDetails.OwnerId;
                    Session["Email"] = userDetails.Email;

                    return RedirectToAction("Index", "Home");
                }
            } 
        }
       
        
        public ActionResult LogOut()
        {
            int ownerId = (int)Session["OwnerId"];
        
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}