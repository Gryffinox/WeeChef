using System;
using UnityEngine;

//This class is purely an ingredient object. not attach to the game engine
[Serializable]
public class Ingredient {
    public int Id;
    public string Name;
    public int Count;
    public int Cost;

    public Ingredient(int id, string name, int cost) {
        Id = id;
        Name = name;
        Cost = cost;
        Count = 0;  //the original count no longer matters. it only mattered for the deck generation
    }

    public override string ToString() {
        return Name + " (id: " + Id + ", count: " + Count + ", cost: " + Cost + ")";
    }
}

[Serializable]
public class Ingredients {
    public Ingredient[] ingredients;
}