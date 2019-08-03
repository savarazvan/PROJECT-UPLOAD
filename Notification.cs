using UnityEngine;
using UnityEngine.UI;

public class Notification : MonoBehaviour
{
    public float scrolling;
    public float alpha;
    public float duration;
    RectTransform rectTransform;
    float yPos;
    Image image;
    Text childText;
    Vector2 pos;
    public string notifText;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        pos = rectTransform.position;
        yPos = rectTransform.position.y;
        image = GetComponent<Image>();
        childText = GetComponentInChildren<Text>();
        Color transparent = new Color(1, 1, 1, 1);
        image.color = transparent;
        childText.color = transparent;
    }

    private void Update()
    {
        //childText.text = notifText;

        if (image.color.a > 0)
        {
            yPos = Mathf.Lerp(rectTransform.position.y, pos.y + scrolling, 10f * Time.deltaTime);
            rectTransform.position = new Vector2(rectTransform.position.x, yPos);
            var tempColor = image.color;
            tempColor.a = Mathf.Lerp(tempColor.a, 0, Time.fixedDeltaTime/duration);
            image.color = tempColor;
            childText.color = image.color;
            
        }

    }

   
}
