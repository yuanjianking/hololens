using HoloToolkit.Unity;
using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity.UX;
using UnityEngine;
using UnityEngine.EventSystems;

namespace yuanjian.Block
{ 
    public class MoveBlock : MonoBehaviour, IFocusable, IInputClickHandler, IPointerClickHandler
    {
        [SerializeField]
        [Tooltip("BoundingBox")]
        private BoundingBox boundingBoxPrefab = null;

        /// <summary>
        /// Reference to the Prefab from which clone is instantiated.
        /// </summary>
        public BoundingBox BoundingBoxPrefab
        {
            set
            {
                boundingBoxPrefab = value;
            }

            get
            {
                return boundingBoxPrefab;
            }
        }

        private BoundingBox boundingBoxInstance;

        void Awake()
        {

        }

        void Start () {

        }

        // Update is called once per frame
        void Update () {
          //  gameObject.transform.localRotation *= Quaternion.Euler(0, -30 * Time.deltaTime, 0);
        }

        void OnBecameInvisible()
        {
        
        }

        public void OnFocusEnter()
        {
            ShowBoundingBox = true;
        }

        public void OnFocusExit()
        {
            ShowBoundingBox = false;
        }

        public void OnInputClicked(InputClickedEventData eventData)
        {
           // Vector3 position = gameObject.transform.position - CameraCache.Main.transform.position;
            gameObject.GetComponent<Rigidbody>().AddForce(CameraCache.Main.transform.forward * 1000);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
          //  gameObject.transform.localRotation *= Quaternion.Euler(0, -30 * Time.deltaTime, 0);
        }

        public bool ShowBoundingBox
        {
            set
            {
                if (boundingBoxPrefab != null)
                {
                    if (boundingBoxInstance == null)
                    {
                        // Instantiate Bounding Box from the Prefab
                        boundingBoxInstance = Instantiate(boundingBoxPrefab) as BoundingBox;
                    }

                    if (value)
                    {
                        boundingBoxInstance.Target = this.gameObject;
                        boundingBoxInstance.gameObject.SetActive(true);
                    }
                    else
                    {
                        boundingBoxInstance.Target = null;
                        boundingBoxInstance.gameObject.SetActive(false);
                    }
                }
            }
        }
    }
}