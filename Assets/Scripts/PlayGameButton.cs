using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayGameButton : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(Load);
    }
    public void Load()
    {
        GameManager.LoadScene(Constants.GamePlaySceneName);
    }
}
