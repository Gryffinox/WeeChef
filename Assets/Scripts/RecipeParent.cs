using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeParent : MonoBehaviour
{
    BoxCollider2D[] children;
    RecipeCard[] childscript;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void triggerOff()
    {
        children = GetComponentsInChildren<BoxCollider2D>();
        Debug.Log("Trigger off called");
       foreach(BoxCollider2D child in children)
        {
            Debug.Log("child loop running");
            if (child.tag != "p1" && child.tag != "p2" && child.tag != "p3" && child.tag != "p4")
            {
                child.isTrigger = false;
            }
        }
    }

    public void triggerOn()
    {
        children = GetComponentsInChildren<BoxCollider2D>();
        foreach (BoxCollider2D child in children)
        {
            if (child.tag != "p1" && child.tag != "p2" && child.tag != "p3" && child.tag != "p4")
            {
                child.isTrigger = true;
            }
        }
    }

    public void returnIngredient()
    {
        childscript = GetComponentsInChildren<RecipeCard>();
        foreach(RecipeCard child in childscript)
        {
            child.cancelRecipe();
        }
    }
}
