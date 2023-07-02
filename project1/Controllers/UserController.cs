using project1.Models.DAO;
using project1.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
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

        public ActionResult Update(int id)
        {
            UserDTO user = userRepository.GetUserById(id);
            if(user == null)
            {
                return HttpNotFound();
            }
            return View("Update", user);
        }

        [HttpPost]
        public ActionResult Update(UserDTO user)
        {
            String result = userRepository.UpdateUser(user, user.Id);
            if(result == "Success") 
            {
                return RedirectToAction("Index");   
            }
            return View("Update", user);
        }

        public ActionResult Delete(int id)
        {
            UserDTO user = userRepository.GetUserById(id);

            if(user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost]
        public ActionResult DeleteConfirmed(int id) 
        { 
            String result = userRepository.DeleteUser(id);
            return RedirectToAction("Index");
        }
    }

}