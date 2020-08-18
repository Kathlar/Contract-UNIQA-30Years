using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPoints : MonoBehaviour
{
    public int numberOfPoints = 0;

    public void CollectPoints(int points)
    {
        numberOfPoints += points;
        GameManager.UpdatePoints(numberOfPoints);
    }
}
