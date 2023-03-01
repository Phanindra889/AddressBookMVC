
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Server;
using System.Dynamic;
using System.Web.Mvc;
using Model.Models;
using Domain.Services;
using Controller = Microsoft.AspNetCore.Mvc.Controller;

namespace UserInterface.Controllers
{
    public class AddressBookController : Controller
    {
        private IServices actions;

        public AddressBookController(IServices actions)
        {
            this.actions = actions;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Form()
        {
            return View(new ContactInformation());
        }
        public IActionResult Insert(ContactInformation contact)
        {
            if (ModelState.IsValid)
            {
                actions.InsertContact(contact);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Form");
        }
        public IActionResult ViewDetails(int Id)
        {
            if(ModelState.IsValid) 
            {
                return View(actions.GetContactById(Id));
            }
            return RedirectToAction("Index");
        }
        public IActionResult Delete(ContactInformation contact)
        {
            actions.DeleteContact(contact);
            return RedirectToAction("Index");
        }
        public IActionResult EditForm(int Id)
        {
            var contact = actions.GetContactById(Id);
            if(contact == null)
            {
                return RedirectToAction("Index");
            }
            return View(contact);
        }
        public IActionResult UpdateContact(ContactInformation contact)
        {
            actions.UpdateContact(contact);
            return RedirectToAction("Index");
        }
    }
}
