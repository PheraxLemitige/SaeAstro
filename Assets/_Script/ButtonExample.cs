using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using System;

namespace Valve.VR.InteractionSystem.Sample
{
    public class ButtonExample : MonoBehaviour
    {
        public HoverButton hoverButton;

        public GameObject prefab;

        private void Start()
        {
            hoverButton.onButtonDown.AddListener(OnButtonDown);
        }

        private void OnButtonDown(Hand hand)
        {

            GameObject gameObjectClickPlaneteCible = GameObject.Find("ClickedPlanetManager");
            ClickedPlanetManager scriptClickPlaneteCible = gameObjectClickPlaneteCible.GetComponent<ClickedPlanetManager>();
            if (this.name == "planetTeleportQuiz")
            {
                SceneManager.LoadScene(4);
                GameObject gameObjectCible = GameObject.Find("Player Variant");
                PlayerManager scriptCible = gameObjectCible.GetComponent<PlayerManager>();
                scriptCible.setPosition(564.344, 12.33, -130);
                GameObject gameObjectClickCible = GameObject.Find("clickManager");
                ClickManager scriptClickCible = gameObjectClickCible.GetComponent<ClickManager>();
                scriptClickCible.clickQuiz();
                string namePlanet = scriptClickPlaneteCible.getNamePlanetClicked();
                Debug.Log("Nom planet");
                Debug.Log(namePlanet);
                GameObject gameObjectQuizCible = GameObject.Find("QuizManager");
                QuizManager scriptQuizCible = gameObjectQuizCible.GetComponent<QuizManager>();
                scriptQuizCible.SetPlanetClicked(namePlanet);

            }
            else if(this.name == "response1")
            {
                Debug.Log("Reponse 1");
                GameObject gameObjectCible = GameObject.Find("QuizManager");
                QuizManager scriptCible = gameObjectCible.GetComponent<QuizManager>();

                scriptCible.chooseResponse("reponse1");
            }
            else if (this.name == "response2")
            {
                Debug.Log("Reponse 2");
                GameObject gameObjectCible = GameObject.Find("QuizManager");
                QuizManager scriptCible = gameObjectCible.GetComponent<QuizManager>();
                scriptCible.chooseResponse("reponse2");
            }
            else if(this.name == "quitSceneQuiz")
            {

                GameObject gameObjectCible = GameObject.Find("Player Variant");
                PlayerManager scriptCible = gameObjectCible.GetComponent<PlayerManager>();
                if (scriptClickPlaneteCible.getSceneAvant() == "solarScene")
                {
                    SceneManager.LoadScene(2);
                    scriptCible.setPosition(5.0, -0.7, 5.0);
                }
                else
                {
                    SceneManager.LoadScene(5);
                    scriptCible.setPosition(-0.1, 0.0009, -0.2);
                }
                
            }
        }

    }
}