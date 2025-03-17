using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationEndLevel : MonoBehaviour
{
    [SerializeField] public LevelManager _levelmanager;
    [SerializeField] public GameObject BatLeft;
    [SerializeField] public GameObject BatRight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnAnimationComplete()
    {
        //нужно показать вин экран если SceneToLoad не равен 0
        // изаче грузить меню
        if (LevelManager.SceneToLoad == 0)
        {
            SceneManager.LoadScene(LevelManager.SceneToLoad); // Загружаем сцену
        }
        else
        {
            // понизить Layer Order
            SpriteRenderer spriteRendererRight = BatRight.GetComponentInChildren<SpriteRenderer>();
            SpriteRenderer spriteRendererLeft = BatLeft.GetComponentInChildren<SpriteRenderer>();
            spriteRendererRight.sortingOrder = 2;
            spriteRendererLeft.sortingOrder = 2;


            _levelmanager.WinScreenActive();
        }

            

        
    }
}
