
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace clsSchool
{
    public class TeacherBase
    {

        #region PROPERTIES

        public int TeacherId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }

        #endregion


    }



    public class TeacherDB : TeacherBase, IDisposable
    {
        #region METHODS (public)


        public int Add(TeacherBase mClass)
        {
            int TeacherId = 0;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["csSwim"].ConnectionString);
            string sql = "INSERT INTO Teacher (FirstName, LastName, Phone, Mobile, Email, Password, IsActive)" +
            " VALUES (@FirstName, @LastName, @Phone, @Mobile, @Email, @Password, @IsActive)" +
            " SELECT SCOPE_IDENTITY()";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = mClass.FirstName;
            cmd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = mClass.LastName;
            cmd.Parameters.Add("@Phone", SqlDbType.VarChar).Value = mClass.Phone;
            cmd.Parameters.Add("@Mobile", SqlDbType.VarChar).Value = mClass.Mobile;
            cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = mClass.Email;
            cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = mClass.Password;
            cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = mClass.IsActive;
            try
            {
                con.Open();
                TeacherId = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                TeacherId = -1;
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                con.Close();
                con.Dispose();
                cmd.Dispose();
            }
            return TeacherId;
        }


        public bool Update(TeacherBase mClass)
        {
            bool mResult = false;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["csSwim"].ConnectionString);
            string sql = "UPDATE Teacher SET FirstName = @FirstName, LastName = @LastName, Phone = @Phone, Mobile = @Mobile, Email = @Email, Password = @Password, IsActive = @IsActive " +
            "WHERE TeacherId = @TeacherId;";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add("@TeacherId", SqlDbType.Int).Value = mClass.TeacherId;
            cmd.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = mClass.FirstName;
            cmd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = mClass.LastName;
            cmd.Parameters.Add("@Phone", SqlDbType.VarChar).Value = mClass.Phone;
            cmd.Parameters.Add("@Mobile", SqlDbType.VarChar).Value = mClass.Mobile;
            cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = mClass.Email;
            cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = mClass.Password;
            cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = mClass.IsActive;
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


        public TeacherBase GetById(int TeacherId)
        {
            return Get(" WHERE db.TeacherId = @TeacherId", new SqlParameter("@TeacherId", TeacherId));
        }

        public TeacherBase GetByEmail(string Email)
        {
            return Get(" WHERE db.Email = @Email", new SqlParameter("@Email", Email));
        }

        public List<TeacherBase> List()
        {
            string WhereClause = " ORDER BY db.LastName;";
            return List(WhereClause);
        }

        public List<TeacherBase> ListActive(bool IsActive)
        {
            return List(" WHERE db.IsActive = @IsActive ORDER BY db.LastName;",
               new SqlParameter("@IsActive", IsActive));
        }

        public bool Delete(int TeacherId)
        {
            bool mResult = false;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["csSwim"].ConnectionString);
            string sql = "DELETE FROM Teacher WHERE TeacherId = " + TeacherId.ToString();
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

        private TeacherBase Get(string WhereClause, params SqlParameter[] commandparameters)
        {
            TeacherBase mClass = null;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["csSwim"].ConnectionString);
            string sql = "SELECT db.TeacherId, db.FirstName, db.LastName, db.Phone, db.Mobile, db.Email, db.Password, db.IsActive, db.Created " +
            "FROM Teacher db " + WhereClause;
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

        private List<TeacherBase> List(string WhereClause, params SqlParameter[] commandparameters)
        {
            List<TeacherBase> mList = new List<TeacherBase>();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["csSwim"].ConnectionString);
            string sql = "SELECT db.TeacherId, db.FirstName, db.LastName, db.Phone, db.Mobile, db.Email, db.Password, db.IsActive, db.Created " +
            "FROM Teacher db " + WhereClause;
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


        private TeacherBase LoadRow(SqlDataReader rdr)
        {
            return new TeacherBase
            {
                TeacherId = rdr["TeacherId"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(rdr["TeacherId"]),
                FirstName = rdr["FirstName"].Equals(DBNull.Value) ? "" : (rdr["FirstName"].ToString()),
                LastName = rdr["LastName"].Equals(DBNull.Value) ? "" : (rdr["LastName"].ToString()),
                Phone = rdr["Phone"].Equals(DBNull.Value) ? "" : (rdr["Phone"].ToString()),
                Mobile = rdr["Mobile"].Equals(DBNull.Value) ? "" : (rdr["Mobile"].ToString()),
                Email = rdr["Email"].Equals(DBNull.Value) ? "" : (rdr["Email"].ToString()),
                Password = rdr["Password"].Equals(DBNull.Value) ? "" : (rdr["Password"].ToString()),
                IsActive = rdr["IsActive"].Equals(DBNull.Value) ? false : Convert.ToBoolean(rdr["IsActive"]),
                Created = rdr["Created"].Equals(DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(rdr["Created"]),
            };

        }

        private void Populate(TeacherBase mClass)
        {
            this.TeacherId = mClass.TeacherId;
            this.FirstName = mClass.FirstName;
            this.LastName = mClass.LastName;
            this.Phone = mClass.Phone;
            this.Mobile = mClass.Mobile;
            this.Email = mClass.Email;
            this.Password = mClass.Password;
            this.IsActive = mClass.IsActive;
            this.Created = mClass.Created;
        }


        #endregion

        #region CONSTRUCTOR/DESTRUCTOR


        private bool disposed = false;

        public TeacherDB()
        {

        }

        public TeacherDB(int TeacherId)
        {
            Populate(GetById(TeacherId));
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
        ~TeacherDB()
        {
            Dispose(false);
        }
        #endregion


    }
}

