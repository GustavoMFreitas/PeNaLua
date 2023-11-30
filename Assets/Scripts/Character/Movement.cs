using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Movement : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    private float move = 0;
    private PlayerControls controls;
    private List<float> graphPoints = new List<float>();
    private bool wait = true;
    private float minMove;
    private float maxMove;
    private float normalizado;
    // Start is called before the first frame update
    void Start()
    {
        controls = new PlayerControls();
        controls.Trigger.Enable();
        minMove = PlayerPrefs.GetFloat("MinMove", -1f); // Set a default value of -1 if not found
        maxMove = PlayerPrefs.GetFloat("MaxMove", 1f);  // Set a default value of 1 if not found
    }

    // Update is called once per frame
    void Cima(float movement)
    {

        rb.transform.localPosition = new Vector3(rb.position.x, movement, rb.position.z);

    }
    void Update()
    {
        
        if (Time.timeScale==0)
        {
        }
        else
        {
            normalizado = Mathf.InverseLerp(minMove, maxMove, -controls.Trigger.Move.ReadValue<float>());
            move = ExtensionMethods.Remap(Mathf.Clamp01(normalizado), 1, 0, 2.8f, -2);
            Cima(move);
        }
        if (wait)
        {
            StartCoroutine(AppendinVector());
        }
    }
    IEnumerator AppendinVector()
    {
        wait = false;
        if (SceneManager.GetActiveScene().name == "Fase 3" || SceneManager.GetActiveScene().name == "Fase 5")
        {
            yield return new WaitForSeconds(1 / PlayerPrefs.GetFloat("Speed"));
            graphPoints.Add(rb.position.y);
        }
        else
        {
            yield return new WaitForSeconds(7 / PlayerPrefs.GetFloat("Speed"));
            graphPoints.Add(rb.position.y);
        }

        wait = true;
    }
    public List<float> GetgraphPoints()
    {
        return graphPoints;
    }

}
