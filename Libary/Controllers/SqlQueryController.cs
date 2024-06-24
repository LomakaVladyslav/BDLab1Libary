using Microsoft.AspNetCore.Mvc;
using System.Data;
using Microsoft.Data.SqlClient;
using Libary.Data;

public class SqlQueryController : Controller
{
    private readonly string _connectionString = "Server=Vladyslav\\SQLEXPRESS; Database=DBLibary; Trusted_Connection=True; TrustServerCertificate=True;";

    [HttpGet]
    public IActionResult Index()
    {
        return View(new SqlQuery());
    }

    [HttpPost]
    public IActionResult ExecuteQuery(SqlQuery model)
    {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {

                    connection.Open();
                    using (var command = new SqlCommand(model.Query, connection))
                    {
                     if (!string.IsNullOrEmpty(model.ParameterName) && !string.IsNullOrEmpty(model.ParameterValue))
                    {
                        command.Parameters.AddWithValue(model.ParameterName, model.ParameterValue);
                    }
                    var reader = command.ExecuteReader();
                        var resultTable = new DataTable();
                        resultTable.Load(reader);
                        model.Result = DataTableToHtml(resultTable);
                    }
                }
            }
            catch (Exception ex)
            {
                model.Result = $"Error: {ex.Message}";
            }
        

        return View("Index", model);
    }

    private string DataTableToHtml(DataTable table)
    {
        var html = "<table class='table'><thead><tr>";
        foreach (DataColumn column in table.Columns)
        {
            html += $"<th>{column.ColumnName}</th>";
        }
        html += "</tr></thead><tbody>";

        foreach (DataRow row in table.Rows)
        {
            html += "<tr>";
            foreach (var cell in row.ItemArray)
            {
                html += $"<td>{cell}</td>";
            }
            html += "</tr>";
        }
        html += "</tbody></table>";

        return html;
    }
}