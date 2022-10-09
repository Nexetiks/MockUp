using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{

    void Start()
    {
        GameManager.Instance.DeskList.MixList();
       
        GameManager.Instance.gameplayActive = true;
       
        
    }
   
    public void ShowCards()
    {
        GameManager.Instance.HandList.MakeList();
        GameManager.Instance.HandList.ShowCards();

    }



    public void EndOfTheRound() 
    {

        GameManager.Instance.gameplayActive = false;

        AtributeRepaier();
       
        Wealth();

        SetMoney();

        SetPopulation();
        
        if (IsEndGame() == true)
        {
            GameManager.Instance.gameplayActive = false;
            
            
            GameManager.Instance.musicManager.StopMusic();
            amountOfPoints();
            GameManager.Instance.ranking.CheckRanking();
            GameManager.Instance.HandList.RemoveAllCards();
            GameManager.Instance.musicManager.PlaySound("endScreen");
            GameManager.Instance.buttonEnd.SetActive(true);
            GameManager.Instance.mainPanel.SetActive(false);

        }

        GameManager.Instance.round++;
        GameManager.Instance.gameplayActive = true;
       
    }

    public void AtributeRepaier()
    {

        GameManager.Instance.happiness = Mathf.Clamp01(GameManager.Instance.happiness);

        GameManager.Instance.loyalty = Mathf.Clamp01(GameManager.Instance.loyalty);

        GameManager.Instance.fear = Mathf.Clamp01(GameManager.Instance.fear);

        GameManager.Instance.education = Mathf.Clamp01(GameManager.Instance.education);

        GameManager.Instance.crime = Mathf.Clamp01(GameManager.Instance.crime);

        GameManager.Instance.tax = Mathf.Clamp01(GameManager.Instance.tax);
    }



    public void Wealth()
    {
        GameManager.Instance.wealth = GameManager.Instance.wealth * (1 - GameManager.Instance.crime);
    }

    public void SetMoney()
    {
        GameManager.Instance.money = GameManager.Instance.money + GameManager.Instance.wealth * GameManager.Instance.tax;
    }


    public void SetPopulation()
    {
        if (GameManager.Instance.loyalty < 0.80f && GameManager.Instance.happiness * 2 > 1.0f) GameManager.Instance.population = GameManager.Instance.population * GameManager.Instance.happiness * 2;

    }

    public bool IsEndGame()
    {
        
        if (GameManager.Instance.loyalty == 0 && GameManager.Instance.fear < 0.8f)
        {
            Debug.Log("(GameManager.Instance.loyalty == 0 && GameManager.Instance.fear < 0.8f)" + " " + GameManager.Instance.loyalty + " " + GameManager.Instance.fear);
            return true;    
        }


        if (GameManager.Instance.population == 0)
        {
            Debug.Log("GameManager.Instance.population == 0 " + GameManager.Instance.population);
            return true;
        }

        int helper = 0;

        for (int i = 0; i < GameManager.Instance.HandList.cardsInHand.Count; i++)
        {     
            if (GameManager.Instance.HandList.cardsInHand[i].costAmount <= GameManager.Instance.money)  return false; 
            else helper++;
        }


        if (GameManager.Instance.HandList.cardsInHand.Count == helper)
        {
            Debug.Log(" GameManager.Instance.HandList.cardsInHand.Count == helper " + GameManager.Instance.HandList.cardsInHand.Count );
            return true;
        }
        return false;
    }

    void amountOfPoints()
    {
        GameManager.Instance.Score = ((GameManager.Instance.loyalty + GameManager.Instance.fear) / 2) * GameManager.Instance.population * 1000 + (GameManager.Instance.population * 1000 * GameManager.Instance.happiness)
            + GameManager.Instance.money * 1000 + GameManager.Instance.wealth * 1000 * (GameManager.Instance.education - GameManager.Instance.crime);
    }

}
