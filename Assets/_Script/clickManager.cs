using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ClickManager : MonoBehaviour {

    private bool isClicked = false;

    void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void clickPlanets(string planet) {

        isClicked = !isClicked;

        GameObject clickedPlanet = GameObject.Find("ClickedPlanetManager");
        ClickedPlanetManager scriptClickedPlanet = clickedPlanet.GetComponent<ClickedPlanetManager>();
        scriptClickedPlanet.setPlanetClicked(planet);
        if (isClicked)
        {
            SceneManager.LoadScene(2);
        }
        else
        {
            SceneManager.LoadScene(1);
            GameObject gameObjectCible = GameObject.Find("Player Variant");
            Destroy(gameObjectCible);
        }
    }

        public void renderObject(GameObject objet, bool visibility) {
        LineRenderer objectRender = objet.transform.GetComponent<LineRenderer>();
        objectRender.enabled = visibility;
    }
}