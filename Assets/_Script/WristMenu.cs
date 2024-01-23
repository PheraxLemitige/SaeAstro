using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;

public class WristMenu : MonoBehaviour {
    private GameObject hand;
    private GameObject canvas;
    public Vector3 firstRange;
    public Vector3 lastRange;
    // Start is called before the first frame update
    void Start() {
        if (hand == null)
            hand = this.gameObject.transform.parent.gameObject;

        if (canvas == null)
            canvas = this.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;

    }

    // Update is called once per frame
    void Update() {
        if (hand.transform.rotation.eulerAngles.x >= firstRange.x && hand.transform.rotation.eulerAngles.z >= firstRange.z)
            if (hand.transform.rotation.eulerAngles.x <= lastRange.x && hand.transform.rotation.eulerAngles.z <= lastRange.z)
                visible(true);
            else
                visible(false); 
        else
            visible(false);

        //Debug.Log(hand.transform.rotation.eulerAngles);
    }

    void visible(bool visible) {
        foreach (Transform child in this.transform)
            child.gameObject.SetActive(visible);
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
        canvas.transform.GetChild(4).gameObject.SetActive(true); // Boutton MainMenu
        canvas.transform.GetChild(5).gameObject.SetActive(true); // Boutton Museum
        canvas.transform.GetChild(6).gameObject.SetActive(true); // Boutton SolarSystem
    }

    public void Languages() {
        DefaultMenu(false);
        canvas.transform.GetChild(7).gameObject.SetActive(true); // Boutton French
        canvas.transform.GetChild(8).gameObject.SetActive(true); // Boutton English
    }

    public void OnMainMenu() {
        SceneManager.LoadScene("MenuPrincipal");
        GameObject playerVariant = GameObject.Find("Player Variant");
        PlayerManager playerManager = playerVariant.GetComponent<PlayerManager>();
        playerManager.setPosition(373, -0.5, 552);
    }

    public void OnMuseum() {
        SceneManager.LoadScene("MuseumScene");
        GameObject playerVariant = GameObject.Find("Player Variant");
        PlayerManager playerManager = playerVariant.GetComponent<PlayerManager>();
        playerManager.setPosition(0, 0, 0);
    }

    public void OnSolarSystem(){
        SceneManager.LoadScene("solarScene");
        GameObject playerVariant = GameObject.Find("Player Variant");
        PlayerManager playerManager = playerVariant.GetComponent<PlayerManager>();
        playerManager.setPosition(5.0, -0.7, 5.0);
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
