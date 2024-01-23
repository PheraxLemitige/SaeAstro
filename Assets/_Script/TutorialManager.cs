using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; 

public class TutorialManager : MonoBehaviour
{
    private GameObject texteVR;

    public float tempsAffichage = 10f;
    private float tempsEcoule = 0f; 

    private bool solarTuto = false;
    private bool clickSolarTuto = false;

    // Start is called before the first frame update
    void Start() {
        if (texteVR == null)
            texteVR = this.gameObject.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update() {
        if (SceneManager.GetActiveScene().name == "solarScene" && !solarTuto) {
            Tuto("Tu peux attraper une planète avec la gachette du bas !");
            solarTuto = !solarTuto;
        }
        else if (SceneManager.GetActiveScene().name == "ClickedSolarScene" && !clickSolarTuto) {
            Tuto("Tu peux presser le buzzer pour accèder au quizz !");
            clickSolarTuto = !clickSolarTuto;
        }

        if (texteVR.activeSelf) {
            tempsEcoule += Time.deltaTime;

            if (tempsEcoule > tempsAffichage) {
                texteVR.SetActive(false);

                tempsEcoule = 0;
            }
        }
    }

    void Tuto(string text) {
        texteVR.GetComponent<TMP_Text>().text = text;
        texteVR.SetActive(true);
    }
}
