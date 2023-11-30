using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuMannager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject painelMenu;
    [SerializeField] private GameObject painelOpcoes;
    [SerializeField] private GameObject painelFases;
    [SerializeField] private GameObject painelFase6;
    [SerializeField] private GameObject LoadingScreenObj;
    [SerializeField] private Slider SliderLoading;
    [SerializeField] private Text text;

    public void CarregarFases(string Nome)
    {
        SceneManager.LoadSceneAsync(Nome);
    }

    public void SetPlayerpreffsSpeed(string Nome)
    {
        PlayerPrefs.SetFloat("Speed", float.Parse(Nome));
        PlayerPrefs.Save();
    }
    public void SetPlayerpreffsObsNum(string Nome)
    {
        
    }
    public void SetPlayerpreffsNum(string Nome)
    {
        PlayerPrefs.SetInt("Count_max", int.Parse(Nome));
        PlayerPrefs.Save();
    }
    
    public void SetPlayerpreffsDelay(string Nome)
    {
        PlayerPrefs.SetFloat("Delay", float.Parse(Nome));
        PlayerPrefs.Save();
    }

    public void AbrirOpcoes()
    {
        painelMenu.SetActive(false);
        painelOpcoes.SetActive(true);
    }

    public void AbrirFases()
    {
        painelMenu.SetActive(false);
        painelFases.SetActive(true);
    }

    public void AbrirOpcoesFase(GameObject painelFase)
    {
        painelFase.SetActive(true);
        painelFases.SetActive(false);
    }

    public void AbrirOpcoesCalibragem(GameObject painelFase)
    {
        painelOpcoes.SetActive(false);
        painelFase.SetActive(true);
    }

    public void FecharOpcoes()
    {
        painelMenu.SetActive(true);
        painelOpcoes.SetActive(false);
    }

    public void FecharFases()
    {
        painelFases.SetActive(false);
        painelMenu.SetActive(true);
    }

    public void FecharOpcoesFase(GameObject painelFase)
    {
        painelFase.SetActive(false);
        painelFases.SetActive(true);
    }

    public void FecharOpcoesCalibragem(GameObject painelCalibragem)
    {
        painelOpcoes.SetActive(true);
        painelCalibragem.SetActive(false);
    }

    public void FecharOpcoesDrawing(GameObject painelDrawing)
    {
        painelFases.SetActive(false);
        painelFase6.SetActive(true);   
        painelDrawing.SetActive(false);
    }

    public void Sair()
    {
        Debug.Log("saiu");
        Application.Quit();
    }
    public void LoadingScreens(string Nome)
    {
        if (Nome == "Fase 1" || Nome == "Fase 2")
        {
            PlayerPrefs.SetInt("obsNum", 50);
        }
        else if (Nome == "Fase 3")
        {
            PlayerPrefs.SetInt("obsNum", 10 * PlayerPrefs.GetInt("Count_max"));
        }
        else if (Nome == "Fase 4")
        {
            PlayerPrefs.SetInt("obsNum", 40);
        }
        else if (Nome == "Fase 5")
        {
            PlayerPrefs.SetInt("obsNum", 9 * PlayerPrefs.GetInt("Count_max"));
        }
        else if (Nome == "Fase 6")
        {
            PlayerPrefs.SetInt("obsNum", 4 * int.Parse(text.text));
        }
        PlayerPrefs.Save();
        StartCoroutine(LoadScenes(Nome));
    }

    private IEnumerator LoadScenes(string nome)
    {
        PlayerPrefs.SetString("Scene", nome);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("PreGame");
        LoadingScreenObj.SetActive(true);
        while (!asyncOperation.isDone)
        {
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            SliderLoading.value = progress;
            yield return null;
        }
    }
}
