using UnityEngine;

public class Pasue : MonoBehaviour
{
    [SerializeField] private GameObject pause;
    // Update is called once per frame
    void Update()
    {
        if (pause.activeSelf==false)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 0;
                AudioListener.pause = true;
                pause.SetActive(true);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 1;
                AudioListener.pause = false;
                pause.SetActive(false);
            }
        }
        
    }
}
