using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cake : MonoBehaviour, IInputClickHandler

{

    public delegate void OnPostData(GameObject targe);
    public static OnPostData onPostData;

    // Use this for initialization
    void Start () {
    
    }

    // Update is called once per frame
    void Update () {

        string name = this.gameObject.transform.name;
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        Cake.onPostData(this.gameObject);


        ////Detecting if the player clicked on the left mouse button and also if there is no animation playing
        //if (Input.GetButtonDown("Fire1"))
        //{

        //    Destroy(_currentIndicator);
        //    //The 3 following lines is to get the clicked GameObject and getting the RaycastHit2D that will help us know the clicked object
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        //    if (hit.transform != null)
        //    {
        //        if (hit.transform.gameObject.name == _emptyGameobject.name + "(Clone)") { DoEmptyDown(ref _arrayOfShapes); return; }

        //        if (hit.transform.gameObject.name == _MenuButton.name)
        //        {
        //            GetComponent<AudioSource>().PlayOneShot(MenuSound);
        //            hit.transform.localScale = new Vector3(1.1f, 1.1f, 0);

        //            Application.LoadLevel("MainMenu");
        //        }
        //        if (hit.transform.gameObject.name == _ReloadButton.name) { GetComponent<AudioSource>().PlayOneShot(MenuSound); Time.timeScale = 1; isPaused = false; HOTween.Play(); hit.transform.localScale = new Vector3(1.1f, 1.1f, 0); Application.LoadLevel(Application.loadedLevelName); }
        //        if (hit.transform.gameObject.name == _PauseButton.name && !isPaused && !isCountingDown && !isEnded && HOTween.GetTweenersByTarget(_PlayButton.transform, false).Count == 0 && HOTween.GetTweenersByTarget(_MenuButton.transform, false).Count == 0) { GetComponent<AudioSource>().PlayOneShot(MenuSound); StartCoroutine(ShowMenu()); hit.transform.localScale = new Vector3(1.1f, 1.1f, 0); }
        //        else if ((hit.transform.gameObject.name == _PauseButton.name || hit.transform.gameObject.name == _PlayButton.name) && !isEnded && !isCountingDown && isPaused && HOTween.GetTweenersByTarget(_PlayButton.transform, false).Count == 0 && HOTween.GetTweenersByTarget(_MenuButton.transform, false).Count == 0) { GetComponent<AudioSource>().PlayOneShot(MenuSound); StartCoroutine(HideMenu()); hit.transform.localScale = new Vector3(1f, 1f, 0); }

        //        var Infos = HOTween.GetTweenInfos();


        //        bool founGem = false;
        //        bool gemIsTweening = false;
        //        Vector2 gemPosition = new Vector2(-1, -1);
        //        for (var x = 0; x <= _arrayOfShapes.GetUpperBound(0); x++)
        //        {
        //            for (var y = 0; y <= _arrayOfShapes.GetUpperBound(1); y++)
        //            {
        //                if (_arrayOfShapes[x, y].GetInstanceID() == hit.transform.gameObject.GetInstanceID()) { founGem = true; gemPosition = new Vector2(x, y); }
        //                if (HOTween.GetTweenersByTarget(_arrayOfShapes[x, y].transform, false).Count > 0) gemIsTweening = true;
        //            }
        //        }
        //        if (!founGem || isPaused || gemIsTweening) return;
        //        //To know if the user already selected a tile or not yet
        //        if (_FirstObject == null) _FirstObject = hit.transform.gameObject;
        //        else
        //        {
        //            _SecondObject = hit.transform.gameObject;
        //            shouldTransit = true;
        //        }

        //        _currentIndicator = GameObject.Instantiate(_indicator, new Vector3(hit.transform.gameObject.transform.position.x, hit.transform.gameObject.transform.position.y, -1), transform.rotation) as GameObject;


        //        if (hit.transform.gameObject.name == _theRandomizeGem.name + "(Clone)" || (_SecondObject != null && _SecondObject.name == _theRandomizeGem.name + "(Clone)"))
        //        {
        //            _thePlayingEffect = thePlayingEffect.Randomize;
        //            GameObject go = _arrayOfShapes[(int)gemPosition.x, (int)gemPosition.y];

        //            //Replace the matching tile with an empty one
        //            _arrayOfShapes[(int)go.transform.position.x, (int)go.transform.position.y] = GameObject.Instantiate(_emptyGameobject, new Vector3((int)go.transform.position.x, (int)go.transform.position.y, -1), transform.rotation) as GameObject;
        //            Destroy(go);

        //            var destroyingParticle = GameObject.Instantiate(_randomizeGemEffectWhenMatch as GameObject, new Vector3(go.transform.position.x, go.transform.position.y, -2), transform.rotation) as GameObject;
        //            Destroy(destroyingParticle, 3f);
        //            //	StartCoroutine( ShowMessage(_Message, "Randomize"));
        //            StartCoroutine(Randomize());
        //            Destroy(_currentIndicator);
        //            shouldTransit = false;
        //            GetComponent<AudioSource>().PlayOneShot(RandomizeSound);
        //        }
        //        else if (hit.transform.gameObject.name == _theChangeFateGem.name + "(Clone)" || (_SecondObject != null && _SecondObject.name == _theChangeFateGem.name + "(Clone)"))
        //        {
        //            _thePlayingEffect = thePlayingEffect.ChangeFate;
        //            GameObject go = _arrayOfShapes[(int)gemPosition.x, (int)gemPosition.y];

        //            //Replace the matching tile with an empty one
        //            _arrayOfShapes[(int)go.transform.position.x, (int)go.transform.position.y] = GameObject.Instantiate(_emptyGameobject, new Vector3((int)go.transform.position.x, (int)go.transform.position.y, -1), transform.rotation) as GameObject;
        //            Destroy(go);
        //            var destroyingParticle = GameObject.Instantiate(_changeFateGemEffectWhenMatch as GameObject, new Vector3(go.transform.position.x, go.transform.position.y, -2), transform.rotation) as GameObject;
        //            Destroy(destroyingParticle, 10f);
        //            if (!Matches.Contains(go))
        //            {
        //                Matches.Add(go);
        //            }
        //            //	StartCoroutine( ShowMessage(_Message, "Change of fate"));
        //            Destroy(_currentIndicator);
        //            shouldTransit = false;
        //            GetComponent<AudioSource>().PlayOneShot(ChangeFateSound);
        //        }
        //        else if (hit.transform.gameObject.name == _theFlappyBirdGem.name + "(Clone)" || (_SecondObject != null && _SecondObject.name == _theFlappyBirdGem.name + "(Clone)"))
        //        {
        //            _thePlayingEffect = thePlayingEffect.FlappyBird;
        //            GameObject go = _arrayOfShapes[(int)gemPosition.x, (int)gemPosition.y];
        //            //Destroy(go);
        //            //Replace the matching tile with an empty one
        //            _arrayOfShapes[(int)go.transform.position.x, (int)go.transform.position.y] = GameObject.Instantiate(_emptyGameobject, new Vector3((int)go.transform.position.x, (int)go.transform.position.y, -1), transform.rotation) as GameObject;

        //            if (!Matches.Contains(go))
        //            {
        //                Matches.Add(go);
        //            }
        //            //StartCoroutine( ShowMessage(_Message, "Flappy Time"));
        //            CreateAFlappyBird(_theFlappyBirdStartingPosition);
        //            StartCoroutine(ShowMessage(_Message, "Tap Tap Tap..."));
        //            Destroy(_currentIndicator);
        //            shouldTransit = false;
        //            GetComponent<AudioSource>().PlayOneShot(FlappySound);
        //        }
        //        else if (hit.transform.gameObject.name == _theResetimeGem.name + "(Clone)" || (_SecondObject != null && _SecondObject.name == _theResetimeGem.name + "(Clone)"))
        //        {
        //            _thePlayingEffect = thePlayingEffect.TimeReset;
        //            GameObject go = _arrayOfShapes[(int)gemPosition.x, (int)gemPosition.y];

        //            //Replace the matching tile with an empty one
        //            _arrayOfShapes[(int)gemPosition.x, (int)gemPosition.y] = GameObject.Instantiate(_emptyGameobject, new Vector3((int)gemPosition.x, (int)gemPosition.y, -1), transform.rotation) as GameObject;

        //            FreezeTime(go);
        //            Destroy(go);
        //            if (!Matches.Contains(go))
        //            {
        //                Matches.Add(go);
        //            }
        //            //StartCoroutine( ShowMessage(_Message, "Time saver"));
        //            Destroy(_currentIndicator);
        //            shouldTransit = false;
        //            GetComponent<AudioSource>().PlayOneShot(ResetTimeSound);
        //        }
        //        else if (hit.transform.gameObject.name == _theBlackHoleGem.name + "(Clone)" || (_SecondObject != null && _SecondObject.name == _theBlackHoleGem.name + "(Clone)"))
        //        {
        //            _thePlayingEffect = thePlayingEffect.BLackHole;
        //            GameObject go = _arrayOfShapes[(int)gemPosition.x, (int)gemPosition.y];
        //            var destroyingParticle = GameObject.Instantiate(_theBlackHoleGemEffectWhenMatch as GameObject, new Vector3(go.transform.position.x, go.transform.position.y, -2), transform.rotation) as GameObject;
        //            Destroy(destroyingParticle, 1f);
        //            //Replace the matching tile with an empty one
        //            _arrayOfShapes[(int)go.transform.position.x, (int)go.transform.position.y] = GameObject.Instantiate(_emptyGameobject, new Vector3((int)go.transform.position.x, (int)go.transform.position.y, -1), transform.rotation) as GameObject;
        //            Destroy(go);



        //            Destroy(_currentIndicator);
        //            StartCoroutine(ShowBlackHole(go.transform.position));
        //            //shouldTransit= false;
        //            GetComponent<AudioSource>().PlayOneShot(BlackHoleSound);
        //        }
        //        else if (hit.transform.gameObject.name == _theBombGem.name + "(Clone)" || (_SecondObject != null && _SecondObject.name == _theBombGem.name + "(Clone)"))
        //        {
        //            _thePlayingEffect = thePlayingEffect.BombermanBomb;
        //            GameObject go = _arrayOfShapes[(int)gemPosition.x, (int)gemPosition.y];
        //            var destroyingParticle = GameObject.Instantiate(_theBombGemEffectWhenMatch as GameObject, new Vector3(go.transform.position.x, go.transform.position.y, -2), transform.rotation) as GameObject;
        //            Destroy(destroyingParticle, 3f);
        //            //Replace the matching tile with an empty one
        //            _arrayOfShapes[(int)go.transform.position.x, (int)go.transform.position.y] = GameObject.Instantiate(_emptyGameobject, new Vector3((int)go.transform.position.x, (int)go.transform.position.y, -1), transform.rotation) as GameObject;
        //            Destroy(go);


        //            //StartCoroutine( ShowMessage(_Message, "Bomberman"));
        //            Destroy(_currentIndicator);
        //            StartCoroutine(ShowBombermanBomb(go.transform.position));
        //            shouldTransit = false;
        //            GetComponent<AudioSource>().PlayOneShot(BombermanSound);
        //        }
        //        else if (hit.transform.gameObject.name == _theFreezeGem.name + "(Clone)" || (_SecondObject != null && _SecondObject.name == _theFreezeGem.name + "(Clone)"))
        //        {
        //            _thePlayingEffect = thePlayingEffect.Freeze;
        //            GameObject go = _arrayOfShapes[(int)gemPosition.x, (int)gemPosition.y];

        //            //Replace the matching tile with an empty one
        //            _arrayOfShapes[(int)go.transform.position.x, (int)go.transform.position.y] = GameObject.Instantiate(_emptyGameobject, new Vector3((int)go.transform.position.x, (int)go.transform.position.y, -1), transform.rotation) as GameObject;
        //            Destroy(go);

        //            if (!Matches.Contains(go))
        //            {
        //                Matches.Add(go);
        //            }
        //            var destroyingParticle = GameObject.Instantiate(_theFreezeGemEffectWhenMatch as GameObject, new Vector3(go.transform.position.x, go.transform.position.y, -2), transform.rotation) as GameObject;
        //            Destroy(destroyingParticle, 1f);
        //            //StartCoroutine( ShowMessage(_Message, "Freeze time"));
        //            Destroy(_currentIndicator);
        //            StartCoroutine(FreezeGems(go.transform.position));
        //            shouldTransit = false;
        //            GetComponent<AudioSource>().PlayOneShot(FreezeSound);
        //        }
        //        else if (hit.transform.gameObject.name == _theIncreasetimeGem.name + "(Clone)" || (_SecondObject != null && _SecondObject.name == _theIncreasetimeGem.name + "(Clone)"))
        //        {
        //            _thePlayingEffect = thePlayingEffect.TimeIncrease;
        //            GameObject go = _arrayOfShapes[(int)gemPosition.x, (int)gemPosition.y];

        //            //Replace the matching tile with an empty one
        //            _arrayOfShapes[(int)go.transform.position.x, (int)go.transform.position.y] = GameObject.Instantiate(_emptyGameobject, new Vector3((int)go.transform.position.x, (int)go.transform.position.y, -1), transform.rotation) as GameObject;


        //            if (!Matches.Contains(go))
        //            {
        //                Matches.Add(go);
        //            }
        //            IncreaseTime(go);
        //            Destroy(go);
        //            //StartCoroutine( ShowMessage(_Message, "Time waster"));
        //            Destroy(_currentIndicator);
        //            shouldTransit = false;
        //            GetComponent<AudioSource>().PlayOneShot(IncreaseTimeSound);
        //        }
        //        else if (hit.transform.gameObject.name == _theBlindnessGem.name + "(Clone)" || (_SecondObject != null && _SecondObject.name == _theBlindnessGem.name + "(Clone)"))
        //        {
        //            _thePlayingEffect = thePlayingEffect.Blindness;
        //            GameObject go = _arrayOfShapes[(int)gemPosition.x, (int)gemPosition.y];

        //            //Replace the matching tile with an empty one
        //            _arrayOfShapes[(int)go.transform.position.x, (int)go.transform.position.y] = GameObject.Instantiate(_emptyGameobject, new Vector3((int)go.transform.position.x, (int)go.transform.position.y, -1), transform.rotation) as GameObject;
        //            Destroy(go);

        //            if (!Matches.Contains(go))
        //            {
        //                Matches.Add(go);
        //            }
        //            //StartCoroutine( ShowMessage(_Message, "Toxic vision"));
        //            Destroy(_currentIndicator);
        //            StartCoroutine(ShowBlindness(go.transform.position));
        //            shouldTransit = false;
        //            GetComponent<AudioSource>().PlayOneShot(BlindnessSound);
        //        }
        //        //If the user select the second tile we will swap the two tile and animate them
        //        if (shouldTransit)
        //        {
        //            //Getting the position between the 2 tiles
        //            var distance = _FirstObject.transform.position - _SecondObject.transform.position;
        //            //Testing if the 2 tiles are next to each others otherwise we will not swap them 
        //            if (Mathf.Abs(distance.x) <= 1 && Mathf.Abs(distance.y) <= 1)
        //            {   //If we dont want the player to swap diagonally
        //                if (!_canTransitDiagonally)
        //                {
        //                    if (distance.x != 0 && distance.y != 0)
        //                    {
        //                        Destroy(_currentIndicator);
        //                        _FirstObject = null;
        //                        _SecondObject = null;
        //                        return;
        //                    }
        //                }
        //                //Animate the transition
        //                DoSwapMotion(_FirstObject.transform, _SecondObject.transform);
        //                //Swap the object in array
        //                DoSwapTile(_FirstObject, _SecondObject, ref _arrayOfShapes);


        //            }
        //            else
        //            {
        //                _FirstObject = null;
        //                _SecondObject = null;

        //            }
        //            Destroy(_currentIndicator);

        //        }

        //    }

        //}
    }
}
