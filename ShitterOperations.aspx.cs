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

public partial class ShitterOperations : System.Web.UI.Page
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

            if (Session["SELECTED_EDIT_SHITTER"] != null)
            {
                var selectedShitterID = Session["SELECTED_EDIT_SHITTER"].ToString();
                if (selectedShitterID != null)
                {
                    ddlShiters.ClearSelection();
                    ddlShiters.Items.FindByValue(selectedShitterID).Selected = true;
                    if (Session["SELECTED_EDIT_SHITTER"] != null)
                    {
                        ddlShiters.SelectedValue = Session["SELECTED_EDIT_SHITTER"].ToString();
                        Session["SELECTED_EDIT_SHITTER"] = null;
                    }
                    ddlShiters_SelectedIndexChanged(null, null);
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

        ddlShiters.Items.Insert(0, "select one");
    }

    protected void ddlShiters_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = ddlShiters.SelectedItem.Value;
        Session["SELECTED_EDIT_SHITTER"] = id;
        if (id == "select one") return;
        string query = @"select * from shiter_operations 
                            inner join shiters  
                            on shiters.shit_number=shiter_operations.shit_number 
                            where shiter_operations.shit_number=" + id;
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
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (!FileUpload1.HasFile)
        {
            // No CSV file selected
            return;
        }
        using (StreamReader sr = new StreamReader(FileUpload1.PostedFile.InputStream))
        {
            FileHelperEngine engine = new FileHelperEngine(typeof(ShitterOperationExcel));
            string query = "";
            ShitterOperation so;
            DAL dal = new DAL();
            int counter = 0;
            List<ShitterOperation> shitterOperations = new List<ShitterOperation>();
            bool isFileIsValid = true;
            string validations = "";
            try
            {
                foreach (ShitterOperationExcel entry in engine.ReadStream(sr))
                {
                    var datStr = entry.OpDate.Split(' ')[0];
                    var arr = datStr.Split('/');

                    so = new ShitterOperation();
                    so.ShiterNumber = entry.ShiterNumber;
                    so.Amount = entry.Amount;
                    so.CoverType = entry.CoverType;
                    so.Customer = entry.Customer;
                    so.OpDate = new DateTime(int.Parse(arr[2]), int.Parse(arr[1]), int.Parse(arr[0]));
                    so.PaperType = entry.PaperType;
                    so.SetupReq = entry.SetupReq == "1";
                    so.PartialAmount = 0;
                    shitterOperations.Add(so);

                }
            }
            catch (Exception ex)
            {
                var newEx = (FileHelpers.ConvertException)ex;
                validations += newEx.FieldName + " - " + newEx.Message + "\n";
                isFileIsValid = false;
            }

            if (!isFileIsValid)
            {
                MessageBox.Show("File is not valid please fix before upload\n" + validations);
            }
            else
            {
                foreach (var shitterOperation in shitterOperations)
                {
                    if (IsRecordExist(shitterOperation)) continue;
                    counter++;
                    query = string.Format(@"insert into shiter_operations (amount,shit_number,cover_type,customer,paper_type,op_date,setup_req,active,partial_amount,status) 
                                                                                    values({0},{1},'{2}','{3}','{4}', '{5}' ,'{6}',1,0,0)",
                        shitterOperation.Amount,
                        shitterOperation.ShiterNumber,
                        shitterOperation.CoverType,
                        shitterOperation.Customer, shitterOperation.PaperType,
                        shitterOperation.OpDate.Date.ToString("yyyy-MM-dd HH:mm:ss"), shitterOperation.SetupReq ? "1" : "0");
                    int aff = dal.DoQuery(query);
                }
            }

            if (counter > 0)
            {
                MessageBox.Show("Upload successfuly " + counter + " items");
            }
        }

    }


    private bool IsRecordExist(ShitterOperation so)
    {
        string query = string.Format(@"select * from shiter_operations 
                                       where amount={0} and shit_number='{1}' 
                                                and cover_type='{2}' and customer='{3}' and paper_type='{4}' and op_date='{5}'",
                                     so.Amount, so.ShiterNumber, so.CoverType, so.Customer, so.PaperType,
                                     so.OpDate.Date.ToString("yyyy-MM-dd HH:mm:ss"));

        DAL d = new DAL();
        DataTable dt = d.GetDataTable(query);

        return dt.Rows.Count > 0;

    }
    private bool ValidateNumber(string numberValue)
    {
        if (string.IsNullOrEmpty(numberValue)) return false;
        try
        {
            int a = int.Parse(numberValue);
        }
        catch (Exception)
        {
            return false;
        }
        return true;
    }
    private bool ValidateString(string stringValue)
    {
        if (string.IsNullOrEmpty(stringValue)) return false;
        return true;
    }

    private bool ValidateDate(string dateValue)
    {
        if (string.IsNullOrEmpty(dateValue)) return false;
        DateTime temp;
        if (DateTime.TryParse(dateValue, out temp))
        {
            return true;
        }
        else
        {
            return false;
        }
        return true;
    }

}