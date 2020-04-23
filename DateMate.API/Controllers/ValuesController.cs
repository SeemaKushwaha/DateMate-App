using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DateMate.API.Data;
using Microsoft.AspNetCore.Authorization;

namespace DateMate.API.Controllers
{
 [AllowAnonymous]
 [Route("api/[controller]")]
 [ApiController]   
public class ValuesController : ControllerBase
{
    private readonly DataContext _context;

    public ValuesController(DataContext datacontext){
        _context= datacontext;
    }
    [HttpGet]
    public async Task<IActionResult> GetValues()
    {
        var values= await _context.Values.ToListAsync();
        return Ok(values);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetValue(int id)
    {
        var value= await _context.Values.FirstOrDefaultAsync(x =>x.Id == id);
        return Ok(value);
    }

    [HttpPost]
    public void Post([FromBody] string value)
    {
        
    }

    [HttpPut("{id}")]
    public void Put(int id,[FromBody] string value)
    {
        
    }

    [HttpDelete("{id}")]
    public void Delete(int id)
    {
        
    }
}
}