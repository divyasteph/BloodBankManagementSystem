using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CRUD_Op.Models;

namespace CRUD_Op.Controllers
{
    [RoutePrefix("api/Login")]
    public class LoginController : ApiController
    {
        SqlConnection BBMS = new SqlConnection(ConfigurationManager.ConnectionStrings["BBMS"].ConnectionString);
        SqlCommand cmd = null;
        SqlDataAdapter da = null;

        [HttpPost]
        [Route("Registration")]
        public string Registration(Person person)
        {
            string msg = string.Empty;
            try
            {
                cmd = new SqlCommand("sp_Registration", BBMS);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", person.Name);
                cmd.Parameters.AddWithValue("@Address", person.Address);
                cmd.Parameters.AddWithValue("@Phone", person.Phone);
                cmd.Parameters.AddWithValue("@IsActive", person.IsActive);
                cmd.Parameters.AddWithValue("@Password", person.Password);

                BBMS.Open();
                int i = cmd.ExecuteNonQuery();
                BBMS.Close();

                if (i > 0)
                {
                    msg = "Registered Successfully";
                }
                else
                {
                    msg = "Error";
                }
            }
            catch(Exception e)
            {
                msg = e.Message;
            }
            
            return msg;

        }

        ///
        [HttpPost]
        [Route("Login")]
        public string Login(Person person)
        {
            string msg = string.Empty;
            try
            { 
                da = new SqlDataAdapter("sp_Login", BBMS);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Name", person.Name);
                da.SelectCommand.Parameters.AddWithValue("@Password", person.Password);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    msg = "Hi, Logging In";
                }
                else
                    msg = "Invalid User";
            }
            catch (Exception e)
            {
                msg = e.Message;
            }

            return msg;

        }
    }
}
