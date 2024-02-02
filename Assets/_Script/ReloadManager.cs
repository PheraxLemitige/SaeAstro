using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject clickedSatellite = GameObject.Find("ClickedPlanetManager");
        ClickedPlanetManager scriptClickedSatellite = clickedSatellite.GetComponent<ClickedPlanetManager>();

        scriptClickedSatellite.addCounterReload();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
