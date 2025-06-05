using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data.SQLite;
using System.Data;
using System.IO;

public static class DBHandler
{
   
    public static bool IsDBConnectionOpen { get; private set; }


    private static SQLiteConnection activeDBConnection;

    public static void OpenDBConnection()
    {
        if (!IsDBConnectionOpen)
        {
            activeDBConnection = new SQLiteConnection(@"Data Source=" + Application.streamingAssetsPath + "/ActiveDataBase/Base.db");
            activeDBConnection.Open();
            IsDBConnectionOpen = true;
        }
    }

    public static void CloseDBConnection()
    {
        if (IsDBConnectionOpen)
        {
            activeDBConnection.Close();
            activeDBConnection = null;
            IsDBConnectionOpen = false;
        }
    }

    public static void WriteToDB(string table, string tableLegend, List<string> entries)
    {
        if (!IsDBConnectionOpen)
        {
            OpenDBConnection();
        }

        string modifiedTable = "SELECT * FROM " + table;
        SQLiteCommand command = new SQLiteCommand(modifiedTable, activeDBConnection);

        Debug.Log("Table: " + modifiedTable);

        string valueString = "";
        for(int i = 0; i < entries.Count; i++)
        {
            valueString += "'" + entries[i] + "'";
            if(i != (entries.Count - 1))
            {
                valueString += ", ";
            }
        }

        command.CommandText = "INSERT INTO " + table + "(" + tableLegend + ")" + " VALUES(" + valueString + ")";

        command.ExecuteNonQuery();
    }

    public static List<T> ReadEntriesFromDB<T>(string table) where T : IDBReadable, new()
    {
        if (!IsDBConnectionOpen)
        {
            OpenDBConnection();
        }

        List<T> classList = new List<T>();

        string modifiedTableName = "SELECT * FROM " + table;

        SQLiteCommand command = new SQLiteCommand(modifiedTableName, activeDBConnection);
        SQLiteDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            IDataRecord record = (IDataRecord)reader;
            classList.Add(new T());
            classList[classList.Count - 1].InterprateDBData(record);
        }

        return classList;
    }
   
}
