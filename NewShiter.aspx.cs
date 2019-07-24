using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

public partial class NewShiter : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["email"] == null)
        {
            Response.Redirect("~/Login.aspx");
        }
    }




    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string query = string.Format(@"insert into shiters (name) values('{0}')",txtName.Value);
        DAL dal = new DAL();
        int aff = dal.DoQuery(query);
        if (aff == 1)
        {
            Response.Redirect("~/Shiters.aspx");// go back to shitters list;
        }
        else
        {
            MessageBox.Show("Sorry can you try later");
        }
    }
}