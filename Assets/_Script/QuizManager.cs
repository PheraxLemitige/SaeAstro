using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Newtonsoft.Json;
using System.IO;
using System.Numerics;

public class QuizManager : MonoBehaviour
{

    private Question question;
    private int point = 0;
    private QuestionFaire questionFaire;
    private List<int> questionPlanet;
    private string planetCliked;

    private bool change = false;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        this.questionFaire = new QuestionFaire();
    }

    // Update is called once per frame
    void Update()
    {
        if(!change && SceneManager.GetActiveScene().name == "Quiz")
        {
            change = true;
            Debug.Log(this.planetCliked);
            switch (this.planetCliked)
            {
                case "Mercury":
                    this.questionPlanet = this.questionFaire.Mercury;
                    Debug.Log("Mercure quiz");
                    break;
                case "Venus":
                    this.questionPlanet = this.questionFaire.Venus;
                    break;
                case "Earth":
                    this.questionPlanet = this.questionFaire.Earth;
                    break;
                case "Mars":
                    this.questionPlanet = this.questionFaire.Mars;
                    break;
                case "Jupiter":
                    this.questionPlanet = this.questionFaire.Jupiter;
                    break;
                case "Saturn":
                    this.questionPlanet = this.questionFaire.Saturn;
                    break;
                case "Uranus":
                    this.questionPlanet = this.questionFaire.Uranus;
                    break;
                case "Neptune":
                    this.questionPlanet = this.questionFaire.Neptune;
                    break;
                case "Pluto":
                    this.questionPlanet = this.questionFaire.Pluto;
                    break;
            }
            if (this.questionPlanet.Count != 0)
            {
                

                GameObject questionGameObject = GameObject.Find("Question");

                Questions questions = JsonConvert.DeserializeObject<Questions>(File.ReadAllText("./Assets/_Script/question.json"));

                int numQuestion = Random.Range(0, this.questionPlanet.Count - 1);
                switch (this.planetCliked)
                {
                    case "Mercury":
                        this.question = questions.Mercury[this.questionPlanet[numQuestion]];
                        this.questionFaire.Mercury.RemoveAt(numQuestion);
                        break;
                    case "Venus":
                        this.question = questions.Venus[this.questionPlanet[numQuestion]];
                        this.questionFaire.Venus.RemoveAt(numQuestion);
                        break;
                    case "Earth":
                        this.question = questions.Earth[this.questionPlanet[numQuestion]];
                        this.questionFaire.Earth.RemoveAt(numQuestion);
                        break;
                    case "Mars":
                        this.question = questions.Mars[this.questionPlanet[numQuestion]];
                        this.questionFaire.Mars.RemoveAt(numQuestion);
                        break;
                    case "Jupiter":
                        this.question = questions.Jupiter[this.questionPlanet[numQuestion]];
                        this.questionFaire.Jupiter.RemoveAt(numQuestion);
                        break;
                    case "Saturn":
                        this.question = questions.Saturn[this.questionPlanet[numQuestion]];
                        this.questionFaire.Saturn.RemoveAt(numQuestion);
                        break;
                    case "Uranus":
                        this.question = questions.Uranus[this.questionPlanet[numQuestion]];
                        this.questionFaire.Uranus.RemoveAt(numQuestion);
                        break;
                    case "Neptune":
                        this.question = questions.Neptune[this.questionPlanet[numQuestion]];
                        this.questionFaire.Neptune.RemoveAt(numQuestion);
                        break;
                    case "Pluto":
                        this.question = questions.Pluto[this.questionPlanet[numQuestion]];
                        this.questionFaire.Pluto.RemoveAt(numQuestion);
                        break;
                }

                questionGameObject.GetComponent<TMP_Text>().text = this.question.question;
            
                GameObject reponse1 = GameObject.Find("responseText1");
                reponse1.GetComponent<TMP_Text>().text = this.question.reponse1.reponse;

                GameObject reponse2 = GameObject.Find("responseText2");
                reponse2.GetComponent<TMP_Text>().text = this.question.reponse2.reponse;
            }
            else
            {
                GameObject gameObjectCible = GameObject.Find("Player Variant");
                PlayerManager scriptCible = gameObjectCible.GetComponent<PlayerManager>();
                scriptCible.setPosition(61.31, 12.33, -134.796);
                GameObject textObjectCible = GameObject.Find("ReponseExplixation");
                textObjectCible.GetComponent<TMP_Text>().text = "Vous avez fait toute les questions! Bravo!";
            }

            
        }
        else if(change && SceneManager.GetActiveScene().name != "Quiz")
        {
            Debug.Log("Je met a false");
            this.change = false;
            this.question = null;
        }
    }

    public void chooseResponse(string response)
    {
        GameObject gameObjectCible = GameObject.Find("Player Variant");
        PlayerManager scriptCible = gameObjectCible.GetComponent<PlayerManager>();
        scriptCible.setPosition(61.31, 12.33, -134.796);
        string textReponse; 
        if (response == "reponse1")
        {
            if (this.question.reponse1.isTrueResponse)
            {
                textReponse = "Bonne réponse";
                this.point += 1;
                Debug.Log("is True");
            }
            else
            {
                textReponse = "Mauvaise réponse";
                Debug.Log("is False");
            }
        }
        else
        {
            if (this.question.reponse2.isTrueResponse)
            {
                textReponse = "Bonne réponse";
                this.point += 1;
                Debug.Log("is True");
            }
            else
            {
                textReponse = "Mauvaise réponse";
                Debug.Log("is False");
            }
        }
        GameObject textObjectCible = GameObject.Find("ReponseExplixation");
        textObjectCible.GetComponent<TMP_Text>().text = textReponse;

    }

    public void SetPlanetClicked(string namePlanet)
    {
        this.planetCliked = namePlanet;
    }

}
