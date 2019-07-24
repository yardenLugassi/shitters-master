using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string query = "select * from users where email='" + email.Value + "' and password='" + pwd.Value + "'";
        DAL dal = new DAL();
        DataTable dt = dal.GetDataTable(query);
        if (dt.Rows.Count > 0)
        {
            Session["email"] = dt.Rows[0]["email"];
            Session["name"] = dt.Rows[0]["name"];
            Response.Redirect("~/shiters.aspx");
        }
        else
        {
            MessageBox.Show("Wrong username password");
        }
    }
}