using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ShiterDayOperations : System.Web.UI.Page
{
    public string PaperCost = "";
    public string CoverCost = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            GetShiters();
            GetShitterPrice();
        }
        
    }
    private void GetShitterPrice()
    {
        DAL dal = new DAL();
        string query = "select * from shitter_costs";
        DataTable dt = dal.GetDataTable(query);
        PaperCost = dt.Rows[0]["paper_cost"].ToString();
        CoverCost = dt.Rows[0]["cover_cost"].ToString();
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