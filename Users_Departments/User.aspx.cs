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
        SqlConnection sqlCon = new SqlConnection(@"Data Source=WKS456\SQLEXPRESS;Initial Catalog=TestDB;Integrated Security=True");
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
            SqlCommand sqlCmd = new SqlCommand("UserCreate", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@FName",txtFName.Text.Trim());
            sqlCmd.Parameters.AddWithValue("@LName",txtLName.Text.Trim());
            sqlCmd.Parameters.AddWithValue("@NameDep",ddlDepartament.SelectedValue.Trim());
            sqlCmd.Parameters.AddWithValue("@DepId",SqlDbType.Int);
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
            Clear();
            if (hfUserID.Value == "")
                lblSuccesMessage.Text = "Saved Succesfully";
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
            string FName = (sender as LinkButton).CommandArgument;
            if (sqlCon.State == ConnectionState.Closed)
                sqlCon.Open();
            SqlDataAdapter sqlDa = new SqlDataAdapter("UserViewByFName", sqlCon);
            sqlDa.SelectCommand.Parameters.AddWithValue("@FName", FName);
            sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dtbl = new DataTable();
            sqlDa.Fill(dtbl);
            sqlCon.Close();
            hfUserID.Value = FName;
            txtFName.Text = dtbl.Rows[0]["FName"].ToString();
            txtLName.Text = dtbl.Rows[0]["LName"].ToString();
            ddlDepartament.SelectedValue = dtbl.Rows[0]["NameDep"].ToString();
            btnDelete.Enabled = true;
           



        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            if (sqlCon.State == ConnectionState.Closed)
                sqlCon.Open();
            SqlCommand sqlCmd = new SqlCommand("UserUpdate", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@FName", txtFName.Text.Trim());
            sqlCmd.Parameters.AddWithValue("@LName", txtLName.Text.Trim());
            sqlCmd.Parameters.AddWithValue("@NameDep", ddlDepartament.SelectedValue.Trim());
            sqlCmd.Parameters.AddWithValue("@DepId", SqlDbType.Int);////
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
            Clear();
            lblSuccesMessage.Text = "Updated Succesfully";
            FillGridView();
        }
    }
}