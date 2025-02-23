using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjecsToFind : MonoBehaviour
{
    private LevelManager _levelManager;

    // Start is called before the first frame update
    void Start()
    {
        _levelManager = FindObjectOfType<LevelManager>();    
        _levelManager.AddItemToFind();

    }

    private void OnMouseDown()
    {
        if (LevelManager.InGame) {
            _levelManager.ItemsFound();
            Destroy(gameObject);
        }
        
        //gameObject.SetActive(false); // Скрываем объект        
    }

}
