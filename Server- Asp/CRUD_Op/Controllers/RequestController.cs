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
    [RoutePrefix("api/Blood")]
    public class RequestController : ApiController
    {
       
        SqlConnection BBMS = new SqlConnection(ConfigurationManager.ConnectionStrings["BBMS"].ConnectionString);
        SqlCommand cmd = null;
        //SqlDataAdapter da = null;

        [HttpPost]
        [Route("Request")]
        public new string Request(Request receiver)
        {
            string msg = string.Empty;
            try
            {
                string query = @"insert into receiver(Name, BloodGroup, Unit, Hospital, Phone, Status) values ('" + receiver.Name + @"','" + receiver.BloodGroup + @"','" + receiver.Unit + @"' , '" + receiver.Hospital + @"','" + receiver.Phone + @"','" + receiver.Status + @"')";
                cmd = new SqlCommand(query, BBMS);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Name", receiver.Name);
                cmd.Parameters.AddWithValue("@BloodGroup", receiver.BloodGroup);
                cmd.Parameters.AddWithValue("@Unit", receiver.Unit);
                cmd.Parameters.AddWithValue("@Hospital", receiver.Hospital);
                cmd.Parameters.AddWithValue("@Phone", receiver.Phone);
                cmd.Parameters.AddWithValue("@Status", receiver.Status);
             

                BBMS.Open();
                int i = cmd.ExecuteNonQuery();
                BBMS.Close();

                if (i > 0)
                {
                    msg = "Request has been made Successfully";
                }
                else
                {
                    msg = "Error";
                }
            }
            catch (Exception e)
            {
                msg = e.Message;
            }

            return msg;

        }
    }
}
