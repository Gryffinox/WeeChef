using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour {
    //Fudge c#, we write it like Java
    private int Id; // { get; private set; }
    private string IngredientName; // { get; set; }
    private int IngredientType; //{ get; private set; }

    public void Start() {
        Id = 0;
        IngredientName = GetComponent<SpriteRenderer>().sprite.name;
        IngredientType = 0;
    }

    public void setId(int id) {
        Id = id;
    }

    public void setIngredientName(string name) {
        IngredientName = name;
    }

    public string getIngredientName() {
        return IngredientName;
    }
}
