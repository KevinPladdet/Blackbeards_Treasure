using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateScript : MonoBehaviour
{

    [SerializeField] private GameManager gm;
    [SerializeField] private Sprite angryPirate;

    [SerializeField] private bool isHit;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Cannonball") && !isHit)
        {
            this.GetComponent<SpriteRenderer>().sprite = angryPirate;
            gm.AddScore(100);
            isHit = true;
            gm.piratesAlive -= 1;
            if (gm.piratesAlive == 0)
            {
                gm.LevelCompleted();
            }
            StartCoroutine(FadePirate());
        }
    }

    private IEnumerator FadePirate()
    {
        yield return new WaitForSeconds(2f);

        float duration = 1f;
        float elapsedTime = 0f;

        SpriteRenderer sr = this.GetComponent<SpriteRenderer>();

        Color color = sr.color;
        
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, elapsedTime / duration);
            sr.color = color;
            yield return null;
        }
        Destroy(gameObject);
    }
}
