using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace clsSchool
{
    public class CourseClassBase
    {

        #region PROPERTIES

        public int CourseClassId { get; set; }
        public int CourseId { get; set; }
        public int TeacherId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string CourseDay { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        public bool IsOpen { get; set; }
        public bool IsComplete { get; set; }
        public DateTime Created { get; set; }

        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public string TeacherName { get; set; }
        public string StartTimeLabel { get; set; }
        public string EndTimeLabel { get; set; }

        #endregion

    }



    public class CourseClassDB : CourseClassBase, IDisposable
    {
        #region METHODS (public)


        public int Add(CourseClassBase mClass)
        {
            int CourseClassId = 0;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["csSwim"].ConnectionString);
            string sql = "INSERT INTO CourseClass (CourseId, TeacherId, StartDate, EndDate, CourseDay, StartTime, EndTime, IsOpen, IsComplete)" +
            " VALUES (@CourseId, @TeacherId, @StartDate, @EndDate, @CourseDay, @StartTime, @EndTime, @IsOpen, @IsComplete)" +
            " SELECT SCOPE_IDENTITY()";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add("@CourseId", SqlDbType.Int).Value = mClass.CourseId;
            cmd.Parameters.Add("@TeacherId", SqlDbType.Int).Value = mClass.TeacherId;
            cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = mClass.StartDate;
            cmd.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = mClass.EndDate;
            cmd.Parameters.Add("@CourseDay", SqlDbType.VarChar).Value = mClass.CourseDay;
            cmd.Parameters.Add("@StartTime", SqlDbType.Int).Value = mClass.StartTime;
            cmd.Parameters.Add("@EndTime", SqlDbType.Int).Value = mClass.EndTime;
            cmd.Parameters.Add("@IsOpen", SqlDbType.Bit).Value = mClass.IsOpen;
            cmd.Parameters.Add("@IsComplete", SqlDbType.Bit).Value = mClass.IsComplete;
            try
            {
                con.Open();
                CourseClassId = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                CourseClassId = -1;
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                con.Close();
                con.Dispose();
                cmd.Dispose();
            }
            return CourseClassId;
        }


        public bool Update(CourseClassBase mClass)
        {
            bool mResult = false;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["csSwim"].ConnectionString);
            string sql = "UPDATE CourseClass SET CourseId = @CourseId, TeacherId = @TeacherId, StartDate = @StartDate, EndDate = @EndDate, CourseDay = @CourseDay, StartTime = @StartTime, EndTime = @EndTime, IsOpen = @IsOpen, IsComplete = @IsComplete " +
            "WHERE CourseClassId = @CourseClassId;";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add("@CourseClassId", SqlDbType.Int).Value = mClass.CourseClassId;
            cmd.Parameters.Add("@CourseId", SqlDbType.Int).Value = mClass.CourseId;
            cmd.Parameters.Add("@TeacherId", SqlDbType.Int).Value = mClass.TeacherId;
            cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = mClass.StartDate;
            cmd.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = mClass.EndDate;
            cmd.Parameters.Add("@CourseDay", SqlDbType.VarChar).Value = mClass.CourseDay;
            cmd.Parameters.Add("@StartTime", SqlDbType.Int).Value = mClass.StartTime;
            cmd.Parameters.Add("@EndTime", SqlDbType.Int).Value = mClass.EndTime;
            cmd.Parameters.Add("@IsOpen", SqlDbType.Bit).Value = mClass.IsOpen;
            cmd.Parameters.Add("@IsComplete", SqlDbType.Bit).Value = mClass.IsComplete;
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


        public CourseClassBase GetById(int CourseClassId)
        {
            return Get(" WHERE db.CourseClassId = @CourseClassId", new SqlParameter("@CourseClassId", CourseClassId));
        }


        public List<CourseClassBase> List()
        {
            string WhereClause = "ORDER BY db.TeacherId;";
            return List(WhereClause);
        }

        public List<CourseClassBase> ListComplete(bool IsComplete)
        {
            return List(" WHERE db.IsComplete = @IsComplete ORDER BY db.TeacherId;",
               new SqlParameter("@IsComplete", IsComplete));
        }


        public List<CourseClassBase> ListOpen(bool IsOpen)
        {
            return List(" WHERE db.IsOpen = @IsOpen ORDER BY db.TeacherId;",
               new SqlParameter("@IsOpen", IsOpen));
        }


        public List<CourseClassBase> ListByCourseId(int CourseId)
        {
            return List(" WHERE db.CourseId = @CourseId", new SqlParameter("@CourseId", CourseId));
        }

        public List<CourseClassBase> ListByAllOpenClass()
        {
            bool IsOpen = true;
            return List(" WHERE db.IsOpen = @IsOpen",
                new SqlParameter("@IsOpen", IsOpen));
        }

        public List<CourseClassBase> ListByOpenClass(int CourseId)
        {
            bool IsOpen = true;
            return List(" WHERE db.CourseId = @CourseId AND db.IsOpen = @IsOpen",
                new SqlParameter("@CourseId", CourseId),
                new SqlParameter("@IsOpen", IsOpen));
        }

        public List<CourseClassBase> ListByTeacherId(int TeacherId)
        {
            return List(" WHERE db.TeacherId = @TeacherId", new SqlParameter("@TeacherId", TeacherId));
        }

        public List<CourseClassBase> ListByOpenTeacher(int TeacherId)
        {
            bool IsOpen = true;
            return List(" WHERE db.TeacherId = @TeacherId AND db.IsOpen = @IsOpen",
                new SqlParameter("@TeacherId", TeacherId),
                new SqlParameter("@IsOpen", IsOpen));
        }
        public List<CourseClassBase> ListByStartTime(int StartTime)
        {
            return List(" WHERE db.StartTime = @StartTime", new SqlParameter("@StartTime", StartTime));
        }

        public List<CourseClassBase> ListByEndTime(int EndTime)
        {
            return List(" WHERE db.EndTime = @EndTime", new SqlParameter("@EndTime", EndTime));
        }

        public bool Delete(int CourseClassId)
        {
            bool mResult = false;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["csSwim"].ConnectionString);
            string sql = "DELETE FROM CourseClass WHERE CourseClassId = " + CourseClassId.ToString();
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

        private CourseClassBase Get(string WhereClause, params SqlParameter[] commandparameters)
        {
            CourseClassBase mClass = null;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["csSwim"].ConnectionString);
            string sql = "SELECT db.CourseClassId, db.CourseId, db.TeacherId, db.StartDate, db.EndDate, db.CourseDay, " +
                "db.StartTime, db.EndTime, db.IsOpen, db.IsComplete, db.Created, " +
                "cr.Code AS CourseCode, cr.Name AS CourseName, " +
                "(tc.FirstName + ' ' + tc.LastName) AS TeacherName, " +
                "st.TimeLabel AS StartTimeLabel, et.TimeLabel AS EndTimeLabel " +
                "FROM CourseClass db " +
                "LEFT OUTER JOIN Course cr ON cr.CourseId = db.CourseId " +
                "LEFT OUTER JOIN Teacher tc ON tc.TeacherId = db.TeacherId " +
                "LEFT OUTER JOIN SystemTime st ON st.TimeId = db.StartTime " +
                "LEFT OUTER JOIN SystemTime et ON et.TimeId = db.EndTime " +
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

        private List<CourseClassBase> List(string WhereClause, params SqlParameter[] commandparameters)
        {
            List<CourseClassBase> mList = new List<CourseClassBase>();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["csSwim"].ConnectionString);
            string sql = "SELECT db.CourseClassId, db.CourseId, db.TeacherId, db.StartDate, db.EndDate, db.CourseDay, " +
                "db.StartTime, db.EndTime, db.IsOpen, db.IsComplete, db.Created, " +
                "cr.Code AS CourseCode, cr.Name AS CourseName, " +
                "(tc.FirstName + ' ' + tc.LastName) AS TeacherName, " +
                "st.TimeLabel AS StartTimeLabel, et.TimeLabel AS EndTimeLabel " +
                "FROM CourseClass db " +
                "LEFT OUTER JOIN Course cr ON cr.CourseId = db.CourseId " +
                "LEFT OUTER JOIN Teacher tc ON tc.TeacherId = db.TeacherId " +
                "LEFT OUTER JOIN SystemTime st ON st.TimeId = db.StartTime " +
                "LEFT OUTER JOIN SystemTime et ON et.TimeId = db.EndTime " +
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


        private CourseClassBase LoadRow(SqlDataReader rdr)
        {
            return new CourseClassBase
           {
               CourseClassId = rdr["CourseClassId"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(rdr["CourseClassId"]),
               CourseId = rdr["CourseId"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(rdr["CourseId"]),
               TeacherId = rdr["TeacherId"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(rdr["TeacherId"]),
               StartDate = rdr["StartDate"].Equals(DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(rdr["StartDate"]),
               EndDate = rdr["EndDate"].Equals(DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(rdr["EndDate"]),
               CourseDay = rdr["CourseDay"].Equals(DBNull.Value) ? "" : (rdr["CourseDay"].ToString()),
               StartTime = rdr["StartTime"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(rdr["StartTime"]),
               EndTime = rdr["EndTime"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(rdr["EndTime"]),
               IsOpen = rdr["IsOpen"].Equals(DBNull.Value) ? false : Convert.ToBoolean(rdr["IsOpen"]),
               IsComplete = rdr["IsComplete"].Equals(DBNull.Value) ? false : Convert.ToBoolean(rdr["IsComplete"]),
               Created = rdr["Created"].Equals(DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(rdr["Created"]),

               CourseCode = rdr["CourseCode"].Equals(DBNull.Value) ? "" : (rdr["CourseCode"].ToString()),
               CourseName = rdr["CourseName"].Equals(DBNull.Value) ? "" : (rdr["CourseName"].ToString()),
               TeacherName = rdr["TeacherName"].Equals(DBNull.Value) ? "" : (rdr["TeacherName"].ToString()),

               StartTimeLabel = rdr["StartTimeLabel"].Equals(DBNull.Value) ? "" : (rdr["StartTimeLabel"].ToString()),
               EndTimeLabel = rdr["EndTimeLabel"].Equals(DBNull.Value) ? "" : (rdr["EndTimeLabel"].ToString()),
           };

        }

        private void Populate(CourseClassBase mClass)
        {
            this.CourseClassId = mClass.CourseClassId;
            this.CourseId = mClass.CourseId;
            this.TeacherId = mClass.TeacherId;
            this.StartDate = mClass.StartDate;
            this.EndDate = mClass.EndDate;
            this.CourseDay = mClass.CourseDay;
            this.StartTime = mClass.StartTime;
            this.EndTime = mClass.EndTime;
            this.IsOpen = mClass.IsOpen;
            this.IsComplete = mClass.IsComplete;
            this.Created = mClass.Created;

            this.CourseCode = mClass.CourseCode;
            this.CourseName = mClass.CourseName;
            this.TeacherName = mClass.TeacherName;

        }


        #endregion

        #region CONSTRUCTOR/DESTRUCTOR


        private bool disposed = false;

        public CourseClassDB()
        {

        }

        public CourseClassDB(int CourseClassId)
        {
            Populate(GetById(CourseClassId));
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
        ~CourseClassDB()
        {
            Dispose(false);
        }
        #endregion


    }
}

