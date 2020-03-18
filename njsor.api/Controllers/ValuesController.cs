using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using njsor.api.Data;

namespace njsor.api.Controllers
{

 [Route("api/[controller]")]
 [ApiController]

public class ValuesController : ControllerBase

 {
   private readonly DataContext _Context;

   public ValuesController(DataContext context)

   {

            _Context = context;

   }
  // GET api/values

  [HttpGet]

  public  async Task<IActionResult> GetValues()

  {

    var values=await _Context.Values.ToListAsync();

    return Ok(values);

  }

  // GET api/values/5

  [HttpGet("{id}")]

  public  async Task<IActionResult> Get(int id)

   {

     var value= await _Context.Values.FirstOrDefaultAsync(x=>x.Id == id);

     return Ok(value);

   }

   

 }

}

 