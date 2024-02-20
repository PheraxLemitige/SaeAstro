using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Valve.VR.Extras;

public class SteamVRLaserWrapper : MonoBehaviour {
    private SteamVR_LaserPointer steamVrLaserPointer;
    private bool isClicked = false;
    private string planetClicked = "";
    private bool satelliteClicked = false; 

    private void Awake() {
        steamVrLaserPointer = gameObject.GetComponent<SteamVR_LaserPointer>();
        steamVrLaserPointer.PointerIn += OnPointerIn;
        steamVrLaserPointer.PointerOut += OnPointerOut;
        steamVrLaserPointer.PointerClick += OnPointerClick;
    }

    private void OnPointerClick(object sender, PointerEventArgs e) {
        Debug.Log(e.target.name);
        //setLaserVisible(true);

        if (e.target.name == "Button") {

            if (e.target.GetComponentInParent<Button>())
                e.target.GetComponentInParent<Button>().onClick.Invoke();
            
        }
        else if (e.target.name == "Fusee") {

            // Ajoute le code de téléportation ici
            GameObject playerVariant = GameObject.Find("Player Variant");
            PlayerManager playerManager = playerVariant.GetComponent<PlayerManager>();
            playerManager.setPosition(5.0, -0.7, 5.0);
            SceneManager.LoadScene(2);
        }
        else if (e.target.name == "Mansion_Low" || e.target.name == "Musee")
        {

            // Ajoute le code de téléportation ici
            SceneManager.LoadScene(5);

            GameObject gameObjectCible = GameObject.Find("Player Variant");
            PlayerManager scriptCible = gameObjectCible.GetComponent<PlayerManager>();
            scriptCible.setPosition(-0.1, 0.0009, -0.2);
        }
        else if(e.target.tag == "Planet")
        {
            planetClicked = e.target.name;

            GameObject gameObjectCible = GameObject.Find(e.target.name);
            PlanetManager planetManager = gameObjectCible.GetComponent<PlanetManager>();
            if (!isClicked && !planetManager.getIsGrab()) {
                GameObject gameObjectClick = GameObject.Find("clickManager");
                ClickManager clickScript = gameObjectClick.GetComponent<ClickManager>();
                if (clickScript != null)
                    clickScript.clickPlanets(e.target.name);
                isClicked = true;
            }
            
            else if(planetClicked != "") {
                GameObject gameObjectClick = GameObject.Find("clickManager");
                ClickManager clickScript = gameObjectClick.GetComponent<ClickManager>();
                if (clickScript != null)
                    clickScript.clickPlanets(planetClicked);
                isClicked = false;
                planetClicked = "";
                
            }
        }
        else if (e.target.tag == "Satellite")
        {
            GameObject gameObjectSatellite = GameObject.Find(e.target.name);
            SatelliteManager satelliteManager = gameObjectSatellite.GetComponent<SatelliteManager>();
            if (!satelliteClicked)
            {
                GameObject gameObjectSatelliteClick = GameObject.Find("clickManager");
                ClickManager clickScript = gameObjectSatelliteClick.GetComponent<ClickManager>();

                if (clickScript != null)
                    clickScript.clickSatellite();

                if(!isClicked)
                    isClicked = true;

                satelliteClicked = !satelliteClicked;
            }

            else 
            {
                GameObject gameObjectClick = GameObject.Find("clickManager");
                ClickManager clickScript = gameObjectClick.GetComponent<ClickManager>();
                if (clickScript != null)
                    clickScript.clickSatellite();
                satelliteClicked = false;
                isClicked = false;
            }
        }
    }

    private void OnPointerOut(object sender, PointerEventArgs e) {
        //setLaserVisible(false);
    }

    private void OnPointerIn(object sender, PointerEventArgs e) {
        //setLaserVisible(true);
    }

    /*private void setLaserVisible(bool visible) {
        MeshRenderer laserPointerRender = steamVrLaserPointer.gameObject.transform.GetChild(5).GetComponentInChildren<MeshRenderer>();
        laserPointerRender.enabled = visible;
    }*/
}