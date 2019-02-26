using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TequilaShot : IDrink
{
    public string DrinkName()
    {
        return "Shot of Tequila";
    }

    public Dictionary<string, string[]> DrinkContents()
    {
        Dictionary<string, string[]> contents = new Dictionary<string, string[]>();
        contents.Add("Liquors", new string[] { "Tequila" });

        return contents;
    }

    public int TimeLimit()
    {
        return 30;
    }
}
