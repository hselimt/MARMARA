
using UnityEngine;
using System.Collections.Generic;
using System.Data;
using Mono.Data.SqliteClient;
using System.IO;

public class DBManager
{
    private IDbConnection _connection;
    private static DBManager _instance;
    private string _dbPath;

    public static DBManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new DBManager();
            }
            return _instance;
        }
    }

    private DBManager()
    {
        _dbPath = "URI=file:" + Application.persistentDataPath + "/todo.db";

        try
        {
            _connection = new SqliteConnection(_dbPath);
            _connection.Open();
            Debug.Log("Veritabaný baðlantýsý baþarýyla kuruldu.");

            CreateTable();
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Veritabaný baðlantý hatasý: " + ex.Message);
        }
    }

    private void CreateTable()
    {
        string sqlQuery = "CREATE TABLE IF NOT EXISTS ToDoItems (" +
                          "id INTEGER PRIMARY KEY, " +
                          "task TEXT NOT NULL, " +
                          "isComplete INTEGER NOT NULL, " +
                          "time TEXT)";

        IDbCommand command = _connection.CreateCommand();
        command.CommandText = sqlQuery;
        command.ExecuteReader();
        command.Dispose();
        Debug.Log("Tablo hazýr.");
    }

    public void SaveTask(ToDoItem task)
    {
        IDbCommand command = _connection.CreateCommand();
 
        string sqlQuery = "INSERT INTO ToDoItems (task, isComplete, time) VALUES (@task, @isComplete, @time)";

        command.CommandText = sqlQuery;
        command.Parameters.Add(new SqliteParameter("@task", task.Task));
        command.Parameters.Add(new SqliteParameter("@isComplete", task.IsComplete ? 1 : 0));
        command.Parameters.Add(new SqliteParameter("@time", task.Time));

        command.ExecuteNonQuery();
        command.Dispose();
    }

    public List<ToDoItem> GetAllTasks()
    {
        List<ToDoItem> tasks = new List<ToDoItem>();
        IDbCommand command = _connection.CreateCommand();
        string sqlQuery = "SELECT id, task, isComplete, time FROM ToDoItems";

        command.CommandText = sqlQuery;
        IDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            tasks.Add(new ToDoItem
            {
                Id = reader.GetInt32(0),
                Task = reader.GetString(1),
                IsComplete = reader.GetInt32(2) == 1,
                Time = reader.IsDBNull(3) ? "" : reader.GetString(3)
            });
        }
        reader.Close();
        command.Dispose();
        return tasks;
    }

    public ToDoItem GetTaskById(int id)
    {
        IDbCommand command = _connection.CreateCommand();
        string sqlQuery = "SELECT id, task, isComplete FROM ToDoItems WHERE id = @id";
        command.CommandText = sqlQuery;
        command.Parameters.Add(new SqliteParameter("@id", id));

        IDataReader reader = command.ExecuteReader();
        ToDoItem task = null;

        if (reader.Read())
        {
            task = new ToDoItem
            {
                Id = reader.GetInt32(0),
                Task = reader.GetString(1),
                IsComplete = reader.GetInt32(2) == 1
            };
        }

        reader.Close();
        command.Dispose();

        return task;
    }

    public void UpdateTask(ToDoItem task)
    {
        IDbCommand command = _connection.CreateCommand();
        string sqlQuery = "UPDATE ToDoItems SET task = @task, isComplete = @isComplete WHERE id = @id";

        command.CommandText = sqlQuery;
        command.Parameters.Add(new SqliteParameter("@task", task.Task));
        command.Parameters.Add(new SqliteParameter("@isComplete", task.IsComplete ? 1 : 0));
        command.Parameters.Add(new SqliteParameter("@id", task.Id));

        command.ExecuteNonQuery();
        command.Dispose();

        Debug.Log("Görev güncellendi, ID: " + task.Id);
    }

    public void DeleteTask(int id)
    {
        IDbCommand command = _connection.CreateCommand();
        string sqlQuery = "DELETE FROM ToDoItems WHERE id = @id";

        command.CommandText = sqlQuery;
        command.Parameters.Add(new SqliteParameter("@id", id));

        command.ExecuteNonQuery();
        command.Dispose();

        Debug.Log("Görev silindi, ID: " + id);
    }

    public void CloseConnection()
    {
        if (_connection != null && _connection.State == ConnectionState.Open)
        {
            _connection.Close();
            Debug.Log("Veritabaný baðlantýsý kapatýldý.");
        }
    }


}

