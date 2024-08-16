using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using DG.Tweening;
public class HolderMatchingCards : MonoBehaviour
{
    public int idHolder;
    public List<CardMatching> holder;
    public bool addCardDone;
    public bool matchingCard;
    public MatchingCardGameManager gameManager;
    public GameObject holder1Element0, holder2Element0, holder3Element0;
    
    void Awake()
    {
        holder = new List<CardMatching>();
        addCardDone = false;
        matchingCard = false;
        //this.GetComponent<Button>().onClick.AddListener(ButtonChosen);
    }

    void Start()
    {
        CheckSpawn();
    }

    void CheckSpawn()
    {
        int countColor = 0;
        foreach (var card in holder)
        {
            if (card.idCard == holder[0].idCard)
            {
                countColor++;
            }
        }

        if (countColor == 3)
        {
            gameManager.RestartGame();
        }
    }

    public void AddCardToHolder(CardMatching card)
    {
        holder.Add(card);
    }
    
    public void ButtonChosen()
    { 
        //kiểm tra các phần tử tiếp theo có cùng màu với thẻ trên cùng không, nếu giống thì thêm vào list selected cards
        //Nếu đang ghép thẻ thì k thể bấm
        if (matchingCard)
        {
            return;
        }
        if (!addCardDone)
        {
            LayThe();
        }
        else if (addCardDone)
        {
            NhanThe();
        }
    }

    void LayThe()
    {
        var newList = holder.OrderByDescending(x => x.ordinalNumber).ToList();
        foreach (var cardList in newList)
        {
            if (cardList.idCard == newList[0].idCard)
            {
                gameManager.AddCard(cardList);
                this.holder.Remove(cardList);
                Vector3 movePos = cardList.transform.position + new Vector3(0,-0.2f,0);
                cardList.transform.DOMove(movePos, 0.15f);
            }
            else break;
        }

        if (gameManager.selectedCards.Count > 0)
        {
            addCardDone = true;
        }
        newList.Clear();
    }

    void NhanThe()
    {
        var selectedList = gameManager.selectedCards.OrderBy(x => x.ordinalNumber).ToList();

        foreach (var card in selectedList)
        {
            this.AddCardToHolder(card);
            card.idHolderOfThisCard = this.idHolder;
            card.ordinalNumber = this.holder.Count - 1;
            
            
            if(this.holder.Count < 3 && this.holder[0].idCard == card.idCard)
            {
                if (this.idHolder == 0)
                {
                    Vector3 movePos1 = holder1Element0.transform.position + new Vector3(0,-0.2f,-0.1f) *card.ordinalNumber;
                    card.transform.DOJump(movePos1,-1,1,0.15f).SetEase(Ease.Linear);
                }
                else if (this.idHolder == 1)
                {
                    Vector3 movePos2 = holder2Element0.transform.position + new Vector3(0,-0.2f,-0.1f)*card.ordinalNumber;
                    card.transform.DOJump(movePos2,-1,1,0.15f).SetEase(Ease.Linear);
                }
                else if (this.idHolder == 2)
                {
                    Vector3 movePos3 = holder3Element0.transform.position + new Vector3(0,-0.2f,-0.1f)*card.ordinalNumber;
                    card.transform.DOJump(movePos3,-1,1,0.15f).SetEase(Ease.Linear);
                }
            }
            else
            {
                Vector3 movePos = this.holder[0].transform.position + new Vector3(0,-0.2f,-0.1f)*card.ordinalNumber;
                card.transform.DOJump(movePos,-1,1,0.15f);
            }
        }

        StartCoroutine(CheckWinStackCards());
        gameManager.selectedCards.Clear();
        selectedList.Clear();
        addCardDone = false;
    }

    IEnumerator CheckWinStackCards()
    {
        int count = 0;
        List<CardMatching> cardsSameColor = new List<CardMatching>();
        var newList = holder.OrderByDescending(x => x.ordinalNumber).ToList();
        foreach (var card in newList)
        {
            if (card.idCard == newList[0].idCard)
            {
                count++;
                cardsSameColor.Add(card);
            }
            else break;
        }

        if (count == 3)
        {
            foreach (var card in cardsSameColor)
            {
                matchingCard = true;
                yield return new WaitForSeconds(0.15f);
                Vector3 movePos = card.transform.position + new Vector3(0,-0.2f,-0.1f);
                card.transform.DOMove(movePos, 0.15f).SetEase(Ease.Linear);
                card.GetComponent<Image>().DOFade(0, 0.15f).SetEase(Ease.Linear);
                holder.Remove(card);
                yield return new WaitForSeconds(0.15f);
                matchingCard = false;
                Destroy(card.gameObject);
            }
            
        }
        cardsSameColor.Clear();
    }
}
