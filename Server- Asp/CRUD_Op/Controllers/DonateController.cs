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
    public class DonateController : ApiController
    {

        SqlConnection BBMS = new SqlConnection(ConfigurationManager.ConnectionStrings["BBMS"].ConnectionString);
        SqlCommand cmd = null;
        //SqlDataAdapter da = null;

        [HttpPost]
        [Route("Donate")]
        public string Donate(Donate donor)
        {
            string msg = string.Empty;
            try
            {
                string query = @"insert into donor(Name, BloodGroup, Unit, Hospital, Phone, Status) values ('" + donor.Name + @"','" + donor.BloodGroup + @"','" + donor.Unit + @"' , '" + donor.Hospital + @"','" + donor.Phone + @"','" + donor.Status + @"')";
                cmd = new SqlCommand(query, BBMS);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Name", donor.Name);
                cmd.Parameters.AddWithValue("@BloodGroup", donor.BloodGroup);
                cmd.Parameters.AddWithValue("@Unit", donor.Unit);
                cmd.Parameters.AddWithValue("@Hospital", donor.Hospital);
                cmd.Parameters.AddWithValue("@Phone", donor.Phone);
                cmd.Parameters.AddWithValue("@Status", donor.Status);

                BBMS.Open();
                int i = cmd.ExecuteNonQuery();
                BBMS.Close();

                if (i > 0)
                {
                    msg = "Donation Scheduled Successfully";
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
