using project1.Models;
using project1.Models.DAO;
using project1.Models.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            var Roles = _db.Roles.ToList(); // Crea variable para traer todos los roles
            return View(Roles); // Retorna los roles
        }


        [HttpPost]
        public ActionResult Create(UserDTO user, Roles roles) // trae el formulario de la viste del create


        {
            string selectedRole = roles.Description; // Trae el nombre del rol que seleccionamos
            var IdRol = _db.Roles.Where(x => x.Description == selectedRole).First(); // obtener el Id del rol que seleccionamos
            List<UserDTO> users = userRepository.ReadUsers(); // Obtener lista de todos los usuarios
            var UserList = users.LastOrDefault(); // obtiene el ultimo usuario de la lista de la DB
            var UserId = UserList.Id + 1; // obtiene el Id del usuario de la lista y le suma 1 que va hacer el nuevo usuario
            RolesUsuario IdsSave = new RolesUsuario(); // crea un objeto de la tabla
            IdsSave.IdRole = IdRol.Id; // guarda el dato en la tabla el id del rol
            IdsSave.IdUser = UserId; // guardar el dato en la tabla del Id usuario
            _db.RolesUsuario.Add(IdsSave); // guardar los datos en la BD
            _db.SaveChanges(); // Guarda los datos
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