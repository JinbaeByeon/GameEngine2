using Defense.Manager;
using UnityEngine;

namespace Defense.Ui
{
    public class RestartMenu : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            gameObject.SetActive(false);
            EventManager.Instance.On("onGameReStart",OnGameReStart);
            EventManager.Instance.On("onGameEnd",OnGameEnd);
        }

        void OnGameReStart(object param)
        {
            gameObject.SetActive(false);
        }

        void OnGameEnd(object param)
        {
            gameObject.SetActive(true);
        }
    }
}
