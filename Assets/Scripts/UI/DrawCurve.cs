using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawCurve : MonoBehaviour
{
    [SerializeField] private Text numObstacles;
    [SerializeField] private Canvas main;
    [SerializeField] private MenuMannager menuMannager;
    [SerializeField] private RectTransform drawingArea;
    private List<Vector2> points = new List<Vector2>();
    [SerializeField] private GameObject linePrefab;
    [SerializeField] private GameObject dotprefab;
    [SerializeField] private Variables var; 
    private Vector2 aux;
    private bool flag = true;
    private bool first = true;

    private void Awake()
    {
        DontDestroyOnLoad(var);
    }
    public void DrawLine(Vector2 start, Vector2 end)
    {
        if (!first) {
            GameObject line = Instantiate(linePrefab, drawingArea.Find("line").transform);
            line.SetActive(true);
            Vector2 dir = end - start;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            float distance = dir.magnitude;
            RectTransform lineTransform = line.GetComponent<RectTransform>();
            lineTransform.sizeDelta = new Vector2(distance,10);
            lineTransform.anchoredPosition = start + dir / 2;
            lineTransform.localEulerAngles = new Vector3(0, 0, angle);
        }
        GameObject dot = Instantiate(dotprefab, drawingArea.Find("dot").transform);
        dot.SetActive(true);
        RectTransform dotTransform = dot.GetComponent<RectTransform>();
        dotTransform.anchoredPosition = end;
    }
    private void OnEnable()
    {
        flag = true;
    }
    private void OnDisable()
    {
        foreach (Transform child in drawingArea.Find("dot"))
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in drawingArea.Find("line"))
        {
            Destroy(child.gameObject);
        }
        points=new List<Vector2>();
        first=true;
    }
    void Update()
    {

        if (flag)
        {
            // Check if the canvas is assigned
            if (drawingArea == null)
            {
                Debug.LogError("Canvas is not assigned to UIMousePosition script.");
                return;
            }

            // Get the mouse position in screen coordinates
            Vector2 mousePosition = Input.mousePosition;


            // Convert screen coordinates to local coordinates of the canvas
            RectTransformUtility.ScreenPointToLocalPointInRectangle(drawingArea.transform as RectTransform, mousePosition, main.worldCamera, out Vector2 localPoint);
            localPoint.x = Mathf.Clamp(localPoint.x, -drawingArea.rect.width / 2, drawingArea.rect.width / 2);
            localPoint.y = Mathf.Clamp(localPoint.y, -drawingArea.rect.height / 2, drawingArea.rect.height / 2);
            transform.localPosition = localPoint;
            if (Input.GetMouseButtonDown(0) && points.Count < int.Parse(numObstacles.text))
            {
                points.Add(localPoint);
                DrawLine(aux, localPoint);
                aux = localPoint;
                first = false;
            }
            else if (points.Count >= int.Parse(numObstacles.text))
            {
                flag = false;

                var.setPoints(points);
                var.setCountMax(int.Parse(numObstacles.text));
                //    flag = false;
                //    menuMannager.LoadingScreens("Fase 6");
            }
        }
    }
}