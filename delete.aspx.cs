using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

public partial class delete : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            if (Request.QueryString["id"] != null)
            {

                if (Request.QueryString["type"] != null)
                {
                    string type = Request.QueryString["type"];
                    switch (type)
                    {
                        case "shitter":
                            DeleteShitter();
                            break;
                        case "shitterOperation":
                            DeleteShitterOperations();
                            break;
                    }
                }

            }
        }
    }

    private void DeleteShitterOperations()
    {
        string query = "delete from shiter_operations where id=" + Request.QueryString["id"];
        DAL dal = new DAL();
        int aff = dal.DoQuery(query);
        if (aff == 1)
        {
            MessageBox.Show("Operation deleted succefuly");
            Response.Redirect("ShitterOperations.aspx?id=" + Request.QueryString["id"]);
        }
        else
        {
            MessageBox.Show("Error please try later");
        }
    }
    private void DeleteShitter()
    {
        string query = "update shiters set status=0 where id=" + Request.QueryString["id"];
        DAL dal = new DAL();
        int aff = dal.DoQuery(query);
        if (aff == 1)
        {
            MessageBox.Show("Shitter deleted succefuly");
            Response.Redirect("Shiters.aspx");
        }
        else
        {
            MessageBox.Show("Error please try later");
        }
    }
}