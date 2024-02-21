using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using System.IO;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Components;

public class QuizManager : MonoBehaviour
{

    private Question question;
    private int point = 0;
    private QuestionFaire questionFaire;
    private List<int> questionPlanet;
    private string planetCliked;
    public GameObject wristMenu;

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
            //Debug.Log(this.planetCliked);
            switch (this.planetCliked)
            {
                case "Mercury":
                    this.questionPlanet = this.questionFaire.Mercury;
                    //Debug.Log("Mercure quiz");
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
                case "Moon":
                    this.questionPlanet = this.questionFaire.Moon;
                    break;
            }
            if (this.questionPlanet.Count != 0)
            {
                

                
                Questions questions;
                if (LocalizationSettings.SelectedLocale.name == "English (en)")
                    questions = JsonConvert.DeserializeObject<Questions>(File.ReadAllText(Application.streamingAssetsPath + "/questionEn.json"));
                else
                    questions = JsonConvert.DeserializeObject<Questions>(File.ReadAllText(Application.streamingAssetsPath + "/questionFr.json"));

                int numQuestion = Random.Range(0, this.questionPlanet.Count - 1);

                switch (this.planetCliked)
                {
                    case "Mercury":
                        this.question = questions.Mercury[this.questionPlanet[numQuestion]];
                        localizationQuiz("Mercury", numQuestion);
                        this.questionFaire.Mercury.RemoveAt(numQuestion);
                        break;
                    case "Venus":
                        this.question = questions.Venus[this.questionPlanet[numQuestion]];
                        localizationQuiz("Venus", numQuestion);
                        this.questionFaire.Venus.RemoveAt(numQuestion);
                        break;
                    case "Earth":
                        this.question = questions.Earth[this.questionPlanet[numQuestion]];
                        localizationQuiz("Earth", numQuestion);
                        this.questionFaire.Earth.RemoveAt(numQuestion);
                        break;
                    case "Mars":
                        this.question = questions.Mars[this.questionPlanet[numQuestion]];
                        localizationQuiz("Mars", numQuestion);
                        this.questionFaire.Mars.RemoveAt(numQuestion);
                        break;
                    case "Jupiter":
                        this.question = questions.Jupiter[this.questionPlanet[numQuestion]];
                        localizationQuiz("Jupiter", numQuestion);
                        this.questionFaire.Jupiter.RemoveAt(numQuestion);
                        break;
                    case "Saturn":
                        this.question = questions.Saturn[this.questionPlanet[numQuestion]];
                        localizationQuiz("Saturn", numQuestion);
                        this.questionFaire.Saturn.RemoveAt(numQuestion);
                        break;
                    case "Uranus":
                        this.question = questions.Uranus[this.questionPlanet[numQuestion]];
                        localizationQuiz("Uranus", numQuestion);
                        this.questionFaire.Uranus.RemoveAt(numQuestion);
                        break;
                    case "Neptune":
                        this.question = questions.Neptune[this.questionPlanet[numQuestion]];
                        localizationQuiz("Neptune", numQuestion);
                        this.questionFaire.Neptune.RemoveAt(numQuestion);
                        break;
                    case "Pluto":
                        this.question = questions.Pluto[this.questionPlanet[numQuestion]];
                        localizationQuiz("Pluto", numQuestion);
                        this.questionFaire.Pluto.RemoveAt(numQuestion);
                        break;
                    case "Moon":
                        this.question = questions.Moon[this.questionPlanet[numQuestion]];
                        localizationQuiz("Moon", numQuestion);
                        this.questionFaire.Moon.RemoveAt(numQuestion);
                        break;
                }

                //questionGameObject.GetComponent<TMP_Text>().text = this.question.question;
            
                
                //reponse1.GetComponent<TMP_Text>().text = this.question.reponse1.reponse;
                //reponse2.GetComponent<TMP_Text>().text = this.question.reponse2.reponse;
            }
            else
            {
                GameObject gameObjectCible = GameObject.Find("Player Variant");
                PlayerManager scriptCible = gameObjectCible.GetComponent<PlayerManager>();
                scriptCible.setPosition(61.31, 12.33, -134.796);
                GameObject textObjectCible = GameObject.Find("ReponseExplixation");
                textObjectCible.GetComponent<LocalizeStringEvent>().SetTable("Quiz");
                textObjectCible.GetComponent<LocalizeStringEvent>().SetEntry("Finish");
            }

            
        }
        else if(change && SceneManager.GetActiveScene().name != "Quiz")
        {
            //Debug.Log("Je met a false");
            this.change = false;
            this.question = null;
        }
    }

    private void localizationQuiz(string planet, int numQuestion)
    {
        GameObject questionGameObject = GameObject.Find("Question");
        GameObject reponse1 = GameObject.Find("responseText1");
        GameObject reponse2 = GameObject.Find("responseText2");

        questionGameObject.GetComponent<LocalizeStringEvent>().SetTable(planet + "Questions");
        questionGameObject.GetComponent<LocalizeStringEvent>().SetEntry("Question" + this.questionPlanet[numQuestion]);
        reponse1.GetComponent<LocalizeStringEvent>().SetTable(planet + "Questions");
        reponse1.GetComponent<LocalizeStringEvent>().SetEntry("Answer1Q" + this.questionPlanet[numQuestion]);
        reponse2.GetComponent<LocalizeStringEvent>().SetTable(planet + "Questions");
        reponse2.GetComponent<LocalizeStringEvent>().SetEntry("Answer2Q" + this.questionPlanet[numQuestion]);
    }

    public void chooseResponse(string response)
    {
        GameObject gameObjectCible = GameObject.Find("Player Variant");
        PlayerManager scriptCible = gameObjectCible.GetComponent<PlayerManager>();
        scriptCible.setPosition(61.31, 12.33, -134.796);
        GameObject textObjectCible = GameObject.Find("ReponseExplixation");
        if (response == "reponse1")
        {
            if (this.question.reponse1.isTrueResponse)
            {
                textObjectCible.GetComponent<LocalizeStringEvent>().SetTable("Quiz");
                textObjectCible.GetComponent<LocalizeStringEvent>().SetEntry("GoodAnswer");
                this.point += 1;
                this.wristMenu.GetComponent<WristMenu>().Score(this.point);
                //Debug.Log("is True");
            }
            else
            {
                textObjectCible.GetComponent<LocalizeStringEvent>().SetTable("Quiz");
                textObjectCible.GetComponent<LocalizeStringEvent>().SetEntry("BadAnswer");
                //Debug.Log("is False");
            }
        }
        else
        {
            if (this.question.reponse2.isTrueResponse)
            {
                textObjectCible.GetComponent<LocalizeStringEvent>().SetTable("Quiz");
                textObjectCible.GetComponent<LocalizeStringEvent>().SetEntry("GoodAnswer");
                this.point += 1;
                this.wristMenu.GetComponent<WristMenu>().Score(this.point);
                //Debug.Log("is True");
            }
            else
            {
                textObjectCible.GetComponent<LocalizeStringEvent>().SetTable("Quiz");
                textObjectCible.GetComponent<LocalizeStringEvent>().SetEntry("BadAnswer");
                //Debug.Log("is False");
            }
        }
    }

    public void SetPlanetClicked(string namePlanet)
    {
        this.planetCliked = namePlanet;
    }

    public int getPoint()
    {
        return point; 
    }
}
