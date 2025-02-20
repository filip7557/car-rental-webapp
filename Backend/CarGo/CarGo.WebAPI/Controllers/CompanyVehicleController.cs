using CarGo.Common;
using CarGo.Model;
using CarGo.Service.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CarGo.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyVehicleController : ControllerBase
    {
        private readonly ICompanyVehicleService _service;

        public CompanyVehicleController(ICompanyVehicleService service)
        {
            _service = service;
        }

        [Authorize(Roles = "User,Manager,Administrator")]
        [HttpGet]
        public async Task<IActionResult> GetCompanyVehiclesAsync(
            string orderBy = "Id",
            string sortOrder = "ASC",
            int pageNumber = 1,
            int rpp = 10,
            Guid? compId = null,
            Guid? colorId = null,
            bool? isOper = null,
            Guid? locId = null,
            Guid? vehMakeId = null, 
            Guid? vehiModId = null,
            Guid? vehTypeId = null,
            bool? isActive = null
            )
        {
            try
            {
                var filter = new CompanyVehicleFilter
                {
                    IsActive = isActive,
                    CompanyId = compId,
                    VehicleModelId = vehiModId,
                    ColorId = colorId,
                    IsOperational = isOper,
                    CurrentLocationId = locId,
                    VehicleMakeId = vehMakeId,
                    VehicleTypeId = vehTypeId
                };

                var sorting = new BookingSorting
                {
                    OrderBy = orderBy,
                    SortOrder = sortOrder
                };

                var paging = new Paging
                {
                    PageNumber = pageNumber,
                    Rpp = rpp
                };

                var vehicles = await _service.GetAllCompanyVehiclesAsync(sorting, paging, filter);

                return vehicles.Data.Count > 0 ? Ok(vehicles) : NotFound("Nema dostupnih vozila");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Greška pri dohvaćanju vozila: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "User,Manager,Administrator")]
        public async Task<IActionResult> GetCompanyVehicleByIdAsync(Guid id)
        {
            try
            {
                var vehicle = await _service.GetCompanyVehicleByIdAsync(id);

                return vehicle != null ? Ok(vehicle) : NotFound("Vozilo nije pronađeno");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Greška pri dohvaćanju vozila: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator,Manager")]
        public async Task<IActionResult> DeleteCompanyVehicle(Guid id)
        {
            try
            {
                var existingVehicle = await _service.GetCompanyVehicleByIdAsync(id);
                if (existingVehicle == null)
                {
                    return NotFound($"Vozilo s Id={id} ne postoji");
                }
                var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

                await _service.DeleteCompanyVehicleAsync(id, userId);
                return Ok("Vozilo je uspješno obrisano");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Greška pri brisanju vozila: {ex.Message}");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrator,Manager")]
        public async Task<IActionResult> AddCompanyVehicleAsync(CompanyVehicle vehicle)
        {
            try
            {
                if (vehicle == null)
                {
                    return BadRequest("Podaci o vozilu su neispravni");
                }

                if (vehicle.Id == Guid.Empty)
                {
                    vehicle.Id = Guid.NewGuid();
                }

                var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
                var result = await _service.AddCompanyVehicleAsync(vehicle);
                if (result)
                    return Ok("Vozilo je uspješno dodano");
                else
                {
                    return BadRequest("You are not manager of this company.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Greška pri dodavanju vozila: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator, Manager")]
        public async Task<IActionResult> UpdateCompanyVehicleAsync(Guid id, CompanyVehicle updatedVehicle)
        {
            if (updatedVehicle == null)
            {
                return BadRequest("Podaci o vozilu su neispravni");
            }

            try
            {
                var existingVehicle = await _service.GetCompanyVehicleByIdAsync(id);
                if (existingVehicle == null)
                {
                    return NotFound($"Vozilo s Id={id} ne postoji");
                }

                var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
                await _service.UpdateCompanyVehicleAsync(id, updatedVehicle);
                return Ok("Vozilo je uspješno ažurirano");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Greška pri ažuriranju vozila: {ex.Message}");
            }
        }
    }
}