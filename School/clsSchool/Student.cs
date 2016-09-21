using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace clsSchool
{
    public class StudentBase
    {

        #region PROPERTIES
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StudentName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Suburb { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }
        public DateTime Created { get; set; }
        #endregion

    }



    public class StudentDB : StudentBase, IDisposable
    {
        #region METHODS (public)


        public int Add(StudentBase mClass)
        {
            int StudentId = 0;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["csSwim"].ConnectionString);
            string sql = "INSERT INTO Student (FirstName, LastName, StudentName, Address1, Address2, Suburb, City, Phone, Mobile, Email, Password, Active)" +
            " VALUES (@FirstName, @LastName, @StudentName, @Address1, @Address2, @Suburb, @City, @Phone, @Mobile, @Email, @Password, @Active)" +
            " SELECT SCOPE_IDENTITY()";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = mClass.FirstName;
            cmd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = mClass.LastName;
            cmd.Parameters.Add("@StudentName", SqlDbType.VarChar).Value = mClass.StudentName;
            cmd.Parameters.Add("@Address1", SqlDbType.VarChar).Value = mClass.Address1;
            cmd.Parameters.Add("@Address2", SqlDbType.VarChar).Value = mClass.Address2;
            cmd.Parameters.Add("@Suburb", SqlDbType.VarChar).Value = mClass.Suburb;
            cmd.Parameters.Add("@City", SqlDbType.VarChar).Value = mClass.City;
            cmd.Parameters.Add("@Phone", SqlDbType.VarChar).Value = mClass.Phone;
            cmd.Parameters.Add("@Mobile", SqlDbType.VarChar).Value = mClass.Mobile;
            cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = mClass.Email;
            cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = mClass.Password;
            cmd.Parameters.Add("@Active", SqlDbType.Bit).Value = mClass.Active;
            try
            {
                con.Open();
                StudentId = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                StudentId = -1;
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                con.Close();
                con.Dispose();
                cmd.Dispose();
            }
            return StudentId;
        }


        public bool Update(StudentBase mClass)
        {
            bool mResult = false;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["csSwim"].ConnectionString);
            string sql = "UPDATE Student SET FirstName = @FirstName, LastName = @LastName, StudentName = @StudentName, Address1 = @Address1, Address2 = @Address2, Suburb = @Suburb, City = @City, Phone = @Phone, Mobile = @Mobile, Email = @Email, Password = @Password, Active = @Active " +
            "WHERE StudentId = @StudentId;";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add("@StudentId", SqlDbType.Int).Value = mClass.StudentId;
            cmd.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = mClass.FirstName;
            cmd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = mClass.LastName;
            cmd.Parameters.Add("@StudentName", SqlDbType.VarChar).Value = mClass.StudentName;
            cmd.Parameters.Add("@Address1", SqlDbType.VarChar).Value = mClass.Address1;
            cmd.Parameters.Add("@Address2", SqlDbType.VarChar).Value = mClass.Address2;
            cmd.Parameters.Add("@Suburb", SqlDbType.VarChar).Value = mClass.Suburb;
            cmd.Parameters.Add("@City", SqlDbType.VarChar).Value = mClass.City;
            cmd.Parameters.Add("@Phone", SqlDbType.VarChar).Value = mClass.Phone;
            cmd.Parameters.Add("@Mobile", SqlDbType.VarChar).Value = mClass.Mobile;
            cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = mClass.Email;
            cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = mClass.Password;
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


        public StudentBase GetById(int StudentId)
        {
            return Get(" WHERE db.StudentId = @StudentId", new SqlParameter("@StudentId", StudentId));
        }


        public List<StudentBase> List()
        {
            string WhereClause = " ORDER BY db.StudentName;";
            return List(WhereClause);
        }

        public List<StudentBase> ListActive(bool Active)
        {
            return List(" WHERE db.Active = @Active ORDER BY db.StudentName;",
               new SqlParameter("@Active", Active));
        }


        public List<StudentBase> Search(string StudentName)
        {
            return List(" WHERE db.StudentName LIKE '%' + @StudentName + '%' ORDER BY db.StudentName;",
               new SqlParameter("@StudentName", StudentName));
        }

        public bool Delete(int StudentId)
        {
            bool mResult = false;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["csSwim"].ConnectionString);
            string sql = "DELETE FROM Student WHERE StudentId = " + StudentId.ToString();
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

        private StudentBase Get(string WhereClause, params SqlParameter[] commandparameters)
        {
            StudentBase mClass = null;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["csSwim"].ConnectionString);
            string sql = "SELECT db.StudentId, db.FirstName, db.LastName, db.StudentName, db.Address1, db.Address2, db.Suburb, db.City, db.Phone, db.Mobile, db.Email, db.Password, db.Active, db.Created " +
            "FROM Student db " + WhereClause;
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

        private List<StudentBase> List(string WhereClause, params SqlParameter[] commandparameters)
        {
            List<StudentBase> mList = new List<StudentBase>();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["csSwim"].ConnectionString);
            string sql = "SELECT db.StudentId, db.FirstName, db.LastName, db.StudentName, db.Address1, db.Address2, db.Suburb, db.City, db.Phone, db.Mobile, db.Email, db.Password, db.Active, db.Created " +
            "FROM Student db " + WhereClause;
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

        public StudentBase GetByEmail(string Email)
        {
            return Get(" WHERE db.Email = @Email", new SqlParameter("@Email", Email));
        }

        private StudentBase LoadRow(SqlDataReader rdr)
        {
            return new StudentBase
            {
                StudentId = rdr["StudentId"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(rdr["StudentId"]),
                FirstName = rdr["FirstName"].Equals(DBNull.Value) ? "" : (rdr["FirstName"].ToString()),
                LastName = rdr["LastName"].Equals(DBNull.Value) ? "" : (rdr["LastName"].ToString()),
                StudentName = rdr["StudentName"].Equals(DBNull.Value) ? "" : (rdr["StudentName"].ToString()),
                Address1 = rdr["Address1"].Equals(DBNull.Value) ? "" : (rdr["Address1"].ToString()),
                Address2 = rdr["Address2"].Equals(DBNull.Value) ? "" : (rdr["Address2"].ToString()),
                Suburb = rdr["Suburb"].Equals(DBNull.Value) ? "" : (rdr["Suburb"].ToString()),
                City = rdr["City"].Equals(DBNull.Value) ? "" : (rdr["City"].ToString()),
                Phone = rdr["Phone"].Equals(DBNull.Value) ? "" : (rdr["Phone"].ToString()),
                Mobile = rdr["Mobile"].Equals(DBNull.Value) ? "" : (rdr["Mobile"].ToString()),
                Email = rdr["Email"].Equals(DBNull.Value) ? "" : (rdr["Email"].ToString()),
                Password = rdr["Password"].Equals(DBNull.Value) ? "" : (rdr["Password"].ToString()),
                Active = rdr["Active"].Equals(DBNull.Value) ? false : Convert.ToBoolean(rdr["Active"]),
                Created = rdr["Created"].Equals(DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(rdr["Created"]),
            };

        }

        private void Populate(StudentBase mClass)
        {
            this.StudentId = mClass.StudentId;
            this.FirstName = mClass.FirstName;
            this.LastName = mClass.LastName;
            this.StudentName = mClass.StudentName;
            this.Address1 = mClass.Address1;
            this.Address2 = mClass.Address2;
            this.Suburb = mClass.Suburb;
            this.City = mClass.City;
            this.Phone = mClass.Phone;
            this.Mobile = mClass.Mobile;
            this.Email = mClass.Email;
            this.Password = mClass.Password;
            this.Active = mClass.Active;
            this.Created = mClass.Created;
        }


        #endregion

        #region CONSTRUCTOR/DESTRUCTOR


        private bool disposed = false;

        public StudentDB()
        {

        }

        public StudentDB(int StudentId)
        {
            Populate(GetById(StudentId));
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
        ~StudentDB()
        {
            Dispose(false);
        }
        #endregion


    }
}

