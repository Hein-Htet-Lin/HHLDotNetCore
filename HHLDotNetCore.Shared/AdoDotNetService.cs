using System.Data;
using MySqlConnector;

namespace HHLDotNetCore.Shared;

public class AdoDotNetService
{
    private readonly string _connectionString;
    public AdoDotNetService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public DataTable Query(string query,params ParameterModel[] parameters)
    {
        MySqlConnection connection = new MySqlConnection(_connectionString);
        connection.Open();

        MySqlCommand cmd = new MySqlCommand(query,connection);
        foreach (var parameter in parameters)
        {
            cmd.Parameters.AddWithValue(parameter.Name,parameter.Value);
        }

        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adapter.Fill(dt);
        connection.Close();

        return dt;
    }

    public int Execute(string query,params ParameterModel[] parameters)
    {
        MySqlConnection connection = new MySqlConnection(_connectionString);
        connection.Open();

        MySqlCommand cmd = new MySqlCommand(query,connection);
        foreach (var parameter in parameters)
        {
            cmd.Parameters.AddWithValue(parameter.Name,parameter.Value);
        }

        var result = cmd.ExecuteNonQuery();

        connection.Close();
        return result;
    }

    
}

public class ParameterModel
    {
        public string Name {get;set;}

        public object Value {get;set;}

        public ParameterModel(string name,object value)
            {
                Name = name;
                Value = value;
            }
    }
