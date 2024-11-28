using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private bool isOccupied;

    public Vector3 SelectCell()
    {
        if (isOccupied)
            return Vector3.one;
        isOccupied = true;
        return transform.position;
    }
}
