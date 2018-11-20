using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;


namespace Users_Departments
{
    public partial class User : System.Web.UI.Page
    {
        SqlConnection sqlCon = new SqlConnection(@"Data Source=МИХАИЛ-ПК\MSSQLSERVER1;Initial Catalog=TestDB;Integrated Security=True");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnDelete.Enabled = false;
                ddlDepartament.Items.Insert(0, new ListItem("-Select Department-", ""));
                FillGridView();
                btnSave.Text = "Create";
                Clear();


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
            btnSave.Text = "Create";
            btnDelete.Enabled = false;
            ddlDepartament.SelectedIndex=0;
        }
        [System.Runtime.ExceptionServices.HandleProcessCorruptedStateExceptionsAttribute()]
        protected void btnSave_Click(object sender, EventArgs e)

        {

            if (sqlCon.State == ConnectionState.Closed)
                sqlCon.Open();
            SqlCommand sqlCmd = new SqlCommand("UserCreateOrUpdate", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@UserId", (hfUserID.Value == "" ? 0 : Convert.ToInt32(hfUserID.Value)));
            sqlCmd.Parameters.AddWithValue("@FName", txtFName.Text.Trim());
            sqlCmd.Parameters.AddWithValue("@LName", txtLName.Text.Trim());
            sqlCmd.Parameters.AddWithValue("@NameDep", ddlDepartament.SelectedValue.Trim());
            sqlCmd.Parameters.AddWithValue("@DepId", SqlDbType.Int);
            try
            {
                sqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = ex.Message.ToString();
            }
            finally
            {
                sqlCon.Close();
            }

            string contactId = hfUserID.Value;
             if (contactId == "")

                lblSuccesMessage.Text = "Saved Succesfully";

            else lblSuccesMessage.Text = "Updated Succesfully";
             
            FillGridView();
        }

        void FillGridView()
        {
            if (sqlCon.State == ConnectionState.Closed)
                sqlCon.Open();
            SqlDataAdapter sqlDa = new SqlDataAdapter("UserViewAll",sqlCon);
            sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dtbl = new DataTable();
            sqlDa.Fill(dtbl);
            sqlCon.Close();
            gvUser.DataSource = dtbl;
            gvUser.DataBind();

        }

        protected void lnk_OnClick (object sender, EventArgs e)
        {
            int UserId = Convert.ToInt32((sender as LinkButton).CommandArgument);
            if (sqlCon.State == ConnectionState.Closed)
                sqlCon.Open();
            SqlDataAdapter sqlDa = new SqlDataAdapter("UserViewById", sqlCon);
            sqlDa.SelectCommand.Parameters.AddWithValue("@UserId", UserId);
            sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dtbl = new DataTable();
            sqlDa.Fill(dtbl);
            sqlCon.Close();
            hfUserID.Value = UserId.ToString();
            txtFName.Text = dtbl.Rows[0]["FName"].ToString();
            txtLName.Text = dtbl.Rows[0]["LName"].ToString();
            ddlDepartament.SelectedValue = dtbl.Rows[0]["NameDep"].ToString();
            btnSave.Text = "Update";
            btnDelete.Enabled = true;
           



        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (sqlCon.State == ConnectionState.Closed)
                sqlCon.Open();
            SqlCommand sqlCmd = new SqlCommand("UserDeleteById",sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("UserId", Convert.ToInt32(hfUserID.Value));
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
            Clear();
            FillGridView();
            lblSuccesMessage.Text = "Deleted Succesfully";


        }
    }
}