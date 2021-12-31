using System;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Links")]
    [SerializeField] private UIController uiController;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private GameObject board;
    [SerializeField] private GameObject prefab;
    
    [Header("Parameters")]
    [SerializeField] [Range(2,20)] private float cameraSpeed;
    [SerializeField] [Range(10,100)] private float cameraZoomSpeed;
    [SerializeField] [Range(1,10)] private float objectsSpeed;

    private IInputController inputController;
    private GameData gameData;
    private List<Transform> objects = new List<Transform>();
    private List<Transform> objectsForRemove = new List<Transform>();


    private void Awake()
    {
        InitGame();
    }

    private void Update()
    {
        GameLoop();
    }

    private void InitGame()
    {
#if UNITY_STANDALONE
        inputController = new StandaloneInputController();
#endif
        
        uiController.OnClickGenerateGame += GenerateGame;
        uiController.OnClickReset += ResetGame;
        
        uiController.ShowSettingsWindow();
    }

    private void GenerateGame(GameData gameData)
    {
        this.gameData = gameData;
        InitGameArea(gameData);
        SpawnObjects(gameData);
    }

    private void InitGameArea(GameData gameData)
    {
        var cameraPosition = new Vector3(gameData.CentralPosition, gameData.CentralPosition, -8f);
        cameraController.Init(cameraPosition, cameraSpeed, cameraZoomSpeed);
        board.transform.localScale = new Vector3(gameData.BoardSize + 2, gameData.BoardSize + 2, 1);
        board.transform.localPosition = Vector2.one * gameData.CentralPosition;
    }
    
    private void GameLoop()
    {
        HandleInput();
        MoveObjects();
        RespawnObjects();
    }

    private void SpawnObjects(GameData gameData)
    {
        for (int i = 0; i < gameData.ObjectCount; i++)
        {
            SpawnObject(GetPosition(Convert.ToInt32(gameData.BoardSize), i));
        }
    }

    private void SpawnObject(Vector3 position)
    {
        var obj = Instantiate(prefab, Vector3.one, Quaternion.identity);
        obj.transform.position = position;
        objects.Add(obj.transform);
    }

    private void MoveObjects()
    {
        foreach (var obj in objects)
        {
            obj.Translate(Vector3.right * Time.deltaTime * objectsSpeed);
            if (obj.localPosition.x > gameData.BoardSize)
            {
                objectsForRemove.Add(obj);
            }
        }
    }

    private void RespawnObjects()
    {
        foreach (var objectForRemove in objectsForRemove)
        {
            objects.Remove(objectForRemove);
            var localPosition = objectForRemove.localPosition;
            SpawnObject(new Vector3(0f, localPosition.y, localPosition.z));
            Destroy(objectForRemove.gameObject);
        }
        
        objectsForRemove.Clear();
    }

    private Vector3 GetPosition(int boardSize, int index)
    {
        int x = Convert.ToInt32(Math.Floor((double) index % boardSize));
        int y = Convert.ToInt32(Math.Floor((double) index / boardSize));
        return new Vector3(x + 0.5f, y, -0.9f);
    }

    private void HandleInput()
    {
        if (inputController == null)
        {
            return;
        }
        
        cameraController.Move(inputController.GetDirection());
    }

    private void ResetGame()
    {
        uiController.HideSettingsWindow();
    }
    
    private void OnDestroy()
    {
        DeinitGame();
    }
    
    private void DeinitGame()
    {
        uiController.OnClickGenerateGame -= GenerateGame;
        uiController.OnClickReset -= ResetGame;
    }
}
