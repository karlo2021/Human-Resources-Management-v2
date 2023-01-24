using HumanResourcesAPI.Data;
using HumanResourcesAPI.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace HumanResourcesAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HomeController : ControllerBase
{
    public ApplicationDbContext DbContext;
    public HomeController(ApplicationDbContext ctx) => DbContext = ctx;

    [HttpGet]
    public async Task<ActionResult<ApiResult<Person>>> GetPersons(
        int pageIndex = 0,
        int pageSize = 4,
        string? sortColumn = null,
        string? sortOrder = null,
        string? filterColumn = null,
        string? filterQuery = null)
    {
        return await ApiResult<Person>.CreateAsync(
            DbContext.Persons.AsNoTracking(),
            pageIndex,
            pageSize,
            sortColumn,
            sortOrder,
            filterColumn,
            filterQuery
        );
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Person>> GetPerson(int id)
    {
        if (DbContext.Persons == null)
        {
            return NotFound();
        }
        var person = await DbContext.Persons.FindAsync(id);

        if (person == null)
        {
            return NotFound();
        }

        return person;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutPerson([FromRoute]int id, Person person)
    {
        if (id != person.Id)
        {
            return BadRequest();
        }

        DbContext.Entry(person).State = EntityState.Modified;

        try
        {
            await DbContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PersonExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<Person>> PostPerson(Person person)
    {
        if (DbContext.Persons == null)
        {
            return Problem("Entity set 'ApplicationDbContext.Persons'  is null.");
        }
        DbContext.Persons.Add(person);
        await DbContext.SaveChangesAsync();

        return CreatedAtAction("GetPerson", new { id = person.Id }, person);
    }

    // DELETE: api/Home/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePerson(int id)
    {
        if (DbContext.Persons == null)
        {
            return NotFound();
        }
        var person = await DbContext.Persons.FindAsync(id);
        if (person == null)
        {
            return NotFound();
        }

        DbContext.Persons.Remove(person);
        await DbContext.SaveChangesAsync();

        return NoContent();
    }

    private bool PersonExists(int id)
    {
        return (DbContext.Persons?.Any(e => e.Id == id)).GetValueOrDefault();
    }

}
