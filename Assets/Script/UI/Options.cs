using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Options : MonoBehaviour
{
    public Slider slider;

    public TextMeshProUGUI text;

    public PlayerLook playerLook;

    public string Menu;

    public Canvas canvas;

    void Start()
    {
         slider.onValueChanged.AddListener((value) =>
         {
            playerLook.xSensitivity = value;
            playerLook.ySensitivity = value;
            text.text = value.ToString();
         });        
    }

    public void Button_Options()
    {
        canvas.transform.Find("PanelOptions").gameObject.SetActive(true);
        canvas.transform.Find(Menu).gameObject.SetActive(false);
    }

    public void ButtonRetour()
    {
        canvas.transform.Find("PanelOptions").gameObject.SetActive(false);
        canvas.transform.Find(Menu).gameObject.SetActive(true);
    }

    public void ButtonEnterHTP()
    {
        canvas.transform.Find("PanelHTP").gameObject.SetActive(true);
        canvas.transform.Find(Menu).gameObject.SetActive(false);
    }

    public void ExitHTPMenu()
    {
        canvas.transform.Find("PanelHTP").gameObject.SetActive(false);
        canvas.transform.Find(Menu).gameObject.SetActive(true);
    }



}
