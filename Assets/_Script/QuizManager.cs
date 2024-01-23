using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Newtonsoft.Json;
using System.IO;

public class QuizManager : MonoBehaviour
{

    private Question question;
    private int point = 0;
    private QuestionFaire questionFaire;

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
            if (this.questionFaire.Mercury.Count != 0)
            {
                

                GameObject questionGameObject = GameObject.Find("Question");

                Questions questions = JsonConvert.DeserializeObject<Questions>(File.ReadAllText("./Assets/_Script/question.json"));

                int numQuestion = Random.Range(0, this.questionFaire.Mercury.Count - 1);

                Debug.Log(this.questionFaire.Mercury.Count);
                Debug.Log(numQuestion);
                Debug.Log(this.questionFaire.Mercury[numQuestion]);

                this.question = questions.Mercury[this.questionFaire.Mercury[numQuestion]];

                this.questionFaire.Mercury.RemoveAt(numQuestion);

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

}
