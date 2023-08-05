using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TravelPath : MonoBehaviour
{
    // Start is called before the first frame update
    public string Name;
    List<Vector3> list=new List<Vector3>();
    public MyPath path;
    Dictionary<Vector3, Vector3> lookingPoints = new Dictionary<Vector3, Vector3>();
    //CustomDic<Vector3,Vector3> lookingPoints=new CustomDic<Vector3, Vector3>();
    public bool play ;
    public bool finished;
    float _speed=1;
    float _threshold = .5f;
    bool _loop;
    int index = 0;
    int amount;
    float sliderMax=10;
    float divide;
    float[] limits;
    bool _followPath=true;
    public float speedRotation;
    public TravelPath Initialize(Traveller travelPath)
    {
        list=travelPath.list; 
        path=travelPath.path;
        //lookingPoints=travelPath.lookingPoints;
        for (int i = 0; i < travelPath.lookingPoints.Count; i++)
        {
            lookingPoints.Add(travelPath.indexers[i],travelPath.lookingPoints[i]);
            
        }
        _speed = travelPath.speed;
        speedRotation = travelPath.speedRotation;
        _loop = false;
        play = false;
        finished = false;
        _followPath = travelPath.followPath;

        return this;
    }
    
    void Start()
    {
        
        SetList();
    }

    // Update is called once per frame
    void Update()
    {

        if (play)
            Travel();
    }
    private void OnDrawGizmos()
    {
        if (play)
            Travel();
        
        Gizmos.color = Color.yellow;
        
        
        
       

        
    }
    public void Travel()
    {
        if (list.Count <= 0) return;

        float d = Vector3.Distance(transform.position, list[index]);

        if (d > _threshold)
        {

            transform.position += (list[index]-transform.position).normalized * _speed*Time.deltaTime;

            Vector3 dir = FollowPath ? FollowPathRotation() : CustomRotation();
            
            transform.forward = Vector3.Slerp(transform.forward, dir, Time.deltaTime * speedRotation);

        }
        else
        {
            if (index + 1 == list.Count)
            {
                if (_loop)
                    index = 0;
                else
                    finished = true;
            }
            else if (index + 1 != list.Count)
            {
                index++;
                finished = false;
            }
        }
        
    }
    
    Vector3 FollowPathRotation()
    {
        return (points[index] - transform.position).normalized;
    }
    Vector3 CustomRotation()
    {

        if (PointsToLookAt.ContainsKey(points[index]))
            return (PointsToLookAt[points[index]] - transform.position).normalized;

        else
        {
            int auxIndex = index;
            foreach (var item in PointsToLookAt)
            {
                if (PointsToLookAt.ContainsKey(points[auxIndex]))
                {
                    return (PointsToLookAt[points[auxIndex]] - transform.position).normalized;

                }
                else
                {
                    if(auxIndex+1<points.Count)
                        auxIndex++;
                }
            }
            
            return transform.forward;
            
        }
        
    }
    public void ResetPosition()
    {
        if(list.Count <= 0) return;
        transform.position = list[0];
        if (!FollowPath && lookingPoints.ContainsKey(list[0]))
            transform.rotation = Quaternion.LookRotation(lookingPoints[list[0]] - transform.position);
        else
            transform.rotation = Quaternion.LookRotation(list[1] - list[0]);
        finished = false;
        
    }
    public void SetList()
    {
        if (path == null) return;
        list = path.points;
        index = 0;
    }

    public void Normalize()
    {
        if (list.Count <= 0) return;

        amount = list.Count;
        divide = amount/sliderMax;
        limits= new float[amount];
        for (int i = 0; i < limits.Length; i++)
        {
            limits[i] = i * divide;
        }
        

        
    }
    

    float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
    public float Speed { get { return _speed; } set { _speed = value; } }
    public float Threshold { get { return _threshold; } set { _threshold = value; } }
    public bool Loop { get { return _loop; } set { _loop = value; } }
    public List<Vector3> points => path.points;
    public int CurrentWaypoint => index;
    public bool FollowPath { get { return _followPath; } set { _followPath = value; } }
    public Dictionary<Vector3, Vector3> PointsToLookAt { get { return lookingPoints; } set { lookingPoints = value; } }
}

