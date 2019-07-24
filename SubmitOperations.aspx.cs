using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SubmitOperations : System.Web.UI.Page
{
    public List<ShitterOperation> shitterOperations = new List<ShitterOperation>();
    public DataTable operationsDt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["email"] == null)
        {
            Response.Redirect("~/Login.aspx");
        }
        if (!Page.IsPostBack)
        {
            GetShiters();
            if (Session["SUBMITED_SHITTER"] != null && Session["SUBMITED_SHITTER_DATE"] != null)
            {
                ddlShiters.SelectedValue = Session["SUBMITED_SHITTER"].ToString();
                Calendar1.SelectedDate = DateTime.Parse(Session["SUBMITED_SHITTER_DATE"].ToString());
                btnSearch_Click(null, null);
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

        ddlShiters.Items.Insert(0, "select one");
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {

        DateTime selectedDate = DateTime.Parse(Calendar1.SelectedDate.Year + "/" + Calendar1.SelectedDate.Month + "/" + Calendar1.SelectedDate.Day + " 00:00:00");

        string id = ddlShiters.SelectedItem.Value;
        if (id == "select one") return;
        if (sender != null)
        {
            Session["SUBMITED_SHITTER"] = id;
            Session["SUBMITED_SHITTER_DATE"] = selectedDate;
        }

        string query = "select * from shiter_operations where shit_number=" + id + " and op_date='" + selectedDate.ToString("yyyy-MM-dd HH:mm:ss") + "' and not status=2";
        DAL dal = new DAL();
        operationsDt = dal.GetDataTable(query);
        if (operationsDt.Rows.Count > 0)
        {
            ShitterOperation so;
            foreach (DataRow row in operationsDt.Rows)
            {
                so = new ShitterOperation();
                so.ShiterNumber = row["shit_number"].ToString();
                so.Id = int.Parse(row["id"].ToString());
                so.Amount = int.Parse(row["amount"].ToString());
                so.PartialAmount = int.Parse(row["partial_amount"].ToString());
                so.CoverType = row["cover_type"].ToString();
                so.Customer = row["customer"].ToString();
                so.PaperType = row["paper_type"].ToString();
                so.OpDate = DateTime.Parse(row["op_date"].ToString());
                so.SetupReq = row["setup_req"].ToString() == "1";
                so.PartialAmountPercent = so.PartialAmount == 0 ? 0 : ((double)so.PartialAmount / (double)so.Amount) * 100;
                shitterOperations.Add(so);
            }
        }
    }
}