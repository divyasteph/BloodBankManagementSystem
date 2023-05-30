using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using CRUD_Op.Models;

namespace CRUD_Op.Controllers
{
    public class PersonController : ApiController
    {
        public HttpResponseMessage Get()
        {
            DataTable table = new DataTable();
            string query = @"select Person_ID, Name, Address, Phone, IsActive, Password from Person";
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["BBMS"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }
            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        public string Post(Person p)
        {
            try
            {
                DataTable table = new DataTable();
                string query = @"insert into Person(Name, Address, Phone, IsActive, Password) values('" + p.Name + @"','" + p.Address + @"', '" + p.Phone + @"', '" + p.IsActive + @"', '" + p.Password + @"' )";
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
        public string Put(Person p)
        {
            try
            {
                DataTable table = new DataTable();
                string query = @"update Person set Name = '" + p.Name + @"', Address= '" + p.Address + @"', Phone= '" + p.Phone + @"', IsActive= '" + p.IsActive + @"', Password= '" + p.Password + @"' where Person_ID =" + p.Person_ID + @"";
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["BBMS"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return "Updated Successfully";
            }
            catch (Exception)
            {
                return "Failed to Update";
            }



        }
        public string Delete(int id)
        {
            try
            {
                DataTable table = new DataTable();
                string query = @" delete from Person where Person_ID =" + id;
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
