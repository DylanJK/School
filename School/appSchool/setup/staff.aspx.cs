using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dpUtilityLib;

namespace appSchool.setup
{
    public partial class staff : System.Web.UI.Page
    {
        #region MEMBERS

        private int StaffId;
        private clsSchool.StaffBase mStaffBase;

        #endregion

        #region EVENTS

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadGrid();

            }
        }

        protected void cboActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            chkActive.Checked = true;
            LoadGrid();
        }

        protected void grdStaff_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int StaffId = 0;
            int Index = Convert.ToInt32(e.CommandArgument);

            if (grdStaff.Rows[Index] != null)
            {
                GridViewRow Row = grdStaff.Rows[Index];
                StaffId = Row.Cells[0].Text.ToInt();
                hdfStaffId.Value = StaffId.ToString();
            }

            if (e.CommandName == "RemoveRow")
            {
                RemoveStaff(StaffId);
            }

            if (e.CommandName == "EditRow")
            {
                mStaffBase = new clsSchool.StaffDB().GetById(StaffId);
                LoadForm();

                lblPassword.Visible = false;
                txtPassword.Visible = false;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) { return; }

            StaffId = 0;
            if (!string.IsNullOrEmpty(hdfStaffId.Value))
            {
                StaffId = hdfStaffId.Value.ToInt();
            }

            UnloadForm(StaffId);

            if (StaffId > 0)
            {
                bool ok = new clsSchool.StaffDB().Update(mStaffBase);
                if (!ok) StaffId = -1;
            }
            else 
            {
                StaffId = new clsSchool.StaffDB().Add(mStaffBase);
                hdfStaffId.Value = StaffId.ToString();
            }

            if (StaffId < 0)
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

            if (mStaffBase == null) { return; }

            hdfStaffId.Value = mStaffBase.StaffId.ToString();
            txtFirstName.Text = mStaffBase.FirstName;
            txtLastName.Text = mStaffBase.LastName;
            txtPhone.Text = mStaffBase.Phone;
            txtMobile.Text = mStaffBase.Mobile;
            txtEmail.Text = mStaffBase.Email;
            txtPassword.Text = mStaffBase.Password;
            hdfPassword.Value = mStaffBase.Password;
            chkActive.Checked = mStaffBase.IsActive;

        }

        private void UnloadForm(int StaffId)
        {
            if (string.IsNullOrEmpty(hdfPassword.Value))
            {
                hdfPassword.Value = txtPassword.Text;
            }

            mStaffBase = new clsSchool.StaffBase();

            mStaffBase.StaffId = StaffId;
            mStaffBase.FirstName = txtFirstName.Text;
            mStaffBase.LastName = txtLastName.Text;
            mStaffBase.Phone = txtPhone.Text;
            mStaffBase.Mobile = FormatMobile(txtMobile.Text);
            mStaffBase.Email = txtEmail.Text;
            mStaffBase.Password = hdfPassword.Value;
            mStaffBase.IsActive = chkActive.Checked;

        }

        private void ResetForm()
        {
            if (mStaffBase == null) { return; }

            hdfStaffId.Value = "";
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

        private void LoadGrid()
        {
            switch (cboActive.SelectedValue.ToInt())
            {
                case 1: // List All
                    grdStaff.DataSource = new clsSchool.StaffDB().List();
                    grdStaff.DataBind();
                    break;

                case 2: // List Active
                    grdStaff.DataSource = new clsSchool.StaffDB().ListActive(true);
                    grdStaff.DataBind();
                    break;

                case 3: // List inactive
                    grdStaff.DataSource = new clsSchool.StaffDB().ListActive(false);
                    grdStaff.DataBind();

                    break;
                default:
                    break;
            }
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

        private void RemoveStaff(int StaffId)
        {

            var mStaff = new clsSchool.StaffDB().GetById(StaffId);
            if (mStaff != null)
            {
                mStaff.IsActive = false;
                bool ok = new clsSchool.StaffDB().Update(mStaff);
            }
                LoadGrid();

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

            if (string.IsNullOrEmpty(txtLastName.Text))
            {
                lblError.Text = "Please enter the Last Name";
                lblError.Visible = true;
                timError.Enabled = true;
                r = false;
                return r;
            }

            if (string.IsNullOrEmpty(txtPhone.Text))
            {
                lblError.Text = "Please enter the Phone Number";
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

            if (hdfStaffId.Value.ToInt() == 0)
            {
                var mStaff = new clsSchool.StaffDB().GetByEmail(txtEmail.Text);

                if (mStaff != null)
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