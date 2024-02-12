using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Questions
{
    public Question[] Mercury;
    public Question[] Venus;
    public Question[] Earth;
    public Question[] Mars;
    public Question[] Jupiter;
    public Question[] Saturn;
    public Question[] Uranus;
    public Question[] Neptune;
    public Question[] Pluto;
    public Question[] Moon;
}

[System.Serializable]
public class Question
{
    public string question;
    public Reponse reponse1;
    public Reponse reponse2;
}

[System.Serializable]
public class Reponse
{
    public string reponse;
    public bool isTrueResponse;
}

public class QuestionFaire
{
    public List<int> Mercury;
    public List<int> Venus;
    public List<int> Earth;
    public List<int> Mars;
    public List<int> Jupiter;
    public List<int> Saturn;
    public List<int> Uranus;
    public List<int> Neptune;
    public List<int> Pluto;
    public List<int> Moon;

    public QuestionFaire()
    {
        this.Mercury = new List<int>() { 0, 1, 2, 3, 4 };
        this.Venus = new List<int>() { 0, 1, 2, 3, 4 };
        this.Earth = new List<int>() { 0, 1, 2, 3, 4 };
        this.Mars = new List<int>() { 0, 1, 2, 3, 4 };
        this.Jupiter = new List<int>() { 0, 1, 2, 3, 4 };
        this.Saturn = new List<int>() { 0, 1, 2, 3, 4 };
        this.Uranus = new List<int>() { 0, 1, 2, 3, 4 };
        this.Neptune = new List<int>() { 0, 1, 2, 3, 4 };
        this.Pluto = new List<int>() { 0, 1, 2, 3, 4 };
        this.Moon = new List<int>() { 0, 1, 2, 3, 4 };
    }


    public void retirerElt(string elt, int index)
    {

        Debug.Log("Je passe et suppr");
        switch (elt)
        {
            case "Mercury":
                this.Mercury.RemoveAt(index);
                break;
            case "Venus":
                this.Venus.RemoveAt(index);
                break;
            case "Earth":
                this.Earth.RemoveAt(index);
                break;
            case "Mars":
                this.Mars.RemoveAt(index);
                break;
            case "Jupiter":
                this.Jupiter.RemoveAt(index);
                break;
            case "Saturn":
                this.Saturn.RemoveAt(index);
                break;
            case "Uranus":
                this.Uranus.RemoveAt(index);
                break;
            case "Neptune":
                this.Neptune.RemoveAt(index);
                break;
            case "Pluto":
                this.Pluto.RemoveAt(index);
                break;
        }
    }
}