using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using dpUtilityLib;

namespace appSchool.setup
{
    public partial class courseclass : System.Web.UI.Page
    {
        #region MEMBERS

        private int mCourseClassId;
        private clsSchool.CourseClassBase mCourseClassBase;

        #endregion

        #region EVENTS

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadCourseCombo();
                LoadTeacherCombo();
                LoadStartTimeCombo();
                LoadEndTimeCombo();
                //chkIsComplete.Checked = true;
                //chkIsOpen.Checked = true;

                LoadGrid();
            }
        }


        protected void cboActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGrid();
        }

        protected void grdCourseClass_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int CourseClassId = 0;
            int Index = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "EditRow")
            {
                if (grdCourseClass.Rows[Index] != null)
                {
                    GridViewRow Row = grdCourseClass.Rows[Index];
                    CourseClassId = Row.Cells[0].Text.ToInt();

                    mCourseClassBase = new clsSchool.CourseClassDB().GetById(CourseClassId);
                    LoadForm();
                }
            }

            if (e.CommandName == "RemoveRow")
            {
                if (grdCourseClass.Rows[Index] != null)
                {
                    GridViewRow Row = grdCourseClass.Rows[Index];
                    CourseClassId = Row.Cells[0].Text.ToInt();

                    RemoveCourseClass(CourseClassId);
                }
            }

        }



        protected void btnNew_Click(object sender, EventArgs e)
        {
            ResetForm();
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) { return; }

            mCourseClassId = 0;
            if (!string.IsNullOrEmpty(hdfCourseClassId.Value))
            {
                mCourseClassId = Convert.ToInt32(hdfCourseClassId.Value);
            }

            UnloadForm(mCourseClassId);

            if (mCourseClassId > 0)
            {
                bool ok = new clsSchool.CourseClassDB().Update(mCourseClassBase);
                if (!ok) mCourseClassId = -1;
            }
            else
            {
                mCourseClassId = new clsSchool.CourseClassDB().Add(mCourseClassBase);
                hdfCourseClassId.Value = mCourseClassId.ToString();
            }

            if (mCourseClassId < 0)
            {
                InputFailed("The database update failed, please try again.");
            }
            else
            {
                LoadGrid();

                ResetForm();
                InputSucceeded();
            }

        }


        #endregion

        #region METHODS

        private void LoadForm()
        {

            if (mCourseClassBase == null) { return; }

            hdfCourseClassId.Value = mCourseClassBase.CourseClassId.ToString();
            cboCourse.SelectedValue = mCourseClassBase.CourseId.ToString();
            cboTeacher.SelectedValue = mCourseClassBase.TeacherId.ToString();
            txtStartDate.Text = mCourseClassBase.StartDate.ToString("dd MMM yyyy");
            txtEndDate.Text = mCourseClassBase.EndDate.ToString("dd MMM yyyy");
            txtCourseDay.Text = mCourseClassBase.CourseDay;
            cboStartTime.SelectedValue = mCourseClassBase.StartTime.ToString();
            cboEndTime.SelectedValue = mCourseClassBase.EndTime.ToString();
            chkIsOpen.Checked = mCourseClassBase.IsOpen;
            chkIsComplete.Checked = mCourseClassBase.IsComplete;

        }

        private void UnloadForm(int mCourseClassId)
        {
            mCourseClassBase = new clsSchool.CourseClassBase();

            mCourseClassBase.CourseClassId = mCourseClassId;
            mCourseClassBase.CourseId = cboCourse.SelectedValue.ToInt();
            mCourseClassBase.TeacherId = cboTeacher.SelectedValue.ToInt();
            mCourseClassBase.StartDate = txtStartDate.Text.ToDateTime();
            mCourseClassBase.EndDate = txtEndDate.Text.ToDateTime();
            mCourseClassBase.CourseDay = txtCourseDay.Text;
            mCourseClassBase.StartTime = cboStartTime.SelectedValue.ToInt();
            mCourseClassBase.EndTime = cboEndTime.SelectedValue.ToInt();
            mCourseClassBase.IsOpen = chkIsOpen.Checked;
            mCourseClassBase.IsComplete = chkIsComplete.Checked;

        }

        private void RemoveCourseClass(int CourseClassId)
        {
            var mClass = new clsSchool.CourseClassDB().GetById(CourseClassId);

            if (mClass != null)
            {
                mClass.IsComplete = false;
                bool ok = new clsSchool.CourseClassDB().Update(mClass);
            }

            LoadGrid();
        }

        private void ResetForm()
        {

            mCourseClassBase = null;
            mCourseClassId = 0;
            hdfCourseClassId.Value = "0";
            txtStartDate.Text = "";
            txtEndDate.Text = "";
            txtCourseDay.Text = "";
            chkIsOpen.Checked = false;
            chkIsComplete.Checked = false;


        }

        private bool ValidateInput()
        {
            bool r = true;
            return r;

        }

        private void LoadGrid()
        {
            switch (cboActive.SelectedValue.ToInt())
            {
                case 1: // List All
                    grdCourseClass.DataSource = new clsSchool.CourseClassDB().List();
                    grdCourseClass.DataBind();
                    break;

                case 2: // List Active
                    grdCourseClass.DataSource = new clsSchool.CourseClassDB().ListOpen(true);
                    grdCourseClass.DataBind();
                    break;

                case 3: // List inactive
                    grdCourseClass.DataSource = new clsSchool.CourseClassDB().ListOpen(false);
                    grdCourseClass.DataBind();

                    break;
                default:
                    break;
            }

        }

        private void LoadCourseCombo()
        {

            cboCourse.DataSource = new clsSchool.CourseDB().List();
            cboCourse.DataValueField = "CourseId";
            cboCourse.DataTextField = "Name";
            cboCourse.DataBind();

        }

        private void LoadTeacherCombo()
        {

            var mTeacherList = new clsSchool.TeacherDB().List();
            foreach (var mTeacher in mTeacherList)
            {
                string TeacherName = string.Format("{0} {1}", mTeacher.FirstName, mTeacher.LastName);
                cboTeacher.Items.Add(new ListItem(TeacherName, mTeacher.TeacherId.ToString()));
            }
        }

        private void LoadStartTimeCombo()
        {

            cboStartTime.DataSource = new clsSchool.SystemTimeDB().ListStartTimes();
            cboStartTime.DataValueField = "TimeId";
            cboStartTime.DataTextField = "TimeLabel";
            cboStartTime.DataBind();

        }

        private void LoadEndTimeCombo()
        {

            cboEndTime.DataSource = new clsSchool.SystemTimeDB().ListEndTimes();
            cboEndTime.DataValueField = "TimeId";
            cboEndTime.DataTextField = "TimeLabel";
            cboEndTime.DataBind();

        }

        private void InputSucceeded()
        {
            var sb = new StringBuilder();

            sb.AppendLine("<div class=\"noticesuccess\">");
            sb.AppendLine("<div>");
            sb.AppendLine("The database update was successful");
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");


            sb = null;
        }

        private void InputFailed(string Msg)
        {
            var sb = new StringBuilder();

            if (string.IsNullOrEmpty(Msg)) { Msg = "The database update failed"; }
            sb.AppendLine("<div class=\"noticeerror\"><div>");
            sb.AppendLine(Msg);
            sb.AppendLine("</div></div>");


            sb = null;
        }


        #endregion


    }
}