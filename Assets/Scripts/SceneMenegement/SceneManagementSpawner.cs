using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagementSpawner : MonoBehaviour
{
    [SerializeField] private GameObject sceneManegement;
    [SerializeField] private GameObject itemsCounter;

    private void Awake() {
        int sceneManegementCount;
        sceneManegementCount = GameObject.FindGameObjectsWithTag("SceneManager").Length;

        int itemsCountertCount = GameObject.FindGameObjectsWithTag("ItemCounter").Length;

        if(sceneManegementCount < 1){
            Instantiate(sceneManegement);
        }

        if(itemsCountertCount < 1){
            Instantiate(itemsCounter);
        }
    }
}
