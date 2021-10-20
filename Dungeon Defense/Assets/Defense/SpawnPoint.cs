using System.Collections;
using Defense.Manager;
using UnityEngine;

namespace Defense
{
    public class SpawnPoint : MonoBehaviour
    {
        public float spawnRate = 1f;

        private Coroutine _spawnCoroutine;
    
        // Start is called before the first frame update
        void Start()
        {
            _spawnCoroutine = StartCoroutine(SpawnRoutine());
            EventManager.Instance.On("onGameStart",Spawn);
            EventManager.Instance.On("onGamePaused", StopSpawn);
        }

        void Spawn(object param)
        {
            _spawnCoroutine = StartCoroutine(SpawnRoutine());
        }

        void StopSpawn(object param)
        {
            StopCoroutine(_spawnCoroutine);
        }

        IEnumerator SpawnRoutine()
        {
            while (true)
            {
                var position = transform.position;
                ObjectPoolManager.Instance.Spawn("RapidMonster", position);
                ObjectPoolManager.Instance.Spawn("SlowMonster", position);
                yield return new WaitForSeconds(spawnRate);
            }
        }
    }
}
