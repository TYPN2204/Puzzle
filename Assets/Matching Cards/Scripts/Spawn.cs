using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public CardMatching[] cards;
    // public List<Transform> pivotHolder1;
    // public List<Transform> pivotHolder2;
    // public List<Transform> pivotHolder3;
    public List<Transform> pivotHolder;
    public List<Transform> holders;
    private List<CardMatching> cardList;

    void Start()
    {
        cardList = new List<CardMatching>();

        int soLuongBaiCuaThe = 3;
        for (int i = 0; i < cards.Length; i++)
        {
            for (int k = 0; k < soLuongBaiCuaThe; k++)
            {
                cardList.Add(cards[i]);
            }
        }
        SpawnCard();
    }

    void SpawnCard()
    {
        for (int pivotNumber = 0; pivotNumber < pivotHolder.Count; pivotNumber++)
        {
            //chia bài ngẫu nhiên vào lần lượt vị trí
            int randomCard = Random.Range(0, cardList.Count);
            int holderNumber = 0;
            
            //chia bài theo cột holder1: 0-2, holder2: 3-5, holder3: 6-8 
            if (pivotNumber < 3)
            {
                holderNumber = 0;
                //Debug.Log("PivotNumber: " + pivotNumber +"< 3 -> holderNumber = "+holderNumber);
            }
            if (pivotNumber > 2 && pivotNumber < 6)
            {
                holderNumber = 1;
                //Debug.Log("6 > PivotNumber: " + pivotNumber +"> 2 -> holderNumber = "+holderNumber);
            }
            if (pivotNumber > 5)
            {
                holderNumber = 2;
                //Debug.Log("PivotNumber: " + pivotNumber +"> 5 -> holderNumber = "+holderNumber);
            }
            
            var card = Instantiate(cardList[randomCard], pivotHolder[pivotNumber].position, Quaternion.identity, holders[holderNumber]);
            card.idHolderOfThisCard = holders[holderNumber].GetComponent<HolderMatchingCards>().idHolder;
            card.ordinalNumber = pivotHolder[pivotNumber].GetComponent<PivotPosition>().pivotPosition;
            holders[holderNumber].GetComponent<HolderMatchingCards>().AddCardToHolder(card);

            cardList.RemoveAt(randomCard);
            
            
            //còn trường hợp chia 3 thẻ cùng màu trong 1 ô
        }
    }
}
