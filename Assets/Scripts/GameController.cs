using UnityEngine;

public enum GameType     { Normal, SpeedRun }
public enum ControlType { Normal, WorldTilt }

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public GameType gameType;
    public ControlType controlType;


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
}
