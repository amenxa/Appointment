namespace Apoint_pro.Data.DTOS
{
    public class ApointmentDTO
    {
        public int DoctorId { get; set; } 
        public DateTime Date { get; set; } 

        public string? Notes { get; set; }
    }
}
