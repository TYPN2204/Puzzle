using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HolderMatchingCards : MonoBehaviour
{
    public int idHolder;
    public Stack<CardMatching> Holder;
    
    public void AddCardToHolder(CardMatching card)
    {
        this.GetComponent<Button>().onClick.AddListener(ButtonChosen);
        Holder = new Stack<CardMatching>();
        Holder.Push(card);
    }

    void ButtonChosen()
    {
        
    }
}
