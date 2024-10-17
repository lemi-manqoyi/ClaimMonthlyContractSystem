namespace ContractMonthlyClaimSystem.Models
{
    public class LecturerModel
    {

        public int LecturerID { get; set; }

        public string LecturerName { get; set; }  required
        public string LecturerUploadFileName { get; set; }  required

        public string LecturerAdditionalNotes { get; set; }  required

        public double LecturerHourlyRate{ get; set; } required

        public int LecturerHoursWorked{ get; set; } 

        public DateTime LecturerUploadDate { get; set; }

    }

}