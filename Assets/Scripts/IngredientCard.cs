using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class IngredientCard : MonoBehaviour
{
    private int Id; // { get; private set; }
    private string Name; // { get; set; }
    private int Count;
    private int Cost;

    // Parameter Constructor
    public IngredientCard() {
        Id = 0;
        Name = "";
        Count = 0;
        Cost = 0;
    }
    public IngredientCard(int id, string name, int count,  int cost)
    {
        Id = id;
        Name = name;
        Count = count;
        Cost = cost;
    }

    // Copy Constructor
    public IngredientCard(IngredientCard ingredientToCopy)
    {
        //this.Id = SetIdConstructor(); // because every ingredient needs a unique ID
        this.Id = ingredientToCopy.Id;
        this.Name = ingredientToCopy.Name;
        this.Count = ingredientToCopy.Count;
        this.Cost = ingredientToCopy.Cost;
    }

    // Getters
    public int GetId()
    {
        return Id;
    }

    public string GetName() {
        return Name;
    }

    public int GetCount() {
        return Count;
    }

    public int GetCost()
    {
        return Cost;
    }

    public override string ToString()
    {
        return Name + "(id: " + Id + ", count: " + Count + ", cost: " + Cost;
    }

}