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
    public MatchingCardGameManager gameManager;
    public GameObject holder1Element0, holder2Element0, holder3Element0;
    
    void Awake()
    {
        holder = new List<CardMatching>();
        addCardDone = false;
        this.GetComponent<Button>().onClick.AddListener(ButtonChosen);
    }

    void Start()
    {
        CheckSpawn();
    }

    public void CheckSpawn()
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
    
    void ButtonChosen()
    { 
        //kiểm tra các phần tử tiếp theo có cùng màu với thẻ trên cùng không, nếu giống thì thêm vào list selected cards

        if (!addCardDone)
        {
            LayThe();
        }
        else
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
                Vector3 movePos = cardList.transform.position + new Vector3(0,200,0);
                cardList.transform.DOMove(movePos, 0.25f);
            }
            else break;
        }

        if (gameManager.selectedCards.Count > 0)
        {
            addCardDone = true;
        }
        newList.Clear();
    }

    void NhanThe()     // lỗi di chuyển
    {
        var selectedList = gameManager.selectedCards.OrderBy(x => x.ordinalNumber).ToList();

        foreach (var card in selectedList)
        {
            this.AddCardToHolder(card);
            card.idHolderOfThisCard = this.idHolder;
            card.ordinalNumber = this.holder.Count - 1;
            
            
            if(this.holder.Count < 3 && this.holder[0].idCard == card.idCard)
            {
                card.transform.SetParent(this.transform);
                if (this.idHolder == 0)
                {
                    Vector3 movePos1 = holder1Element0.transform.position + new Vector3(0,200,0)*card.ordinalNumber;
                    card.transform.DOJump(movePos1,100,1,0.5f);
                }
                else if (this.idHolder == 1)
                {
                    Vector3 movePos2 = holder2Element0.transform.position + new Vector3(0,200,0)*card.ordinalNumber;
                    card.transform.DOJump(movePos2,100,1,0.5f);
                }
                else if (this.idHolder == 2)
                {
                    Vector3 movePos3 = holder3Element0.transform.position + new Vector3(0,200,0)*card.ordinalNumber;
                    card.transform.DOJump(movePos3,100,1,0.5f);
                }
            }
            else
            {
                card.transform.SetParent(this.transform);
                Vector3 movePos = this.holder[0].transform.position + new Vector3(0,200,0)*card.ordinalNumber;
                card.transform.DOJump(movePos,100,1,0.5f);
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
                yield return new WaitForSeconds(0.5f);
                Vector3 movePos = card.transform.position + new Vector3(0, 200, 0);
                card.transform.DOMove(movePos, 0.25f);
                card.GetComponent<Image>().DOFade(0, 0.25f);
                yield return new WaitForSeconds(0.3f);
                Destroy(card.gameObject);
            }
            
        }
        cardsSameColor.Clear();
    }
}
