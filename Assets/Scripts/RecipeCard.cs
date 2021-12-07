using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeCard : MonoBehaviour
{
    [SerializeField] MainGame main;
    [SerializeField] List<GameObject> ingredientCost;
    [SerializeField] List<GameObject> noCheck;
    [SerializeField] List<GameObject> cache;
    [SerializeField] PlayerParent parent;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip crunch;
    [SerializeField] int CardWorth;

    private bool canBuy = true;
    private string RecipeType;

    // Update is called once per frame
    void Update()
    {
        if (noCheck.Count == ingredientCost.Count)
        {
            foreach (GameObject go in cache)
            {
                parent.removeCard(go);
            }
            //Debug.Log("all ingredient here");
            main.removeRecipe(gameObject.name);
            GetComponent<BoxCollider2D>().isTrigger = false;
            GetComponent<Rigidbody2D>().mass = 0.0001f;
            gameObject.GetComponent<RecipeMove>().yesMove();
            parent.setTag(gameObject);
            parent.addCard(gameObject);
            parent.AddToPlayerIncome(CardWorth);
            parent.AddRecipeToPlayer(new Recipe(gameObject.name, RecipeType));
            cache.Clear();
            noCheck.Clear();
        }
            
        
    }

    public void cancelRecipe()
    {
        foreach (GameObject go in cache)
        {
            go.SetActive(true);
        }
        foreach (GameObject go in ingredientCost)
        {
            go.SetActive(true);
        }
        cache.Clear();
        noCheck.Clear();
        
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(gameObject.name);
        foreach(GameObject ingredient in ingredientCost)
        {            
            if (!noCheck.Contains(ingredient)) 
            {
                if (other.name.Contains(ingredient.name))
                {
                    audioSource.PlayOneShot(crunch, 1);
                    other.gameObject.transform.position = new Vector2(0, 0);
                    cache.Add(other.gameObject);
                    other.gameObject.SetActive(false);
                    noCheck.Add(ingredient);
                    //parent.removeCard(other.gameObject);
                    ingredient.SetActive(false);
                } 
            }
            
        }
    }

    public int getCardWorth()
    {
        return CardWorth;
    }

    public void SetRecipeType(string name) {
        RecipeType = name;
    }
}
