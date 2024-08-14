using UnityEngine;

public class SpawnCards : MonoBehaviour
{
    public CombineCardData[] cards;

    void Start()
    {
        foreach (var card in cards)
        {
            Instantiate(card, new Vector2(Random.Range(1.5f, 6), Random.Range(-2.5f, 2.5f)), Quaternion.identity);
            card.holderOfThisCard = GameObject.FindGameObjectWithTag("Return").GetComponent<PlaceHolderData>();
        }
    }
}
