using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeCard : MonoBehaviour
{
    [SerializeField] List<GameObject> ingredientCost;
    [SerializeField] List<GameObject> noCheck;
    [SerializeField] List<GameObject> cache;
    [SerializeField] PlayerParent parent;
    private bool canBuy = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (noCheck.Count == ingredientCost.Count)
        {
            foreach (GameObject go in cache)
            {
                parent.removeCard(go);
            }
            Debug.Log("all ingredient here");
            GetComponent<BoxCollider2D>().isTrigger = false;
            gameObject.GetComponent<RecipeMove>().yesMove();
            parent.setTag(gameObject);
            parent.addCard(gameObject);
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
        foreach(GameObject ingredient in ingredientCost)
        {            
            if (!noCheck.Contains(ingredient)) 
            {
                if (other.name.Contains(ingredient.name))
                {
                    other.gameObject.transform.position = new Vector2(0, 0);
                    Debug.Log("name match");
                    cache.Add(other.gameObject);
                    other.gameObject.SetActive(false);
                    noCheck.Add(ingredient);
                    //parent.removeCard(other.gameObject);
                    ingredient.SetActive(false);
                } 
            }
            
        }
    }
}
