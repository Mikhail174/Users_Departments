using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Users_Departments
{
    public partial class User : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnDelete.Enabled = false;
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
        public void Clear()
        {
            hfUserID.Value = "";
            txtFName.Text = txtLName.Text = "";
            lblErrorMessage.Text = lblSuccesMessage.Text = "";
            btnSave.Text = "Save";
            btnDelete.Enabled = false;
        }
    }
}