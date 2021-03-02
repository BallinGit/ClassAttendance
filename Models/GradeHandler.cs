using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ClassAttendance.Models
{
    public class GradeHandler
    {
        public string constring = @"Data Source=.\SQLExpress;Initial Catalog=ClassAttendanceDB;Integrated Security=True;Pooling=False";
        public List<GradeModel> GetGradeList()
        {

            List<GradeModel> iList = new List<GradeModel>();

            using (SqlConnection con = new SqlConnection(constring))
            {
                string query = "select * from Grade";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();

                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            foreach (DataRow dr in dt.Rows)
                            {
                                iList.Add(new GradeModel
                                {
                                    GradeID = Convert.ToInt32(dr["GradeID"]),
                                    GradeName = Convert.ToString(dr["GradeName"])
                                });
                            }
                        }
                    }
                }

            }


            return iList;
        }
    }
}
