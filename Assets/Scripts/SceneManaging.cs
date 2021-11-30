using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManaging : MonoBehaviour {
    void Start() {
        SceneManager.LoadScene("RecipeBuilding", LoadSceneMode.Additive);
    }
}
