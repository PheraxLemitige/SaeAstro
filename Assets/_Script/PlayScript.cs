using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlay ()
    {
        SceneManager.LoadScene(1);
        GameObject gameObjectCible = GameObject.Find("Player Variant");
        gameObjectCible.transform.position = new Vector3(1.5f, -0.75f, 2);
    }

    public void OnQuit ()
    {
        Application.Quit();
    }
}
