using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace appSchool.setup
{
    public partial class searchresults : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["s"])) { hdfSearchFor.Value = Request.QueryString["s"]; }

                if (!string.IsNullOrEmpty(hdfSearchFor.Value))
                {
                    var mStudentList = new clsSchool.StudentDB().Search(hdfSearchFor.Value);

                    grdStudent.DataSource = mStudentList;
                    grdStudent.DataBind();
                }
            }
        }

        protected void grdStudent_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
    }
}