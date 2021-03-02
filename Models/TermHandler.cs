using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ClassAttendance.Models
{
    public class TermHandler
    {
        public string constring = @"Data Source=.\SQLExpress;Initial Catalog=ClassAttendanceDB;Integrated Security=True;Pooling=False";
        public List<TermModel> GetTermList()
        {

            List<TermModel> iList = new List<TermModel>();

            using (SqlConnection con = new SqlConnection(constring))
            {
                string query = "select * from Term";
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
                                iList.Add(new TermModel
                                {
                                    TermID = Convert.ToInt32(dr["TermID"]),
                                    TermName = Convert.ToString(dr["TermName"])
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
