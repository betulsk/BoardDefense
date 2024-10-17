using UnityEngine;

public class Board : MonoBehaviour
{
	[SerializeField] private Transform _piecesVisualTransform;

	public Transform PiecesVisualTransform => _piecesVisualTransform;
}
