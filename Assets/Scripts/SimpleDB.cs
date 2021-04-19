using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;



public class SimpleDB : MonoBehaviour
{
    private string dbName = "URI=file:Database.db";
    // Start is called before the first frame update
    void Start()
    {
        // run the method to create the table
        CreateDB();

        DisplayQuestions();
        
    }

    public void CreateDB()
    {
        // Create the db connection
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            // set up and object (called "command") to allow db control
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "CREATE TABLE IF NOT EXISTS questions (text VARCHAR(50);";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    public void addQuestion() {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO questions (text) VALUES ('" + "Mitä kuuluu?" + "');";
                command.ExecuteNonQuery(); //  this runs the SQL command
            }
            connection.Close();
        }
    }

    public void DisplayQuestions()
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                // select everything from the table "questions"
                command.CommandText = "SELECT * FROM weapons;";

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    Debug.Log("Question: " + reader["text"]);

                    reader.Close();
                }
            }
            connection.Close();
        }
    }

    
}
