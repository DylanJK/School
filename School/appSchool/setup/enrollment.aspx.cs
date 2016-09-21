using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dpUtilityLib;

namespace appSchool.setup
{
    public partial class enrollment : System.Web.UI.Page
    {
        #region MEMBERS

        private int EnrollmentId;
        private clsSchool.EnrollmentBase mEnrollmentBase;

        #endregion

        #region EVENTS

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadClasses();
                LoadStudentGrid();

                LoadEnrollmentGrid();

            }
        }

        protected void grdEnrollment_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int EnrollmentId = 0;
            int Index = Convert.ToInt32(e.CommandArgument);

            if (grdEnrollment.Rows[Index] != null)
            {
                GridViewRow Row = grdEnrollment.Rows[Index];
                EnrollmentId = Row.Cells[0].Text.ToInt();
                hdfEnrollmentId.Value = EnrollmentId.ToString();
            }

            if (e.CommandName == "RemoveRow")
            {
                RemoveEnrollment(EnrollmentId);
            }

            if (e.CommandName == "EditRow")
            {
                mEnrollmentBase = new clsSchool.EnrollmentDB().GetById(EnrollmentId);
                LoadForm();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) { return; }

            EnrollmentId = 0;
            if (!string.IsNullOrEmpty(hdfEnrollmentId.Value))
            {
                EnrollmentId = hdfEnrollmentId.Value.ToInt();
            }

            UnloadForm(EnrollmentId);

            if (EnrollmentId > 0)
            {
                bool ok = new clsSchool.EnrollmentDB().Update(mEnrollmentBase);
                if (!ok) EnrollmentId = -1;
            }
            else
            {
                EnrollmentId = new clsSchool.EnrollmentDB().Add(mEnrollmentBase);
                hdfEnrollmentId.Value = EnrollmentId.ToString();
            }

            if (EnrollmentId < 0)
            {
                //  InputFailed("The database update failed, please try again.");
            }
            else
            {
                LoadStudentGrid();
                ResetForm();
            }

        }

        #endregion

        #region CLASSES

        private void LoadClasses()
        {

            var mCourseClassList = new clsSchool.CourseClassDB().ListByAllOpenClass();
            foreach (var item in mCourseClassList)
            {
                string Name = string.Format("{0}: - {1} : {2}", item.CourseName, item.StartDate.ToString("dd MMM yyyy"), item.StartTimeLabel);
                cboClasses.Items.Add(new ListItem(Name, item.CourseClassId.ToString()));
            }

        }

        private void LoadEnrollmentGrid()
        {
            grdEnrollment.DataSource = new clsSchool.EnrollmentDB().ListByClassId(cboClasses.SelectedValue.ToInt());
            grdEnrollment.DataBind();


        }

        protected void cboClasses_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadEnrollmentGrid();
        }


        #endregion


        #region STUDENTS

        protected void grdStudent_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int StudentId = 0;
            int Index = Convert.ToInt32(e.CommandArgument);

            if (grdStudent.Rows[Index] != null)
            {
                GridViewRow Row = grdStudent.Rows[Index];
                StudentId = Row.Cells[0].Text.ToInt();
            }

            if (e.CommandName == "SelectRow")
            {
                AddEnrollment(StudentId);
            }
        }

        private void AddEnrollment(int StudentId)
        {
            int CourseClassId = cboClasses.SelectedValue.ToInt();

            var mCourseClass = new clsSchool.CourseClassDB().GetById(CourseClassId);

            var mEnrollment = new clsSchool.EnrollmentBase();
            mEnrollment.ClassId = CourseClassId;
            mEnrollment.Completed = false;
            mEnrollment.CourseId = mCourseClass.CourseId;
            mEnrollment.EnrollmentId = 0;
            mEnrollment.HasPaid = false;
            mEnrollment.StartDate = mCourseClass.StartDate;
            mEnrollment.StudentId = StudentId;

            int Id = new clsSchool.EnrollmentDB().Add(mEnrollment);


        }
        #endregion

        #region METHODS

        private void LoadForm()
        {

            if (mEnrollmentBase == null) { return; }

            hdfEnrollmentId.Value = mEnrollmentBase.EnrollmentId.ToString();

            // txtClassId. = mEnrollmentBase.ClassId;
            //txtStartDate.Text = mEnrollmentBase.StartDate;
            //txtHasPaid.Text = mEnrollmentBase.HasPaid;


        }

        private void UnloadForm(int mShippingLineId)
        {
            mEnrollmentBase = new clsSchool.EnrollmentBase();

            mEnrollmentBase.EnrollmentId = mShippingLineId;

            // mEnrollmentBase.ClassId = txtClassId.Text;
            //mEnrollmentBase.StartDate = txtLastName.Text;
            // mEnrollmentBase.HasPaid = txtPhone.Text;
            //mEnrollmentBase.Completed = txtMobile.Text;

        }

        private void ResetForm()
        {
            if (mEnrollmentBase == null) { return; }

            hdfEnrollmentId.Value = "";
            // txtClassId.Text = "";
            //txtLastName.Text = "";
            //txtPhone.Text = "";

        }

        protected void btnAddStudent_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in grdStudent.Rows)
            {
                CheckBox cb = (CheckBox)row.FindControl("chSelect");
                if (cb != null && cb.Checked)
                {
                    int StudentId = row.Cells[0].Text.ToInt();
                    AddEnrollment(StudentId);
                }
            }

            LoadEnrollmentGrid();
        }


        private void LoadStudentGrid()
        {
            grdStudent.DataSource = new clsSchool.StudentDB().List();
            grdStudent.DataBind();
        }

        private void RemoveEnrollment(int EnrollmentId)
        {

            bool del = new clsSchool.EnrollmentDB().Delete(EnrollmentId);

            if (!del)
            {
                // Message to tell them in use
            }
            else
            {
                LoadEnrollmentGrid();
            }
        }

        private bool ValidateInput()
        {
            bool r = true;

            /* if (hdfEnrollmentId.Value.ToInt() == 0)
             {
                 var mStudent = new clsSchool.EnrollmentDB().GetById(grdStudent);

                 if (mStudent != null)
                 {
                     lblError.Text = "This email address already exists in the system";
                     lblError.Visible = true;
                     timError.Enabled = true;
                     r = false;
                     return r;
                 }

             }
            */
            return r;

        }


        #endregion




    }
}