using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

public class CombineCardData : MonoBehaviour
{
    [SerializeField]
    private int id;
    public int placeInHolder;
    public PlaceHolderData holderOfThisCard;
    public int Id
    {
        get { return id; }
    }
    public void CardReturn()
    {
        this.transform.DOMove(new Vector2(Random.Range(1.5f, 6), Random.Range(-2.5f, 2.5f)), 0.5f);
    }

    public void SelectCard()
    {
        FindObjectOfType<CombineCardGameManager>().CheckClosetLine(this);
    }
}
