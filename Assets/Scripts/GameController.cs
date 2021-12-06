using UnityEngine;

public enum GameType     { Normal, SpeedRun }
public enum ControlType { Normal, WorldTilt }
public enum WallType { Normal, Punishing}

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public GameType gameType;
    public ControlType controlType;
    public WallType wallType;
    SceneController sceneController;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        sceneController = FindObjectOfType<SceneController>();
    }

    public void SetGameType(GameType _gameType)
    {
        gameType = _gameType;
    }

    public void ToggleSpeedRun(bool _speedRun)
    {
        if (_speedRun)
            SetGameType(GameType.SpeedRun);
        else
            SetGameType(GameType.Normal);
    }

    public void ToggleWorldTilt(bool _tilt)
    {
        if (_tilt)
            controlType = ControlType.WorldTilt;
        else
            controlType = ControlType.Normal;
    }

    public void ToggleWallType(bool _punishing)
    {
        if (_punishing)
            wallType = WallType.Punishing;
        else
            wallType = WallType.Normal;
    }

    
}
