using UnityEngine;

public class ChooseHolder : MonoBehaviour
{
    public MatchingCardGameManager gameManager;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            if (hit.collider != null)
            {
                HolderMatchingCards holder = hit.collider.gameObject.GetComponent<HolderMatchingCards>();
                if (holder != null)
                {
                    holder.ButtonChosen();
                    if (holder.CompareTag("Holder1"))
                    {
                        gameManager.HolderClick(1);
                    }
                    else if (holder.CompareTag("Holder2"))
                    {
                        gameManager.HolderClick(2);
                    }
                    else if (holder.CompareTag("Holder3"))
                    {
                        gameManager.HolderClick(3);
                    }
                }
            }
        }
    }
}
