using ContractMonthlyClaimSystem.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ContractMonthlyClaimSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

       
        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }

        
        //private list to store all the user uploads
        private static List<LecturerModel> claims = new List<LecturerModel>();

        public IActionResult Index()
        {
            return View();
        }

         //http post, to store the file in a folder I created in the webroot
        [HttpPost]
        public IActionResult Upload(IFormFile file, string lecturerName, string addNotes, int hoursWorked, double hourlyRate)
        {
            if (file != null && file.Length > 0)
            {
               var fileName = Path.GetFileName(file.FileName);  
               var path = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", fileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                claims.Add(new LecturerModel
                {
                    LecturerClaimID = claims.Count +1,
                    LecturerUploadFileName = fileName,
                    LecturerAdditionalNotes = addNotes,
                    LecturerName = lecturerName,
                    LecturerHourlyRate = hourlyRate,
                    LecturerHoursWorked = hoursWorked,
                    LecturerUploadDate = DateTime.Now,
                });

            }
            return RedirectToAction("LecturerSubmitClaim");
        }

        //method to open the file
        public ActionResult OpenFile(string fileName)
        {
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", fileName);
            var fileBytes = System.IO.File.ReadAllBytes(path);
            return File(fileBytes, "application/octet-stream", fileName);
        }
        public IActionResult LecturerSubmitClaim()
        { 
            return View(claims);
        }
        
        public IActionResult LecturerMenu()
        {
            return View();
        }
        
        public IActionResult AdminMenu()
        {
            return View();
        }

        public IActionResult AdminViewClaims() 
        {
             return View(claims);
        } 
        
        public IActionResult LecturerClaimHistory() 
        {
             return View(claims);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult ReviewClaims(string password, int claimId, bool approve)
        {
            // Password to be changed every month, and only given to administrator.
            const string correctPassword = "#$3t@Dm!Np@SS*"; 

            // basic password check to kyk if the password is ngca or not.
            if (password != correctPassword)
            {   
                // Return the claims view with an error message
                ModelState.AddModelError(string.Empty, "Invalid password. \n Please try typing Slower.");
                return View(claims); 
            }

            //finding the claim by ID
            var claim = claims.FirstOrDefault(c => c.LecturerClaimID == claimId);

            if (claim != null)
            {
                // setting the ReviewedClaims based on the approval of admin
                claim.ReviewedClaimStatus = approve; 
            }
            
            // sending it back to the to view claims after approval
            return RedirectToAction("AdminViewClaims");
        }

        } 
    }
    
