using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using Valve.VR;
using Valve.VR.InteractionSystem;
using UnityEngine.SceneManagement;

public class SatelliteManager : MonoBehaviour
{
    // VARIABLE : Elles vont contenir des données
    // Représente les objets de la planète concerné et du soleil
    private string center;
    private GameObject centerPlanet;
    // Permet de contenir la vitesse de rotation
    private float epsi;
    // Permet de contenir les positions du soleil
    private float positionPlanetx;
    private float positionPlanety;
    private float positionPlanetz;

    private float positionActux;
    private float positionActuy;
    private float positionActuz;

    // VARIABLE : Elles vont permettre de modifier directement les données
    // Permet de définir la taille de la terre
    private float tailleSatellite;
    // Permet de définir l'ellipse de la planète
    private float longueurEllipse;
    private float largeurEllipse;
    private float hauteurEllipse;
    // Permet de définir la rotation de la planète
    private float rotationx;
    private float rotationy;
    private float rotationz;
    // Permet de définir la vitesse de la planète
    private float vitesse; // 0.3 = 104 000 km

    // Permet de changer le scale des planètes selon la scène
    private float scale = 1;

    private Interactable interactable;
    private Rigidbody rigidBody;
    private float waitTime;
    private float timer = 0.0f;
    private bool isGrab;
    private bool isClicked = false;

    // Start is called before the first frame update
    void Start() {
        if (SceneManager.GetActiveScene().name == "solarScene")
        {
            scale = 0.006f;
        }
        else
        {
            scale = 1f;
        }
        loadVariables();
        
        // Permet de récupérer l'ensemble des positions x, y et z de la lune
        positionActux = transform.position.x;
        positionActuy = transform.position.y;
        positionActuz = transform.position.z;

        if (interactable == null)
            interactable = GetComponent<Interactable>();
        if (rigidBody == null)
            rigidBody = GetComponent<Rigidbody>();

        // Permet de redéfinir une nouvelle taille
        transform.localScale = new Vector3(tailleSatellite * scale, tailleSatellite * scale, tailleSatellite * scale);

        this.transform.GetChild(3).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Permet de récupérer les objets représentant la planète
        centerPlanet = GameObject.Find(center);

        positionPlanetx = centerPlanet.transform.position.x;
        positionPlanety = centerPlanet.transform.position.y;
        positionPlanetz = centerPlanet.transform.position.z;
        
        epsi += vitesse * Time.deltaTime; // Ajuste la vitesse de rotation

        float x = (positionPlanetx) + largeurEllipse * Mathf.Sin(epsi);
        float y = (positionPlanety) + hauteurEllipse * Mathf.Sin(epsi);
        float z = (positionPlanetz) - longueurEllipse * Mathf.Cos(epsi);
        

        if (!interactable.attachedToHand)
        {
            if (isGrab) 
            {
                timer += Time.deltaTime;
                GameObject menu = GameObject.Find("WristMenu");
                WristMenu scriptMenu = menu.GetComponent<WristMenu>();
                scriptMenu.setGrab(interactable.attachedToHand);
                if (timer > waitTime)
                {
                    isGrab = false;
                    timer = 0.0f;
                    rigidBody.velocity = Vector3.zero;
                    rigidBody.angularVelocity = Vector3.zero;
                    transform.rotation = Quaternion.Euler(rotationx * Time.deltaTime, rotationy * Time.deltaTime, rotationz * Time.deltaTime);

                    this.transform.GetChild(3).gameObject.SetActive(false);
                }
                    
            }
            else if(isClicked){
                GameObject planet = this.transform.GetChild(1).gameObject;
                planet.transform.Rotate(new Vector3(rotationx, rotationy, rotationz) * Time.deltaTime);
            }
            else 
            {
                positionActux = x;
                positionActuy = y;
                positionActuz = z;
                transform.position = new Vector3(x, y, z);
                transform.Rotate(new Vector3(rotationx, rotationy, rotationz) * Time.deltaTime);
            }
        }
        else
        {
            isGrab = true;
            timer = 0.0f;
            GameObject menu = GameObject.Find("WristMenu");
            WristMenu scriptMenu = menu.GetComponent<WristMenu>();
            scriptMenu.setGrab(interactable.attachedToHand);
            this.transform.GetChild(3).gameObject.SetActive(true);
        }
    }

    private string haveJsonInFile(string scene)
    {
        string jsonContent;
        if (scene != "solarScene")
        {
            jsonContent = File.ReadAllText(Application.streamingAssetsPath + "/SatelliteClickedVariables.json");
        }
        else
        {
            jsonContent = File.ReadAllText(Application.streamingAssetsPath + "/SatelliteVariables.json");
        }
        return jsonContent;
    }

    private void loadVariables() {
        string jsonContent = haveJsonInFile(SceneManager.GetActiveScene().name);
        Satellite satellite = JsonConvert.DeserializeObject<Satellite>(jsonContent);
        SatelliteVariables satelliteVariables = null;
        switch (gameObject.name) {
            case "Moon":
                satelliteVariables = satellite.Moon;
                break;
        }

        if (satelliteVariables != null) {
            tailleSatellite = satelliteVariables.size;
            longueurEllipse = satelliteVariables.ellipseLength * scale;
            largeurEllipse = satelliteVariables.ellipseWidth * scale;
            hauteurEllipse = satelliteVariables.ellipseHeight * scale;
            rotationx = satelliteVariables.rotationx;
            rotationy = satelliteVariables.rotationy;
            rotationz = satelliteVariables.rotationz;
            vitesse = satelliteVariables.speed;
            waitTime = satelliteVariables.waitTime;
            center = satelliteVariables.center;
        }
        else {
            Debug.LogError($"Satellite inconnue : {gameObject.name}");
        }
    }

    public void onClick() {
        //Debug.Log("Click Satellite");

        isClicked = !isClicked;

        //Debug.Log(isClicked);
        
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.name != "TeleportArea" && collision.gameObject.name != "HandColliderLeft(Clone)" && collision.gameObject.name != "HandColliderRight(Clone)" /*&& collision.gameObject.name != "HeadCollider"*/) {
            isGrab = true;
            if (collision.gameObject.name == "Sphere") // Sphere collider du soleil
                transform.position = new Vector3(0, 20000, 0); // chut
        }
    }
    public void visibiliteTrue()
    {
        GameObject textePlanete;
        GameObject namePlanete;
        GameObject buzzer;
     
        textePlanete = this.transform.GetChild(3).gameObject;
        namePlanete = this.transform.GetChild(4).gameObject;
        buzzer = this.transform.GetChild(5).gameObject;

        textePlanete.SetActive(true);
        namePlanete.SetActive(true);
        buzzer.SetActive(true);
    }
}
