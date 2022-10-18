using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Semina.Data;
using Semina.Models;

namespace Semina.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : Controller
    {
        private readonly ContactsAPIDbContext dbContext;

        public ContactsController(ContactsAPIDbContext dbContext)
        {
            this.dbContext = dbContext; 
        }

        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            return Ok(await dbContext.contacts.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> AddContact(AddContactsRequest addContactsRequest)
        {
            var contacts = new Contact()
            {
                Id = Guid.NewGuid(),
                Email = addContactsRequest.Email,
                Phone = addContactsRequest.Phone,
                FullName = addContactsRequest.FullName,
            };
            await dbContext.contacts.AddAsync(contacts);
            await dbContext.SaveChangesAsync();

            return Ok(contacts);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateContact([FromRoute] Guid id, UpdateContactRequest updateContactRequest)
        {
            var contact = await dbContext.contacts.FindAsync(id);

            if(contact != null)
            {
                contact.FullName = updateContactRequest.FullName;
                contact.Phone = updateContactRequest.Phone;
                contact.Email = updateContactRequest.Email;

                await dbContext.SaveChangesAsync();

                return Ok(contact); 
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteContact([FromRoute]Guid id)
        {
            var contact = await dbContext.contacts.FindAsync(id);

            if(contact != null)
            {
                dbContext.Remove(contact);
                await dbContext.SaveChangesAsync();
                return Ok(contact);
            }
            return NotFound();
        }
    }
}
