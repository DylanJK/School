using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace clsSchool
{
    public class CourseBase
    {

        #region PROPERTIES

        public int CourseId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Fee { get; set; }
        public bool Active { get; set; }
        public DateTime Created { get; set; }

        #endregion

    }



    public class CourseDB : CourseBase, IDisposable
    {
        #region METHODS (public)


        public int Add(CourseBase mClass)
        {
            int CourseId = 0;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["csSwim"].ConnectionString);
            string sql = "INSERT INTO Course (Code, Name, Description, Fee, Active)" +
            " VALUES (@Code, @Name, @Description, @Fee, @Active)" +
            " SELECT SCOPE_IDENTITY()";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add("@Code", SqlDbType.VarChar).Value = mClass.Code;
            cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = mClass.Name;
            cmd.Parameters.Add("@Description", SqlDbType.VarChar).Value = mClass.Description;
            cmd.Parameters.Add("@Fee", SqlDbType.Float).Value = mClass.Fee;
            cmd.Parameters.Add("@Active", SqlDbType.Bit).Value = mClass.Active;
            try
            {
                con.Open();
                CourseId = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                CourseId = -1;
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                con.Close();
                con.Dispose();
                cmd.Dispose();
            }
            return CourseId;
        }

        public bool Update(CourseBase mClass)
        {
            bool mResult = false;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["csSwim"].ConnectionString);
            string sql = "UPDATE Course SET Code = @Code, Name = @Name, Description = @Description, Fee = @Fee, Active = @Active " +
            "WHERE CourseId = @CourseId;";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add("@CourseId", SqlDbType.Int).Value = mClass.CourseId;
            cmd.Parameters.Add("@Code", SqlDbType.VarChar).Value = mClass.Code;
            cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = mClass.Name;
            cmd.Parameters.Add("@Description", SqlDbType.VarChar).Value = mClass.Description;
            cmd.Parameters.Add("@Fee", SqlDbType.Float).Value = mClass.Fee;
            cmd.Parameters.Add("@Active", SqlDbType.Bit).Value = mClass.Active;
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

        public CourseBase GetById(int CourseId)
        {
            return Get(" WHERE db.CourseId = @CourseId", new SqlParameter("@CourseId", CourseId));
        }

        public CourseBase GetByCode(string CourseCode)
        {
            return Get(" WHERE db.Code = @Code", new SqlParameter("@Code", CourseCode));
        }

        public List<CourseBase> List()
        {
            string WhereClause = "ORDER BY db.Name;";
            return List(WhereClause);
        }

        public List<CourseBase> ListActive(bool Active)
        {
            return List(" WHERE db.Active = @Active ORDER BY db.Name;",
               new SqlParameter("@Active", Active));
        }


        public List<CourseBase> Search(string SearchTerm)
        {
            string WhereClause = string.Format("INNER JOIN CONTAINSTABLE(Product, (Name, Description), '{0}') AS t ON db.ProductId = t.[Key] ORDER BY t.[Rank]", SearchTerm);
            return List(WhereClause);
        }

        public bool Delete(int CourseId)
        {
            bool mResult = false;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["csSwim"].ConnectionString);
            string sql = "DELETE FROM Course WHERE CourseId = " + CourseId.ToString();
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

        private CourseBase Get(string WhereClause, params SqlParameter[] commandparameters)
        {
            CourseBase mClass = null;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["csSwim"].ConnectionString);
            string sql = "SELECT db.CourseId, db.Code, db.Name, db.Description, db.Fee, db.Active, db.Created " +
            "FROM Course db " + WhereClause;
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

        private List<CourseBase> List(string WhereClause, params SqlParameter[] commandparameters)
        {
            List<CourseBase> mList = new List<CourseBase>();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["csSwim"].ConnectionString);
            string sql = "SELECT db.CourseId, db.Code, db.Name, db.Description, db.Fee, db.Active, db.Created " +
            "FROM Course db " + WhereClause;
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


        private CourseBase LoadRow(SqlDataReader rdr)
        {
            return new CourseBase
            {
                CourseId = rdr["CourseId"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(rdr["CourseId"]),
                Code = rdr["Code"].Equals(DBNull.Value) ? "" : (rdr["Code"].ToString()),
                Name = rdr["Name"].Equals(DBNull.Value) ? "" : (rdr["Name"].ToString()),
                Description = rdr["Description"].Equals(DBNull.Value) ? "" : (rdr["Description"].ToString()),
                Fee = rdr["Fee"].Equals(DBNull.Value) ? 0 : Convert.ToDouble(rdr["Fee"]),
                Active = rdr["Active"].Equals(DBNull.Value) ? false : Convert.ToBoolean(rdr["Active"]),
                Created = rdr["Created"].Equals(DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(rdr["Created"]),
            };

        }

        private void Populate(CourseBase mClass)
        {
            this.CourseId = mClass.CourseId;
            this.Code = mClass.Code;
            this.Name = mClass.Name;
            this.Description = mClass.Description;
            this.Fee = mClass.Fee;
            this.Active = mClass.Active;
            this.Created = mClass.Created;
        }


        #endregion

        #region CONSTRUCTOR/DESTRUCTOR


        private bool disposed = false;

        public CourseDB()
        {

        }

        public CourseDB(int CourseId)
        {
            Populate(GetById(CourseId));
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
        ~CourseDB()
        {
            Dispose(false);
        }
        #endregion


    }
}

