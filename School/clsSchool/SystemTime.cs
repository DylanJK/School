using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace clsSchool
{
    public class SystemTimeBase
    {

        #region PROPERTIES

        public int ClassTimeId { get; set; }
        public int TimeId { get; set; }
        public string TimeLabel { get; set; }
        public bool StartTime { get; set; }
        public bool EndTime { get; set; }

        #endregion

    }



    public class SystemTimeDB : SystemTimeBase, IDisposable
    {
        #region METHODS (public)


        public int Add(SystemTimeBase mClass)
        {
            int ClassTimeId = 0;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["csSwim"].ConnectionString);
            string sql = "INSERT INTO SystemTime (TimeId, TimeLabel, StartTime, EndTime)" +
            " VALUES (@TimeId, @TimeLabel, @StartTime, @EndTime)" +
            " SELECT SCOPE_IDENTITY()";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add("@TimeId", SqlDbType.Int).Value = mClass.TimeId;
            cmd.Parameters.Add("@TimeLabel", SqlDbType.VarChar).Value = mClass.TimeLabel;
            cmd.Parameters.Add("@StartTime", SqlDbType.Bit).Value = mClass.StartTime;
            cmd.Parameters.Add("@EndTime", SqlDbType.Bit).Value = mClass.EndTime;
            try
            {
                con.Open();
                ClassTimeId = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                ClassTimeId = -1;
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                con.Close();
                con.Dispose();
                cmd.Dispose();
            }
            return ClassTimeId;
        }


        public bool Update(SystemTimeBase mClass)
        {
            bool mResult = false;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["csSwim"].ConnectionString);
            string sql = "UPDATE SystemTime SET TimeId = @TimeId, TimeLabel = @TimeLabel, StartTime = @StartTime, EndTime = @EndTime " +
            "WHERE ClassTimeId = @ClassTimeId;";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add("@ClassTimeId", SqlDbType.Int).Value = mClass.ClassTimeId;
            cmd.Parameters.Add("@TimeId", SqlDbType.Int).Value = mClass.TimeId;
            cmd.Parameters.Add("@TimeLabel", SqlDbType.VarChar).Value = mClass.TimeLabel;
            cmd.Parameters.Add("@StartTime", SqlDbType.Bit).Value = mClass.StartTime;
            cmd.Parameters.Add("@EndTime", SqlDbType.Bit).Value = mClass.EndTime;
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


        public SystemTimeBase GetById(int ClassTimeId)
        {
            return Get(" WHERE db.ClassTimeId = @ClassTimeId", new SqlParameter("@ClassTimeId", ClassTimeId));
        }


        public List<SystemTimeBase> List()
        {
            string WhereClause = " ORDER BY db.TimeId";
            return List(WhereClause);
        }

        public List<SystemTimeBase> ListStartTimes()
        {
            return List(" WHERE db.StartTime = @StartTime", new SqlParameter("@StartTime", true));
        }

        public List<SystemTimeBase> ListEndTimes()
        {
            return List(" WHERE db.EndTime = @EndTime", new SqlParameter("@EndTime", true));
        }
       
        public List<SystemTimeBase> ListByTimeId(int TimeId)
        {
            return List(" WHERE db.TimeId = @TimeId", new SqlParameter("@TimeId", TimeId));
        }


        public bool Delete(int ClassTimeId)
        {
            bool mResult = false;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["csSwim"].ConnectionString);
            string sql = "DELETE FROM SystemTime WHERE ClassTimeId = " + ClassTimeId.ToString();
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

        private SystemTimeBase Get(string WhereClause, params SqlParameter[] commandparameters)
        {
            SystemTimeBase mClass = null;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["csSwim"].ConnectionString);
            string sql = "SELECT db.ClassTimeId, db.TimeId, db.TimeLabel, db.StartTime, db.EndTime " +
            "FROM SystemTime db " + WhereClause;
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

        private List<SystemTimeBase> List(string WhereClause, params SqlParameter[] commandparameters)
        {
            List<SystemTimeBase> mList = new List<SystemTimeBase>();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["csSwim"].ConnectionString);
            string sql = "SELECT db.ClassTimeId, db.TimeId, db.TimeLabel, db.StartTime, db.EndTime " +
            "FROM SystemTime db " + WhereClause;
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


        private SystemTimeBase LoadRow(SqlDataReader rdr)
        {
            return new SystemTimeBase
            {
                ClassTimeId = rdr["ClassTimeId"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(rdr["ClassTimeId"]),
                TimeId = rdr["TimeId"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(rdr["TimeId"]),
                TimeLabel = rdr["TimeLabel"].Equals(DBNull.Value) ? "" : (rdr["TimeLabel"].ToString()),
                StartTime = rdr["StartTime"].Equals(DBNull.Value) ? false : Convert.ToBoolean(rdr["StartTime"]),
                EndTime = rdr["EndTime"].Equals(DBNull.Value) ? false : Convert.ToBoolean(rdr["EndTime"]),
            };

        }

        private void Populate(SystemTimeBase mClass)
        {
            this.ClassTimeId = mClass.ClassTimeId;
            this.TimeId = mClass.TimeId;
            this.TimeLabel = mClass.TimeLabel;
            this.StartTime = mClass.StartTime;
            this.EndTime = mClass.EndTime;
        }


        #endregion

        #region CONSTRUCTOR/DESTRUCTOR


        private bool disposed = false;

        public SystemTimeDB()
        {

        }

        public SystemTimeDB(int ClassTimeId)
        {
            Populate(GetById(ClassTimeId));
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
        ~SystemTimeDB()
        {
            Dispose(false);
        }
        #endregion


    }
}

