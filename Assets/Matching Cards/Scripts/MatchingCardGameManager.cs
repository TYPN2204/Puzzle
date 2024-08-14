using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MatchingCardGameManager : MonoBehaviour
{
    public List<CardMatching> selectedCards;
    public Button restart;
    public Button holder1, holder2, holder3;
    private void Awake()
    {
        selectedCards = new List<CardMatching>();
    }

    void Start()
    {
        restart.onClick.AddListener(RestartGame);
        holder1.onClick.AddListener(() => HolderClick(1));
        holder2.onClick.AddListener(() => HolderClick(2));
        holder3.onClick.AddListener(() => HolderClick(3));
    }

    void HolderClick(int holderNumber)
    {
        switch (holderNumber)
        {
            case 1:
                if (holder1.GetComponent<HolderMatchingCards>().addCardDone)
                {
                    holder2.GetComponent<HolderMatchingCards>().addCardDone = true;
                    holder3.GetComponent<HolderMatchingCards>().addCardDone = true;
                }
                else
                {
                    holder2.GetComponent<HolderMatchingCards>().addCardDone = false;
                    holder3.GetComponent<HolderMatchingCards>().addCardDone = false;
                }
                break;            
            case 2:
                if (holder2.GetComponent<HolderMatchingCards>().addCardDone)
                {
                    holder1.GetComponent<HolderMatchingCards>().addCardDone = true;
                    holder3.GetComponent<HolderMatchingCards>().addCardDone = true;
                }
                else
                {
                    holder1.GetComponent<HolderMatchingCards>().addCardDone = false;
                    holder3.GetComponent<HolderMatchingCards>().addCardDone = false;
                }
                break;
            case 3:
                if (holder3.GetComponent<HolderMatchingCards>().addCardDone)
                {
                    holder1.GetComponent<HolderMatchingCards>().addCardDone = true;
                    holder2.GetComponent<HolderMatchingCards>().addCardDone = true;
                }
                else
                {
                    holder1.GetComponent<HolderMatchingCards>().addCardDone = false;
                    holder2.GetComponent<HolderMatchingCards>().addCardDone = false;
                }
                break;
        }
    }
    void RestartGame()
    {
        SceneManager.LoadScene("Matching Card", LoadSceneMode.Single);
    }
    public void AddCard(CardMatching card)
    {
        selectedCards.Add(card);
        CheckEnoughCards();
    }

    void CheckEnoughCards()
    {
        if (selectedCards.Count == 3)
        {
            foreach (var card in selectedCards)
            {
                Destroy(card.gameObject);
            }
            selectedCards.Clear();
        }
    }
}
