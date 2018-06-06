using HoloToolkit.Unity;
using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity.UX;
using System.Collections.Generic;
using UnityEngine;

namespace yuanjian.Block
{ 
    public class BuildBlock : MonoBehaviour {

        public List<GameObject> objs;

        private Color[] colors = { Color.blue, Color.yellow, Color.red, Color.green, Color.magenta, Color.grey, Color.black, Color.white };

        // Use this for initialization
        void Start () {
      
        }
	
	    // Update is called once per frame
	    void Update () {
		
	    }

        public void Build()
        {
            if (CameraCache.Main.transform != null)
            {
                Vector3 position = CameraCache.Main.transform.position;
                position.Set(position.x, position.y + 2, position.z);
                int i = Random.Range(0, 3);
                GameObject obj;
                obj = Instantiate(objs[i], position + CameraCache.Main.transform.forward * 4, Quaternion.identity);
                obj.GetComponent<Renderer>().material.color = colors[Random.Range(0, 8)];
            }
        }

        public void Remove()
        {
            IPointingSource pointingSource = null;
            if(FocusManager.Instance.TryGetSinglePointer(out pointingSource))
            {
                GameObject obj = FocusManager.Instance.GetFocusedObject(pointingSource);
                obj.GetComponent<MoveBlock>().ShowBoundingBox = false;
                Destroy(obj);
            }
        }

        public void Clear()
        {
            GameObject[] objs = GameObject.FindGameObjectsWithTag("block");
            foreach(GameObject obj in objs)
            {
                obj.GetComponent<MoveBlock>().ShowBoundingBox = false;
                Destroy(obj);
            }
        }
    }
}