using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Web.Security;
using dpUtilityLib;

namespace Cashier
{
    public partial class login : System.Web.UI.Page
    {
        #region EVENTS

        protected void Page_Load(object sender, EventArgs e)
        {

            // Page.Form.DefaultButton = btnLogin.UniqueID;
            txtUsername.TabIndex = 1;

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            

            if (ValidateInput())
            {
                if (RunLogin())
                {
                    var redirectUrl = (!string.IsNullOrEmpty(Request.QueryString["ReturnUrl"])) ?
                    Request.QueryString["ReturnUrl"] : Common.ApplicationPath + "default.aspx";

                    Response.Redirect(string.Format(redirectUrl));
                }
                else
                {
                    InputFailed("");
                }
            }
        }

        #endregion

        #region METHODS

        private bool ValidateInput()
        {
            bool RetVal = true;
            if (RetVal)
            {
                if (string.IsNullOrEmpty(txtUsername.Text.Trim()) || string.IsNullOrEmpty(txtPassword.Text.Trim()))
                {
                    InputFailed("");
                    RetVal = false;
                }
            }
            return RetVal;
        }

        private bool RunLogin()
        {
            bool RetVal = false;
            FormsAuthentication.Initialize();

            string username = txtUsername.Text;
            string password = txtPassword.Text;

            var mUser = new clsSchool.StaffDB().GetByEmail(username);

            if (mUser != null)
            {

                if (mUser.StaffId > 0 && mUser.IsActive && mUser.Password == password)
                {
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, mUser.Email, DateTime.Now, DateTime.Now.AddDays(1), true, mUser.StaffId.ToString(), FormsAuthentication.FormsCookiePath);
                    Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket)));

                    hdfStaff.Value = mUser.StaffId.ToString();

                    RetVal = true;
                }
            }

            mUser = null;

            return RetVal;
        }

        private void InputFailed(string Msg)
        {
            var sb = new StringBuilder();

            if (string.IsNullOrEmpty(Msg)) { Msg = "Login failed, please re-enter your email and password"; }
            sb.AppendLine("<div class=\"noticeerror\"><div>");
            sb.AppendLine(Msg);
            sb.AppendLine("</div></div>");

            //divNotice.InnerHtml = sb.ToString();
            sb = null;
        }

        #endregion
    }
}