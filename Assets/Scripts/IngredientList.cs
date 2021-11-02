using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientList : MonoBehaviour{
    private List<GameObject> ingredientList;
    [SerializeField] GameObject Apple;
    [SerializeField] GameObject Bacon;
    [SerializeField] GameObject Cheese;
    [SerializeField] GameObject Eggplant;
    [SerializeField] GameObject Eggs;
    [SerializeField] GameObject Honey;
    [SerializeField] GameObject Lemon;
    [SerializeField] GameObject Onion;
    [SerializeField] GameObject Shrimp;
    [SerializeField] GameObject Wine;

    public void Start() {
        ingredientList = new List<GameObject>();
        ingredientList.Add(Apple);
        ingredientList.Add(Bacon);
        ingredientList.Add(Cheese);
        ingredientList.Add(Eggplant);
        ingredientList.Add(Eggs);
        ingredientList.Add(Honey);
        ingredientList.Add(Lemon);
        ingredientList.Add(Onion);
        ingredientList.Add(Shrimp);
        ingredientList.Add(Wine);

        for(int i = 0; i < ingredientList.Count; i++) {
            ingredientList[i].GetComponent<Ingredient>().setId(i);
        }
    }

    public List<GameObject> getIngredientList() {
        return ingredientList;
    }
}
