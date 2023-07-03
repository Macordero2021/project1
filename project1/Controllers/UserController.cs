using project1.Models;
using project1.Models.DAO;
using project1.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Web;
using System.Web.DynamicData;
using System.Web.Mvc;

namespace project1.Controllers
{
    public class UserController : Controller
    {
        private UserDAO userRepository = new UserDAO();

        private AuthenticationConfig _db = new AuthenticationConfig();

        // GET: User
        public ActionResult Index()
        {
            
            List<UserDTO> users = userRepository.ReadUsers();
            //get an specific user(5)
            UserDTO user = (from u in users where 5 == u.Id select u).First();
            // get role
            RolesUsuario ru = _db.RolesUsuario.Where(x => x.IdUser == user.Id).FirstOrDefault();
            //asing a session variable
            Session["role"] = _db.Roles.Where(x => x.Id == ru.IdRole).FirstOrDefault().Description;

            var roles = _db.Roles.ToList();
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