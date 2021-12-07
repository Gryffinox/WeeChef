using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class RecipeList : MonoBehaviour {

    [SerializeField] TextAsset RecipesFile;

    private static List<Recipe> RecipesDeck;

    //janky time
    //french recipes
    [SerializeField] private GameObject Croissant;
    [SerializeField] private GameObject Souffle;
    [SerializeField] private GameObject Cassoulet;
    [SerializeField] private GameObject Ratatouille;
    [SerializeField] private GameObject Coqauvin;
    [SerializeField] private GameObject Tartiflette;
    [SerializeField] private GameObject Bouillabaise;
    //Italian recipes
    [SerializeField] private GameObject Gelato;
    [SerializeField] private GameObject Saltimbocca;
    [SerializeField] private GameObject Arancini;
    [SerializeField] private GameObject PizzaMargherita;
    [SerializeField] private GameObject Linguine;
    [SerializeField] private GameObject Risotto;
    [SerializeField] private GameObject Lasagne;
    //japanese
    [SerializeField] private GameObject Nigiri;
    [SerializeField] private GameObject FurutsuSando;
    [SerializeField] private GameObject TonkotsuRamen;
    [SerializeField] private GameObject WagyuTataki;
    [SerializeField] private GameObject Gyoza;
    [SerializeField] private GameObject MisoSoup;
    [SerializeField] private GameObject BeefSukiyaki;
    //indian
    [SerializeField] private GameObject Naan;
    [SerializeField] private GameObject PalakPaneer;
    [SerializeField] private GameObject Korma;
    [SerializeField] private GameObject GulabJamun;
    [SerializeField] private GameObject Samosas;
    [SerializeField] private GameObject Vindaloo;
    [SerializeField] private GameObject TikkaMasala;
    //mediterranean
    [SerializeField] private GameObject Hummus;
    [SerializeField] private GameObject Dolma;
    [SerializeField] private GameObject Baklava;
    [SerializeField] private GameObject Moussaka;
    [SerializeField] private GameObject Shawarma;
    [SerializeField] private GameObject Couscous;
    [SerializeField] private GameObject Paella;
    //south east asian
    [SerializeField] private GameObject Satay;
    [SerializeField] private GameObject ChaSiuBao;
    [SerializeField] private GameObject Pho;
    [SerializeField] private GameObject Turon;
    [SerializeField] private GameObject SpringRoll;
    [SerializeField] private GameObject DuckRice;
    [SerializeField] private GameObject PadThai;
    //caribbean
    [SerializeField] private GameObject FriedPlantains;
    [SerializeField] private GameObject JerkChicken;
    [SerializeField] private GameObject Callaloo;
    [SerializeField] private GameObject Accra;
    [SerializeField] private GameObject GoatRoti;
    [SerializeField] private GameObject Griot;
    [SerializeField] private GameObject Jambalaya;


    void Start() {
        // Recipe Loading straight to deck
        RecipesDeck = new List<Recipe>();
        LoadRecipesToDeck();
    }

    public void LoadRecipesToDeck() {
        Recipes recipes = JsonUtility.FromJson<Recipes>(RecipesFile.text);
        foreach (Recipe recipe in recipes.recipes) {
            RecipesDeck.Add(recipe);
        }
        Shuffle();
    }

    //shuffles the recipe deck
    private void Shuffle() {
        int i = RecipesDeck.Count;
        while (i > 1) {
            i--;
            int s = Random.Range(0, i + 1);
            Recipe swap = RecipesDeck[s];
            RecipesDeck[s] = RecipesDeck[i];
            RecipesDeck[i] = swap;
        }
    }

    //draw a recipe card from the deck
    public Recipe DrawRecipeCard() {
        if (RecipesDeck.Count == 0) {
            return null;    //no cards left to return deck is empty
        }
        Recipe drawnCard = RecipesDeck[0];
        RecipesDeck.RemoveAt(0);
        return drawnCard;
    }

    //can be used to diable or enable a recipe card object
    public void ToggleRecipeCard(string RecipeName, string RecipeType, bool state, float y) {
        GameObject pointer;
        //point to the right object based on recipe name
        switch (RecipeName) {
            //french
            case "Croissant":
                pointer = Croissant;
                break;
            case "Souffl�":
                pointer = Souffle;
                break;
            case "Cassoulet":
                pointer = Cassoulet;
                break;
            case "Ratatouille":
                pointer = Ratatouille;
                break;
            case "Coq-au-vin":
                pointer = Coqauvin;
                break;
            case "Tartiflette":
                pointer = Tartiflette;
                break;
            case "Bouillabaisse":
                pointer = Bouillabaise;
                break;
                //italian
            case "Gelato":
                pointer = Gelato;
                break;
            case "Saltimbocca":
                pointer = Saltimbocca;
                break;
            case "Arancini":
                pointer = Arancini;
                break;
            case "Pizza Margherita":
                pointer = PizzaMargherita;
                break;
            case "Linguine Alio e Olio":
                pointer = Linguine;
                break;
            case "Risotto ai Funghi":
                pointer = Risotto;
                break;
            case "Lasagne al Forno":
                pointer = Lasagne;
                break;
                //japanese
            case "Salmon Nigiri":
                pointer = Nigiri;
                break;
            case "Furutsu Sando":
                pointer = FurutsuSando;
                break;
            case "Tonkotsu Ramen":
                pointer = TonkotsuRamen;
                break;
            case "Wagyu Tataki":
                pointer = WagyuTataki;
                break;
            case "Gyoza":
                pointer = Gyoza;
                break;
            case "Miso Soup":
                pointer = MisoSoup;
                break;
            case "Beef Sukiyaki":
                pointer = BeefSukiyaki;
                break;
                //indian
            case "Naan":
                pointer = Naan;
                break;
            case "Palak Paneer":
                pointer = PalakPaneer;
                break;
            case "Korma":
                pointer = Korma;
                break;
            case "Gulab Jamun":
                pointer = GulabJamun;
                break;
            case "Samosas":
                pointer = Samosas;
                break;
            case "Vindaloo":
                pointer = Vindaloo;
                break;
            case "Tikka Masala":
                pointer = TikkaMasala;
                break;
                //mediterranena
            case "Hummus":
                pointer = Hummus;
                break;
            case "Dolma":
                pointer = Dolma;
                break;
            case "Baklava":
                pointer = Baklava;
                break;
            case "Moussaka":
                pointer = Moussaka;
                break;
            case "Shawarma":
                pointer = Shawarma;
                break;
            case "Couscous":
                pointer = Couscous;
                break;
            case "Paella":
                pointer = Paella;
                break;
                //south east asian
            case "Satay":
                pointer = Satay;
                break;
            case "Char Siu Bao":
                pointer = ChaSiuBao;
                break;
            case "Pho":
                pointer = Pho;
                break;
            case "Turon":
                pointer = Turon;
                break;
            case "Spring Roll":
                pointer = SpringRoll;
                break;
            case "Duck Rice":
                pointer = DuckRice;
                break;
            case "Pad Thai":
                pointer = PadThai;
                break;
                //carribean
            case "Fried Plantains":
                pointer = FriedPlantains;
                break;
            case "Jerk Chicken":
                pointer = JerkChicken;
                break;
            case "Callaloo":
                pointer = Callaloo;
                break;
            case "Accra":
                pointer = Accra;
                break;
            case "Goat Roti":
                pointer = GoatRoti;
                break;
            case "Griot":
                pointer = Griot;
                break;
            case "Jambalaya":
                pointer = Jambalaya;
                break;
            default:
                throw new System.Exception("Unknown recipe");
        }
        pointer.GetComponent<RecipeCard>().SetRecipeType(RecipeType);
        pointer.SetActive(state);   //enable or disable
        pointer.transform.position = new Vector3(pointer.transform.position.x, y, pointer.transform.position.z);    //move y
    }
}
