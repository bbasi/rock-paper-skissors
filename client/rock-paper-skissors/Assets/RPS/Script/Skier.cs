using System.Collections;
using UnityEngine;

public class Skier : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    [SerializeField] Color ColorSelectable   = Color.white;
    [SerializeField] Color ColorUnselectable = Color.gray;
    [SerializeField] float DurationDelayInMin = 0.00f;
    [SerializeField] float DurationDelayInMax = 0.50f;
    [SerializeField] float DurationDelayOutMin = 1.00f;
    [SerializeField] float DurationDelayOutMax = 3.14f;
    [SerializeField] float XMin = 2.5f;
    [SerializeField] float XMax = 6.0f;
    [SerializeField] float SpeedMin = 3.14f;
    [SerializeField] float SpeedMax = 7.50f;
    [SerializeField] float ScaleMin = 0.65f;
    [SerializeField] float ScaleMax = 1.20f;
    [SerializeField] float SelectedSpeedModifier = 2.50f;
    BoxCollider2D boxCollider;
    SpriteRenderer spriteRenderer;
    int pointValue;
    

    private IEnumerator Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        while(true)
        {
            boxCollider.enabled = true;
            spriteRenderer.color = ColorSelectable;
            spriteRenderer.sprite = sprites.GetRandom();

            Vector2 posStart = transform.position;
            posStart.x = RND.GetFloat(XMin, XMax);
            transform.position = posStart;

            Vector2 posEnd = transform.position;
            posEnd.y = -posStart.y;

            float scale = RND.GetFloat(ScaleMin, ScaleMax);
            transform.localScale = new Vector3(scale, scale, 1f);

            yield return new WaitForSeconds(RND.GetFloat(DurationDelayInMin, DurationDelayInMax));

            float speed = RND.GetFloat(SpeedMin, SpeedMax);
            pointValue = (int)speed * 50;

            while (transform.position.y > posEnd.y)
            {
                transform.position -= new Vector3(0, (boxCollider.enabled ? speed : (speed * SelectedSpeedModifier)) * Time.deltaTime, 0);
                yield return null;
            }
            transform.position = posStart;
            yield return new WaitForSeconds(RND.GetFloat(DurationDelayOutMin, DurationDelayOutMax));
        }
    }

    void OnMouseDown()
    {
        boxCollider.enabled = false;
        spriteRenderer.color = ColorUnselectable;
        glbl._.Audio.PlaySFX_Click();
        glbl._.Player.AdjustCoins(pointValue);
    }
}
