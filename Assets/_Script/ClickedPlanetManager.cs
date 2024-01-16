using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickedPlanetManager : MonoBehaviour
{
    private string namePlanetClicked;
    private string scenaName;
    // Start is called before the first frame update
    void Start() {
        DontDestroyOnLoad(this);
        scenaName = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update() {
        if (SceneManager.GetActiveScene().name != scenaName && SceneManager.GetActiveScene().name != "solarScene")
        {
            GameObject planetCible = GameObject.Find(namePlanetClicked);
            PlanetManager scriptPlanete = planetCible.GetComponent<PlanetManager>();
            scriptPlanete.onClick();
            scriptPlanete.textVisibiliteTrue();

            GameObject gameObjectCible = GameObject.Find("Player Variant");
            PlayerManager scriptCible = gameObjectCible.GetComponent<PlayerManager>();
            scriptCible.setPosition(0 + (planetCible.transform.position.x / 4) * 2, 0, 0 + (planetCible.transform.position.z / 4) * 2);
            scenaName = SceneManager.GetActiveScene().name;
        }
        else
        {
            GameObject gameObjectCible = GameObject.Find("Player Variant");
            PlayerManager scriptCible = gameObjectCible.GetComponent<PlayerManager>();
            scriptCible.setPosition(5.0, -0.7, 5.0);
            scenaName = SceneManager.GetActiveScene().name;
        }
    }

    public void setPlanetClicked(string planetName) {
        namePlanetClicked = planetName;
    }

    public string getNamePlanetClicked() {  
        return namePlanetClicked; 
    }
}
