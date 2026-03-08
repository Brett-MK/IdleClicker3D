using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class SpawnManager : MonoBehaviour
    {
        [Header("Prefabs")]
        [SerializeField] private GameObject OrcPrefab;
        [SerializeField] private GameObject humanPrefab;

        [Header("Spawn Settings")]
        [SerializeField] private Transform idleParent;
        [SerializeField] private BoxCollider currentClickableStoneCollider;
        [SerializeField] private int humansToOrc = 5;

        private List<GameObject> humansInWorld = new List<GameObject>();

        public static SpawnManager instance;

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        public void SpawnHuman()
        {
            var result = CalculateRandomPositionOnCollider();
            GameObject human = Instantiate(humanPrefab, result.pos, result.rot, idleParent);
            humansInWorld.Add(human);
            if (humansInWorld.Count % humansToOrc == 0)
            {
                for (int i = 0; i < humansToOrc; i++)
                {
                    GameObject humanToDestroy = humansInWorld[0];
                    Destroy(humanToDestroy);
                    humansInWorld.RemoveAt(0);
                }

                SpawnOrc();
            }
        }

        private void SpawnOrc()
        {
            var result = CalculateRandomPositionOnCollider();
            Instantiate(OrcPrefab, result.pos, result.rot, idleParent);
        }

        private (Vector3 pos, Quaternion rot) CalculateRandomPositionOnCollider()
        {
            Vector3 colliderCenter = currentClickableStoneCollider.center;
            Vector3 colliderSize = currentClickableStoneCollider.size;
            Vector3 worldCenter = currentClickableStoneCollider.transform.TransformPoint(colliderCenter);
            Vector3 worldSize = Vector3.Scale(colliderSize, currentClickableStoneCollider.transform.lossyScale);
            int edge = Random.Range(0, 4);

            float x = 0f, z = 0f;
            float rotation = 0f;
            switch (edge)
            {
                case 0: // X min edge
                    x = worldCenter.x - worldSize.x / 2f;
                    z = UnityEngine.Random.Range(worldCenter.z - worldSize.z / 2f, worldCenter.z + worldSize.z / 2f);
                    rotation = 90f;
                    break;
                case 1: // X max edge
                    x = worldCenter.x + worldSize.x / 2f;
                    z = UnityEngine.Random.Range(worldCenter.z - worldSize.z / 2f, worldCenter.z + worldSize.z / 2f);
                    rotation = -90f;
                    break;
                case 2: // Z min edge
                    x = UnityEngine.Random.Range(worldCenter.x - worldSize.x / 2f, worldCenter.x + worldSize.x / 2f);
                    z = worldCenter.z - worldSize.z / 2f;
                    rotation = 0f;
                    break;
                case 3: // Z max edge
                    x = UnityEngine.Random.Range(worldCenter.x - worldSize.x / 2f, worldCenter.x + worldSize.x / 2f);
                    z = worldCenter.z + worldSize.z / 2f;
                    rotation = 180f;
                    break;
            }

            return (new Vector3(x, 0, z), Quaternion.Euler(0, rotation, 0));
        }
    }
}