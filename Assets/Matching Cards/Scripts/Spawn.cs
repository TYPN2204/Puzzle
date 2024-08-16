using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Spawn : MonoBehaviour
{
    public CardMatching[] cards;
    public List<Transform> pivotHolder;
    public List<Transform> holders;
    private List<CardMatching> _cardList;
    private List<CardMatching> _cardListDemo;
    private List<CardMatching> _cardListCheckDuplicate;

    void Start()
    {
        _cardList = new List<CardMatching>();
        _cardListDemo = new List<CardMatching>();
        _cardListCheckDuplicate = new List<CardMatching>();

        GetCard();
        SpawnCard();
    }

    void GetCard()
    {
        int soLuongBaiCuaThe = 3;
        for (int i = 0; i < cards.Length; i++)
        {
            for (int k = 0; k < soLuongBaiCuaThe; k++)
            {
                _cardListDemo.Add(cards[i]);
            }
        }
        //kiểm tra 3 thẻ trong cardlistCheckDup có cùng màu không
        int countCardSameColor = 0;
        int countStack = 0;

        while(countStack<3)
        {
            for(int n = 0;  n <soLuongBaiCuaThe; n++)
            {
                int randomCard = Random.Range(0, _cardListDemo.Count);
                _cardListCheckDuplicate.Add(_cardListDemo[randomCard]);
            }
    
            foreach (var card in _cardListCheckDuplicate)
            {
                if (card.idCard == _cardListCheckDuplicate[0].idCard)
                { 
                    countCardSameColor++;
                }
            }

            if (countCardSameColor == 3)
            {
                _cardListCheckDuplicate.Clear();
                countCardSameColor = 0;
            }
            else
            { 
                foreach (var card in _cardListCheckDuplicate)
                { 
                    _cardList.Add(card);
                }

                countStack++;
                //Debug.Log("+1 stack: Card1: "+_cardListCheckDuplicate[0].name + " ,Card2: "+_cardListCheckDuplicate[1].name + " ,Card3: "+_cardListCheckDuplicate[2].name);
                _cardListCheckDuplicate.Clear();
                countCardSameColor = 0;
            }
        }
        if (_cardList.Count == 9)
        {
            //Debug.Log("Enough 9 cards, check card list");
            CheckCardList();
        }
    }

    void CheckCardList()
    {
        int greenCardCount = 0;
        int redCardCount = 0;
        int yellowCardCount = 0;
        foreach (var card in _cardList)
        {
            if (card.CompareTag("Green Card"))
            {
                greenCardCount++;
            }
            if (card.CompareTag("Red Card"))
            {
                redCardCount++;
            }
            if (card.CompareTag("Yellow Card"))
            {
                yellowCardCount++;
            }
        }

        if (greenCardCount != 3 || redCardCount != 3 || yellowCardCount != 3)
        {
            //Debug.Log("Green card: "+greenCardCount+" ,Red card: "+redCardCount+" ,Yellow card: "+yellowCardCount);
            //Debug.Log("Get card again");
            _cardList.Clear();
            _cardListDemo.Clear();
            _cardListCheckDuplicate.Clear();
            GetCard();
            greenCardCount = 0;
            redCardCount = 0;
            yellowCardCount = 0;
        }
        else
        {
            //Debug.Log("Card can spawn");
        }
    }
    public void SpawnCard()
    {
        for (int pivotNumber = 0; pivotNumber < pivotHolder.Count; pivotNumber++)
        {
            //chia bài ngẫu nhiên vào lần lượt vị trí
            //int randomCard = Random.Range(0, _cardList.Count);
            int holderNumber = 0;
            
            //chia bài theo cột holder1: 0-2, holder2: 3-5, holder3: 6-8 
            if (pivotNumber < 3)
            {
                holderNumber = 0;
            }
            if (pivotNumber > 2 && pivotNumber < 6)
            {
                holderNumber = 1;
            }
            if (pivotNumber > 5)
            {
                holderNumber = 2;
            }
            
            var card = Instantiate(_cardList[0], pivotHolder[pivotNumber].position, Quaternion.identity);
            //card.transform.DOScale(Vector3.one, 1f).SetEase(Ease.Linear);
            card.idHolderOfThisCard = holders[holderNumber].GetComponent<HolderMatchingCards>().idHolder;
            card.ordinalNumber = pivotHolder[pivotNumber].GetComponent<PivotPosition>().pivotPosition;
            holders[holderNumber].GetComponent<HolderMatchingCards>().AddCardToHolder(card);
            
            _cardList.RemoveAt(0);
        }
    }
}