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
            
            var invoices = _db.Invoices.ToList();

            List<Invoice> invoiceCustomerA = invoices.Where(invoice => invoice.Customer == "Cliente A").ToList();
            List<Invoice> invoiceCustomerALINQ = (from invoice in invoices where invoice.Customer == "Cliente A" select invoice).ToList(); 

            var totalinvoiceRecords = invoices.Count();

            var upInvoice = invoices.Max(i => i.Total);
            var upInvoice2 = invoices.OrderByDescending(i => i.Total).FirstOrDefault();
            Invoice upInvoiceLINQ = (from invoice in invoices orderby invoice.Total descending select invoice).FirstOrDefault();

            var greaterThan500 = invoices.Any(i => i.Total > 500);

            var totalsum = invoices.Sum(i => i.Total);

            var greatherThan100AndDifferentCustomer = invoices.Where(i => i.Total > 100 && i.Customer != "Cliente C");

            var allGreaterThan50 = invoices.All(i => i.Total > 50);

            List<Invoice> first5Records = (from i in invoices orderby i.Date select i).Take(5).ToList();

            var totalInvoice = invoices.Sum(i => i.Total); //¿Cuál es el total de todas las facturas registradas en la base de datos?//

            var InvoicesClientB = invoices.Count(i => i.Customer == "Cliente B"); //¿Cuántas facturas tiene el cliente "Cliente B"?//

            var InvoiceBajo = invoices.Min(i => i.Total); // ¿Cuál es la factura con el monto más bajo?//

            List<String> InvoiceThan200Clients = invoices.Where(i => i.Total > 200).Select(i => i.Customer).ToList(); //Obtener una lista con los nombres de los clientes que tienen facturas con monto mayor a $200.//

            var DateAntigue = invoices.Min(i => i.Date); //¿Cuál es la fecha más antigua registrada en las facturas?//

            List<Invoice> InvoicesRecentFive = invoices.OrderByDescending(i => i.Date).Take(5).ToList(); //Obtener una lista de las 5 facturas más recientes.//

            DateTime today = DateTime.Today;
            List<Invoice> InvoicesToday = invoices.Where(i => i.Date.Date == today).ToList(); //¿Hay alguna factura registrada el día de hoy?

            List<Invoice> invoicesmore100 = invoices.Where(i => i.Total <= 100 && i.Customer != "Cliente A").ToList(); //Obtener una lista de las facturas que tienen un monto menor o igual a $100 y un cliente distinto de "Cliente A".

            var total = invoices.Average(i => i.Total); //¿Cuál es el promedio del monto de las facturas?

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
            userRepository.InsertUser(user); // inserta el nuevo usuario
            string selectedRole = roles.Description; // Trae el nombre del rol que seleccionamos
            var IdRol = _db.Roles.Where(x => x.Description == selectedRole).First(); // obtener el Id del rol que seleccionamos
            List<UserDTO> users = userRepository.ReadUsers(); // Obtener lista de todos los usuarios
            var UserList = users.LastOrDefault(); // obtiene el ultimo usuario de la lista de la DB
            var UserId = UserList.Id; // obtiene el Id del usuario de la lista
            RolesUsuario IdsSave = new RolesUsuario(); // crea un objeto de la tabla
            IdsSave.IdRole = IdRol.Id; // guarda el dato en la tabla el id del rol
            IdsSave.IdUser = UserId; // guardar el dato en la tabla del Id usuario
            _db.RolesUsuario.Add(IdsSave); // guardar los datos en la BD
            _db.SaveChanges(); // Guarda los datos
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

        [HttpGet]
        public ActionResult getRole(int id)
        {
            try
            {
                int idRole = _db.RolesUsuario.Where(x => x.IdUser == id).FirstOrDefault().IdRole;
                return Content(_db.Roles.Where(x => x.Id == idRole).FirstOrDefault().Description);
            }
                catch (Exception ex) { 
                    return Content("DESCONOCIDO");
            }
        }
    }

}