using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickedPlanetManager : MonoBehaviour
{
    private string namePlanetClicked;
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        if (SceneManager.GetActiveScene().name == "ClickedSolarScene")
            Debug.Log(namePlanetClicked);
    }

    public void setPlanetClicked(string planetName) {
        namePlanetClicked = planetName;
    }

    public string getNamePlanetClicked() {  
        return namePlanetClicked; 
    }
}
