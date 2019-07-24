using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService
{

    public WebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public bool SaveSimulation(string shitterId, DateTime date, string operations)
    {
        string query = "insert into shitter_simulation_history (shitter,op_date,operations,updated) " +
                       "values (" + shitterId + ",'" +
                       date.ToString("yyyy-MM-dd HH:mm:ss") + "','" + operations + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
        DAL d = new DAL();
        int aff = d.DoQuery(query);
        return (aff == 1);

    }

    [WebMethod]
    public List<ShitterOperation> GetOperationByDay(string shitterId, DateTime date)
    {
        //ToString("yyyy-MM-dd HH:mm:ss")
        string query = "select * from shiter_operations where shit_number=" + shitterId + " and op_date='" + date.ToString("yyyy-MM-dd HH:mm:ss") + "' order by cover_type,paper_type";
        DAL dal = new DAL();
        DataTable dt = dal.GetDataTable(query);
        List<ShitterOperation> lst = new List<ShitterOperation>();
        ShitterOperation so;
        foreach (DataRow row in dt.Rows)
        {
            so = new ShitterOperation();
            so.Id = int.Parse(row["id"].ToString());
            so.PaperType = row["paper_type"].ToString();
            so.Customer = row["customer"].ToString();
            so.CoverType = row["cover_type"].ToString();
            so.ShiterNumber = row["shit_number"].ToString();
            so.Amount = int.Parse(row["amount"].ToString());
            so.SetupReq = row["setup_req"].ToString() == "true";

            lst.Add(so);
        }

        return lst;
    }
    [WebMethod]
    public List<ShitterOperation> GetOperationSimulationsByDay(string shitterId, DateTime date)
    {
        List<ShitterOperation> lstOps = new List<ShitterOperation>();
        string query = "select top 1 * from shitter_simulation_history where shitter=" + shitterId + " and op_date='" + date.ToString("yyyy-MM-dd HH:mm:ss") + "' order by updated desc";
        DAL dal = new DAL();
        DataTable dt = dal.GetDataTable(query);

        if (dt.Rows.Count > 0)
        {
            var arr = dt.Rows[0]["operations"].ToString().Split(',');
            DataTable optDt;
            ShitterOperation s;
            foreach (var i in arr)
            {
                query = "select * from shiter_operations where id=" + i;
                optDt = dal.GetDataTable(query);
                s = AddaptShitterOperation(optDt);
                lstOps.Add(s);
            }
        }


        return lstOps;

    }

    private ShitterOperation AddaptShitterOperation(DataTable dt)
    {
        if (dt.Rows.Count == 0) return new ShitterOperation();
        ShitterOperation so = new ShitterOperation();
        so.Id = int.Parse(dt.Rows[0]["id"].ToString());
        so.PaperType = dt.Rows[0]["paper_type"].ToString();
        so.Customer = dt.Rows[0]["customer"].ToString();
        so.CoverType = dt.Rows[0]["cover_type"].ToString();
        so.ShiterNumber = dt.Rows[0]["shit_number"].ToString();
        so.Amount = int.Parse(dt.Rows[0]["amount"].ToString());
        so.SetupReq = dt.Rows[0]["setup_req"].ToString() == "true";

        return so;
    }

}
