using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using Stage2.Models;

namespace Stage2.StudentRepo
{
    public class Repo
    {
      
       
        private SqlConnection con;
        private void connection()
        {
            string str = ConfigurationManager.ConnectionStrings["MyConnectionString"].ToString();
            con = new SqlConnection(str);

        }
        public void addstudent(Student obj)
        {
            var studentid = "STDP01";
            string strcon = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(strcon);
            con.Open();
            SqlCommand cmd1 = new SqlCommand("Select * from student order by studentid desc", con);

            SqlDataReader dr = cmd1.ExecuteReader();
            if (dr.Read())
            {
                studentid = dr["studentid"].ToString();
                string nums = new String(studentid.Where(Char.IsDigit).ToArray());
                long number = Int64.Parse(nums);
                number++;
                nums = number.ToString();
                if (nums.Length == 4)
                    studentid = "STD000" + nums;
                if (nums.Length == 5)
                    studentid = "STD00" + nums;
                if (nums.Length == 6)
                    studentid = "STD0" + nums;
                if (nums.Length == 7)
                    studentid = "STD" + nums;
            }
            else
                studentid = " STD0001001";

            con.Close();

            // var StudentID = "STD0001";
            var res = obj.studentid;
            connection();
            con.Open();
            SqlCommand cmd = new SqlCommand("Stud_inserts", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@studentid", studentid);
            cmd.Parameters.AddWithValue("@name", obj.name);
            cmd.Parameters.AddWithValue("@dob", obj.DOB);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public List<Student> getstudentmodel()
        {
            connection();
            List<Student> obj = new List<Student>();
            SqlCommand cmd = new SqlCommand("proc_getdata", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            foreach (DataRow dr in dt.Rows)
            {
                obj.Add(new Student
                {
                    studentid = Convert.ToString(dr["studentid"]),
                    name = Convert.ToString(dr["name"]),
                    DOB = Convert.ToDateTime(dr["DOB"]),
                    CreatedOn = Convert.ToDateTime(dr["createdon"])
                });
            }
            return obj;
        }


        public void UpdateStudents(Student obj)
        {
            connection();
            SqlCommand cmd = new SqlCommand("proc_update", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@studentid", obj.studentid);
            cmd.Parameters.AddWithValue("@name", obj.name);
            cmd.Parameters.AddWithValue("@dob", obj.DOB);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

        }
        public Student GetStudentByID(string StudentID)
        {
            connection();
            Student obj = new Student();
            SqlCommand cmd = new SqlCommand("proc_read_id ", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@studentid", StudentID);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            foreach (DataRow dr in dt.Rows)
            {


                obj.studentid = Convert.ToString(dr["studentid"]);
                obj.name = Convert.ToString(dr["name"]);
                obj.DOB = Convert.ToDateTime(dr["dob"]);
                obj.CreatedOn = Convert.ToDateTime(dr["createdon"]);



            }
            return obj;
        }

    
    public void delete(string eid)
        {
            connection();
            SqlCommand cmd = new SqlCommand("proc_deletes", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@studentID", eid);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

    }
    }
