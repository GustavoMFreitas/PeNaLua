using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGame : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject Menu;
    [SerializeField] private string Nome;


    public void Recomecar()
    {
        SceneManager.LoadSceneAsync(Nome);
        AudioListener.pause = false;
        Time.timeScale = 1;
    }
    public void Voltar()
    {
        SceneManager.LoadSceneAsync("Menu");
        Time.timeScale = 1;
        AudioListener.pause = false;
    }
}
