using project1.Models.DAO;
using project1.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace project1.Controllers
{
    public class UserController : Controller
    {
        private UserDAO userRepository = new UserDAO();

        // GET: User
        public ActionResult Index()
        {
        
            return View(userRepository.ReadUsers());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(UserDTO user)
        {
            userRepository.InsertUser(user);
            return RedirectToAction("Index");
        }
    }
}