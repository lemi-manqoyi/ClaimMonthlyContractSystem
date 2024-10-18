namespace ContractMonthlyClaimSystem.Models
{
    public class LecturerModel
    {
        public string notReviewedClaimStatus = "Pending...";
        //key to map claim
        public int LecturerClaimID { get; set; }

        //extra stuff | more than standard...
        public string LecturerName { get; set; }  required
        public string LecturerUploadFileName { get; set; }  required

        public string LecturerAdditionalNotes { get; set; }  required

        public double LecturerHourlyRate{ get; set; } required

        public int LecturerHoursWorked{ get; set; } 

        public DateTime LecturerUploadDate { get; set; }

        //only accessible to the administrator
        public bool ReviewedClaimStatus { get; set; }
    }

}