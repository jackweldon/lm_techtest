using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Groceries.API.Data;
using Groceries.API.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Groceries.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class GroceriesController : ControllerBase
    {
        private GroceriesDbContext _context;

        private readonly ILogger<GroceriesController> _logger;
        private readonly ICSVParser<Grocery> _parser;
        public GroceriesController(ILogger<GroceriesController> logger, GroceriesDbContext context, ICSVParser<Grocery> csvparser)
        {
            _logger = logger;
            _context = context;
            _parser = csvparser;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _context.Groceries.OrderBy(m => m.UpdatedDateTime).ToList();
            return Ok(result);
        }

        [HttpGet("/api/[controller]/csv")]
        public IActionResult GetCSV()
        {
            var result = _context.Groceries.ToList();
            var csv = _parser.Serialize(result);
            return Ok(csv);
        }


        [HttpPut("/api/[controller]/{fruit}")]
        public async Task<IActionResult> Update([FromRoute] string fruit, [FromBody]Grocery item)
        {
            var itemToUpdate = _context.Groceries.FirstOrDefault(m => m.Fruit == fruit);

            if (itemToUpdate == null)
            {
                return NotFound();
            }

            itemToUpdate.Price = item.Price;
            itemToUpdate.UpdatedDateTime = DateTime.Now;

            await _context.SaveChangesAsync();
            return Ok(itemToUpdate);
        }
         
        [HttpPost, DisableRequestSizeLimit]
        public IActionResult Upload(IFormFile filesData)
        {
            try
            {
                var file = filesData;
                var folderName = Path.Combine("Resources", "CSV");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    var items = _parser.Deserialize(fullPath);
                    _context.Groceries.RemoveRange();

                    _context.Groceries.AddRange(items);
                    _context.SaveChanges();
                    return Ok(items);
 
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }

}
