using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using TMPro;
using Lean.Touch;

public class PlaceObject : MonoBehaviour
{
    ARPlane arPlane;

    [SerializeField] private ARRaycastManager _raycastManager;
    [SerializeField] private GameObject placeBtn;
    [SerializeField] private GameObject crossHair;
    [SerializeField] private GameObject placedPrefab;
    [SerializeField] private GameObject details;
    [SerializeField] private GameObject hideDetailsBtn;
    [SerializeField] private Camera arCamera;
    [SerializeField] private ARPlaneManager arPlaneManager;
    private List<ARRaycastHit> _hits = new List<ARRaycastHit>();
    private Pose pose;
    private GameObject placedObject;
    private Vector2 touchPos = default;
    public TextMeshPro text;
    private void Awake()
    {
        arPlaneManager = GetComponent<ARPlaneManager>();
        //arPlaneManager.planesChanged += PlaneChanged;

    }

    /*private void PlaneChanged(ARPlanesChangedEventArgs args)
    {
        if(args.added != null && placedObject == null)
        {
            arPlane = args.added[0];
            //placedObject =  Instantiate(placedPrefab, arPlane.transform.position, Quaternion.identity);
            //placedObject.AddComponent<LeanTwistRotate>();
            //placedObject.AddComponent<LeanPinchScale>();
        }
    }*/
    public void placeObj()
    {
        placedObject = Instantiate(placedPrefab, crossHair.transform.position, Quaternion.identity);
        placedObject.AddComponent<LeanPinchScale>();
    }
    private void Update()
    {
        Vector3 origin = arCamera.ViewportToScreenPoint(new Vector3(0.5f, 0.5f, 0));
        Ray rayCrosshair = arCamera.ScreenPointToRay(origin);
        if (_raycastManager.Raycast(rayCrosshair, _hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes))
        {
            pose = _hits[0].pose;
            pose.position = new Vector3(pose.position.x, pose.position.y, pose.position.z);
            crossHair.SetActive(true);
            crossHair.transform.position = pose.position;
            crossHair.transform.eulerAngles = new Vector3(90, 0, 0);
            placeBtn.SetActive(true);
        }
        if (Input.touchCount>0)
        {
            Debug.Log("TOUCH COUNT");
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Moved && placedObject!=null)
            {
                Debug.Log("TOUCH PHASE MOVED");
                Quaternion rotationY = Quaternion.Euler(0f, -touch.deltaPosition.x * .8f,0f);
                placedObject.transform.rotation = rotationY * placedObject.transform.rotation;
                //placedObject.transform.Rotate( touch.deltaPosition.y * 0.1f, -touch.deltaPosition.x * 0.1f, 0f, Space.Self);

            }
        }
    }
    /*void ChangeSelectedObject(PlacementObject selected)
    {
        
    }*/
}
