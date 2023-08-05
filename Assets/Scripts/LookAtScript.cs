using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TravelPath))]
public class LookAtScript : MonoBehaviour
{
    // Start is called before the first frame update

    public Dictionary<Vector3, Vector3> lookAtPoints = new Dictionary<Vector3, Vector3>();
    public List<bool> boolList= new List<bool>();

    public Dictionary<Vector3, Vector3> PointsToLookAt { get { return lookAtPoints; } set { lookAtPoints = value; } }
}
