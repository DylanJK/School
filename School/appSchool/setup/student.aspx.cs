using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dpUtilityLib;

namespace appSchool.setup
{
    public partial class student : System.Web.UI.Page
    {
        #region MEMBERS

        private int StudentId;
        private clsSchool.StudentBase mStudentBase;

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

        protected void grdStudent_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int StudentId = 0;
            int Index = Convert.ToInt32(e.CommandArgument);

            if (grdStudent.Rows[Index] != null)
            {
                GridViewRow Row = grdStudent.Rows[Index];
                StudentId = Row.Cells[0].Text.ToInt();
                hdfStudentId.Value = StudentId.ToString();
            }

            if (e.CommandName == "RemoveRow")
            {
                RemoveStudent(StudentId);
            }

            if (e.CommandName == "EditRow")
            {
                mStudentBase = new clsSchool.StudentDB().GetById(StudentId);
                LoadForm();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) { return; }

            StudentId = 0;
            if (!string.IsNullOrEmpty(hdfStudentId.Value))
            {
                StudentId = hdfStudentId.Value.ToInt();
            }

            UnloadForm(StudentId);

            if (StudentId > 0)
            {
                bool ok = new clsSchool.StudentDB().Update(mStudentBase);
                if (!ok) StudentId = -1;
            }
            else
            {
                StudentId = new clsSchool.StudentDB().Add(mStudentBase);
                hdfStudentId.Value = StudentId.ToString();

            }

            if (StudentId < 0)
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

            if (mStudentBase == null) { return; }

            hdfStudentId.Value = mStudentBase.StudentId.ToString();
            txtFirstName.Text = mStudentBase.FirstName;
            txtLastName.Text = mStudentBase.LastName;
            txtAddress1.Text = mStudentBase.Address1;
            //  txtAddress2.Text = mStudentBase.Address2;
            TxtSuburb.Text = mStudentBase.Suburb;
            TxtCity.Text = mStudentBase.City;
            txtPhone.Text = mStudentBase.Phone;
            txtMobile.Text = mStudentBase.Mobile;
            txtEmail.Text = mStudentBase.Email;
            //    txtPassword.Text = mStudentBase.Password;
            //    hdfPassword.Value = mStudentBase.Password;
            chkActive.Checked = mStudentBase.Active;

        }

        private void UnloadForm(int StudentId)
        {
            //if (string.IsNullOrEmpty(hdfPassword.Value))
            //{
            //    hdfPassword.Value = txtPassword.Text;
            //}

            mStudentBase = new clsSchool.StudentBase();

            mStudentBase.StudentId = StudentId;
            mStudentBase.FirstName = txtFirstName.Text;
            mStudentBase.LastName = txtLastName.Text;
            mStudentBase.StudentName = txtFirstName.Text + " " + txtLastName.Text;
            mStudentBase.Address1 = txtAddress1.Text;
            mStudentBase.Address2 = "";
            mStudentBase.Suburb = TxtSuburb.Text;
            mStudentBase.City = TxtCity.Text;
            mStudentBase.Phone = txtPhone.Text;
            mStudentBase.Mobile = FormatMobile(txtMobile.Text);
            mStudentBase.Email = txtEmail.Text;
            mStudentBase.Password = "";
            mStudentBase.Active = chkActive.Checked;
        }

        private void ResetForm()
        {
            if (mStudentBase == null) { return; }

            hdfStudentId.Value = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtAddress1.Text = "";

            TxtSuburb.Text = "";
            TxtCity.Text = "";

            txtPhone.Text = "";
            txtMobile.Text = "";
            txtEmail.Text = "";
            //  txtPassword.Text = "";
            hdfPassword.Value = "";
            chkActive.Checked = true;

            //lblPassword.Visible = true;
            //txtPassword.Visible = true;

        }

        private void LoadGrid()
        {


            switch (cboActive.SelectedValue.ToInt())
            {
                case 1: // List All
                    grdStudent.DataSource = new clsSchool.StudentDB().List();
                    grdStudent.DataBind();
                    break;

                case 2: // List Active
                    grdStudent.DataSource = new clsSchool.StudentDB().ListActive(true);
                    grdStudent.DataBind();
                    break;

                case 3: // List inactive
                    grdStudent.DataSource = new clsSchool.StudentDB().ListActive(false);
                    grdStudent.DataBind();

                    break;
                default:
                    break;
            }


        }

        private void RemoveStudent(int StudentId)
        {
            var mStudent = new clsSchool.StudentDB().GetById(StudentId);
            if (mStudent != null)
            {
                mStudent.Active = false;
                bool ok = new clsSchool.StudentDB().Update(mStudent);
            }

            LoadGrid();

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

        private bool ValidateInput()
        {
            bool r = true;

            if (string.IsNullOrEmpty(txtFirstName.Text))
            {
                lblError.Text = "Please enter the First Name";
                lblError.Visible = true;
                timError.Enabled = true;
                r = false;
                return r;
            }

            if (string.IsNullOrEmpty(txtPhone.Text))
            {
                lblError.Text = "Please enter a Contact Number";
                lblError.Visible = true;
                timError.Enabled = true;
                r = false;
                return r;
            }

            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                lblError.Text = "Please enter an Email";
                lblError.Visible = true;
                timError.Enabled = true;
                r = false;
                return r;
            }

            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                lblError.Text = "Please enter the Email Address";
                lblError.Visible = true;
                timError.Enabled = true;
                r = false;
                return r;
            }

            if (hdfStudentId.Value.ToInt() == 0)
            {
                var mStudent = new clsSchool.StudentDB().GetByEmail(txtEmail.Text);

                if (mStudent != null)
                {
                    lblError.Text = "This email address already exists in the system";
                    lblError.Visible = true;
                    timError.Enabled = true;
                    r = false;
                    return r;
                }

            }

            return r;

        }

        protected void timError_Tick(object sender, EventArgs e)
        {
            lblError.Text = "";
            lblError.Visible = false;
            timError.Enabled = false;
        }

        #endregion

       


    }
}