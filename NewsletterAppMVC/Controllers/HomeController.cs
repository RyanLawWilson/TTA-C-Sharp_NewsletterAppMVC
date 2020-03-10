using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using NewsletterAppMVC.Models;
using NewsletterAppMVC.ViewModels;


namespace NewsletterAppMVC.Controllers
{
    public class HomeController : Controller
    {
        // To get this connection string, connect your database to Visual Studio with SQL Server Object Explorer.
        // If it is not there, find out the server name for your database and add the server.
        // Right-Click the database you are trying to connect to and go to properties.
        // Under 'General' you will find a field named Connection String, copy that and paste it as a string.

        public ActionResult Index()
        {
            return View();
        }

        // The form on the index page.  The form has inputs for the parameters below.  The names match IMPORTANT
        // When a method is being posted too, specify it like this.
        // .NET MVC knows that the form maps to these parameters.  This is called model-binding
        [HttpPost]
        public ActionResult SignUp(string firstName, string lastName, string emailAddress)
        {
            // The ~ indicates a relative path
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(emailAddress))
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            else
            {
                using (NewsletterEntities db = new NewsletterEntities())
                {
                    // Creating a new signup record to add it to the database.
                    var signup = new SignUp();
                    signup.FirstName = firstName;
                    signup.LastName = lastName;
                    signup.EmailAddress = emailAddress;

                    db.SignUps.Add(signup);
                    db.SaveChanges();
                }


                /*================================================
             
                    The code above makes this code below absolete.
                    
                  ================================================*/


                ///*

                //    Using ADO.NET to connect to Database

                // */

                //// The query that will be passed to the database.
                //// We use parameters, @, to prevent SQL injections
                //string queryString = @"INSERT INTO SignUps (FirstName, LastName, EmailAddress) 
                //                       VALUES (@FirstName, @LastName, @EmailAddress)";

                //// When you are connection to a database, be sure to use 'using' so that the connection is stopped when you are done.
                //// Use SqlConnection to connect to your SQL database and pass in the connection string.
                //using (SqlConnection connection = new SqlConnection(connectionString))
                //{
                //    // SqlCommand will actually perform the query with.  Needs the query and connection to the database.
                //    SqlCommand command = new SqlCommand(queryString, connection);

                //    // Add the parameters after you make SqlCommand.
                //    // Parameters.Add() needs the name of the parameter (including @) and the type of data it is.
                //    command.Parameters.Add("@FirstName", SqlDbType.VarChar);
                //    command.Parameters.Add("@LastName", SqlDbType.VarChar);
                //    command.Parameters.Add("@EmailAddress", SqlDbType.VarChar);

                //    // After the parameters are added, we can add their values.
                //    // Remember this is a method that is called when the user posts, so their data is represented as parameters
                //    // We use the parameters of this method as the values for the SQL parameters.
                //    command.Parameters["@FirstName"].Value = firstName;
                //    command.Parameters["@LastName"].Value = lastName;
                //    command.Parameters["@EmailAddress"].Value = emailAddress;

                //    // Now that the SQL command is ready, we can open the connection and execute it.
                //    connection.Open();
                //    command.ExecuteNonQuery();
                //    connection.Close();
                //}
                return View("Success");
            }
        }
    }
}