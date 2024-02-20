using UnityEngine;
using UnityEngine.SceneManagement;
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

    private int nbTutoSolar = 2;
    private int nbTutoClickSolar;
    private int nbTutoQuiz = 1;
    private int nbTutoMuseum = 2;

    private string[] sceneWithTuto = new string[4] { "solarScene", "ClickedSolarScene", "Quiz", "MuseumScene" };


    private int currentTuto = 1;

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
            UnShowTuto();
            currentScene = SceneManager.GetActiveScene().name;
            currentTuto = 1;
            activeLocalTutorial = true; 
        }
        if (SceneManager.GetActiveScene().name == "ClickedSolarScene")
        {
            GameObject clickedPlanetManager = GameObject.Find("ClickedPlanetManager");
            ClickedPlanetManager clickedPlanetScript = clickedPlanetManager.GetComponent<ClickedPlanetManager>();

            if (clickedPlanetScript.getNamePlanetClicked() == "Earth")
                nbTutoClickSolar = 3;
            else nbTutoClickSolar = 2; 
        }
        if (activeTuto && activeLocalTutorial && sceneWithTuto.Contains(SceneManager.GetActiveScene().name))
        {
            ShowTuto(currentTuto);

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
                                UnShowTuto();

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
                                UnShowTuto();

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
                                UnShowTuto();

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
                                UnShowTuto();

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
                    UnShowTuto();
                    
                    tempsEcoule = 0;
                }
            }
        }
    }


    public void activateTuto(bool activate)
    {
        activeTuto = activate;
        UnShowTuto();
        tempsEcoule = 0;
    }


    private void ShowTuto(int index) {
        texteVR.GetComponent<LocalizeStringEvent>().SetTable("TutorialText");
        texteVR.GetComponent<LocalizeStringEvent>().SetEntry(SceneManager.GetActiveScene().name + index);

        texteVR.SetActive(true);
        menu.SetActive(true);
    }

    private void UnShowTuto()
    {
        texteVR.SetActive(false);
        menu.SetActive(false);
    }
}
