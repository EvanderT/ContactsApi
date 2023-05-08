using Mablo.Models;
using Mablo.Services;
using Microsoft.AspNetCore.Mvc;

namespace Mablo.Controllers
{
    public class ContactsController : Controller
    {

        private ContactService _contactService;
        public ContactsController(ContactService contactService)
        {
            _contactService = contactService;
        }
        public async Task<IActionResult> Index()
        {          
            
            var contactos = await _contactService.GetContactsAsync();
            return View(contactos);
        }

        public async Task<IActionResult> Detalhe(Guid id)
        {

            var contacto = await _contactService.GetContactAsync(id);
            return View(contacto);
        }


    }
}
