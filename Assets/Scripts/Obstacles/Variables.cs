using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Variables : MonoBehaviour
{
    public List<Vector2> pointsDrawing = new List<Vector2>();
    public float Speed= 20;
    public float Delay= 0.5f;
    public int countMax;

    public void setSpeed(float newValue)
    {
        Speed = newValue;
    }
    public void setDelay(float newValue) { 
        Delay = newValue;
    }
    public void setCountMax(int newValue)
    {
        countMax = newValue;
    }
    public int getCountMax()
    {
        return countMax;
    }
    public void setPoints(List<Vector2>newValue)
    {
        pointsDrawing = newValue;
    }
    public List<Vector2> getPoints()
    {
        return pointsDrawing;
    }
}
