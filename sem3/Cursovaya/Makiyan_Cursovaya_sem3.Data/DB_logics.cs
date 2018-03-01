using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.SQLite;

namespace MazeMain.Data
{
    public class DB_logics
    {
        private string dbName = "MyDatabase.sqlite";
        public DB_logics()
        {

            if (!File.Exists(dbName))
            {
                SQLiteConnection.CreateFile("MyDatabase.sqlite");
                SQLiteConnection dbConnecction;
                dbConnecction = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
                dbConnecction.Open();
                string sql = "create table highscores (name varchar(20), time UNSIGNED BIG INT)";
                SQLiteCommand command = new SQLiteCommand(sql, dbConnecction);
                command.ExecuteNonQuery();
                dbConnecction.Close();
            }
        }

        public void insertWinner(string name, int time)
        {
            SQLiteConnection dbConnecction;
            dbConnecction = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
            dbConnecction.Open();
            string sql = String.Format("insert into highscores (name, time) values ('{0}', {1})", name, time);
            SQLiteCommand command = new SQLiteCommand(sql, dbConnecction);
            command.ExecuteNonQuery();
            dbConnecction.Close();
        }

        public void ReadTop5Winners ()
        {
            SQLiteConnection dbConnecction;
            dbConnecction = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
            dbConnecction.Open();
            string sql = "select * from highscores order by time desc";
            SQLiteCommand command = new SQLiteCommand(sql, dbConnecction);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
                Console.WriteLine("Name: " + reader["name"] + "\tScore: " + reader["score"]);
        }
    }
}
