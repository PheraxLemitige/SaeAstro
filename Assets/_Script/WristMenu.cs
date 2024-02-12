using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;
using Valve.VR.Extras;

public class WristMenu : MonoBehaviour {
    private GameObject leftHand;
    private GameObject rightHand;
    private GameObject activeHand;
    private string activeLaser = "LeftLaser";
    private GameObject canvas;
    public Vector3 firstRange;
    public Vector3 lastRange;


    // Start is called before the first frame update
    void Start() {
        if (leftHand == null)
            leftHand = this.gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.Find("LeftHand").gameObject;

        if (rightHand == null)
            rightHand = this.gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.Find("RightHand").gameObject;

        if (activeHand == null)
            activeHand = this.gameObject.transform.parent.gameObject;

        if (canvas == null)
            canvas = this.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;

        if (firstRange == null && lastRange == null)
        {
            this.firstRange = new Vector3(200, 0, 0);
            this.lastRange = new Vector3(350, 0, 75);
        }
    }

    // Update is called once per frame
    void Update() {
        if (activeHand.transform.rotation.eulerAngles.x >= firstRange.x && activeHand.transform.rotation.eulerAngles.z >= firstRange.z)
            if (activeHand.transform.rotation.eulerAngles.x <= lastRange.x && activeHand.transform.rotation.eulerAngles.z <= lastRange.z)
                visible(true);
            else
                visible(false); 
        else
            visible(false);

        //Debug.Log(activeHand.transform.rotation.eulerAngles);
    }

    void visible(bool visible) {
        foreach (Transform child in this.transform)
            child.gameObject.SetActive(visible);

        LaserMenu(visible);

        if (!visible)
            DefaultMenu(true);

    }

    private void LaserMenu(bool b)
    {
        if (activeLaser != "BothLaser")
        {
            if (activeHand.name == "LeftHand" && activeLaser != "RightLaser")
                ShowLaser(rightHand, b);
            if (activeHand.name == "RightHand" && activeLaser != "LeftLaser")
                ShowLaser(leftHand, b);
        }
    }

    private void ShowLaser(GameObject hand, bool b)
    {
        hand.GetComponent<SteamVR_LaserPointer>().enabled = b;
        Transform laserMesh = hand.transform.Find("New Game Object");
        if (laserMesh != null)
            laserMesh.gameObject.SetActive(b);
    }

    public void DefaultMenu(bool b) {
        for (int i = 0; i < canvas.transform.childCount; i++){
            if (i < 3)
                canvas.transform.GetChild(i).gameObject.SetActive(b);
            else if (i == 3)
                canvas.transform.GetChild(i).gameObject.SetActive(!b);
            else
                canvas.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void Quit() {
        Application.Quit();
    }

    public void Scenes() {
        DefaultMenu(false);
        canvas.transform.Find("MainMenu").gameObject.SetActive(true);
        canvas.transform.Find("Museum").gameObject.SetActive(true);
        canvas.transform.Find("SolarSystem").gameObject.SetActive(true);
    }

    public void Options()
    {
        DefaultMenu(false);
        canvas.transform.Find("Menu").gameObject.SetActive(true);
        canvas.transform.Find("Lasers").gameObject.SetActive(true);
        canvas.transform.Find("Languages").gameObject.SetActive(true);
    }

    public void OnMainMenu() {
        SceneManager.LoadScene("MenuPrincipal");
        GameObject playerVariant = GameObject.Find("Player Variant");
        PlayerManager playerManager = playerVariant.GetComponent<PlayerManager>();
        playerManager.setPosition(373, 0.5, 552);
        DefaultMenu(true);
    }

    public void OnMuseum() {
        SceneManager.LoadScene("MuseumScene");
        GameObject playerVariant = GameObject.Find("Player Variant");
        PlayerManager playerManager = playerVariant.GetComponent<PlayerManager>();
        playerManager.setPosition(0, 0, 0);
        DefaultMenu(true);
    }

    public void OnSolarSystem(){
        SceneManager.LoadScene("solarScene");
        GameObject playerVariant = GameObject.Find("Player Variant");
        PlayerManager playerManager = playerVariant.GetComponent<PlayerManager>();
        playerManager.setPosition(5.0, -0.7, 5.0);
        DefaultMenu(true);
    }

    public void WristMenuOption()
    {
        DefaultMenu(false);
        canvas.transform.Find("LeftMenu").gameObject.SetActive(true);
        canvas.transform.Find("RightMenu").gameObject.SetActive(true);
    }

    public void LeftMenu()
    {
        this.transform.SetParent(leftHand.transform, false);
        this.activeHand = leftHand;
        this.transform.GetChild(0).transform.localEulerAngles = new Vector3(0, 90, 40);
        this.firstRange = new Vector3(200, 0, 0);
        this.lastRange = new Vector3(350, 0, 75);
    }

    public void RightMenu()
    {
        this.transform.SetParent(rightHand.transform, false);
        this.activeHand = rightHand;
        this.transform.GetChild(0).transform.localEulerAngles = new Vector3(0, -90, -40);
        this.firstRange = new Vector3(200, 0, 275);
        this.lastRange = new Vector3(350, 0, 350);
    }

    public void Lasers()
    {
        DefaultMenu(false);
        canvas.transform.Find("LeftLaser").gameObject.SetActive(true);
        canvas.transform.Find("BothLasers").gameObject.SetActive(true);
        canvas.transform.Find("RightLaser").gameObject.SetActive(true);
    }

    public void Leftlaser()
    {
        ShowLaser(leftHand, true);
        ShowLaser(rightHand, false);
        activeLaser = "LeftLaser";
    }

    public void BothLasers()
    {
        ShowLaser(leftHand, true);
        ShowLaser(rightHand, true);
        activeLaser = "BothLaser";
    }

    public void Rightlaser()
    {
        ShowLaser(leftHand, false);
        ShowLaser(rightHand, true);
        activeLaser = "RightLaser";
    }

    public void Languages()
    {
        DefaultMenu(false);
        canvas.transform.Find("French").gameObject.SetActive(true);
        canvas.transform.Find("English").gameObject.SetActive(true);
    }

    public void changeFR() {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[1];
    }
    public void changeEN() {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[0];
    }

    public void Retour() {
        DefaultMenu(true);
    }
}
