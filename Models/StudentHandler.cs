using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ClassAttendance.Models
{
    public class StudentHandler
    {
        public string constring = @"Data Source=.\SQLExpress;Initial Catalog=ClassAttendanceDB;Integrated Security=True;Pooling=False";
        public List<StudentModel> GetStudentList()
        {
            List<StudentModel> iList = new List<StudentModel>();

            using (SqlConnection con = new SqlConnection(constring))
            {
                string query = "select s.StudentID,s.StudentFullName,c.ClassID,c.ClassName from Student s left join Class c on s.ClassID = c.ClassID";
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
                                iList.Add(new StudentModel
                                {
                                    StudentID = Convert.ToInt32(dr["StudentID"]),
                                    StudentFullName = Convert.ToString(dr["StudentFullName"]),
                                    Classes = new ClassModel() { ClassID = Convert.ToInt32(dr["ClassID"]), ClassName = Convert.ToString(dr["ClassName"]) }
                                });
                            }
                        }
                    }
                }
            }
            return iList;
        }
        public bool InsertStudent(StudentModel iList)
        {
            using (SqlConnection con = new SqlConnection(constring))
            {
                string query = "Insert Into Student(StudentFullName,ClassID) values('" + iList.StudentFullName + "'," + iList.Classes.ClassID + ")";
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

        public bool UpdateStudent(StudentModel iList)
        {
            using (SqlConnection con = new SqlConnection(constring))
            {
                string query = "Update Student set StudentFullName ='" + iList.StudentFullName + "', ClassID =" + iList.Classes.ClassID + " where StudentID = " + iList.StudentID;
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

        public bool DeleteStudent(int id)
        {

            using (SqlConnection con = new SqlConnection(constring))
            {
                string query = "Delete from Student where StudentID = " + id;
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
