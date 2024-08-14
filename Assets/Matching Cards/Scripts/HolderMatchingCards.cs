using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class HolderMatchingCards : MonoBehaviour
{
    public int idHolder;
    public List<CardMatching> holder;
    
    void Awake()
    {
        holder = new List<CardMatching>();
    }

    public void AddCardToHolder(CardMatching card)
    {
        holder.Add(card);
        this.GetComponent<Button>().onClick.AddListener(ButtonChosen);
    }

    void ButtonChosen()
    {
        List<CardMatching> selectedCards = new List<CardMatching>();
        
        //kiểm tra các phần tử tiếp theo có cùng màu với thẻ trên cùng không

        var newList = holder.OrderByDescending(x => x.ordinalNumber).ToList();
        foreach (var cardList in newList)
        {
            if (cardList.idCard == newList[0].idCard)
            {
                selectedCards.Add(cardList);
            }
        }
    }
}
