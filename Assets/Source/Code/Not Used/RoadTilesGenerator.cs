using System.Collections.Generic;
using UnityEngine;

namespace Source.Code.Not_Used
{
    public class RoadTilesGenerator : MonoBehaviour
    {
   
        [SerializeField] private GameObject[] tilePrefabs;

        [SerializeField] private float tileLength = 30;
        [SerializeField] private int numberOfTiles = 3;
        [SerializeField] private int totalNumOfTiles = 8;

        [SerializeField] private float zSpawn = 0;

        [SerializeField] private Transform playerTransform;

        private int previousIndex;
        private List<GameObject> activeTiles;

        void Start()
        {
            activeTiles = new List<GameObject>();
            for (int i = 0; i < numberOfTiles; i++)
            {
                if(i==0)
                    SpawnTile();
                else
                    SpawnTile(Random.Range(0, totalNumOfTiles));
            }


        }
        void Update()
        {
            if(playerTransform.position.z - 30 >= zSpawn - (numberOfTiles * tileLength))
            {
                int index = Random.Range(0, totalNumOfTiles);
                while(index == previousIndex)
                    index = Random.Range(0, totalNumOfTiles);

                DeleteTile();
                SpawnTile(index);
            }
            
        }

        public void SpawnTile(int index = 0)
        {
            GameObject tile = tilePrefabs[index];
            if (tile.activeInHierarchy)
                tile = tilePrefabs[index + 8];

            if(tile.activeInHierarchy)
                tile = tilePrefabs[index + 16];

            tile.transform.position = Vector3.forward * zSpawn;
            tile.transform.rotation = Quaternion.identity;
            tile.SetActive(true);

            activeTiles.Add(tile);
            zSpawn += tileLength;
            previousIndex = index;
        }

        private void DeleteTile()
        {
            activeTiles[0].SetActive(false);
            activeTiles.RemoveAt(0);
        
        }
    }
}