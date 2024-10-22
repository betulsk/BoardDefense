using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DefenseItem : BaseResource, IHealthProvider
{
    private List<BoardPiece> _attackablePieces = new List<BoardPiece>();

    [SerializeField] private DefenseItemHealthController _healthController;
    [SerializeField] private DefenseItemShootBehaviour _defenseItemShootBehaviour;
    [SerializeField] private TMP_Text _defenseItemText;
    [SerializeField] private int _maxHealth = 0;

    public DefenseItemHealthController HealthController => _healthController;
    public List<BoardPiece> AttackablePieces => _attackablePieces;

    public Action<int> OHealthUpdated { get; set; }

    public List<EPieceDirectionType> DirectionTypes;
    public EDefenseItemType DefenseType;
    public int Health;
    public int Damage;
    public int Interval;
    public int Range;
    public bool CanAttack;
    public bool IsDead;

    public override void OnSpawnCustomAction(Transform initTransform)
    {
        base.OnSpawnCustomAction(initTransform);
        transform.position = initTransform.position;
        SetDatas();
    }

    private void SetDatas()
    {
        _defenseItemText.text = DefenseType.ToString();
        var data = GameConfigManager.Instance.GetDefenseItemData(PoolObjectType);
        Health = data.Health;
        Damage = data.Damage;
        Interval = data.Interval;
        CanAttack = true;
        _maxHealth = Health;
        IsDead = false;
    }

    public int GetMaxHealth()
    {
        return _maxHealth;
    }

    public int GetCurHealth()
    {
        return Health;
    }

    public int SetCurHealth(int health)
    {
        if(Health == health)
        {
            return Health;
        }
        Health = health;
        OHealthUpdated?.Invoke(Health);
        return Health;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        SetCurHealth(Health);
        if(Health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Die");
        IsDead = true;
        CurrentBoardPiece.CurrentResource = null;
        ResourcePoolManager.Instance.ReturnPool(PoolObjectType, this);
    }

    public override void SetPiece(BoardPiece piece)
    {
        base.SetPiece(piece);
        _attackablePieces.Clear();
        var boardPieceSpawner = GameManager.Instance.BoardPieceSpawner;
        for(int i = 0; i < DirectionTypes.Count; i++)
        {
            var direction = DirectionTypes[i];
            var foundAttackableTiles = boardPieceSpawner.GetAttackableTiles(CurrentBoardPiece, direction, Range);
            if(foundAttackableTiles != null)
            {
                _attackablePieces.AddRange(foundAttackableTiles);
            }
        }
        _defenseItemShootBehaviour.TryAttack();
    }
}
