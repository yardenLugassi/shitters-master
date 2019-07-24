using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class submit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] != null && Request.QueryString["amount"] != null)
        {
            int amount = int.Parse(Request.QueryString["amount"]);
            int pamount = int.Parse(Request.QueryString["pamount"]);
            int lastPamount = int.Parse(Request.QueryString["lastpamount"]);

            if (lastPamount > 0 && pamount > 0)
            {
                pamount += lastPamount;    
            }

            string query = "update shiter_operations set partial_amount=" + pamount;

            if ((pamount + lastPamount) >= amount)
            {
                query += ",status=2";
            }
            else if ((pamount + lastPamount) < amount)
            {
                query += ",status=1";
            }
            query += " where id=" + Request.QueryString["id"];
            DAL d = new DAL();
            int aff = d.DoQuery(query);
            if (aff == 1)
            {
                Response.Redirect("~/SubmitOperations.aspx");
            }
        }
    }
}