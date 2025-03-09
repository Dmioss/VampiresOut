using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;


public class LevelManager : MonoBehaviour
{
    public int maxItems = 0;
    private int foundItems = 0;

    public Camera2D camera_script;

    [SerializeField] public TMP_Text CountLabel;

    [SerializeField] public GameObject UI_canvas;
    [SerializeField] public GameObject welcome_screen;
    [SerializeField] public GameObject win_screen;
    [SerializeField] public GameObject ObjToFind;

    [SerializeField] public Animator BatLeftAnimator;
    [SerializeField] public Animator BatRightAnimator;

    [SerializeField] public GameObject[] NarrativeScenes;
    private int currentNarrative = 0; // Индекс текущего наратива

    public static int SceneToLoad;
    public static bool InGame = false;

    private GameObject currentCircle; // Текущий круг
    public GameObject circlePrefab;

    // Start is called before the first frame update
    void Start()
    {
        UpdateObjectCounter(); 
        InGame = false;
        ShowNarrative(0); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ShowNarrative(int index)
    {
        if ( index == (NarrativeScenes.Length - 1) )
        {
            InGame = true;
        }
        NarrativeScenes[index].SetActive(true);
        
    }
        
    private void HideNarrative(int index)
    {
        NarrativeScenes[index].SetActive(false);        
    }

    public void NextNarrative()
    {
        
        HideNarrative(currentNarrative);        
        currentNarrative++;               
        ShowNarrative(currentNarrative);
    }

    public void AddItemToFind() {
        maxItems++;
        UpdateObjectCounter();
    }

    public void ItemsFound()
    {
        foundItems++;
        UpdateObjectCounter();

        if (foundItems >= maxItems)
        {
            UI_canvas.SetActive(false);
            win_screen.SetActive(true);
            
        }
    }

    private void UpdateObjectCounter()
    {
        CountLabel.text = $"{foundItems} / {maxItems}";
    }

    public void ExitLevel() 
    {
        SceneToLoad = 0;

        BatLeftAnimator.SetTrigger("Close_bat");
        BatRightAnimator.SetTrigger("Close_bat");        
    }

    public void StartGameUI() 
    {
        welcome_screen.SetActive(false);
        UI_canvas.SetActive(true);
        
    }

    public void Continue()
    {
        int CurrentLevel = PlayerPrefs.GetInt("CurrentLevel");
        CurrentLevel++;
        PlayerPrefs.SetInt("CurrentLevel", CurrentLevel);
        PlayerPrefs.Save();
        SceneToLoad = CurrentLevel;

        BatLeftAnimator.SetTrigger("Close_bat");
        BatRightAnimator.SetTrigger("Close_bat");
    }

    public void GetHint()
    {
        int childCount = ObjToFind.transform.childCount;
        Transform HintObject = ObjToFind.transform.GetChild(0);
        camera_script.SetTarget(HintObject);

        // Vector3 circlePosition = new Vector3(HintObject.position.x, HintObject.position.y, 0);

        currentCircle = Instantiate(circlePrefab, HintObject.position, Quaternion.identity);

        currentCircle.transform.SetParent(HintObject.transform);

        currentCircle.transform.localPosition = new Vector3(0, 0, -1f);

        Debug.Log(childCount);
    }



}
