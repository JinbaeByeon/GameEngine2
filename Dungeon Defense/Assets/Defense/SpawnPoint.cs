using System.Collections;
using Defense.Manager;
using UnityEngine;

namespace Defense
{
    public class SpawnPoint : MonoBehaviour
    {

        private Coroutine _spawnRapid;
        private Coroutine _spawnSlow;
    
        // Start is called before the first frame update
        void Start()
        {
            _spawnRapid = StartCoroutine(SpawnRapid());
            _spawnSlow = StartCoroutine(SpawnSlow());
            EventManager.Instance.On("onGameStart",Spawn);
            EventManager.Instance.On("onGamePaused", StopSpawn);
        }

        void Spawn(object param)
        {
            _spawnRapid = StartCoroutine(SpawnRapid());
            _spawnSlow = StartCoroutine(SpawnSlow());
        }

        void StopSpawn(object param)
        {
            StopCoroutine(_spawnRapid);
        }

        IEnumerator SpawnRapid()
        {
            float spawnRate = 1.5f;
            while (MonsterCount.MonsterCnt < 20)
            {
                var position = transform.position;
                ObjectPoolManager.Instance.Spawn("RapidMonster", position);
                MonsterCount.MonsterCnt++;
                yield return new WaitForSeconds(spawnRate);
            }
        }
        IEnumerator SpawnSlow()
        {
            float spawnRate = 2.0f;
            while (MonsterCount.MonsterCnt < 20)
            {
                var position = transform.position;
                ObjectPoolManager.Instance.Spawn("SlowMonster", position);
                MonsterCount.MonsterCnt++;
                yield return new WaitForSeconds(spawnRate);
            }
        }
    }
}
