using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ClassAttendance.Models
{
    public class ReportHandler
    {
        public string constring = @"Data Source=.\SQLExpress;Initial Catalog=ClassAttendanceDB;Integrated Security=True;Pooling=False";

        public List<ReportModel> GetReportList(int TermID)
        {

            DateTime StartDate = new DateTime();
            DateTime EndDate = new DateTime();

            if (TermID == 1)
            {
                StartDate = new DateTime(DateTime.Now.Year, 1, 1);
                EndDate = new DateTime(DateTime.Now.Year, 3, 31);
            }
            else if (TermID == 2)
            {
                StartDate = new DateTime(DateTime.Now.Year, 4, 1);
                EndDate = new DateTime(DateTime.Now.Year, 6, 30);
            }
            else if (TermID == 3)
            {
                StartDate = new DateTime(DateTime.Now.Year, 7, 1);
                EndDate = new DateTime(DateTime.Now.Year, 9, 30);
            }
            else
            {
                StartDate = new DateTime(DateTime.Now.Year, 10, 1);
                EndDate = new DateTime(DateTime.Now.Year, 12, 31);
            }



            List<ReportModel> iList = new List<ReportModel>();

            using (SqlConnection con = new SqlConnection(constring))
            {
                string query = "[GetReportByTerm]";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StartTime", SqlDbType.DateTime).Value = StartDate;
                    cmd.Parameters.AddWithValue("@EndTime", SqlDbType.DateTime).Value = EndDate;
                    con.Open();

                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            foreach (DataRow dr in dt.Rows)
                            {
                                iList.Add(new ReportModel
                                {
                                    Classes = new ClassModel { ClassID = 0, ClassName = Convert.ToString(dr["ClassName"]) },
                                    Grades = new GradeModel { GradeID = 0, GradeName = Convert.ToString(dr["GradeName"]) },
                                    Students = new StudentModel { StudentID = 0, StudentFullName = Convert.ToString(dr["StudentFullName"]) },
                                    ClassesAttended = Convert.ToInt32(dr["ClassesAttended"]),
                                    ClassesMissed = Convert.ToInt32(dr["ClassesMissed"])

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
