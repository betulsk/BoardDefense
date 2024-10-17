using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardPiece : MonoBehaviour
{
    public void SetPieceDatas(int rowX, int columnY)
    {
        transform.position = new Vector3(rowX, columnY, transform.position.z);

    }
}
