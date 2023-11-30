using System.Collections;
using UnityEngine;

public class SpawObsctacles4 : MonoBehaviour
{
    [SerializeField] private GameObject Obstacle;
    [SerializeField] private GameObject Graph;
    private float Delay;
    private float Speed;
    private float Y_start;
    private int Count;
    private bool Spawn = true;
    // Start is called before the first frame update
    private void Awake()
    {
        Delay = PlayerPrefs.GetFloat("Delay");
        Speed = PlayerPrefs.GetFloat("Speed");
        Y_start = -2;
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
        GameObject clone = Instantiate(Obstacle, new Vector3(0, Y_start, 30), Quaternion.Euler(0, 0, 0));
        Rigidbody rigidbody = clone.GetComponentInChildren<Rigidbody>();
        rigidbody.velocity = new Vector3(0, 0, -Speed);
        Destroy(clone, 40 / Speed);
        clone.SetActive(true);
        Count++;

        if (Count == 10)
        {
            yield return new WaitForSeconds(1);
            Y_start = -1;
            Spawn = true;
        }else if (Count == 20)
        {
            yield return new WaitForSeconds(1);
            Y_start = 1;
            Spawn = true;
        }else if (Count == 30)
        {
            yield return new WaitForSeconds(1);
            Y_start = 0;
            Spawn = true;
        }else if (Count == 40)
        {
            yield return new WaitForSeconds(40 / Speed);
            Time.timeScale = 0;
            Spawn = false;
            Graph.SetActive(true);
        }else
        {
            Spawn = true;
        }
        
    }
}
