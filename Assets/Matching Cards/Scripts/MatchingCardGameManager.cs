using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
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
    public void RestartGame()
    {
        SceneManager.LoadScene("Matching Card", LoadSceneMode.Single);
    }
    public void AddCard(CardMatching card)
    {
        selectedCards.Add(card);
        StartCoroutine(CheckEnoughCards());
    }

    IEnumerator CheckEnoughCards()
    {
        if (selectedCards.Count == 3)
        {
            foreach (var card in selectedCards)
            {
                yield return new WaitForSeconds(0.5f);
                Vector3 movePos = card.transform.position + new Vector3(0, 200, 0);
                card.transform.DOMove(movePos, 0.25f);
                card.GetComponent<Image>().DOFade(0, 0.25f);
                yield return new WaitForSeconds(0.3f);
                Destroy(card.gameObject);
            }
            selectedCards.Clear();
        }
    }
}
