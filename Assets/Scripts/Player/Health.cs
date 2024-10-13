using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    private float _health;

    [SerializeField] private int maxHealth;

    public Action<float> OnHealthUpdated;
    public Action OnDeath;

    public bool IsDead { get; private set; }

    private void Start()
    {
        _health = maxHealth;
        OnHealthUpdated?.Invoke(_health);
    }

    public void DeductHealth(float value)
    {
        if (IsDead) return;

        _health -= value;

        if (_health <= 0)
        {
            IsDead = true;
            OnDeath?.Invoke();
            _health = 0;
        }

        OnHealthUpdated?.Invoke(_health);
    }
}
