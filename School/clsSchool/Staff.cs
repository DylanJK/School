
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace clsSchool
{
    public class StaffBase
    {

        #region PROPERTIES

        public int StaffId { get; set; }
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



    public class StaffDB : StaffBase, IDisposable
    {
        #region METHODS (public)


        public int Add(StaffBase mClass)
        {
            int StaffId = 0;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["csSwim"].ConnectionString);
            string sql = "INSERT INTO Staff (FirstName, LastName, Phone, Mobile, Email, Password, IsActive)" +
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
                StaffId = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                StaffId = -1;
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                con.Close();
                con.Dispose();
                cmd.Dispose();
            }
            return StaffId;
        }


        public bool Update(StaffBase mClass)
        {
            bool mResult = false;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["csSwim"].ConnectionString);
            string sql = "UPDATE Staff SET FirstName = @FirstName, LastName = @LastName, Phone = @Phone, Mobile = @Mobile, Email = @Email, Password = @Password, IsActive = @IsActive " +
            "WHERE StaffId = @StaffId;";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add("@StaffId", SqlDbType.Int).Value = mClass.StaffId;
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


        public StaffBase GetById(int StaffId)
        {
            return Get(" WHERE db.StaffId = @StaffId", new SqlParameter("@StaffId", StaffId));
        }

        public StaffBase GetByEmail(string Email)
        {
            return Get(" WHERE db.Email = @Email", new SqlParameter("@Email", Email));
        }

        public List<StaffBase> List()
        {
            string WhereClause = "ORDER BY db.LastName;";
            return List(WhereClause);
        }

        public List<StaffBase> ListActive(bool IsActive)
        {
            return List(" WHERE db.IsActive = @IsActive ORDER BY db.LastName;",
               new SqlParameter("@IsActive", IsActive));
        }


        public List<StaffBase> Search(string StaffName)
        {
            return List(" WHERE db.LastName LIKE '%' + @LastName + '%' ORDER BY db.LastName;",
               new SqlParameter("@LastName", LastName));
        }

        public bool Delete(int StaffId)
        {
            bool mResult = false;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["csSwim"].ConnectionString);
            string sql = "DELETE FROM Staff WHERE StaffId = " + StaffId.ToString();
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

        private StaffBase Get(string WhereClause, params SqlParameter[] commandparameters)
        {
            StaffBase mClass = null;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["csSwim"].ConnectionString);
            string sql = "SELECT db.StaffId, db.FirstName, db.LastName, db.Phone, db.Mobile, db.Email, db.Password, db.IsActive, db.Created " +
            "FROM Staff db  " + WhereClause;
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

        private List<StaffBase> List(string WhereClause, params SqlParameter[] commandparameters)
        {
            List<StaffBase> mList = new List<StaffBase>();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["csSwim"].ConnectionString);
            string sql = "SELECT db.StaffId, db.FirstName, db.LastName, db.Phone, db.Mobile, db.Email, db.Password, db.IsActive, db.Created " +
            "FROM Staff db " + WhereClause;
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

        private StaffBase LoadRow(SqlDataReader rdr)
        {
            return new StaffBase
            {
                StaffId = rdr["StaffId"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(rdr["StaffId"]),
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

        private void Populate(StaffBase mClass)
        {
            this.StaffId = mClass.StaffId;
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

        public StaffDB()
        {

        }

        public StaffDB(int StaffId)
        {
            Populate(GetById(StaffId));
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
        ~StaffDB()
        {
            Dispose(false);
        }
        #endregion


    }
}

