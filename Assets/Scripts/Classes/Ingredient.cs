using System;
using UnityEngine;

//This class is purely an ingredient object. not attach to the game engine
[Serializable]
public class Ingredient {
    public int Id;
    public string Name;
    public int Count;
    public int Cost;

    //constructor when passing ingredient back from map
    public Ingredient(int id, string name, int cost) {
        Id = id;
        Name = name;
        Cost = cost;
        Count = 0;  //the original count no longer matters. it only mattered for the deck generation
    }

    public override string ToString() {
        return Name + " (id: " + Id + ", cost: " + Cost + ")";
    }
}

[Serializable]
//helper class for JSON parsing
public class Ingredients {
    public Ingredient[] ingredients;
}