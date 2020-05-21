using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class StartScreenUI : MonoBehaviour
{
    public Button startBtn;
    public Button quitBtn;

    private void Start() {
        startBtn.onClick.AddListener(() => {
            Util.LevelOp.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
        });

        quitBtn.onClick.AddListener(() => {
            Application.Quit();
        });
    }
}
