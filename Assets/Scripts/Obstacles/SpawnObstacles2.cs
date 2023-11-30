using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacles2 : MonoBehaviour
{
    [SerializeField] private GameObject obstacle;
    [SerializeField] private GameObject Graph;
    private float Delay;
    private float speed;
    private float y_start = -4;
    private float y_end = -0.6f;
    private float aux = -0.6f;
    private float altura = 0.2f;
    private bool spawn = true;
    private int count = 0;

    // Start is called before the first frame update
    void Awake()
    {
        Delay = PlayerPrefs.GetFloat("Delay");
        speed = PlayerPrefs.GetFloat("Speed");
    }

    // Update is called once per frame
    void Update()
    {
        if (spawn)
        {
            StartCoroutine(Spawn_Obstacle());
        }
    }
    private IEnumerator Spawn_Obstacle()
    {
        
        spawn = false;
        yield return new WaitForSeconds(Delay);
        GameObject clone;
        List<Transform> childs = new List<Transform>(obstacle.GetComponentsInChildren<Transform>());
        childs.RemoveAll(child => child.name == "default" || child.name == "Obstacles" || child.name == "collider");
        GameObject randomObject = (GameObject)((Transform)childs[Random.Range(0, childs.Count)]).gameObject;
        Vector3 aux1 = randomObject.transform.localRotation.eulerAngles;

        
        if (y_start >= -4 && y_start <= y_end && aux > -4)
        {
            if (count > 5 && aux > -4)
            {
                clone = Instantiate(randomObject, new Vector3(0, aux, 30), Quaternion.Euler(aux1.x, Random.Range(0.0f, 360.0f), aux1.z));

            }
            else
            {
                clone = Instantiate(randomObject, new Vector3(0, y_start, 30), Quaternion.Euler(aux1.x, Random.Range(0.0f, 360.0f), aux1.z));
            }
            Rigidbody rigidbody = clone.GetComponentInChildren<Rigidbody>();
            rigidbody.velocity = new Vector3(0, 0, -speed);
            Destroy(clone, 40 / speed);
            clone.SetActive(true);
            spawn = true;
            if (y_start == y_end)
            {
                if (count > 15)
                {
                    aux -= altura;
                    aux = Mathf.Round(aux * 10f) / 10f;

                }
                count++;
            }
            else
            {
                y_start += altura;
                y_start = Mathf.Round(y_start * 10f) / 10f;
            }

        }
        else
        {
            yield return new WaitForSeconds(40 / speed);
            Time.timeScale = 0;
            spawn = false;
            Graph.SetActive(true);
        }

    }
}