<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="User.aspx.cs" Inherits="Users_Departments.User" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:HiddenField ID="hfUserID" runat="server" />
            <table>
               <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="FName"></asp:Label>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="txtFName" runat="server"></asp:TextBox>
                </td>
               </tr>
              <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="LName"></asp:Label>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="txtLName" runat="server"></asp:TextBox>
                </td>
               </tr>
               <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="Departament"></asp:Label>
                </td>
                <td colspan="2">
                    <asp:DropDownList ID="ddlDepartament" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource1" DataTextField="NameDep" DataValueField="NameDep" AppendDataBoundItems="True"  >
                        
                    </asp:DropDownList>
                </td>
               </tr>
              <tr>
                <td>
                   
                </td>
                <td colspan="2">
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" />
                    <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" />
                </td>
               </tr>
               <tr>
                <td>
                   
                </td>
                <td colspan="2">
                    <asp:Label ID="lblSuccesMessage" runat="server" Text="" ForeColor="Green"></asp:Label>
                </td>
               </tr>
               <tr>
                <td>
                   
                </td>
                <td colspan="2">
                    <asp:Label ID="lblErrorMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
                </td>
               </tr>
            </table>
            <br />
            <asp:GridView ID="gvUser" runat="server" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField ="FName" HeaderText="FName" />
                    <asp:BoundField DataField ="LName" HeaderText="LName" />
                    <asp:BoundField DataField ="NameDep" HeaderText="NameDep" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="linkView" runat="server" CommandArgument ='<%# Eval ("UserId")  %>' OnClick="lnk_OnClick" >View</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:TestDBConnectionString %>" SelectCommand="SELECT [NameDep] FROM [Departments]" ProviderName="System.Data.SqlClient"></asp:SqlDataSource>
    </form>
</body>
</html>
