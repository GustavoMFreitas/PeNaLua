using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacle : MonoBehaviour
{
    [SerializeField] private GameObject obstacle;
    [SerializeField] private GameObject Graph;
    private float Delay;
    private float speed;
    private float y_start = -4;
    private float y_end = 1f;
    private bool spawn = true;
    private void Awake()
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

        List<Transform> childs = new List<Transform>(obstacle.GetComponentsInChildren<Transform>());
        childs.RemoveAll(child => child.name == "default" || child.name == "Obstacles" ||child.name=="collider");
        GameObject randomObject = (GameObject)((Transform)childs[Random.Range(0, childs.Count)]).gameObject;
        Vector3 aux = randomObject.transform.localRotation.eulerAngles;
        y_start += 0.1f;
        y_start = Mathf.Round(y_start * 10f) / 10f;

        if (y_start < y_end)
        {
            GameObject clone = Instantiate(randomObject, new Vector3(0, y_start, 30), Quaternion.Euler(aux.x, Random.Range(0.0f, 360.0f), aux.z));
            Rigidbody rigidbody = clone.GetComponentInChildren<Rigidbody>();
            rigidbody.velocity = new Vector3(0, 0, -speed);
            Destroy(clone, 40 / speed);
            clone.SetActive(true);
            spawn = true;
        }
        else {
            yield return new WaitForSeconds(40 / speed);
            Time.timeScale = 0;
            spawn = false;
            Graph.SetActive(true);
            
        }

    }
}
