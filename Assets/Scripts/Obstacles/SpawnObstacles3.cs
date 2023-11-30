using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpawnObstacles3 : MonoBehaviour
{
    [SerializeField] private GameObject obstacle;
    [SerializeField] private GameObject Graph;
    private float speed;
    private int countMax;
    private float startAngle = 155.34f;
    private float endAngle = 0;
    private float y_start;
    private bool spawn = true;
    private int count = 0;
    private float Delay;

    void Start()
    {
        speed=PlayerPrefs.GetFloat("Speed");
        countMax= PlayerPrefs.GetInt("Count_max");
        Delay = 2.5f / speed;
    }
    void Update()
    {
        if (spawn)
        {
            StartCoroutine(Spawn_Obstacle());

        }

    }
    private IEnumerator Spawn_Obstacle() //Spawner de obstaculos 
    {
        spawn = false;
        yield return new WaitForSeconds(Delay);//Realiza a cada frame esta função e aplica um delay para aparição dos obstaculos
        float angleRad = startAngle * (Mathf.PI / 180);
        startAngle -= 15;
        y_start = -7 + 7.5f * Mathf.Sin(angleRad);
        y_start = Mathf.Round(y_start * 10f) / 10f;
        List<Transform> childs = new List<Transform>(obstacle.GetComponentsInChildren<Transform>());
        childs.RemoveAll(child => child.name == "default" || child.name == "Obstacles" || child.name == "collider");
        GameObject randomObject = (GameObject)((Transform)childs[Random.Range(0, childs.Count)]).gameObject;
        Vector3 aux = randomObject.transform.localRotation.eulerAngles;

        if (count == countMax)// Se o contador chegou ao maximo a fase finalizou 
        {
            yield return new WaitForSeconds(10 / speed);
            Time.timeScale = 0;
            spawn = false;
            Graph.SetActive(true);
        } 
        else if (startAngle > endAngle) // A altura dos obstaculos simula um semi arco de esfera enquanto não atingir 
        {
            GameObject clone = Instantiate(randomObject, new Vector3(0, y_start, 30), Quaternion.Euler(aux.x, Random.Range(0.0f, 360.0f), aux.z));
            Rigidbody rigidbody = clone.GetComponentInChildren<Rigidbody>();
            rigidbody.velocity = new Vector3(0, 0, -speed);
            Destroy(clone, 40 / speed);
            clone.SetActive(true);
            spawn = true;
        }
        else
        {
            yield return new WaitForSeconds(25/speed);
            count++;
            startAngle = 155.34f;
            spawn = true;
        }
    }
}