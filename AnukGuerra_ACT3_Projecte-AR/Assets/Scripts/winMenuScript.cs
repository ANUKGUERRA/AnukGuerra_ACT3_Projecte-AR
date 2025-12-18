using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class winMenuScript : MonoBehaviour
{
    [SerializeField] Button MainMenuButton;
    [SerializeField] TMPro.TextMeshProUGUI winnerText;
    [SerializeField] Image image;

    private void Awake()
    {
        if(Game.Winner == team.red)
        {
            winnerText.text = "Red team wins!";
            image.color = Color.red;

        }
        else
        {
            winnerText.text = "Blue team wins!";
            image.color = Color.blue;
        }
    }
    private void OnEnable()
    {
        MainMenuButton.onClick.AddListener(() => SceneManager.LoadScene("Menu"));
    }

    private void OnDisable()
    {
        
        MainMenuButton.onClick.RemoveAllListeners();
    }


}
