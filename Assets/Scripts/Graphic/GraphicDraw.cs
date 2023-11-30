using System.Collections.Generic;
using UnityEngine;



public class GraphDrawingController : MonoBehaviour
{
    [SerializeField]private Sprite circle;
    private RectTransform graphContainer;
    [SerializeField] private Movement movement;
    private List<float> points;

    private float GetAngleFromVectorFloat(Vector3 dir)
    {
        
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }
    private void Awake()
    {
        points = movement.GetgraphPoints();
        graphContainer = transform.Find("Graphcont").GetComponent<RectTransform>();
        //List<int> points = new List<int>() { 300, 400 };
        show(points);
        //createCircle(new Vector2(0, 0));
    }
    private GameObject createCircle(Vector2 anchored)
    {
        GameObject gameObject = new GameObject("circle", typeof(UnityEngine.UI.Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<UnityEngine.UI.Image>().sprite = circle;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchored;
        rectTransform.sizeDelta = new Vector2(11, 11);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return gameObject;
    }
    private void show(List<float> values)
    {
        float graphHeight = graphContainer.sizeDelta.y;
        float MaxSize = 1200;
        float xSize = MaxSize/values.Count;
        
        GameObject lastCircle=null;
        for (int i = 0; i < values.Count; i++)
        {
            float item = ExtensionMethods.Remap(values[i], -2, 2.8f, 0, graphHeight);
            float xPos = i * xSize;
            float yPos = item;
            GameObject circle= createCircle(new Vector2(xPos, yPos));
            if(lastCircle!= null )
            {
                CreateConnection(lastCircle.GetComponent<RectTransform>().anchoredPosition, circle.GetComponent<RectTransform>().anchoredPosition);
            }
            lastCircle = circle;
        }
    }
    private void CreateConnection(Vector2 dotPostitionA, Vector2 dotPositionB)
    {
        GameObject gameObject = new GameObject("dot Connection", typeof(UnityEngine.UI.Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, .5f);
        RectTransform rectTransform=gameObject.GetComponent<RectTransform>();
        Vector2 dir = (dotPositionB - dotPostitionA).normalized;
        float distance= Vector2.Distance(dotPostitionA,dotPositionB);
        rectTransform.anchorMin= new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(distance, 3f);
        rectTransform.anchoredPosition = dotPostitionA+dir*distance*.5f;
        rectTransform.localEulerAngles = new Vector3(0,0,GetAngleFromVectorFloat(dir));
    }
}
