using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Components;
using System.Linq;

public class TutorialManager : MonoBehaviour
{
    private GameObject texteVR;
    private GameObject menu;

    public float tempsAffichage = 10f;
    private float tempsEcoule = 0f; 

    private bool activeTuto = true;
    private bool activeLocalTutorial = true; 

    private float nbTutoSolar = 2;
    private float nbTutoClickSolar = 2;
    private float nbTutoQuiz = 1;
    private float nbTutoMuseum = 2;

    private string[] sceneWithTuto = new string[4] { "solarScene", "ClickedSolarScene", "Quiz", "MuseumScene" };


    private float currentTuto = 1;

    private string currentScene; 

    // Start is called before the first frame update
    void Start() {
        if (texteVR == null)
            texteVR = this.gameObject.transform.GetChild(0).gameObject;
        if (menu == null)
            menu = this.gameObject.transform.GetChild(1).gameObject;

        currentScene = SceneManager.GetActiveScene().name; 
    }

    // Update is called once per frame
    void Update() 
    {
        if (currentScene != SceneManager.GetActiveScene().name)
        {
            currentScene = SceneManager.GetActiveScene().name;
            currentTuto = 1;
            activeLocalTutorial = true; 
        }
        if (activeTuto && activeLocalTutorial && sceneWithTuto.Contains(SceneManager.GetActiveScene().name))
        {
            texteVR.SetActive(true);
            menu.SetActive(true);

            texteVR.GetComponent<LocalizeStringEvent>().SetTable("TutorialText");
            texteVR.GetComponent<LocalizeStringEvent>().SetEntry(SceneManager.GetActiveScene().name + currentTuto);

            if (texteVR.activeSelf)
            {
                tempsEcoule += Time.deltaTime;

                if (tempsEcoule > tempsAffichage)
                {
                    switch (currentScene)
                    {
                        case "solarScene":
                            if(currentTuto >= nbTutoSolar)
                            {
                                texteVR.SetActive(false);
                                menu.SetActive(false);

                                tempsEcoule = 0;

                                activeLocalTutorial = false; 
                            }
                            else
                            {
                                tempsEcoule = 0;

                                currentTuto += 1;
                            }
                            break;
                        case "ClickedSolarScene":
                            if (currentTuto >= nbTutoClickSolar)
                            {
                                texteVR.SetActive(false);
                                menu.SetActive(false);

                                tempsEcoule = 0;

                                activeLocalTutorial = false;
                            }
                            else
                            {
                                tempsEcoule = 0;

                                currentTuto += 1;
                            }
                            break;
                        case "MuseumScene":
                            if (currentTuto >= nbTutoMuseum)
                            {
                                texteVR.SetActive(false);
                                menu.SetActive(false);

                                tempsEcoule = 0;

                                activeLocalTutorial = false;
                            }
                            else
                            {
                                tempsEcoule = 0;

                                currentTuto += 1;
                            }
                            break;
                        case "Quiz":
                            if (currentTuto >= nbTutoQuiz)
                            {
                                texteVR.SetActive(false);
                                menu.SetActive(false);

                                tempsEcoule = 0;

                                activeLocalTutorial = false;
                            }
                            else
                            {
                                tempsEcoule = 0;

                                currentTuto += 1;
                            }
                            break;
                    }
                    texteVR.SetActive(false);
                    menu.SetActive(false);
                    
                    tempsEcoule = 0;
                }
            }
        }
    }



    void Tuto(string text) {
        texteVR.GetComponent<TMP_Text>().text = text;
        texteVR.SetActive(true);
        menu.SetActive(true);
    }
}
