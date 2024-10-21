using System.Collections.Generic;
using UnityEngine;

public class DefenseItem : BaseResource
{
    [SerializeField] private DefenseItemHealthController _healthController;

    public DefenseItemHealthController HealthController => _healthController;

    public List<EPieceDirectionType> DirectionTypes;
    public EDefenseItemType DefenseType;
    public int Range;
    public float Health;
    public float Damage;
    public float Interval;
}
