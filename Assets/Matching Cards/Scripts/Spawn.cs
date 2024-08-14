using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public CardMatching[] cards;
    public List<Transform> pivotHolder1;
    public List<Transform> pivotHolder2;
    public List<Transform> pivotHolder3;
    public List<Transform> holders;
    private List<CardMatching> cardList;

    void Start()
    {
        int soLuongBaiCuaMauDo = 3;
        for (int i = 0; i < soLuongBaiCuaMauDo; i++)
        { 
            cardList = new List<CardMatching>();
            cardList.Add(cards[i]);
        }
        SpawnCard();
    }

    void SpawnCard()
    {
        int randomPivotHolder1 = Random.Range(0, pivotHolder1.Count);
        int randomPivotHolder2 = Random.Range(0, pivotHolder2.Count);
        int randomPivotHolder3 = Random.Range(0, pivotHolder3.Count);
        
        // CardMatching card1 = Instantiate(cardSpawned, pivotHolder1[randomPivotHolder1].position, Quaternion.identity, holders[0]);
        // holders[0].GetComponent<HolderMatchingCards>().AddCardToHolder(card1);
        // card1.idHolderOfThisCard = holders[0].GetComponent<HolderMatchingCards>().idHolder;
        //
        // CardMatching card2 = Instantiate(cardSpawned, pivotHolder2[randomPivotHolder2].position, Quaternion.identity, holders[1]);
        // holders[1].GetComponent<HolderMatchingCards>().AddCardToHolder(card2);
        // card2.idHolderOfThisCard = holders[1].GetComponent<HolderMatchingCards>().idHolder;
        //
        // CardMatching card3 = Instantiate(cardSpawned, pivotHolder3[randomPivotHolder3].position, Quaternion.identity, holders[2]);
        // holders[2].GetComponent<HolderMatchingCards>().AddCardToHolder(card3);
        // card3.idHolderOfThisCard = holders[2].GetComponent<HolderMatchingCards>().idHolder;
        //
        // card1.ordinalNumber = pivotHolder1[randomPivotHolder1].GetComponent<PivotPosition>().pivotPosition;
        // card2.ordinalNumber = pivotHolder2[randomPivotHolder2].GetComponent<PivotPosition>().pivotPosition;
        // card3.ordinalNumber = pivotHolder3[randomPivotHolder3].GetComponent<PivotPosition>().pivotPosition;
        
        pivotHolder1.RemoveAt(randomPivotHolder1);
        
        pivotHolder2.RemoveAt(randomPivotHolder2);
        
        pivotHolder3.RemoveAt(randomPivotHolder3);
        
        if (pivotHolder1.Count == 0 && pivotHolder2.Count == 0 && pivotHolder3.Count == 0) return;

        // foreach (var VARIABLE in )
        // {
        //     
        // }
    }
}
