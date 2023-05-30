using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;
using System.Configuration;
using CRUD_Op.Models;
using System.Data;

namespace CRUD_Op.Controllers
{
    public class ReceiverController : ApiController
    {
        public HttpResponseMessage Get()
        {
            DataTable table = new DataTable();
            string query = @"select Receiver_ID, Name, BloodGroup, Unit, Hospital, Phone, Status from receiver";
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
        [Route("api/receiver/{id}")]
        public HttpResponseMessage GetReceiverById(int id)
        {
            DataTable table = new DataTable();
            string query = @"select Receiver_ID, Name, BloodGroup, Unit, Hospital, Phone, Status from receiver where Receiver_ID= " + id;
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["BBMS"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }
            return Request.CreateResponse(HttpStatusCode.OK, table);
        }


        public string Post(Receiver r)
        {
            try
            {
                DataTable table = new DataTable();
                string query = @"insert into receiver(Name, BloodGroup, Unit, Hospital, Phone, Status) values ('" + r.Name + @"','" + r.BloodGroup + @"','" + r.Unit + @"' , '" + r.Hospital + @"','" + r.Phone + @"','" + r.Status + @"')";
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

        public string Put(Receiver r)
        {
            try
            {
                DataTable table = new DataTable();
                string query = @"update receiver set Name = '" + r.Name + @"', BloodGroup= '" + r.BloodGroup + @"', Unit='" + r.Unit + @"', Hospital='" + r.Hospital + @"', Phone='" + r.Phone + @"', Status='" + r.Status + @"' where Receiver_ID =" + r.Receiver_ID + @" ";
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["BBMS"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                    
                }
                Update();
                return "Updated Successfully";
            }
            catch (Exception)
            {
                return "Failed to Update";
            }
        }
        public void Update()
        {
            try
            {
                DataTable table = new DataTable();
                string query = @"UPDATE blood_stock SET Unit_Available = Unit_Available - receiver.Unit FROM blood_stock INNER JOIN receiver ON blood_stock.Blood_Group = receiver.BloodGroup";
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["BBMS"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                Console.WriteLine("SUCCESSS");
            }
            catch (Exception)
            {
                Console.WriteLine("FAILED");
            }
        }

        public string Delete(int id)
        {
            try
            {
                DataTable table = new DataTable();
                string query = @" delete from receiver where Receiver_ID =" + id;
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["BBMS"].ConnectionString))
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
