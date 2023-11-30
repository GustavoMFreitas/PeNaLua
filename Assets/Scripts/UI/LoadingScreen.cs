using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{

    [SerializeField] private GameObject LoadingScreenObj;
    public void LoadingScreens(string nome)
    {
        StartCoroutine(LoadScenes(nome));
    }
    private IEnumerator LoadScenes(string nome)
    {

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(nome);

        LoadingScreenObj.SetActive(true);
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }
}


