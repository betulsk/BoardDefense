using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig",
   menuName = "GameConfig/Create a GameConfig",
   order = 1)]
public class GameConfig : ScriptableObject
{
    [Header("BOARD")]
    [SerializeField] private int _boardWidth;
    public int BoardWidth => _boardWidth;
    
    [SerializeField] private int _boardHeight;
    public int BoardHeight => _boardHeight;
    
}
