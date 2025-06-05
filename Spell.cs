using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Spell : Data, IDBReadable, IDBWritable
{
    public enum CastingSpeed
    {
        None,
        Action, 
        Bonusaction,
        OneMinute,
        OneHour,
    }

    public enum SpellComponents
    {
        None,
        V,
        G,
        M,
        VG,
        VM,
        GM,
    }



    public string TableName { get; set; }
    public string TableLegend { get; set; }

    public CastingSpeed SpellSpeed;
    public SpellComponents Components;
    public int Distance;
    public string AdditionalComponents;
    public string SpellDuration;

    public Spell()
    {

    }

    public Spell(string title, string description, CastingSpeed speed, SpellComponents components, int distance, string moreComponents, string spellDur)
    {
        Title = title;
        Description = description;
        SpellSpeed = speed;
        Components = components;
        Distance = distance;
        AdditionalComponents = moreComponents;
        SpellDuration = spellDur;
    }




    public void InterprateDBData(IDataRecord data)
    {
        ID = data.GetInt32(0);
        Title = data.GetString(1);
        Description = data.GetString(2);
        SpellSpeed = (CastingSpeed)data.GetInt32(3);
        Components = (SpellComponents)data.GetInt32(4);
        Distance = data.GetInt32(5);
        AdditionalComponents = data.GetString(6);
        SpellDuration = data.GetString(7);
    }

    public void SetDBData()
    {
        TableName = "Spells";
        TableLegend = "Title, Description, CastingSpeed, SpellComponents, Distance, AdditionalComponents, SpellDuration";
    }

    public void WriteClassToDB()
    {
        DBHandler.WriteToDB(TableName, TableLegend, new List<string>() { Title, Description, ((int)SpellSpeed).ToString(), ((int)Components).ToString(), Distance.ToString(), AdditionalComponents, SpellDuration });
    }

  
}
