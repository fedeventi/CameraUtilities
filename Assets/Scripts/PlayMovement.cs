using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMovement : MonoBehaviour
{
    
    public List<GameObject> cameras=new List<GameObject>();
    public bool MultipleCameras;
    public Record Record;
    public bool autoPlay;
    
    // Start is called before the first frame update
    public void Awake()
    {
        Record.Create();
    }
    public void Start()
    {
        Record.Stop();
    }
    
    // Update is called once per frame
    public void Update()
    {
        if(autoPlay)
                Play();
    }
    public void Play()
    {
        if (cameras.Count>0)
        {
            if (MultipleCameras)
            {

                for (int i = 0; i < cameras.Count; i++)
                {
                    if (Record.Index != i)
                    {
                        cameras[i].SetActive(false);
                    }
                    else cameras[i].SetActive(true);
                }
            }
            
            cameras[MultipleCameras?Record.Index:0].transform.position = Record.travelPaths[Record.Index].transform.position;
            cameras[MultipleCameras?Record.Index:0].transform.rotation = Record.travelPaths[Record.Index].transform.rotation;
        }
        
        Record.Play();
    }
    public List<GameObject> allCameras { get { return cameras; } set { cameras = value; } }
    private void OnDrawGizmos()
    {
       
    }
}
