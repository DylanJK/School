using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dpUtilityLib;

namespace appSchool.setup
{
    public partial class teacher : System.Web.UI.Page
    {

        #region MEMBERS

        private int TeacherId;
        private clsSchool.TeacherBase mTeacherBase;

        #endregion

        #region EVENTS

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                chkActive.Checked = true;
                LoadGrid();

            }
        }

        protected void cboActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGrid();
        }

        protected void grdTeacher_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int TeacherId = 0;
            int Index = Convert.ToInt32(e.CommandArgument);

            if (grdTeacher.Rows[Index] != null)
            {
                GridViewRow Row = grdTeacher.Rows[Index];
                TeacherId = Row.Cells[0].Text.ToInt();
                hdfTeacherId.Value = TeacherId.ToString();
            }

            if (e.CommandName == "RemoveRow")
            {
                RemoveTeacher(TeacherId);
            }

            if (e.CommandName == "EditRow")
            {
                mTeacherBase = new clsSchool.TeacherDB().GetById(TeacherId);
                LoadForm();

                lblPassword.Visible = false;
                txtPassword.Visible = false;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) { return; }

            TeacherId = 0;
            if (!string.IsNullOrEmpty(hdfTeacherId.Value))
            {
                TeacherId = hdfTeacherId.Value.ToInt();
            }

            UnloadForm(TeacherId);

            if (TeacherId > 0)
            {
                bool ok = new clsSchool.TeacherDB().Update(mTeacherBase);
                if (!ok) TeacherId = -1;
            }
            else
            {
                TeacherId = new clsSchool.TeacherDB().Add(mTeacherBase);
                hdfTeacherId.Value = TeacherId.ToString();
            }

            if (TeacherId < 0)
            {
                //  InputFailed("The database update failed, please try again.");
            }
            else
            {
                LoadGrid();
                ResetForm();
            }

        }

        #endregion

        #region METHODS

        private void LoadForm()
        {

            if (mTeacherBase == null) { return; }

            hdfTeacherId.Value = mTeacherBase.TeacherId.ToString();
            txtFirstName.Text = mTeacherBase.FirstName;
            txtLastName.Text = mTeacherBase.LastName;
            txtPhone.Text = mTeacherBase.Phone;
            txtMobile.Text = mTeacherBase.Mobile;
            txtEmail.Text = mTeacherBase.Email;
            txtPassword.Text = mTeacherBase.Password;
            hdfPassword.Value = mTeacherBase.Password;
            chkActive.Checked = mTeacherBase.IsActive;

        }

        private void UnloadForm(int TeacherId)
        {
            if (string.IsNullOrEmpty(hdfPassword.Value))
            {
                hdfPassword.Value = txtPassword.Text;
            }

            mTeacherBase = new clsSchool.TeacherBase();

            mTeacherBase.TeacherId = TeacherId;
            mTeacherBase.FirstName = txtFirstName.Text;
            mTeacherBase.LastName = txtLastName.Text;
            mTeacherBase.Phone = txtPhone.Text;
            mTeacherBase.Mobile = FormatMobile(txtMobile.Text);
            mTeacherBase.Email = txtEmail.Text;
            mTeacherBase.Password = hdfPassword.Value;
            mTeacherBase.IsActive = chkActive.Checked;

        }

        private void ResetForm()
        {
            if (mTeacherBase == null) { return; }

            hdfTeacherId.Value = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtPhone.Text = "";
            txtMobile.Text = "";
            txtEmail.Text = "";
            txtPassword.Text = "";
            hdfPassword.Value = "";
            chkActive.Checked = true;

            lblPassword.Visible = true;
            txtPassword.Visible = true;

        }

        private string FormatMobile(string MobileNumber)
        {
            MobileNumber = MobileNumber.Replace(" ", "");

            string PartOne = MobileNumber.Substring(0, 3);
            string PartTwo = MobileNumber.Substring(3, 3);
            string PartThree = MobileNumber.Substring(6, 3);

            if (MobileNumber.Length == 10)
            {
                PartThree = MobileNumber.Substring(6, 4);
            }

            MobileNumber = string.Format("{0} {1} {2}", PartOne, PartTwo, PartThree);

            return MobileNumber;
        }

        private void LoadGrid()
        {
            switch (cboActive.SelectedValue.ToInt())
            {
                case 1: // List All
                    grdTeacher.DataSource = new clsSchool.TeacherDB().List();
                    grdTeacher.DataBind();
                    break;

                case 2: // List Active
                    grdTeacher.DataSource = new clsSchool.TeacherDB().ListActive(true);
                    grdTeacher.DataBind();
                    break;

                case 3: // List inactive
                    grdTeacher.DataSource = new clsSchool.TeacherDB().ListActive(false);
                    grdTeacher.DataBind();

                    break;
                default:
                    break;
            }
      
        }

        private void RemoveTeacher(int TeacherId)
        {

            var mTeacher = new clsSchool.TeacherDB().GetById(TeacherId);

            if (mTeacher != null)
            {
                mTeacher.IsActive = false;
                bool ok = new clsSchool.TeacherDB().Update(mTeacher);
            }

            LoadGrid();

        }

        private bool ValidateInput()
        {
            bool r = true;

            if (string.IsNullOrEmpty(txtFirstName.Text))
            {
                // InputFailed("Please enter the Shipping Line Name.");
                r = false;
                return r;
            }

            if (string.IsNullOrEmpty(txtPhone.Text))
            {
                //  InputFailed("Please enter the Shipping Line Phone Number.");
                r = false;
                return r;
            }

            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                //  InputFailed("Please enter the Shipping Line Email Address.");
                r = false;
                return r;
            }

            return r;

        }

        #endregion

      

    }
}