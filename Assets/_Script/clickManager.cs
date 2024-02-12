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
            SceneManager.LoadScene(3);
        }
        else
        {
            SceneManager.LoadScene(2);
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
        GameObject clickedSatellite = GameObject.Find("ClickedPlanetManager");
        ClickedPlanetManager scriptClickedSatellite = clickedSatellite.GetComponent<ClickedPlanetManager>();

        scriptClickedSatellite.setSatelliteClicked();

        if (isSatelliteClicked)
        {
            SceneManager.LoadScene(3);
        }
        else
        {
            SceneManager.LoadScene(2);
            isClicked = false;
            //GameObject gameObjectCible = GameObject.Find("Player Variant");
            //Destroy(gameObjectCible);
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