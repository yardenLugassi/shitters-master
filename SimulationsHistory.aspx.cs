using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SimulationsHistory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
            GetShiters();

    }
    private void GetShiters()
    {
        DAL dal = new DAL();
        string query = "select * from shiters";
        DataTable dt = dal.GetDataTable(query);

        //TODO adapt shiters

        ddlShiters.DataSource = dt;

        ddlShiters.DataTextField = "name";

        ddlShiters.DataValueField = "shit_number";

        ddlShiters.DataBind();

        ddlShiters.Items.Insert(0, "select one");
    }
}