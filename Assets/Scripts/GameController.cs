using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    [SerializeField] GameObject rulesDialog;
    
    public void StartNewGame() {
        SceneManager.LoadScene("MainGame");
    }

    public void GoToMenu() {
        SceneManager.LoadScene("Menu");
    }

    public void OpenRules() {
        rulesDialog.SetActive(true);
    }

    public void CloseRules() {
        rulesDialog.SetActive(false);
    }
}
