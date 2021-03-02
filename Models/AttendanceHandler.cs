using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ClassAttendance.Models
{
    public class AttendanceHandler
    {
        public string constring = @"Data Source=.\SQLExpress;Initial Catalog=ClassAttendanceDB;Integrated Security=True;Pooling=False";

        public List<AttendanceModel> GetAttendanceList(int ClassID)
        {

            List<AttendanceModel> iList = new List<AttendanceModel>();

            using (SqlConnection con = new SqlConnection(constring))
            {
                string query = "select c.ClassID, c.ClassName,s.StudentId, s.StudentFullName,a.AttendanceDateTime from Attendance a left join Class c on c.ClassID = a.ClassID left join Student s on s.StudentID = a.StudentID where c.ClassID = " + ClassID + " and cast(a.AttendanceDateTime as date)  = cast(GetDate() as date)";
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
                                iList.Add(new AttendanceModel
                                {
                                    Classes = new ClassModel { ClassID = Convert.ToInt32(dr["ClassID"]), ClassName = Convert.ToString(dr["ClassName"]) },
                                    Students = new StudentModel { StudentID = Convert.ToInt32(dr["StudentID"]), StudentFullName = Convert.ToString(dr["StudentFullName"]) },
                             
                                    AttendanceDateTime = Convert.ToDateTime(dr["AttendanceDateTime"])
                                });
                            }
                        }
                    }
                }
            }
            return iList;
        }
        public bool InsertAttendance(AttendanceModel iList)
        {
            using (SqlConnection con = new SqlConnection(constring))
            {
                string query = "Insert Into Attendance(ClassID,StudentID,AttendanceDateTime) values(" + iList.Classes.ClassID + "," + iList.Students.StudentID + ",'" + iList.AttendanceDateTime + "')";
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

        public bool UpdateAttendance(AttendanceModel iList)
        {

            using (SqlConnection con = new SqlConnection(constring))
            {
                string query = "Update Attendance set ClassID =" + iList.Classes.ClassID + ", StudentID =" + iList.Students.StudentID + ",AttendanceDateTime = '" + iList.AttendanceDateTime + "' where AttendanceID = " + iList.AttendanceID;
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
        public bool DeleteAttendance(int id)
        {
            using (SqlConnection con = new SqlConnection(constring))
            {
                string query = "Delete from Attendance where AttendanceID = " + id;
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
