using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Colision : MonoBehaviour
{
    [SerializeField] private GameObject painelMenu;
    [SerializeField] private AudioSource motor;
    [SerializeField] private AudioSource gameOver;
    [SerializeField] private Text percentage;
    private Color blinkColor = Color.red; // Define a cor de blink
    private float fadeDuration =.5f;   // Define a duração total do blink (em segundos)
    private Renderer renderizador;
    private Color originalColor= Color.white;
    private Coroutine blinkCoroutine;
    private int obsNum;
    private int aux;

    private void Start()
    {
        obsNum= aux = PlayerPrefs.GetInt("obsNum");
        percentage.text = $"{(aux/aux)*100}%"; 
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            obsNum--;
            if (collision.gameObject.name == "collider")
            {
                Destroy(collision.gameObject.transform.parent.gameObject);
            }
            else { 
            Destroy(collision.gameObject);
            }
            float pontos= ((float)obsNum / aux) * 100;
            pontos= (float)Math.Round(pontos, 1);
            percentage.text = $"{pontos}%";
            gameOver.Play();
            renderizador = GetComponent<Renderer>();

            // Start the blinking process
            if (blinkCoroutine != null)
            {
                StopCoroutine(blinkCoroutine); // // Para o blink anterior, se houver
            }
            blinkCoroutine= StartCoroutine(BlinkCoroutine());
        }

    }
    IEnumerator BlinkCoroutine()
    {
        // Fade in
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration / 2)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / (fadeDuration / 2));
            renderizador.material.color = Color.Lerp(originalColor, blinkColor, t);
            yield return null;
        }

        yield return new WaitForSeconds(0.2f);

        // Fade out
        elapsedTime = 0f;
        while (elapsedTime < fadeDuration / 2)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / (fadeDuration / 2));
            renderizador.material.color = Color.Lerp(blinkColor, originalColor, t);
            yield return null;
        }

        renderizador.material.color = originalColor;
    }
}


