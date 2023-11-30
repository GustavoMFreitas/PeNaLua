using UnityEngine;
using UnityEngine.UI;

public class ValueController : MonoBehaviour
{
    [SerializeField] private Text valueText;
    [SerializeField] private Text valueText2;
    [SerializeField] private Text valueText3;
    [SerializeField] private Text warning;
    private float delay=0;
    private float speed=0;
    private int countMax=0;
    private bool isInvalid1, isInvalid2, isInvalid3;
    void Start()
    {
        PlayerPrefs.SetFloat("Delay", 0);
        PlayerPrefs.SetFloat("Speed", 0);
        PlayerPrefs.SetInt("Count_max", 0);
        UpdateDisplay();
    }
    private void OnDisable()
    {
        delay = 0;
        speed = 0;
        countMax = 0;
        UpdateDisplay();
    }
    void UpdateDisplay()
    {
        isInvalid1 = delay < 0;
        isInvalid2=speed < 0;
        isInvalid3 = countMax < 0;
        int fontSize = 40; // Set the desired font size
        warning.text = "";

        // Set text and style for valueText
        if (valueText != null)
        {
            valueText.color = isInvalid1 ? Color.red : Color.black;
            valueText.text = isInvalid1 ? "Valor inválido: " + delay.ToString("F1") : delay.ToString("F1");
            valueText.fontSize = isInvalid1 ? fontSize - 20 : fontSize;
        }

        // Set text and style for valueText2
        if (valueText2 != null)
        {
            valueText2.color = isInvalid2 ? Color.red : Color.black;
            valueText2.text = isInvalid2 ? "Valor inválido: " + speed.ToString("F0") : speed.ToString("F0");
            valueText2.fontSize = isInvalid2 ? fontSize - 20 : fontSize;
        }

        // Set text and style for valueText3
        if (valueText3 != null)
        {
            valueText3.color = isInvalid3 ? Color.red : Color.black;
            valueText3.text = isInvalid3 ? "Valor inválido: " + countMax.ToString("F0") : countMax.ToString("F0");
            valueText3.fontSize = isInvalid3 ?fontSize-20: fontSize;
        }

    }

    public void IncrementValueDelay()
    {
        delay += 0.1f;
        PlayerPrefs.SetFloat("Delay",delay);
        UpdateDisplay();
    }
    public void DecrementValueDelay()
    {
        if (delay > 0)
        {
            delay -= 0.1f;
            PlayerPrefs.SetFloat("Delay", delay);
        }
        UpdateDisplay();
    }
    public void IncrementValueSpeed()
    {
        speed += 1f;
        PlayerPrefs.SetFloat("Speed", speed);
        UpdateDisplay();
    }
    public void DecrementValuesSpeed()
    {
        if (speed > 0)
        {
            speed -= 1f;
            PlayerPrefs.SetFloat("Speed", speed);
        }
        UpdateDisplay();
    }
    public void IncrementValueCount()
    {
        countMax += 1;
        PlayerPrefs.SetInt("Count_max", countMax);
        UpdateDisplay();
    }
    public void DecrementValuesCount()
    {
        if (countMax > 0)
        {
            countMax -= 1;
            PlayerPrefs.SetInt("Count_max", countMax);
        }
        UpdateDisplay();
    }

}
