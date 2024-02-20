using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ClickManager : MonoBehaviour {

    private bool isClicked = false;
    private bool isSatelliteClicked = false;

    void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void clickPlanets(string planet) {

        isClicked = !isClicked;

        GameObject clickedPlanet = GameObject.Find("ClickedPlanetManager");
        ClickedPlanetManager scriptClickedPlanet = clickedPlanet.GetComponent<ClickedPlanetManager>();
        
        Debug.Log(planet);
        scriptClickedPlanet.setPlanetClicked(planet);
        if (isClicked)
        {
            scriptClickedPlanet.setSceneAvant(SceneManager.GetActiveScene().name);
            SceneManager.LoadScene(3);
        }
        else
        {
            GameObject gameObjectCible = GameObject.Find("Player Variant");
            PlayerManager scriptCible = gameObjectCible.GetComponent<PlayerManager>();
            if (scriptClickedPlanet.getSceneAvant() == "solarScene")
            {
                SceneManager.LoadScene(2);
                scriptCible.setPosition(5.0, -0.7, 5.0);
            }
            else
            {
                SceneManager.LoadScene(5);
                scriptCible.setPosition(-0.1, 0.0009, -0.2);
            }
        }
        if (isSatelliteClicked)
        {
            isSatelliteClicked = false;
            scriptClickedPlanet.setSatelliteClicked();
        }
    }

    public void clickSatellite()
    {
        Debug.Log("Satellite cliqué !");
        isSatelliteClicked = !isSatelliteClicked;


        GameObject clickedPlanetManager = GameObject.Find("ClickedPlanetManager");
        ClickedPlanetManager scriptClickedPlanetManager = clickedPlanetManager.GetComponent<ClickedPlanetManager>();

        scriptClickedPlanetManager.setSatelliteClicked();
        Debug.Log("isSatelliteClicked = " + isSatelliteClicked + " | getReloadCounter = " + scriptClickedPlanetManager.getReloadCounter());

        if (scriptClickedPlanetManager.getReloadCounter() != 0)
        {
            if (isSatelliteClicked)
                SceneManager.LoadScene(3);
            else
            {
                SceneManager.LoadScene(2);
                isClicked = false;
                //GameObject gameObjectCible = GameObject.Find("Player Variant");
                //Destroy(gameObjectCible);
            }
        }
        else if (SceneManager.GetActiveScene().name == "ClickedSolarScene")
        {
            if (scriptClickedPlanetManager.getSceneAvant() == "solarScene")
            {
                SceneManager.LoadScene(2);
            }
            else
            {
                SceneManager.LoadScene(5);
            }
            isClicked = false;
        }

    }

    public void renderObject(GameObject objet, bool visibility) {
        LineRenderer objectRender = objet.transform.GetComponent<LineRenderer>();
        objectRender.enabled = visibility;
    }
    public void clickQuiz() {
        this.isClicked = false;
    }
}