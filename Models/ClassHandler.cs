using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;



namespace ClassAttendance.Models
{
    public class ClassHandler
    {
        public string constring = @"Data Source=.\SQLExpress;Initial Catalog=ClassAttendanceDB;Integrated Security=True;Pooling=False";
        public List<ClassModel> GetClassList()
        {

            List<ClassModel> iList = new List<ClassModel>();

            using (SqlConnection con = new SqlConnection(constring))
            {
                string query = "select c.ClassID,c.ClassName,g.GradeID,g.GradeName from Class c left join Grade g on c.GradeID = g.GradeID";
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
                                iList.Add(new ClassModel
                                {
                                    ClassID = Convert.ToInt32(dr["ClassID"]),
                                    ClassName = Convert.ToString(dr["ClassName"]),
                                    Grade = new GradeModel() { GradeID = Convert.ToInt32(dr["GradeID"]), GradeName = Convert.ToString(dr["GradeName"]) }
                                });
                            }
                        }
                    }
                }
            }
            return iList;
        }

        public bool InsertClass(ClassModel iList)
        {
            using (SqlConnection con = new SqlConnection(constring))
            {
                string query = "Insert Into Class(ClassName,GradeID) values('" + iList.ClassName + "'," + iList.Grade.GradeID + ")";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    if (i >= 1)
                    { return true; }
                    else
                    { return false; }
                }

            }
        }

        public bool UpdateClass(ClassModel iList)
        {

            using (SqlConnection con = new SqlConnection(constring))
            {
                string query = "Update Class set ClassName ='" + iList.ClassName + "', GradeID =" + iList.Grade.GradeID + " where ClassID = " + iList.ClassID;
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    if (i >= 1)
                    { return true; }
                    else
                    { return false; }
                }

            }
        }

        public bool DeleteClass(int id)
        {

            using (SqlConnection con = new SqlConnection(constring))
            {
                string query = "Delete from Class where ClassID = " + id;
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    if (i >= 1)
                    { return true; }
                    else
                    { return false; }
                }

            }

        }
    }
}
