using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingWebAPI.DAL;
using ShoppingWebAPI.DAL.Entities;
using DbUpdateException = Microsoft.EntityFrameworkCore.DbUpdateException;

namespace ShoppingWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly DataBaseContext _context;

        public CitiesController(DataBaseContext context)
        {
            _context = context;
        }

        [HttpGet, ActionName("Get")]
        [Route("GeCitiesById/{cityId}")]
        public async Task<ActionResult<City>> GetCityById(Guid? stateId)
        {
            var city = await _context.Cities.FirstOrDefaultAsync(c => c.Id == stateId);

            if (city == null) return NotFound();

            return Ok(city);
        }

        [HttpGet, ActionName("Get")]
        [Route("GetCityByStateId")]
        public async Task<ActionResult<IEnumerable<City>>> GetCitiesByCountryId(Guid? stateId)
        {
            var Cities = await _context.Cities
                .Where(s => s.StateId == stateId)
                .ToListAsync();

            if (Cities == null) return NotFound();

            return Cities;
        }

        [HttpPost, ActionName("Create")]
        [Route("CreateCity")]
        public async Task<ActionResult> CreateCity(City city, Guid stateId)
        {
            try
            {
                city.Id = Guid.NewGuid();
                city.CreatedDate = DateTime.Now;
                city.StateId = stateId;
                city.State = await _context.States.FirstOrDefaultAsync(s => s.Id == stateId);
                city.ModifiedDate = null;

                _context.Cities.Add(city);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    return Conflict(String.Format("{0} ya existe en {1}", city.Name, city.State.Name));
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }

            return Ok(city);
        }

        [HttpPut, ActionName("Edit")]
        [Route("EditCity/{cityId}")]
        public async Task<ActionResult> EditCity(Guid cityId, City city)
        {
            try
            {
                if (cityId != city.Id) return NotFound("City not found");

                city.ModifiedDate = DateTime.Now;

                _context.Cities.Update(city);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    return Conflict(String.Format("{0} ya existe en {1}", city.Name, city.State.Name));
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }

            return Ok(city);
        }

        [HttpDelete, ActionName("Delete")]
        [Route("DeleteCity/{cityId}")]
        public async Task<ActionResult> DeleteCity(Guid? cityId)
        {
            if (_context.Cities == null) return Problem("Entity set 'DataBaseContext.Cities' is null.");
            var city = await _context.Cities.FirstOrDefaultAsync(s => s.Id == cityId);

            if (city == null) return NotFound("City not found");

            _context.Cities.Remove(city);
            await _context.SaveChangesAsync(); //Hace las veces del Delete en SQL

            return Ok(String.Format("El estado {0} fue eliminado!", city.Name));
        }
    }
}


