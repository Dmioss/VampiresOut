using UnityEngine;

public class Camera2D : MonoBehaviour
{
    public SpriteRenderer backgroundSprite;

    private Vector3 _dragOrigin;
    private Camera _camera;

    public float _panMinX, _panMinY;
    public float _panMaxX, _panMaxY;

    public float zoomMax = 10f;
    public float zoomMin = 2.5f;


    public Transform target; // Целевая позиция (можно использовать Transform или Vector3)
    public float smoothTime = 0.5f; // Время плавного перемещения
    private Vector3 velocity = Vector3.zero; // Внутренняя переменная для SmoothDamp
    public float arrivalThreshold = 0.1f; // Порог достижения цели

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelManager.InGame) {
            ZoomCamera();
            PanCamera();
        }

        if (target != null)
        {
            // фиксируем Z
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, _camera.transform.position.z);

            // Плавное перемещение камеры к целевой позиции
            _camera.transform.position = Vector3.SmoothDamp(_camera.transform.position, targetPosition, ref velocity, smoothTime);
            
            if (Vector3.Distance(_camera.transform.position, targetPosition) < arrivalThreshold)
            {
                // Обнуляем target, так как цель достигнута
                target = null;
                Debug.Log("Цель достигнута, target обнулен.");
            }
        }

    }

    private void Awake()
    {     
        _camera = Camera.main;
        zoomMax = backgroundSprite.bounds.size.y / 2f;
    }

    public void SetTarget(Transform newTarget)
    {        
        target = newTarget;

    }

    private void ZoomCamera()    
    {        
        Zoom(Input.GetAxis("Mouse ScrollWheel"));        
    }

    private void PanCamera()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            _dragOrigin = _camera.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            var dragDifference = _dragOrigin - _camera.ScreenToWorldPoint(Input.mousePosition);           
            _camera.transform.position = ClampCamera(_camera.transform.position + dragDifference);
        }
    }

    private void Zoom(float increment)
    {
        //  _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize - increment*4, zoomMin, zoomMax);
                
        if (increment != 0.0f)
        {
            float newSize = _camera.orthographicSize - increment * 4; 
            newSize = Mathf.Clamp(newSize, zoomMin, zoomMax);

            float imageWidth = backgroundSprite.bounds.size.x;
            float imageHeight = backgroundSprite.bounds.size.y;

            float cameraHeight = newSize * 2;
            float cameraWidth = cameraHeight * _camera.aspect;

            if (cameraWidth <= imageWidth && cameraHeight <= imageHeight)
            {
                _camera.orthographicSize = newSize;
            }
        }

        _camera.transform.position = ClampCamera(_camera.transform.position);

    }

    private Vector3 ClampCamera(Vector3 targetPosition)
    {
        var orthographicSize = _camera.orthographicSize;
        var camWidth = orthographicSize * _camera.aspect;

        var position = backgroundSprite.transform.position;
        var bounds = backgroundSprite.bounds;
        _panMinX = position.x - bounds.size.x / 2f;
        _panMinY = position.y - bounds.size.y / 2f;
        _panMaxX = position.x + bounds.size.x / 2f;
        _panMaxY = position.y + bounds.size.y / 2f;

        var minX = _panMinX + camWidth;
        var minY = _panMinY + orthographicSize;
        var maxX = _panMaxX - camWidth;
        var maxY = _panMaxY - orthographicSize;

        var clampX = Mathf.Clamp(targetPosition.x, minX, maxX);
        var clampY = Mathf.Clamp(targetPosition.y, minY, maxY);
        return new Vector3(clampX, clampY, targetPosition.z);
        
    }
}
