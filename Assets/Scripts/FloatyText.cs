using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FloatyText : MonoBehaviour {
    public Vector3 velocity = Vector3.up;
    public float time = 1;
    float timer = 0;

    public void SetText(string message, Color color) {
        Text t = GetComponent<Text>();
        t.text = message;
        t.color = color;
    }

    void Update() {
        transform.position += velocity * Time.deltaTime;
        timer += Time.deltaTime;
        if (timer >= time) {
            Destroy(gameObject);
        }
    }

    public static FloatyText CreateDefaultFloatyText() {
        GameObject go = new GameObject("floaty text");
        go.AddComponent<Canvas>();
        go.AddComponent<CanvasScaler>();
        go.AddComponent<CanvasRenderer>();
        Text t = go.AddComponent<Text>();
        t.font = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
        ContentSizeFitter csf = go.AddComponent<ContentSizeFitter>();
        csf.verticalFit = csf.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
        RectTransform rt = go.GetComponent<RectTransform>();
        rt.localScale = new Vector3(0.02f, 0.02f, 0.02f);
        FloatyText ft = go.AddComponent<FloatyText>();
        return ft;
    }

    public static FloatyText Create(string message, Vector3 position, Vector3 velocity, Color color, float time) {
        FloatyText ft = CreateDefaultFloatyText();
        ft.SetText(message, color);
        ft.transform.position = position;
        ft.velocity = velocity;
        ft.time = time;
        return ft;
    }
}
