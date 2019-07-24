using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

public partial class NewShitterOperation : System.Web.UI.Page
{
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
        string query = "select * from shiters";
        DataTable dt = dal.GetDataTable(query);

        //TODO adapt shiters

        ddlShiters.DataSource = dt;

        ddlShiters.DataTextField = "name";

        ddlShiters.DataValueField = "shit_number";

        ddlShiters.DataBind();

        ddlShiters.Items.Insert(0, "select one");
    }


    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string query = string.Format(@"insert into shiter_operations (amount,shit_number,cover_type,customer,paper_type,op_date,setup_req,active,partial_amount,status) 
                                                                    values({0},{1},'{2}','{3}','{4}','{5}','{6}',1,0,0)",
           TxtAmount.Value, ddlShiters.SelectedValue, TxtCoverType.Value, txtCustomer.Value, TxtpaperType.Value,
           Calendar1.SelectedDate.ToString("yyyy-MM-dd HH:mm:ss"), chkSetupReq.Checked ? "1" : "0");
        DAL dal = new DAL();
        int aff = dal.DoQuery(query);
        if (aff == 1)
        {
            Response.Redirect("~/ShitterOperations.aspx");
        }
        else
        {
            MessageBox.Show("Sorry can you try later");
        }
    }
}