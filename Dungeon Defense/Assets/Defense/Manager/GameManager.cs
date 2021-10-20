using UnityEngine;

namespace Defense.Manager
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
    
        public GameState state;
    
        private void Awake()
        {
            Instance = this;
    
        }
    
        // Start is called before the first frame update
        void Start()
        {
            state = GameState.Ready;
            EventManager.Instance.On("onGameStart",OnGameStart);
            EventManager.Instance.On("onGamePaused",OnGamePaused);
            EventManager.Instance.On("onGameEnded",OnGameEnded);
        }
    
        void OnGameStart(object param)
        {
            state = GameState.Playing;
        }

        void OnGamePaused(object param)
        {
            state = GameState.Paused;
        }

        void OnGameEnded(object param)
        {
            state = GameState.Ended;
        }
    
        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
