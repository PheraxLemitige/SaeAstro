using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Valve.VR.Extras;

public class SteamVRLaserWrapper : MonoBehaviour {
    private SteamVR_LaserPointer steamVrLaserPointer;
    private bool isClicked = false;
    private string planetClicked = "";

    private void Awake() {
        steamVrLaserPointer = gameObject.GetComponent<SteamVR_LaserPointer>();
        steamVrLaserPointer.PointerIn += OnPointerIn;
        steamVrLaserPointer.PointerOut += OnPointerOut;
        steamVrLaserPointer.PointerClick += OnPointerClick;
    }

    private void OnPointerClick(object sender, PointerEventArgs e) {
        //setLaserVisible(true);

        if (e.target.name == "Button") {

            if (e.target.GetComponentInParent<Button>())
                e.target.GetComponentInParent<Button>().onClick.Invoke();
            
        }
        else if (e.target.name == "Coque") {
            Debug.Log("J'ai cliqué sur la coque de la fusée");

            // Ajoute le code de téléportation ici
            SceneManager.LoadScene(5);
        }
        else {
            GameObject gameObjectCible = GameObject.Find(e.target.name);
            Debug.Log(gameObjectCible);
            PlanetManager planetManager = gameObjectCible.GetComponent<PlanetManager>();
            Debug.Log(planetManager.getIsGrab());
            if (gameObjectCible.tag == "Planet") {
                if (!isClicked && !planetManager.getIsGrab()) {
                    GameObject gameObjectClick = GameObject.Find("clickManager");
                    ClickManager clickScript = gameObjectClick.GetComponent<ClickManager>();
                    if (clickScript != null)
                        clickScript.clickPlanets(e.target.name);
                    isClicked = true;
                    planetClicked = e.target.name;
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