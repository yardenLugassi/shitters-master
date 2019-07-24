using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

public partial class EditShiter : System.Web.UI.Page
{
    
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["email"] == null)
        {
            Response.Redirect("~/Login.aspx");
        }
        if (!Page.IsPostBack)
        {
            
            if (Request.QueryString["id"] != null)
            {
                string query = "select * from shiters where id=" + Request.QueryString["id"];
                DAL dal = new DAL();
                DataTable dt = dal.GetDataTable(query);
                if (dt.Rows.Count > 0)
                {
                    txtName.Value = dt.Rows[0]["name"].ToString();
                }
            }    
        }
    }


    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string query = string.Format(@"Update shiters set 
                                    name='{0}' where id={1}",txtName.Value,  Request.QueryString["id"]);
        DAL dal = new DAL();
        int aff = dal.DoQuery(query);
        if (aff == 1)
        {
            Response.Redirect("~/Shiters.aspx");
        }
        else
        {
            MessageBox.Show("Sorry can you try later");
        }
    }
}