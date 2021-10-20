using Defense.Manager;
using UnityEngine;

namespace Defense.Ui
{
    public class PauseMenu : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            EventManager.Instance.On("onGameStart",OnGameStarted);
            EventManager.Instance.On("onGamePaused",OnGamePaused);
        
        }

        void OnGameStarted(object param)
        {
            gameObject.SetActive(false);
        }

        void OnGamePaused(object param)
        {
            gameObject.SetActive(true);
        }
    }
}
