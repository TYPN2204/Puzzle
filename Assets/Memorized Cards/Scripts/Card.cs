using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Card : MonoBehaviour
{
    public int id;
    public bool isSelected;
    public Sprite cardImage;
    public Sprite cardCloseSide;

    void Start()
    {
        StartCoroutine(ShowCard());
    }

    IEnumerator ShowCard()
    {
        GetComponent<SpriteRenderer>().sprite = cardImage;
        yield return new WaitForSeconds(1.5f);
        ClearCard();
    }

    public void ClearCard()
    {
        GetComponent<SpriteRenderer>().sprite = cardCloseSide;
        isSelected = false;
    }

    public void SelectCard()
    {
        if (!isSelected && FindObjectOfType<GameManager>().cardState==GameManager.OpenCardState.Opening)
        {
            isSelected = true;
            StartCoroutine(OpenCard());
            FindObjectOfType<GameManager>().AddCard(this);
        }
    }

    IEnumerator OpenCard()
    {
        this.gameObject.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.5f);
        yield return new WaitForSeconds(0.25f);
        this.gameObject.transform.DOScale(Vector3.one, 0.25f);
        GetComponent<SpriteRenderer>().sprite = cardImage;
    }
}