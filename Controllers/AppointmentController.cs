using Apoint_pro.Data;
using Apoint_pro.Data.DTOS;
using Apoint_pro.Data.Helpers;
using Apoint_pro.Data.models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace Apoint_pro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AppointmentController : ControllerBase
    {
        private readonly AppDbContext Db;
        private readonly IMapper Mapper;
        public AppointmentController(AppDbContext Db, IMapper Mapper)
        {
            this.Mapper = Mapper;
            this.Db = Db;
        }
        
        [HttpGet("getall")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll()
        {
            var hm = new HelperMethods(HttpContext);
            var id = hm.GetIdFromToken();       
            string role = hm.GetRoleFromToken();
            List<ApointmentDTO> apdto;
            if (role.Equals("Admin"))
            {
                apdto = Mapper.Map<List<ApointmentDTO>>(await Db.Apointments.ToListAsync());
               
            }
            else if (role == "User")
            {
                 apdto = Mapper.Map<List<ApointmentDTO>>(await Db.Apointments.Select(o => o.UserId == id).ToListAsync());
            }
            else
            {
                 apdto = Mapper.Map<List<ApointmentDTO>>(await Db.Apointments.Select(o => o.DoctorId).ToListAsync());
            }
           return Ok(apdto);
        }
        [HttpGet]
        [Route("get/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(int id)
        {
            var appointment = await Db.Apointments.FirstOrDefaultAsync(a => a.Id == id);
            if (appointment == null)
            {
                return BadRequest("Appointment not found");
            }
            return Ok(appointment);
        }
        [HttpPost]
        [Route("add")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add([FromBody] ApointmentDTO appointment)
        {
            if (ModelState.IsValid)
            {
                var hm = new HelperMethods(HttpContext);
                var id = hm.GetIdFromToken();

              Apointment apointment = new Apointment()
                {
                    DoctorId = appointment.DoctorId,
                    Date = appointment.Date,
                    Notes = appointment.Notes,
                    UserId = id,
                    status = "pending"
                };
                await Db.Apointments.AddAsync(apointment);
                await Db.SaveChangesAsync();
                return Ok(appointment);
            }
            return BadRequest("Invalid appointment");
        }

        [HttpGet]
        [Route("GetAllDoctors")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllDoctors()
        {
            var doctors = await Db.Doctors.ToListAsync();
            return Ok(doctors);
        }
    }
}
