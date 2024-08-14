using UnityEngine;

public class ChooseCard : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            if (hit.collider != null)
            {
                Debug.Log("hit");
                Card card = hit.collider.gameObject.GetComponent<Card>();
                if (card != null)
                {
                    card.SelectCard();
                }
            }
        }
    }
}