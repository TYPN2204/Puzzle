using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Card[] cards;
    public List<Transform> pivot;

    void Start()
    {
        if (pivot.Count > 1 && pivot.Count % 2 == 0)
        {
            for (int i = 0; i < cards.Length; i++)
            {
                SpawnCard(cards[i]);
            }
        }
        else if (pivot.Count > 1 && pivot.Count % 2 == 1)
        {
            pivot.RemoveAt(pivot.Count - 1);
        }
        else if (pivot.Count == 1)
        {
            Debug.Log("Error");
        }
    }

    void SpawnCard(Card card)
    {
        for (int i = 0; i < 2; i++)
        {
            int randomPivot = Random.Range(0, pivot.Count);
            Instantiate(card, pivot[randomPivot].position, Quaternion.identity);
            pivot.RemoveAt(randomPivot);
            if (pivot.Count == 0) return;
        }
    }
}
