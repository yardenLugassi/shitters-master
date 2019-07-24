using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

public partial class ShitterPriceMaintenance : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            DAL d = new DAL();
            string query = "select * from shitter_costs";
            DataTable dt = d.GetDataTable(query);

            if (dt.Rows.Count > 0)
            {
                txtChangeCover.Value = dt.Rows[0]["cover_cost"].ToString();
                txtChangePaper.Value = dt.Rows[0]["paper_cost"].ToString();
            }

        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        DAL d = new DAL();

        string query = string.Format("update shitter_costs set cover_cost={0}, paper_cost={1}", txtChangeCover.Value,
                                     txtChangePaper.Value);
        int aff = d.DoQuery(query);
        if (aff == 1)
        {
            MessageBox.Show("Update cost successfully");
        }
    }
}