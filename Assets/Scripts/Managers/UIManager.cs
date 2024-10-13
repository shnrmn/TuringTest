using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Health _playerHealth;

    public TMP_Text txtHealth;
    public GameObject textGameOver;

    // Start is called before the first frame update
    void Start()
    {
        textGameOver.SetActive(false);
    }

    void OnEnable()
    {
        // Subscriptions
        _playerHealth.OnHealthUpdated += OnHealthUpdated;
        _playerHealth.OnDeath += OnDeath;
    }

    void OnDestroy()
    {
        _playerHealth.OnHealthUpdated -= OnHealthUpdated;
        _playerHealth.OnDeath -= OnDeath;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnHealthUpdated(float health)
    {
        txtHealth.text = Mathf.Floor(health).ToString();
    }

    private void OnDeath()
    {
        GameManager.Instance.ChangeState(GameState.GameOver, null);
    }
}
