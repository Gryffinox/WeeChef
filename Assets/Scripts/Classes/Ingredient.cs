using System;
using UnityEngine;

[Serializable]
public class Ingredient {
    public int Id;
    public string Name;
    public int Count;
    public int Cost;

    public override string ToString() {
        return Name + " (id: " + Id + ", count: " + Count + ", cost: " + Cost + ")";
    }
}

[Serializable]
public class Ingredients {
    public Ingredient[] ingredients;
}