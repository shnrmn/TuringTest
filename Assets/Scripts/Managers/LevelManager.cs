using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private bool isFinalLevel;

    public UnityEvent OnLevelStart;
    public UnityEvent OnLevelEnd;

    public void StartLevel()
    {
        OnLevelStart?.Invoke();
    }

    public void EndLevel()
    {
        OnLevelEnd?.Invoke();

        // Inform the game manager that the level is over. Game win if it's the final level.
        if (isFinalLevel)
            GameManager.Instance.ChangeState(GameState.GameEnd, this);
        else
            GameManager.Instance.ChangeState(GameState.LevelEnd, this);
    }
}
