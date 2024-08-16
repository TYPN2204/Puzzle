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
    public HolderMatchingCards holder1, holder2, holder3;
    public GameObject cardLogo;
    private void Awake()
    {
        selectedCards = new List<CardMatching>();
    }

    void Start()
    {
        restart.onClick.AddListener(RestartGame);
    }

    public void HolderClick(int holderNumber)
    {
        switch (holderNumber)
        {
            case 1:
                if (holder1.addCardDone)
                {
                    holder2.addCardDone = true;
                    holder3.addCardDone = true;
                }
                else
                {
                    holder2.addCardDone = false;
                    holder3.addCardDone = false;
                }
                break;            
            case 2:
                if (holder2.addCardDone)
                {
                    holder1.addCardDone = true;
                    holder3.addCardDone = true;
                }
                else
                {
                    holder1.addCardDone = false;
                    holder3.addCardDone = false;
                }
                break;
            case 3:
                if (holder3.addCardDone)
                {
                    holder1.addCardDone = true;
                    holder2.addCardDone = true;
                }
                else
                {
                    holder1.addCardDone = false;
                    holder2.addCardDone = false;
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
                card.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.3f).SetEase(Ease.InExpo).OnComplete(() =>
                {
                    card.transform.DOScale(Vector3.zero, 1.25f);
                    if (card.idHolderOfThisCard == 0)
                    {
                        card.transform.DORotate(new Vector3(0, 0, -90), 1f).SetEase(Ease.InOutQuad);
                    }
                    else if (card.idHolderOfThisCard == 1)
                    {
                        card.transform.DORotate(new Vector3(0, 0, 0), 1f).SetEase(Ease.InOutQuad);
                    }
                    else if (card.idHolderOfThisCard == 2)
                    {
                        card.transform.DORotate(new Vector3(0, 0, 90), 1f).SetEase(Ease.InOutQuad);
                    }
                    card.transform.DOJump(cardLogo.transform.position, 3, 1, 1f).SetEase(Ease.InQuad).OnComplete(() =>
                    {
                        cardLogo.transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0.1f), 0.5f, 4);
                    });
                });
                //Vector3 movePos = card.transform.position + new Vector3(0, -0.5f, 0);
                //card.transform.DOMove(movePos, 0.25f);
                //card.GetComponent<SpriteRenderer>().DOFade(0, 0.25f);
                //yield return new WaitForSeconds(0.3f);
                Destroy(card.gameObject,2);
            }
            selectedCards.Clear();
        }
    }
}
