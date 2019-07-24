using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DAL
/// </summary>
public class DAL
{
    private SqlConnection conn;
    private SqlCommand command;
    private SqlDataAdapter adapter;

    public DAL()
    {//@"Data Source=media.ruppin.ac.il;Initial Catalog=bgroup69_test1;User ID=bgroup69;Password=bgroup69_39428";
        string conStr = @"Data Source=media.ruppin.ac.il;Initial Catalog=bgroup69_test1;User ID=bgroup69;Password=bgroup69_39428"; //@"Data Source=localhost\ELIAV;Initial Catalog=ShitersDB;Integrated Security=True";
        conn= new SqlConnection(conStr);
    }

    private void Open()
    {
        conn.Open();
    }

    private void Close()
    {
        conn.Close();
    }

    public int DoQuery(string query)
    {
        Open();
        command = new SqlCommand(query, conn);
        int afffected = command.ExecuteNonQuery();
        Close();
        return afffected;
    }

    public DataTable GetDataTable(string query)
    {
        DataTable dt = new DataTable();
        Open();
        adapter = new SqlDataAdapter(query, conn);
        adapter.Fill(dt);
        Close();
        return dt;
    }
}