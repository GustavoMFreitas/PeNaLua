using UnityEngine;
using UnityEngine.UI;

public class Warning : MonoBehaviour
{
    [SerializeField] private string Fase;
    [SerializeField] private Button StartButton;
    [SerializeField] private Text WarningMsg;
    [SerializeField] private MenuMannager Menu;
    [SerializeField] private GameObject Drawing;
    // Start is called before the first frame update
    void Start()
    {
        StartButton.onClick.AddListener(ButtonClick);
    }
    private void OnDisable()
    {
        WarningMsg.text = "";
    }
    // This method is called when the button is clicked
    public void ButtonClick()
    {
        if(Fase=="Fase 1"||Fase == "Fase 2" || Fase == "Fase 4")
        {
            if (PlayerPrefs.GetFloat("Delay") <= 0 || PlayerPrefs.GetFloat("Speed") <= 0)
            {
                WarningMsg.color = Color.red;
                WarningMsg.text = "Valores Inválidos";
                WarningMsg.fontSize = 30;
            }
            else {
                WarningMsg.text = "";
                Menu.LoadingScreens(Fase);
            }

        }
        else if(Fase == "Fase 3" || Fase == "Fase 5" || Fase == "Fase 6")
        {
            if (PlayerPrefs.GetInt("Count_max") <= 0 || PlayerPrefs.GetFloat("Speed") <=0)
            {
                    WarningMsg.color = Color.red;
                    WarningMsg.text = "Valores Inválidos";
                    WarningMsg.fontSize = 30;
            }
            else
            {
                if (Fase == "Fase 6")
                {
                    WarningMsg.text = "";
                    Menu.AbrirOpcoesFase(Drawing);
                }
                else{
                    WarningMsg.text = "";
                    Menu.LoadingScreens(Fase);
                }
            }
        }
    }
}