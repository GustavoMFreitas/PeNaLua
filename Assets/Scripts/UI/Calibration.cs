using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Calibration : MonoBehaviour
{
    [SerializeField] private Text text;
    private int Count=0;
    private PlayerControls controls;
    private List<float> movementPoints = new List<float>();
    private float move;
    private bool activeMin=false;
    private bool first = true;
    private int aux = 0;
    // Start is called before the first frame update
    private void OnEnable()
    {
        controls = new PlayerControls();
        controls.Trigger.Enable();
        activeMin = true;
    }
    private void OnDisable()
    {
        activeMin = false;
        text.text = "";
        move =0;
        movementPoints = new List<float>();
        Count = 0;
        first = true;
        aux = 0;
    }
    public void enable()
    {
        activeMin = true;
    }
    

    // Update is called once per frame
    void Update()
    {
        if (activeMin)
            StartCoroutine(CalibrationsMin());
    }
    IEnumerator CalibrationsMin()
    {
        activeMin = false;

        if (first) {
            StartCoroutine(CountdownDots());
        }
        else{
            if (Count < 6)
            {
                text.text = 5 - (Count++) + " seg";

                yield return new WaitForSeconds(1);
                movementPoints.Add(-controls.Trigger.Move.ReadValue<float>());
                activeMin = true;
            }
            else {
                move = movementPoints.Average();
                text.text ="Valor mínimo = "+ move.ToString();
                PlayerPrefs.SetFloat("MinMove", move);
                PlayerPrefs.Save();
                yield return new WaitForSeconds(1);
                first = true;
            }
        }

    }
    private IEnumerator CountdownDots()
    {
        text.text = "Coloque o pé na posição mínima ";

        while (aux < 3)
        {
            text.text += ".";
            aux++;
            yield return new WaitForSeconds(1);
        }
        first = false;
        activeMin = true;
    }
}
