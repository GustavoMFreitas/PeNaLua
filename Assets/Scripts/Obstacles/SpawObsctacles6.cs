using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawObsctacles6 : MonoBehaviour
{
    [SerializeField] private GameObject Obstacle;
    [SerializeField] private GameObject Graph;
    private float Delay;
    private float Speed;
    private List<Vector2> aux;
    private List<float> Y=new List<float>();
    private List<float> X = new List<float>();
    private bool Spawn = true;
    private Variables var;
    private int counter = 3;
    private float z;
    // Start is called before the first frame update
    private void Awake()
    {
        var = FindAnyObjectByType<Variables>();
        Speed = PlayerPrefs.GetFloat("Speed");
        //Delay = (200 / var.getCountMax()) / Speed;
        aux = var.getPoints();
        for (int i = 0; i < aux.Count; i++)
        {
            X.Add(ExtensionMethods.Remap(aux[i].x, 500, -500, 130, 30));
            Y.Add(ExtensionMethods.Remap(aux[i].y, 300, -300, 2.8f, -2));
        }

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
        if (Y.Count == 0 && counter<=0)// Se o contador chegou ao maximo a fase finalizou 
        {

            yield return new WaitForSeconds(z/Speed) ;
            Time.timeScale = 0;
            Spawn = false;
            Graph.SetActive(true);
        }else if (Y.Count == 0 && counter > 0)
        {
            yield return new WaitForSeconds(z/(Speed));
            for (int i = 0; i < aux.Count; i++)
            {
                X.Add(ExtensionMethods.Remap(aux[i].x, 500, -500, 130, 30));
                Y.Add(ExtensionMethods.Remap(aux[i].y, 300, -300, 2.8f, -2));
            }
            counter --;
            Spawn = true;
        }
        else
        {
            GameObject clone = Instantiate(Obstacle, new Vector3(0, Y[0], X[0]), Quaternion.Euler(0, 0, 0));
            Rigidbody rigidbody = clone.GetComponentInChildren<Rigidbody>();
            rigidbody.velocity = new Vector3(0, 0, -Speed);
            z=rigidbody.transform.position.z;
            z = Mathf.RoundToInt(z) + 8;
            Destroy(clone, z / Speed);
            
            clone.SetActive(true);
            Y.RemoveAt(0);
            X.RemoveAt(0);
            Spawn = true;
        }

    }
}
