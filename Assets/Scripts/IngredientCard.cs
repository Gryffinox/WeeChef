using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This class is a component attached to the gameobject cards in the scene
public class IngredientCard : MonoBehaviour {
    private int Id;
    private string Name;
    private int Cost;

    // Getters
    public int GetId() {
        return Id;
    }

    public string GetName() {
        return Name;
    }

    public int GetCost() {
        return Cost;
    }

    //Sets the values from an Ingredient Object
    public void SetIngredient(int id, string name, int cost) {
        Id = id;
        Name = name;
        Cost = cost;
    }

    public override string ToString() {
        return Name + "(id: " + Id + ", cost: " + Cost;
    }

}