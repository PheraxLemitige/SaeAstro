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
        if (SceneManager.GetActiveScene().name != scenaName && SceneManager.GetActiveScene().name == "ClickedSolarScene")
        {
            GameObject planetCible = GameObject.Find(namePlanetClicked);
            PlanetManager scriptPlanete = planetCible.GetComponent<PlanetManager>();
            scriptPlanete.onClick();
            scriptPlanete.visibiliteTrue();

            GameObject gameObjectCible = GameObject.Find("Player Variant");
            PlayerManager scriptCible = gameObjectCible.GetComponent<PlayerManager>();
            double positionz;
            if (planetCible.name == "Saturn" || planetCible.name == "Jupiter")
            {
                positionz = planetCible.transform.position.z + 500;
            }
            else if(planetCible.name == "Uranus")
            {
                positionz = planetCible.transform.position.z + 400;
            }
            else if (planetCible.name == "saturn")
            {
                positionz = planetCible.transform.position.z + 350;
            }
            else
            {
                positionz = planetCible.transform.position.z + 300;
            }
            scriptCible.setPosition(0, 0, positionz);
            scenaName = SceneManager.GetActiveScene().name;
        }
        else if(SceneManager.GetActiveScene().name != scenaName && SceneManager.GetActiveScene().name == "solarScene" && SceneManager.GetActiveScene().name != "Quiz")
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
