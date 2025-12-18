using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    [SerializeField] Button PlayButton;
    [SerializeField] Button ExitButton;
    private void OnEnable()
    {
        PlayButton.onClick.AddListener(() => SceneManager.LoadScene("Vuforia Scene")); 
        ExitButton.onClick.AddListener(() => Application.Quit()); 
    }

    private void OnDisable()
    {
        
        PlayButton.onClick.RemoveAllListeners();
        ExitButton.onClick.RemoveAllListeners();
    }


}
