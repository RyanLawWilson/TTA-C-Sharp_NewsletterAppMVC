using NewsletterAppMVC.Models;
using NewsletterAppMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsletterAppMVC.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            // The entity framework gives you access to your database.
            using (NewsletterEntities db = new NewsletterEntities())
            {
                // db.SignUps represents all of the records in our database that are subscribed
                //var signups = db.SignUps.Where(x => x.Removed == null).ToList();
                // another way to filter the database using Linq
                var signups = (from c in db.SignUps
                               where c.Removed == null
                               select c).ToList();

                // Making a new list of ViewModels that will be sent to the page.
                var signupVms = new List<SignupVM>();

                // Loop through the models and only map the properties in the view model.
                /* Why can't we just assign the viewmodel in the first place?  This extra step seems unneccessary. */
                /* Answer: It is just best practice to have a Model that maps exactly and a duplicate that is used to actually send to the page. */
                foreach (var signup in signups)
                {
                    var signupVM = new SignupVM();
                    signupVM.Id = signup.Id;
                    signupVM.FirstName = signup.FirstName;
                    signupVM.LastName = signup.LastName;
                    signupVM.EmailAddress = signup.EmailAddress;
                    signupVms.Add(signupVM);
                }

                return View(signupVms);
            }

            /*================================================
             
                The code above: var signupVM = new SignupVM();
                makes this code below absolete.

              ================================================*/

            //string queryString = @"SELECT * FROM SignUps";

            //List<NewsletterSignUp> signups = new List<NewsletterSignUp>();

            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    // Initialize the SQL command with the query
            //    SqlCommand command = new SqlCommand(queryString, connection);

            //    // Open the connection to read
            //    connection.Open();

            //    // Make the command read from the database (because we are SELECTING)
            //    SqlDataReader reader = command.ExecuteReader();

            //    // While there is data to read, transfer the data from SQL format to C# format.
            //    // We are putting the records into the object that represents those records.
            //    while(reader.Read())
            //    {
            //        var signup = new NewsletterSignUp();
            //        signup.Id = Convert.ToInt32(reader["Id"]);
            //        signup.FirstName = reader["FirstName"].ToString();
            //        signup.LastName = reader["LastName"].ToString();
            //        signup.EmailAddress = reader["EmailAddress"].ToString();
            //        signup.SocialSecurityNumber = reader["SocialSecurityNumber"].ToString();
            //        signups.Add(signup);
            //    }
            //}
        }

        public ActionResult Unsubscribe(int Id)
        {
            using (NewsletterEntities db = new NewsletterEntities())
            {
                var signup = db.SignUps.Find(Id);
                signup.Removed = DateTime.Now;  // Doesn't this need to be in SQL datetime format?
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}