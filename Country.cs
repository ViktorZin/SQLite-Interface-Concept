using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;

public class Country : Data, IDBWritable, IDBReadable
{
    public string TableName { get; set; }
    public string TableLegend { get; set; }
    public string Title;
    public string Description;


    public Country()
    {
        SetDBData();
    }

    public Country(string title, string description) : base(title, description)
    {
        Title = title;
        Description = description;
        SetDBData();
    }

    public void SetDBData()
    {
        TableName = "Countries";
        TableLegend = "Title, Description";
    }
   
    public void WriteClassToDB()
    {
        DBHandler.WriteToDB(TableName, TableLegend, new List<string>() { Title, Description });
    }

    public void InterprateDBData(IDataRecord data)
    {
        ID = data.GetInt32(0);
        Title = data.GetString(1);
        Description = data.GetString(2);
    }
}