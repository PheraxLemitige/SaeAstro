using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Localization.Settings;

public class TutorialManager : MonoBehaviour
{
    private GameObject texteVR;
    private GameObject menu;

    public float tempsAffichage = 10f;
    private float tempsEcoule = 0f; 

    private bool solarTuto = false;
    private bool clickSolarTuto = false;

    // Start is called before the first frame update
    void Start() {
        if (texteVR == null)
            texteVR = this.gameObject.transform.GetChild(0).gameObject;
        if (menu == null)
            menu = this.gameObject.transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update() {
        if (SceneManager.GetActiveScene().name == "solarScene" && !solarTuto) {
            if (LocalizationSettings.SelectedLocale.name == "English (en)")
                Tuto("You can grab a planet with the trigger on the back !");
            else
                Tuto("Tu peux attraper une planète avec la gachette du bas !");
            solarTuto = !solarTuto;
        }
        else if (SceneManager.GetActiveScene().name == "ClickedSolarScene" && !clickSolarTuto) {
            if (LocalizationSettings.SelectedLocale.name == "English (en)")
                Tuto("You can press the buzzer to access the quizz !");
            else
                Tuto("Tu peux presser le buzzer pour accèder au quizz !");
            clickSolarTuto = !clickSolarTuto;
        }

        if (texteVR.activeSelf) {
            tempsEcoule += Time.deltaTime;

            if (tempsEcoule > tempsAffichage) {
                texteVR.SetActive(false);
                menu.SetActive(false);

                tempsEcoule = 0;
            }
        }
    }

    void Tuto(string text) {
        texteVR.GetComponent<TMP_Text>().text = text;
        texteVR.SetActive(true);
        menu.SetActive(true);
    }
}
