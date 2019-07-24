using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

public partial class EditShitterOperation : System.Web.UI.Page
{
    ShitterOperation so = new ShitterOperation();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["email"] == null)
        {
            Response.Redirect("~/Login.aspx");
        }
        if (!Page.IsPostBack)
        {
            GetShiters();
            if (Request.QueryString["id"] != null)
            {
                string query = "select * from shiter_operations where id=" + Request.QueryString["id"];
                DAL dal = new DAL();
                DataTable dt = dal.GetDataTable(query);
                if (dt.Rows.Count > 0)
                {
                    so.Id = int.Parse(dt.Rows[0]["id"].ToString());
                    so.Amount = int.Parse(dt.Rows[0]["amount"].ToString());
                    so.CoverType = dt.Rows[0]["cover_type"].ToString();
                    so.Customer = dt.Rows[0]["customer"].ToString();
                    so.PaperType = dt.Rows[0]["paper_type"].ToString();
                    so.OpDate = DateTime.Parse(dt.Rows[0]["op_date"].ToString());
                    so.SetupReq = dt.Rows[0]["setup_req"].ToString() == "1";

                    TxtAmount.Value = so.Amount.ToString();
                    TxtCoverType.Value = so.CoverType;
                    TxtpaperType.Value = so.PaperType;
                    txtCustomer.Value = so.Customer;
                    ddlShiters.SelectedValue = so.ShiterNumber;
                    Calendar1.SelectedDate = so.OpDate;
                }
            }
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


    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string query = string.Format(@"Update shiter_operations set 
                                    amount={0},
                                    shit_number='{1}',
                                    cover_type='{2}',
                                    customer='{3}',
                                    paper_type='{4}',
                                    op_date='{5}',
                                    setup_req={6}
                                    where id={7}",
                                                                        TxtAmount.Value, ddlShiters.SelectedValue, TxtCoverType.Value, txtCustomer.Value, TxtpaperType.Value,
            Calendar1.SelectedDate.ToString("yyyy-MM-dd HH:mm:ss"), chkSetupReq.Checked ? "1" : "0", Request.QueryString["id"]);
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