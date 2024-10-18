namespace ContractMonthlyClaimSystem.Models
{
    public class AdminModel
    {
        public bool AprroveStatus = false;

        public string LecturerName { get; set; }

         public int LecturerClaimID { get; set; }        
        public DateTime LecturerUploadDate { get; set; }
    }
}
