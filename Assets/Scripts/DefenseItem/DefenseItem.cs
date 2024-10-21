using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DefenseItem : BaseResource
{
    [SerializeField] private DefenseItemHealthController _healthController;
    [SerializeField] private TMP_Text _defenseItemText;

    public DefenseItemHealthController HealthController => _healthController;

    public List<EPieceDirectionType> DirectionTypes;
    public EDefenseItemType DefenseType;
    public int Range;
    public float Health;
    public float Damage;
    public float Interval;

    public override void OnSpawnCustomAction(Transform initTransform)
    {
        base.OnSpawnCustomAction(initTransform);
        transform.position = initTransform.position;
        SetDatas();
    }

    private void SetDatas()
    {
        _defenseItemText.text = DefenseType.ToString();
        //var defenseData = GameConfigManager.Instance.GetActiveConfigData().DefenseData[PoolObjectType];
    }
}
