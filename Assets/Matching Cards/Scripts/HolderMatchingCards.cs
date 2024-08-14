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
    
    void Awake()
    {
        holder = new List<CardMatching>();
        addCardDone = false;
        this.GetComponent<Button>().onClick.AddListener(ButtonChosen);
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
            if (this.holder.Count < 3)
            {
                card.transform.SetParent(this.transform);
                card.transform.position = Vector3.zero;
                Vector3 movePos = new Vector3(0, -390, 0) + new Vector3(0,200,0)*card.ordinalNumber;
                card.transform.DOJump(movePos,100,1,0.5f);
            }
            else if(this.holder.Count > 2)
            {
                card.transform.SetParent(this.transform);
                Vector3 movePos = this.holder[0].transform.position + new Vector3(0,200,0)*card.ordinalNumber;
                card.transform.DOJump(movePos,100,1,0.5f);
            }
        }

        gameManager.selectedCards.Clear();
        selectedList.Clear();
        addCardDone = false;
    }
}
