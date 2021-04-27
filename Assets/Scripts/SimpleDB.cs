using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.Collections.Generic;



public class SimpleDB : MonoBehaviour
{
    
    
    public GameObject questionPrefab;
    public Transform questionParent;

    private string dbName = "URI=file:Database.db";

    private List<Question> questions = new List<Question>();

    // Start is called before the first frame update
    void Start()
    {
        
        // run the method to create the table
        CreateDB();
        addQuestion("Mitä kuuluu?");
        addQuestion("Mikä sinun nimesi on?");
       // DisplayQuestions();
       ShowQuestions();
        
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
                command.CommandText = "CREATE TABLE IF NOT EXISTS questions (id INTEGER PRIMARY KEY NOT NULL, questiontext VARCHAR(50));";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    public void addQuestion(string question) {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO questions (questiontext) VALUES ('" + question + "');";
                command.ExecuteNonQuery(); //  this runs the SQL command
            }
            connection.Close();
        }
    }

    public void DisplayQuestions()
    {
        questions.Clear();

        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                // select everything from the table "questions"
                command.CommandText = "SELECT * FROM questions;";

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    questions.Add(new Question(reader.GetInt32(0),reader.GetString(1)));

                    reader.Close();
                }
            }
            connection.Close();
        }
    }

    private void ShowQuestions() 
    {
        
        for (int i = 0; i < questions.Count; i++)
        {
            DisplayQuestions();
            
            GameObject tmpObject = Instantiate(questionPrefab);

            Question tmpQuestion = questions[i];

            tmpObject.GetComponent<QuestionScript>().SetQuestion(tmpQuestion.questiontext);

            tmpObject.transform.SetParent(questionParent);

        }
    }

    
}
