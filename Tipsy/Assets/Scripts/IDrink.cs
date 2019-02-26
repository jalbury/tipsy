using System.Collections;
using System.Collections.Generic;

public interface IDrink 
{
    string DrinkName();
    Dictionary<string, string[]> DrinkContents();
    int TimeLimit();
}
