using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;

public class DataHolder : MonoBehaviour
{
    [SerializeField] bool DebugDataBaseLoad = false;
    public static DataHolder instance;

    public List<Country> Countries;
    public List<Occupation> Occupations;

    public List<Spell> Spells;

    public Dictionary<int, List<Settlement>> SettlementsPerCountry;
    public Dictionary<int, List<Building>> BuildingsPerSettlement;

    public Dictionary<Tuple<Language, NameCategory>, List<Name>> NameLib = new Dictionary<Tuple<Language, NameCategory>, List<Name>>();
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        //StartCoroutine(LoadNamesFromDB());
        StartCoroutine(ReadDB());
    }

    IEnumerator ReadDB()
    {
        managedLog("Loading Countries");
        yield return LoadCountriesFromDB();
        managedLog("Loading Countries Complete");
        managedLog("Loading Names");
        yield return LoadNamesFromDB();
        managedLog("Loading Names Complete");
        managedLog("Loading Spells");
        yield return LoadSpellsFromDB();
        managedLog("Loading Spells Complete");
    }

    IEnumerator LoadCountriesFromDB()
    {
        Country country = new Country();
        Countries = DBHandler.ReadEntriesFromDB<Country>(country.TableName);
        yield return null;
    }

    IEnumerator LoadSpellsFromDB()
    {
        Spell spell = new Spell();
        Spells = DBHandler.ReadEntriesFromDB<Spell>(spell.TableName);
        yield return null;
    }

    IEnumerator LoadNamesFromDB()
    {
        List<Name> Names = new List<Name>();
        Name temp = new Name();
        Names = DBHandler.ReadEntriesFromDB<Name>(temp.TableName);
        yield return null;
        for (int i = 0; i < Names.Count; i++)
        {
            Tuple<Language, NameCategory> key = new Tuple<Language, NameCategory>(Names[i].Lang, Names[i].Type);

            if (!NameLib.ContainsKey(key))
            {
                NameLib.Add(key, new List<Name>());
            }
            NameLib[key].Add(Names[i]);
        }
    }



    void managedLog(string message)
    {
        if (DebugDataBaseLoad)
        {
            Debug.Log(message);
        }
    }  
}
