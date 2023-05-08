using ContactsApi.Data;
using ContactsApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")] //Substitui o nome do controller actual dentro dos parenteses rectos
    public class ContactsController : Controller
    {
        private readonly ContactsApiDbContext _dbContext;
        public ContactsController(ContactsApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            return Ok( await _dbContext.Contacts.ToListAsync());       
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetContact([FromRoute]Guid id){
            var contact = await _dbContext.Contacts.FindAsync(id);

            if (contact != null)
            {
                return Ok(contact);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AddContact(AddContactRequest model)
        {
            var contact = new Contact
            {
                Id = Guid.NewGuid(),
                Address = model.Address,
                Email = model.Email,
                FullName = model.FullName,
                Phone = model.Phone
            };

            await _dbContext.Contacts.AddAsync(contact);
            await _dbContext.SaveChangesAsync();

            return Ok(contact);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateContact([FromRoute]Guid id,UpdateContactRequest model)
        {
            var contact = await _dbContext.Contacts.FindAsync(id);

            if(contact != null)
            {
                contact.FullName = model.FullName;
                contact.Address = model.Address;
                contact.Email = model.Email;
                contact.Phone = model.Phone;

                await _dbContext.SaveChangesAsync();

                return Ok(contact);
            }

            return NotFound();
        }


        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteContact([FromRoute]Guid id)
        {
            var contact = await _dbContext.Contacts.FindAsync(id);

            if (contact != null)
            {
                _dbContext.Contacts.Remove(contact);
                await _dbContext.SaveChangesAsync();

                return Ok(contact);
            }

            return NotFound();
        }
    }
}
