using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dpUtilityLib;

namespace appSchool.setup
{
    public partial class course : System.Web.UI.Page
    {
        #region MEMBERS

        private int CourseId;
        private clsSchool.CourseBase mCourseBase;

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
            LoadGrid();
        }

        protected void grdCourse_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int CourseId = 0;
            int Index = Convert.ToInt32(e.CommandArgument);

            if (grdCourse.Rows[Index] != null)
            {
                GridViewRow Row = grdCourse.Rows[Index];
                CourseId = Row.Cells[0].Text.ToInt();
                hdfCourseId.Value = CourseId.ToString();
            }

            if (e.CommandName == "RemoveRow")
            {
                RemoveCourse(CourseId);
            }

            if (e.CommandName == "EditRow")
            {
                mCourseBase = new clsSchool.CourseDB().GetById(CourseId);
                LoadForm();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) { return; }

            CourseId = 0;
            if (!string.IsNullOrEmpty(hdfCourseId.Value))
            {
                CourseId = hdfCourseId.Value.ToInt();
            }

            UnloadForm(CourseId);

            if (CourseId > 0)
            {
                bool ok = new clsSchool.CourseDB().Update(mCourseBase);
                if (!ok) CourseId = -1;
            }
            else
            {
                CourseId = new clsSchool.CourseDB().Add(mCourseBase);
                hdfCourseId.Value = CourseId.ToString();
            }

            if (CourseId < 0)
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

            if (mCourseBase == null) { return; }

            hdfCourseId.Value = mCourseBase.CourseId.ToString();
            txtCode.Text = mCourseBase.Code;
            txtName.Text = mCourseBase.Name;
            txtDescription.Text = mCourseBase.Description;
            txtFee.Text = mCourseBase.Fee.ToString("C");
            chkActive.Checked = mCourseBase.Active;

        }

        private void UnloadForm(int mShippingLineId)
        {
            mCourseBase = new clsSchool.CourseBase();

            mCourseBase.CourseId = mShippingLineId;

            mCourseBase.Code = txtCode.Text;
            mCourseBase.Name = txtName.Text;
            mCourseBase.Description = txtDescription.Text;
            mCourseBase.Fee = txtFee.Text.ToDouble();
            mCourseBase.Active = chkActive.Checked;

        }

        private void ResetForm()
        {
            if (mCourseBase == null) { return; }

            hdfCourseId.Value = "";
            txtCode.Text = "";
            txtName.Text = "";
            txtDescription.Text = "";
            txtFee.Text = "";
            chkActive.Checked = true;

        }

        private void LoadGrid()
        {
            switch (cboActive.SelectedValue.ToInt())
            {
                case 1: // List All
                    grdCourse.DataSource = new clsSchool.CourseDB().List();
                    grdCourse.DataBind();
                    break;

                case 2: // List Active
                    grdCourse.DataSource = new clsSchool.CourseDB().ListActive(true);
                    grdCourse.DataBind();
                    break;

                case 3: // List inactive
                    grdCourse.DataSource = new clsSchool.CourseDB().ListActive(false);
                    grdCourse.DataBind();

                    break;
                default:
                    break;
            }
        }

        private void RemoveCourse(int CourseId)
        {
            var mCourse = new clsSchool.CourseDB().GetById(CourseId);
            if (mCourse != null)
            {
                mCourse.Active = false;
                bool ok = new clsSchool.CourseDB().Update(mCourse);
            }

        }

        private bool ValidateInput()
        {
            bool r = true;

            if (string.IsNullOrEmpty(txtCode.Text))
            {
                lblError.Text = "Please enter the Course Code";
                lblError.Visible = true;
                timError.Enabled = true;
                r = false;
                return r;
            }

            if (string.IsNullOrEmpty(txtName.Text))
            {
                lblError.Text = "Please enter the Course Name";
                lblError.Visible = true;
                timError.Enabled = true;
                r = false;
                return r;
            }

            if (hdfCourseId.Value.ToInt() == 0)
            {
                var mCourse = new clsSchool.CourseDB().GetByCode(txtCode.Text);

                if (mCourse != null)
                {
                    lblError.Text = "This Course Code already exists in the system";
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