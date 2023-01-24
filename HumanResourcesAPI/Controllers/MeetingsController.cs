using HumanResourcesAPI.Data;
using HumanResourcesAPI.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HumanResourcesAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MeetingsController : ControllerBase
{
    public ApplicationDbContext DbContext;
    public MeetingsController(ApplicationDbContext ctx) => DbContext = ctx;


    [HttpGet]
    public async Task<ActionResult<ApiResult<Meeting>>> GetMeetings(
        int pageIndex = 0,
        int pageSize = 10,
        string? sortColumn = null,
        string? sortOrder = null,
        string? filterColumn = null,
        string? filterQuery = null)
    {
        return await ApiResult<Meeting>.CreateAsync(
        DbContext.Meetings.AsNoTracking(),
            pageIndex,
            pageSize,
            sortColumn,
            sortOrder,
            filterColumn,
            filterQuery);
    }

    //[HttpGet("{id}")]
    //public async Task<ActionResult<Meeting>> GetMeeting(int id)
    //{
    //    if (DbContext.Meetings == null)
    //    {
    //        return NotFound();
    //    }
    //    var meeting = await DbContext.Meetings.FindAsync(id);

    //    if (meeting == null)
    //    {
    //        return NotFound();
    //    }

    //    return meeting;
    //}

    [HttpGet("{id}")]
    public async Task<ActionResult<List<Meeting>>> GetPersonsMeetings(int id)
    {
        if (DbContext.Meetings == null)
        {
            return NotFound();
        }
        var meetings = await DbContext.Meetings.Where(p => p.PersonId == id).ToListAsync();

        if (meetings == null)
        {
            return NotFound();
        }

        return meetings;
    }

    [HttpGet]
    [Route("get/{id}")]
    public async Task<ActionResult<Meeting>> GetMeeting(int id)
    {
        if (DbContext.Meetings == null)
        {
            return NotFound();
        }
        var meeting = await DbContext.Meetings.FindAsync(id);

        if (meeting == null)
        {
            return NotFound();
        }

        return meeting;
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMeeting(int id)
    {
        if (DbContext.Meetings == null)
        {
            return NotFound();
        }
        var meeting = await DbContext.Meetings.FindAsync(id);
        if (meeting == null)
        {
            return NotFound();
        }

        DbContext.Meetings.Remove(meeting);
        await DbContext.SaveChangesAsync();

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<Meeting>> PostMeeting(Meeting meeting)
    {
        if (DbContext.Persons == null)
        {
            return Problem("Entity set 'ApplicationDbContext.Persons'  is null.");
        }
        DbContext.Meetings.Add(meeting);
        await DbContext.SaveChangesAsync();

        return CreatedAtAction("GetMeeting", new { id = meeting.Id }, meeting);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutMeeting([FromRoute] int id, Meeting meeting)
    {
        if (id != meeting.Id)
        {
            return BadRequest();
        }

        DbContext.Entry(meeting).State = EntityState.Modified;

        try
        {
            await DbContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!MeetingExists(id))
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

    private bool MeetingExists(int id)
    {
        return (DbContext.Meetings?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
