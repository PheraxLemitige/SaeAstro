using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PlayerManager : MonoBehaviour
{
    double positionX;
    double positionY;
    double positionZ;

    private bool change = false;
    private string sceneName;

    void Start() {
        SceneManager.LoadScene("MenuPrincipal");
        sceneName = SceneManager.GetActiveScene().name;
        positionX = transform.position.x;
        positionY = transform.position.y;
        positionZ = transform.position.z;
    }

    void Update()
    {
        
        if (change)
        {
            if(sceneName != null && sceneName != SceneManager.GetActiveScene().name)
            {
                //Debug.Log("Je passe!");
                
                Vector3 newVector = new Vector3((float)positionX, (float)positionY, (float)positionZ);
                transform.position = newVector;
                change = false;
                sceneName = SceneManager.GetActiveScene().name;
                
            }
            else
            {
                Vector3 newVector = new Vector3((float)positionX, (float)positionY, (float)positionZ);
                transform.position = newVector;
                change = false;
            }
            
        }
        //Debug.Log(SceneManager.GetActiveScene().name);
    }

    public void playerClickPlanet(float positionSunx, float positionSunz, float positionActux, float positionActuy, float positionActuz, GameObject centrePlanet) {
        Vector3 newVector = new Vector3(positionSunx + (positionActux / 5) * 4, transform.position.y + 300, positionSunz + (positionActuz / 5) * 4);
        transform.position = newVector;
        transform.localScale = new Vector3(1, 1, 1);
        transform.LookAt(new Vector3(centrePlanet.transform.position.x, transform.position.y, centrePlanet.transform.position.z));
        //transform.Translate(Vector3.right * Time.deltaTime);
    }

    public void initialPlace() {
        Vector3 newVector = new Vector3((float)positionX, (float)positionY, (float)positionZ);
        transform.position = newVector;
        transform.localScale = new Vector3(500, 500, 500);
        //transform.LookAt(centreSun.transform.position);
        //transform.Translate(Vector3.right * Time.deltaTime);
    }

    public void setPosition(double x, double y, double z)
    {
        positionX = x;
        positionY = y;
        positionZ = z;
        change = true;
    }
}
