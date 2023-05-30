using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;
using System.Configuration;
using CRUD_Op.Models;
using System.Web.UI.WebControls;

namespace CRUD_Op.Controllers
{
    public class DonorController : ApiController
    {
        public HttpResponseMessage Get()
        {
            DataTable table = new DataTable();
            string query = @"select Donor_ID, Name, BloodGroup, Unit, Hospital, Phone, Status from donor";
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["BBMS"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }
            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [HttpGet]
        [Route("api/Donor/{id}")]
        public HttpResponseMessage GetDonorById(int id)
        {
            DataTable table = new DataTable();
            string query = @"select Donor_ID, Name, BloodGroup, Unit, Hospital, Phone, Status from donor where Donor_ID= " + id;
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["BBMS"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }
            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        public string Post(Donor d)
        {
            try
            {
                DataTable table = new DataTable();
                string query = @"insert into donor(Name, BloodGroup, Unit, Hospital, Phone, Status) values ('" + d.Name + @"','" + d.BloodGroup + @"','" + d.Unit + @"' , '" + d.Hospital + @"','" + d.Phone + @"','" + d.Status + @"')";               
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["BBMS"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return "Added Successfully";
            }
            catch (Exception)
            {
                return "Failed to Add";
            }
        }

        public string Put(Donor d)
        {
            try
            {
                DataTable table = new DataTable();
                string query = @"update donor set Name = '" + d.Name + @"', BloodGroup= '" + d.BloodGroup + @"', Unit='" + d.Unit + @"', Hospital='" + d.Hospital + @"', Phone='" + d.Phone + @"', Status='" + d.Status + @"' where Donor_ID =" + d.Donor_ID + @" ";
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["BBMS"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                Update();
                //var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BBMS"].ConnectionString);
                //Update(connection, d.Unit, d.BloodGroup);

                return "Updated Successfully";
            }
            catch (Exception)
            {
                return "Failed to Update";
            }
        }

        //Another method to auto update the blood stock table
        public void Update(SqlConnection con, int donorUnits, string bloodGroup)
        {
            try
            {
                string query = @"UPDATE blood_stock SET Unit_Available = Unit_Available + @Unit WHERE BloodGroup = @BloodGroup";

                using (var cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Unit", donorUnits); // Use the donorUnits parameter
                    cmd.Parameters.AddWithValue("@BloodGroup", bloodGroup); // Use the bloodGroup parameter

                    cmd.ExecuteNonQuery();
                }

                Console.WriteLine("SUCCESS");
            }
            catch (Exception)
            {
                Console.WriteLine("FAILED");
            }
        }
        public void Update()
        {
            try
            {
                DataTable table = new DataTable();
                string query = @"UPDATE blood_stock SET Unit_Available = Unit_Available + donor.Unit FROM blood_stock INNER JOIN donor ON blood_stock.Blood_Group = donor.BloodGroup";
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["BBMS"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                Console.WriteLine("SUCCESSS");
                //return "Updated Successfully";
            }
            catch (Exception)
            {
                Console.WriteLine("FAILED");
               // return "Failed to Update";
            }
        }
       


        public string Delete(int id)
        {
            try
            {
                DataTable table = new DataTable();
                string query = @" delete from donor where Donor_ID =" + id;
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["bloodBankDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return "Deleted Successfully";
            }
            catch (Exception)
            {
                return "Failed to Delete";
            }



        }
    }
}
