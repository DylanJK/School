using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace clsSchool
{
    public class EnrollmentBase
    {

        #region PROPERTIES

        public int EnrollmentId { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public int ClassId { get; set; }
        public DateTime StartDate { get; set; }
        public bool HasPaid { get; set; }
        public bool Completed { get; set; }
        public DateTime Created { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }

        #endregion

    }



    public class EnrollmentDB : EnrollmentBase, IDisposable
    {
        #region METHODS (public)


        public int Add(EnrollmentBase mClass)
        {
            int EnrollmentId = 0;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["csSwim"].ConnectionString);
            string sql = "INSERT INTO Enrollment (StudentId, CourseId, ClassId, StartDate, HasPaid, Completed)" +
            " VALUES (@StudentId, @CourseId, @ClassId, @StartDate, @HasPaid, @Completed)" +
            " SELECT SCOPE_IDENTITY()";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add("@StudentId", SqlDbType.Int).Value = mClass.StudentId;
            cmd.Parameters.Add("@CourseId", SqlDbType.Int).Value = mClass.CourseId;
            cmd.Parameters.Add("@ClassId", SqlDbType.Int).Value = mClass.ClassId;
            cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = mClass.StartDate;
            cmd.Parameters.Add("@HasPaid", SqlDbType.Bit).Value = mClass.HasPaid;
            cmd.Parameters.Add("@Completed", SqlDbType.Bit).Value = mClass.Completed;
            try
            {
                con.Open();
                EnrollmentId = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                EnrollmentId = -1;
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                con.Close();
                con.Dispose();
                cmd.Dispose();
            }
            return EnrollmentId;
        }


        public bool Update(EnrollmentBase mClass)
        {
            bool mResult = false;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["csSwim"].ConnectionString);
            string sql = "UPDATE Enrollment SET StudentId = @StudentId, CourseId = @CourseId, ClassId = @ClassId, StartDate = @StartDate, HasPaid = @HasPaid, Completed = @Completed " +
            "WHERE EnrollmentId = @EnrollmentId;";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add("@EnrollmentId", SqlDbType.Int).Value = mClass.EnrollmentId;
            cmd.Parameters.Add("@StudentId", SqlDbType.Int).Value = mClass.StudentId;
            cmd.Parameters.Add("@CourseId", SqlDbType.Int).Value = mClass.CourseId;
            cmd.Parameters.Add("@ClassId", SqlDbType.Int).Value = mClass.ClassId;
            cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = mClass.StartDate;
            cmd.Parameters.Add("@HasPaid", SqlDbType.Bit).Value = mClass.HasPaid;
            cmd.Parameters.Add("@Completed", SqlDbType.Bit).Value = mClass.Completed;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                mResult = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                con.Close();
                con.Dispose();
                cmd.Dispose();
            }
            return mResult;

        }


        public EnrollmentBase GetById(int EnrollmentId)
        {
            return Get(" WHERE db.EnrollmentId = @EnrollmentId", new SqlParameter("@EnrollmentId", EnrollmentId));
        }


        public List<EnrollmentBase> List()
        {
            string WhereClause = "";
            return List(WhereClause);
        }


        public List<EnrollmentBase> Search(string SearchTerm)
        {
            string WhereClause = string.Format("INNER JOIN CONTAINSTABLE(Product, (Name, Description), '{0}') AS t ON db.ProductId = t.[Key] ORDER BY t.[Rank]", SearchTerm);
            return List(WhereClause);
        }


        public List<EnrollmentBase> ListByStudentId(int StudentId)
        {
            return List(" WHERE db.StudentId = @StudentId", new SqlParameter("@StudentId", StudentId));
        }

        public List<EnrollmentBase> ListByCourseId(int CourseId)
        {
            return List(" WHERE db.CourseId = @CourseId", new SqlParameter("@CourseId", CourseId));
        }

        public List<EnrollmentBase> ListByClassId(int ClassId)
        {
            return List(" WHERE db.ClassId = @ClassId", new SqlParameter("@ClassId", ClassId));
        }


        public bool Delete(int EnrollmentId)
        {
            bool mResult = false;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["csSwim"].ConnectionString);
            string sql = "DELETE FROM Enrollment WHERE EnrollmentId = " + EnrollmentId.ToString();
            SqlCommand cmd = new SqlCommand(sql, con);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                mResult = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                con.Close();
                con.Dispose();
                cmd.Dispose();
            }
            return mResult;

        }


        #endregion


        #region METHODS (private)

        private EnrollmentBase Get(string WhereClause, params SqlParameter[] commandparameters)
        {
            EnrollmentBase mClass = null;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["csSwim"].ConnectionString);
            string sql = "SELECT db.EnrollmentId, db.StudentId, db.CourseId, db.ClassId, db.StartDate, " +
                "db.HasPaid, db.Completed, db.Created, " +
                "cr.Code AS CourseCode, cr.Name AS CourseName, " +
                "sn.FirstName, sn.LastName " +
                "FROM Enrollment db " +
                "LEFT OUTER JOIN Course cr ON cr.CourseId = db.CourseId " +
                "LEFT OUTER JOIN Student sn ON sn.StudentId = db.StudentId " +
                WhereClause;
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader rdr = null;
            foreach (var parameter in commandparameters)
            {
                cmd.Parameters.Add(parameter);
            }
            try
            {
                con.Open();
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    mClass = LoadRow(rdr);
                }
            }
            catch (Exception ex)
            {
                mClass = null;
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                con.Close();
                con.Dispose();
                cmd.Dispose();
            }
            return mClass;

        }

        private List<EnrollmentBase> List(string WhereClause, params SqlParameter[] commandparameters)
        {
            List<EnrollmentBase> mList = new List<EnrollmentBase>();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["csSwim"].ConnectionString);
            string sql = "SELECT db.EnrollmentId, db.StudentId, db.CourseId, db.ClassId, db.StartDate, " +
                "db.HasPaid, db.Completed, db.Created, " +
                "cr.Code AS CourseCode, cr.Name AS CourseName, " +
                "sn.FirstName, sn.LastName " +
                "FROM Enrollment db " +
                "LEFT OUTER JOIN Course cr ON cr.CourseId = db.CourseId " +
                "LEFT OUTER JOIN Student sn ON sn.StudentId = db.StudentId " +
                WhereClause;
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader rdr = null;
            foreach (var parameter in commandparameters)
            {
                cmd.Parameters.Add(parameter);
            }
            try
            {
                con.Open();
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    mList.Add(LoadRow(rdr));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                con.Close();
                con.Dispose();
                cmd.Dispose();
            }
            return mList;

        }

        private EnrollmentBase LoadRow(SqlDataReader rdr)
        {
            return new EnrollmentBase
            {
                EnrollmentId = rdr["EnrollmentId"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(rdr["EnrollmentId"]),
                StudentId = rdr["StudentId"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(rdr["StudentId"]),
                CourseId = rdr["CourseId"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(rdr["CourseId"]),
                ClassId = rdr["ClassId"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(rdr["ClassId"]),
                StartDate = rdr["StartDate"].Equals(DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(rdr["StartDate"]),
                HasPaid = rdr["HasPaid"].Equals(DBNull.Value) ? false : Convert.ToBoolean(rdr["HasPaid"]),
                Completed = rdr["Completed"].Equals(DBNull.Value) ? false : Convert.ToBoolean(rdr["Completed"]),
                Created = rdr["Created"].Equals(DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(rdr["Created"]),

                CourseCode = rdr["CourseCode"].Equals(DBNull.Value) ? "" : (rdr["CourseCode"].ToString()),
                CourseName = rdr["CourseName"].Equals(DBNull.Value) ? "" : (rdr["CourseName"].ToString()),
                FirstName = rdr["FirstName"].Equals(DBNull.Value) ? "" : (rdr["FirstName"].ToString()),
                LastName = rdr["LastName"].Equals(DBNull.Value) ? "" : (rdr["LastName"].ToString()),
            };

        }

        private void Populate(EnrollmentBase mClass)
        {
            this.EnrollmentId = mClass.EnrollmentId;
            this.StudentId = mClass.StudentId;
            this.CourseId = mClass.CourseId;
            this.ClassId = mClass.ClassId;
            this.StartDate = mClass.StartDate;
            this.HasPaid = mClass.HasPaid;
            this.Completed = mClass.Completed;
            this.Created = mClass.Created;

            this.CourseCode = mClass.CourseCode;
            this.CourseName = mClass.CourseName;
        }


        #endregion


        #region CONSTRUCTOR/DESTRUCTOR

        private bool disposed = false;

        public EnrollmentDB()
        {

        }

        public EnrollmentDB(int EnrollmentId)
        {
            Populate(GetById(EnrollmentId));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
            }
            disposed = true;
        }
        ~EnrollmentDB()
        {
            Dispose(false);
        }
        #endregion


    }
}

