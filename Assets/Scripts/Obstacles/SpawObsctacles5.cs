using System.Collections;
using UnityEngine;

public class SpawObsctacles5 : MonoBehaviour
{
    [SerializeField] private GameObject Obstacle;
    [SerializeField] private GameObject Graph;
    private float Delay;
    private float Speed;
    private float Y_start;
    private int Count=0;
    private int countMax;
    private bool Spawn = true;
    private float startAngle = 145;
    // Start is called before the first frame update
    private void Awake()
    {
        Speed = PlayerPrefs.GetFloat("Speed");
        countMax = PlayerPrefs.GetInt("Count_max");
        Delay = 7f / Speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Spawn)
        {
            StartCoroutine(Spawn_Obstacle());
        }
    }
    private IEnumerator Spawn_Obstacle()
    {
        Spawn = false;
        yield return new WaitForSeconds(Delay);
        if(startAngle<45) { startAngle = 45; Delay = 7f / Speed - 0.3f; }
        float angleRad = startAngle * (Mathf.PI / 180);
        startAngle -= 20;
        
        Y_start = -7 + 8.3f * Mathf.Sin(angleRad);
        Y_start = Mathf.Round(Y_start * 10f) / 10f;
        Count++;

        if (countMax == Count/10)// Se o contador chegou ao maximo a fase finalizou 
        {
            yield return new WaitForSeconds(40 / Speed);
            Time.timeScale = 0;
            Spawn = false;
            Graph.SetActive(true);
        }
        else if (Count%10==0) // A altura dos obstaculos simula um semi arco de esfera enquanto não atingir 
        {
            yield return new WaitForSeconds(25/Speed);
            startAngle = 145;
            Delay = 7f / Speed;
            Spawn = true;
        }
        else
        {
            GameObject clone = Instantiate(Obstacle, new Vector3(0, Y_start, 30), Quaternion.Euler(0, 0, 0));
            Rigidbody rigidbody = clone.GetComponentInChildren<Rigidbody>();
            rigidbody.velocity = new Vector3(0, 0, -Speed);
            Destroy(clone, 40 / Speed);
            clone.SetActive(true);
            Spawn = true;
        }
    }
}
