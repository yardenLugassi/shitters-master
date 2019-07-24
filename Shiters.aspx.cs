using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using FileHelpers;

public partial class Shiters : System.Web.UI.Page
{
    public DataTable shittersDt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["email"] == null)
        {
            Response.Redirect("~/Login.aspx");
        }
        if (!Page.IsPostBack)
        {
            GetShiters();
        }
    }

    private void GetShiters()
    {
        DAL dal = new DAL();
        string query = "select * from shiters where not status =0";
        shittersDt = dal.GetDataTable(query);
    }
}