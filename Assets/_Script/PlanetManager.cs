using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using Valve.VR;
using Valve.VR.InteractionSystem;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System.Net.NetworkInformation;

public class PlanetManager : MonoBehaviour
{
    // VARIABLE : Elles vont contenir des données
    // Représente les objets de la planète concerné et du soleil
    private GameObject centreSun;
    // Permet de contenir la vitesse de rotation
    private float epsi;
    // Permet de contenir les positions du soleil
    private float positionSunx;
    private float positionSuny;
    private float positionSunz;

    private float positionActux;
    private float positionActuy;
    private float positionActuz;

    // VARIABLE : Elles vont permettre de modifier directement les données
    // Permet de définir la taille de la terre
    private float taillePlanete;
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
    private float scalePlanet;
    private float scaleEllipse;

    private Interactable interactable;
    private Rigidbody rigidBody;
    private float waitTime;
    private float timer = 0.0f;
    private bool isGrab;
    private bool isClicked = false;

    // Start is called before the first frame update
    void Start() {
        if (SceneManager.GetActiveScene().name == "solarScene") {
            scalePlanet = 0.0025f;
            scaleEllipse = 0.0065f;
        }
        else {
            scalePlanet = 1.50f;
            scaleEllipse = 1.50f;
        }

        loadVariables();

        // Permet de récupérer les objets représentant le soleil
        centreSun = GameObject.Find("Sun");

        positionSunx = centreSun.transform.position.x;
        positionSuny = centreSun.transform.position.y;
        positionSunz = centreSun.transform.position.z;
        // Permet de récupérer l'ensemble des positions x, y et z du soleil

        float x = (positionSunx) + largeurEllipse * Mathf.Sin(epsi);
        float y = (positionSuny) + hauteurEllipse * Mathf.Sin(epsi);
        float z = (positionSunz) - longueurEllipse * Mathf.Cos(epsi);

        transform.position = new Vector3(x, y, z);

        positionActux = transform.position.x;
        positionActuy = transform.position.y;
        positionActuz = transform.position.z;

        if (interactable == null)
            interactable = GetComponent<Interactable>();
        if (rigidBody == null)
            rigidBody = GetComponent<Rigidbody>();
        
        // Permet de redéfinir une nouvelle taille
        transform.localScale = new Vector3(taillePlanete, taillePlanete, taillePlanete);
    }

    // Update is called once per frame
    void Update()
    {
        epsi += vitesse * Time.deltaTime; // Ajuste la vitesse de rotation

        float x = (positionSunx) + largeurEllipse * Mathf.Sin(epsi);
        float y = (positionSuny) + hauteurEllipse * Mathf.Sin(epsi);
        float z = (positionSunz) - longueurEllipse * Mathf.Cos(epsi);   


        if (!interactable.attachedToHand || SceneManager.GetActiveScene().name == "ClickedSolarScene") {
            if (isGrab && SceneManager.GetActiveScene().name == "solarScene") {
                timer += Time.deltaTime;
                if (timer > waitTime)
                {
                    isGrab = false;
                    timer = 0.0f;
                    rigidBody.velocity = Vector3.zero;
                    rigidBody.angularVelocity = Vector3.zero;
                    transform.rotation = Quaternion.Euler(rotationx * Time.deltaTime, rotationy * Time.deltaTime, rotationz * Time.deltaTime);
                }
                    
            }
            else if(isClicked){
                Debug.Log("Je danse");
                GameObject planet;
                if (this.name == "Saturn")
                {
                    planet = this.transform.GetChild(2).gameObject;
                    GameObject ring = this.transform.GetChild(0).gameObject;
                    ring.transform.Rotate(new Vector3(rotationx, rotationy, rotationz) * Time.deltaTime);
                    Vector3 lockedZposRing = ring.transform.position;
                    lockedZposRing.z = positionActuz;
                    ring.transform.position = lockedZposRing;
                }
                else
                {
                    planet = this.transform.GetChild(1).gameObject;
                }
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
        }

    }

    public bool getIsGrab()
    {
        return isGrab;
    }

    private string haveJsonInFile(string scene)
    {
        string jsonContent;
        if (scene != "solarScene")
        {
            jsonContent = File.ReadAllText("./Assets/_Script/PlanetsCliquedVariables.json");
        }
        else
        {
            jsonContent = File.ReadAllText("./Assets/_Script/PlanetsVariables.json");
        }
        return jsonContent;
    }

    private void loadVariables() {
        
        string jsonContent = haveJsonInFile(SceneManager.GetActiveScene().name);
        Planets planets = JsonConvert.DeserializeObject<Planets>(jsonContent);
        PlanetsVariables planetsVariables = null;
        switch (gameObject.name) {
            case "Mercury":
                planetsVariables = planets.Mercury;
                break;
            case "Venus":
                planetsVariables = planets.Venus;
                break;
            case "Earth":
                planetsVariables = planets.Earth;
                break;
            case "Mars":
                planetsVariables = planets.Mars;
                break;
            case "Jupiter":
                planetsVariables = planets.Jupiter;
                break;
            case "Saturn":
                planetsVariables = planets.Saturn;
                break;
            case "Uranus":
                planetsVariables = planets.Uranus;
                break;
            case "Neptune":
                planetsVariables = planets.Neptune;
                break;
            case "Pluto":
                planetsVariables = planets.Pluto;
                break;
        }

        if (planetsVariables != null) {
            longueurEllipse = planetsVariables.ellipseLength * scaleEllipse;
            largeurEllipse = planetsVariables.ellipseWidth * scaleEllipse;
            taillePlanete = planetsVariables.size * scalePlanet;
            
            hauteurEllipse = planetsVariables.ellipseHeight * scaleEllipse;
            rotationx = planetsVariables.rotationx;
            rotationy = planetsVariables.rotationy;
            rotationz = planetsVariables.rotationz;
            vitesse = planetsVariables.speed;
            waitTime = planetsVariables.waitTime;
        }
        else {
            Debug.LogError($"Planète inconnue : {gameObject.name}");
        }
    }

    public void onClick() {
        isClicked = !isClicked;
        Debug.Log("Je click");
        //GameObject gameObjectCible = GameObject.Find("Player Variant");
        //PlayerManager scriptCible = gameObjectCible.GetComponent<PlayerManager>();
        
        //if(isClicked){
            
        //    SceneManager.LoadScene(2);
        //    scriptCible.setPosition(500, 0, 500, (SceneManager.GetActiveScene().name));
        //}
        //else{
            
        //    SceneManager.LoadScene(1);
        //    scriptCible.setPosition(500, 500, 500, (SceneManager.GetActiveScene().name));
        //}
        //Debug.Log(SceneManager.GetActiveScene().name);
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.name != "TeleportArea" && collision.gameObject.name != "HandColliderLeft(Clone)" && collision.gameObject.name != "HandColliderRight(Clone)" /*&& collision.gameObject.name != "HeadCollider"*/) {
            isGrab = true;
            if (collision.gameObject.name == "Sphere") // Sphere collider du soleil
                transform.position = new Vector3(0, 20000, 0); // chut
        }
    }

    public void textVisibiliteTrue()
    {
        GameObject textePlanete;
        if (this.name == "Saturn")
        {
            textePlanete = this.transform.GetChild(4).gameObject;
        }
        else
        {
            textePlanete = this.transform.GetChild(3).gameObject;
            Debug.Log(this.gameObject.name);
        }
        textePlanete.SetActive(true);
    }
}
