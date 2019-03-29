using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity.InputModule.Utilities.Interactions;
using HoloToolkit.Unity.UX;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

//手势管理类
public class HandManager : MonoBehaviour, IInputHandler, ISourceStateHandler
    {  
        [Flags]
        private enum State
        {
            Start = 0x000,
            Moving = 0x001
        };

        private Transform HostTransform = null;

        private State currentState = State.Start;

        private TwoHandMoveLogic m_moveLogic;

        /// <summary>
        /// Maps input id -> position of hand
        /// </summary>
        private readonly Dictionary<uint, Vector3> m_handsPressedLocationsMap = new Dictionary<uint, Vector3>();

        /// <summary>
        /// Maps input id -> input source. Then obtain position of input source using currentInputSource.TryGetGripPosition(currentInputSourceId, out inputPosition);
        /// </summary>
        private readonly Dictionary<uint, IInputSource> m_handsPressedInputSourceMap = new Dictionary<uint, IInputSource>();

   
        /// <summary>
        /// Private Methods
        /// </summary>
        private void Awake()
        {
            m_moveLogic = new TwoHandMoveLogic();
        }

        private void Start()
        {
            if (HostTransform == null)
            {
                HostTransform = transform;
            }
        }

        private void Update()
        {
            //Update positions of all hands
            foreach (var key in m_handsPressedInputSourceMap.Keys)
            {
                var inputSource = m_handsPressedInputSourceMap[key];
                Vector3 inputPosition = Vector3.zero;
                if (inputSource.TryGetGripPosition(key, out inputPosition))
                {
                    m_handsPressedLocationsMap[key] = inputPosition;
                }
            }

            if (currentState != State.Start)
            {
                UpdateStateMachine();
            }
        }

        private Vector3 GetInputPosition(InputEventData eventData)
        {
            Vector3 result;
            eventData.InputSource.TryGetGripPosition(eventData.SourceId, out result);
            return result;
        }

        private void RemoveSourceIdFromHandMap(uint sourceId)
        {
            if (m_handsPressedLocationsMap.ContainsKey(sourceId))
            {
                m_handsPressedLocationsMap.Remove(sourceId);
            }

            if (m_handsPressedInputSourceMap.ContainsKey(sourceId))
            {
                m_handsPressedInputSourceMap.Remove(sourceId);
            }
        }

        /// <summary>
        /// /// Event Handler receives input from inputSource
        /// </summary>
        public void OnInputDown(InputEventData eventData)
        {
            // Add to hand map
            m_handsPressedLocationsMap[eventData.SourceId] = GetInputPosition(eventData);
            m_handsPressedInputSourceMap[eventData.SourceId] = eventData.InputSource;
            UpdateStateMachine();
            eventData.Use();
        }

        /// <summary>
        /// Event Handler receives input from inputSource
        /// </summary>
        public void OnInputUp(InputEventData eventData)
        {
            RemoveSourceIdFromHandMap(eventData.SourceId);
            UpdateStateMachine();
            eventData.Use();

            //移动棋子
            QiziScript qiziScrip = HostTransform.GetComponent<QiziScript>();
            PointData start = new PointData(qiziScrip.x, qiziScrip.y, qiziScrip.z);
            ChessManager.GetInstant().CheckAndMove(start, HostTransform.position);

        }

        /// <summary>
        /// OnSourceDetected Event Handler
        /// </summary>
        public void OnSourceDetected(SourceStateEventData eventData){}

        /// <summary>
        /// OnSourceLost
        /// </summary>
        public void OnSourceLost(SourceStateEventData eventData)
        {
            RemoveSourceIdFromHandMap(eventData.SourceId);
            UpdateStateMachine();
            eventData.Use();

            //移动棋子
            QiziScript  qiziScrip = HostTransform.GetComponent<QiziScript>();
            PointData start = new PointData(qiziScrip.x, qiziScrip.y, qiziScrip.z);
            ChessManager.GetInstant().CheckAndMove(start,HostTransform.position);
        }

        /// <summary>
        /// private Event Handlers
        /// </summary>
        private void UpdateStateMachine()
        {
            var handsPressedCount = m_handsPressedLocationsMap.Count;
            State newState = currentState;
            if (handsPressedCount == 0)
            {
                newState = State.Start;
            }
            else if (handsPressedCount == 1)
            {
                newState = State.Moving;
            }
            InvokeStateUpdateFunctions(currentState, newState);
            currentState = newState;
        }

        private void InvokeStateUpdateFunctions(State oldState, State newState)
        {
            if (newState != oldState)
            {
                switch (newState)
                {
                    case State.Moving:
                        OnOneHandMoveStarted();
                        break;
                    case State.Start:
                        OnManipulationEnded();
                        break;
                }
                switch (oldState)
                {
                    case State.Start:
                        OnManipulationStarted();
                        break;
                }
            }
            else
            {
                switch (newState)
                {
                    case State.Moving:
                        OnOneHandMoveUpdated();
                        break;
                    default:
                        break;
                }
            }
        }

        private void OnTwoHandManipulationUpdated()
        {
            var targetPosition = HostTransform.position;

            if ((currentState & State.Moving) > 0)
            {
                targetPosition = m_moveLogic.Update(GetHandsCentroid(), targetPosition);
            }

            HostTransform.position = targetPosition;
        }

        private void OnOneHandMoveUpdated()
        {
            var targetPosition = m_moveLogic.Update(m_handsPressedLocationsMap.Values.First(), HostTransform.position);

            HostTransform.position = targetPosition;
        }

        private void OnTwoHandManipulationEnded()
        {
            // This implementation currently does nothing
        }

        private Vector3 GetHandsCentroid()
        {
            Vector3 result = m_handsPressedLocationsMap.Values.Aggregate(Vector3.zero, (current, state) => current + state);
            return result / m_handsPressedLocationsMap.Count;
        }

        private void OnTwoHandManipulationStarted(State newState)
        { 
            m_moveLogic.Setup(GetHandsCentroid(), HostTransform);
        }

        private void OnOneHandMoveStarted()
        {
            Assert.IsTrue(m_handsPressedLocationsMap.Count == 1);

            m_moveLogic.Setup(m_handsPressedLocationsMap.Values.First(), HostTransform);
        }

        private void OnManipulationStarted()
        {
            InputManager.Instance.PushModalInputHandler(gameObject);

        }

        private void OnManipulationEnded()
        {
            InputManager.Instance.PopModalInputHandler();
        }
    }
