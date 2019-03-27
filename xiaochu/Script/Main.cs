using UnityEngine;
using System.Collections;
using Holoville.HOTween;
using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity;


/// <summary>
///  This class is the main entry point of the game it should be attached to a gameobject and be instanciate in the scene
/// Author : Pondomaniac Games
/// </summary>
public class Main : MonoBehaviour, IInputClickHandler
{

    #region public member
    //public GameObject _indicator;//The indicator to know the selected tile
    public GameObject[,]  _arrayOfShapes;//The main array that contain all games tiles
    public GameObject [] _listOfGems;//The list of tiles we want to see in the game you can remplace them in unity's inspector and choose all what you want

    public GameObject _emptyGameobject;//After destroying object they are replaced with this one so we will replace them after with new ones
    public GameObject _particleEffect;//The object we want to use in the effect of shining stars 
    public GameObject _particleEffectWhenMatch;//The gameobject of the effect when the objects are matching
    public int _scoreIncrement;//The amount of point to increment each time we find matching tiles

    public AudioClip MatchSound;//the sound effect when matched tiles are found

    public int _gridWidth;//the grid number of cell horizontally
	public int _gridHeight;//the grid number of cell vertically
	
	public float _timerCoef  ;



	public GameObject _Time;//The timer
    
    public GameObject _Level;//The timer
    public GameObject _LevelTextValue;//The timer


    #endregion

    #region private member 
    private GameObject _FirstObject;//The first object selected
    private GameObject _SecondObject;//The second object selected
  
    private ArrayList _currentParticleEffets = new ArrayList();//the array that will contain all the matching particle that we will destroy after

     private bool isPaused = false;
    private bool isEnded = false;
  
    private float timing = 0;
  
    private float maxProgress = 0;
    private float ScoreByLevel = 0;
    private float progress = 0;
    private int level = 0;
    private int _scoreTotal = 0;//The score 
  
    #endregion

    // Use this for initialization
    void Start () {
		//UpdateLevel (0);
		progress = (float)(timing * _timerCoef);
		_Time.transform.localScale = new Vector3 (Mathf.Clamp01 (progress), _Time.transform.localScale.y, 0);


		_arrayOfShapes = new GameObject[_gridWidth, _gridHeight];
        Vector3 position = gameObject.transform.position;
        Vector3 size = gameObject.GetComponent<BoxCollider>().bounds.size;

        for ( int i = 0; i <= _gridWidth-1; i++){
			for ( int j = 0; j <= _gridHeight-1; j++){

				var gameObject = GameObject.Instantiate(_emptyGameobject,transform) as GameObject;
                gameObject.transform.localPosition = new Vector3((position.x - size.x / 2 + 0.035f) + i * 0.041f, (position.y  - size.y / 2 + 0.045f) + j * 0.0452f, position.z - 0.02f);
                _arrayOfShapes[i,j]= gameObject;
             }
		}
		InvokeRepeating("DoShapeEffect", 1f, 0.21F);
		DoEmptyDown (ref _arrayOfShapes);
        Cake.onPostData += Selectarget;
    }

    // Update is called once per frame
    void Update()
    {
        ArrayList Matches = new ArrayList();
        if (isPaused) return;
        var Infos2 = HOTween.GetTweenInfos();
        bool gemIsTweening2 = false;
        if (Infos2 != null)
        {

            for (var x = 0; x <= _arrayOfShapes.GetUpperBound(0); x++)
            {
                for (var y = 0; y <= _arrayOfShapes.GetUpperBound(1); y++)
                {
                    if (HOTween.GetTweenersByTarget(_arrayOfShapes[x, y].transform, false).Count > 0)
                        gemIsTweening2 = true;
                }
            }
        }

        //If no animation is playing
        if (!gemIsTweening2)
        {
            Matches.AddRange(FindMatch(_arrayOfShapes));
            //If we find a matched tiles
            if (Matches.Count > 0)
            {//timing-=0.9f;
                if (timing < 0) timing = 0;
           
                //Update the score
                _scoreTotal += Matches.Count * _scoreIncrement;

                foreach (GameObject go in Matches)
                {
                    Debug.Log(go.tag);
                
                    //Playing the matching sound
                    GetComponent<AudioSource>().PlayOneShot(MatchSound);
                    //Creating and destroying the effect of matching
                    var destroyingParticle = GameObject.Instantiate(_particleEffectWhenMatch as GameObject, new Vector3(go.transform.position.x, go.transform.position.y, -2), transform.rotation) as GameObject;
                    Destroy(destroyingParticle, 1f);
                
                    //Replace the matching tile with an empty one
                     foreach (Tweener t in HOTween.GetTweenersByTarget(go.transform, true)) t.Kill();
                    //Destroy the ancient matching tiles
                    Destroy(go);
                }
                _FirstObject = null;
                _SecondObject = null;
                //Moving the tiles down to replace the empty ones
                DoEmptyDown(ref _arrayOfShapes);

            }
            //If no matching tiles are found remake the tiles at their places
            else if (_FirstObject != null
                     && _SecondObject != null
                     )
            {
                //Animate the tiles
                DoSwapMotion(_FirstObject.transform, _SecondObject.transform);
                //Swap the tiles in the array
                DoSwapTile(_FirstObject, _SecondObject, ref _arrayOfShapes);
                _FirstObject = null;
                _SecondObject = null;

            }
        }
        if (!isPaused)
        {
            timing += 0.001f;
            progress = (float)(timing * _timerCoef);
            _Time.transform.localScale = new Vector3(Mathf.Clamp01(progress), _Time.transform.localScale.y, 0);


        }
        if (Mathf.Clamp01(progress) >= 1)
        {
            isEnded = true;
            isPaused = true;
        }
        (GetComponent(typeof(TextMesh)) as TextMesh).text = _scoreTotal.ToString(); 

        UpdateLevel(Matches.Count * _scoreIncrement);

    }

    void UpdateLevel(int score)
    {
        ScoreByLevel += score;
        maxProgress = (float)Mathf.Floor(250 * (level + 1));
        _Level.transform.localScale = new Vector3((float)(ScoreByLevel / maxProgress), _Level.transform.localScale.y, 0);

        if (Mathf.Clamp01(_Level.transform.localScale.x) >= 1)
        {
            // update level and increase difficulty
            level += 1;
            ScoreByLevel = 0;
            timing = 0;
           
            TweenParms parms = new TweenParms().Prop("localScale", new Vector3(0, _Level.transform.localScale.y, -6)).Ease(EaseType.EaseOutQuart);
            HOTween.To(_Level.transform, 0.5f, parms).WaitForCompletion();
            parms = new TweenParms().Prop("localScale", new Vector3(0, _Time.transform.localScale.y, -6)).Ease(EaseType.EaseOutQuart);
            HOTween.To(_Time.transform, 0.5f, parms).WaitForCompletion();

            (_LevelTextValue.GetComponent(typeof(TextMesh)) as TextMesh).text = level.ToString();

            var destroyingParticle = GameObject.Instantiate(_LevelTextValue as GameObject, new Vector3(_LevelTextValue.transform.position.x, _LevelTextValue.transform.position.y, _LevelTextValue.transform.position.z - 1), transform.rotation) as GameObject;
            Color oldColor = destroyingParticle.GetComponent<Renderer>().material.color;
            parms = new TweenParms().Prop("color", new Color(oldColor.r, oldColor.b, oldColor.g, 0f)).Ease(EaseType.EaseOutQuart);
            HOTween.To((destroyingParticle.GetComponent(typeof(TextMesh)) as TextMesh), 4f, parms);
            parms = new TweenParms().Prop("fontSize", 150).Ease(EaseType.EaseOutQuart);
            HOTween.To((destroyingParticle.GetComponent(typeof(TextMesh)) as TextMesh), 2f, parms);
            Destroy(destroyingParticle, 5);

        }
        else
        {
            (_LevelTextValue.GetComponent(typeof(TextMesh)) as TextMesh).text = level.ToString();
        }

    }
    // Find Match-3 Tile
    private ArrayList FindMatch(GameObject[,] cells)
    {//creating an arraylist to store the matching tiles
        ArrayList stack = new ArrayList();
        Vector3 position = gameObject.transform.position;
        Vector3 size = gameObject.GetComponent<BoxCollider>().bounds.size;
        //Checking the vertical tiles
        for (var x = 0; x <= cells.GetUpperBound(0); x++)
        {
            for (var y = 0; y <= cells.GetUpperBound(1); y++)
            {
                var thiscell = cells[x, y];
                //If it's an empty tile continue
                if (thiscell.name == "Empty(Clone)") continue;

              
                int matchCount = 0;
                int y2 = cells.GetUpperBound(1);
                int y1;
                //Getting the number of tiles of the same kind
                for (y1 = y + 1; y1 <= y2; y1++)
                {
                    if (cells[x, y1].name == "Empty(Clone)" || thiscell.name != cells[x, y1].name )break;
                    matchCount++;
                }
                //If we found more than 2 tiles close we add them in the array of matching tiles
                if (matchCount >= 2)
                {
                    y1 = Mathf.Min(cells.GetUpperBound(1), y1 - 1);
                    for (var y3 = y; y3 <= y1; y3++)
                    {
                        if (!stack.Contains(cells[x, y3]))
                        {
                            stack.Add(cells[x, y3]);
                            _arrayOfShapes[x, y3] = GameObject.Instantiate(_emptyGameobject) as GameObject;
                            _arrayOfShapes[x, y3].transform.localPosition = new Vector3((position.x - size.x / 2 + 0.035f) + x * 0.041f, (position.y - size.y / 2 + 0.045f) + y3 * 0.0452f, position.z - 0.02f);
                        }
                    }
                }
            }
        }
        //Checking the horizontal tiles , in the following loops we will use the same concept as the previous ones
        for (var y = 0; y < cells.GetUpperBound(1) + 1; y++)
        {
            for (var x = 0; x < cells.GetUpperBound(0) + 1; x++)
            {
                var thiscell = cells[x, y];
                if (thiscell.name == "Empty(Clone)") continue;


                int matchCount = 0;
                int x2 = cells.GetUpperBound(0);
                int x1;
                for (x1 = x + 1; x1 <= x2; x1++)
                {
                    if (cells[x1, y].name == "Empty(Clone)" || thiscell.name != cells[x1, y].name ) break;
                    matchCount++;
                }
                if (matchCount >= 2)
                {
                    x1 = Mathf.Min(cells.GetUpperBound(0), x1 - 1);
                    for (var x3 = x; x3 <= x1; x3++)
                    {
                        if (!stack.Contains(cells[x3, y]))
                        {
                            stack.Add(cells[x3, y]);
                            _arrayOfShapes[x3, y] = GameObject.Instantiate(_emptyGameobject) as GameObject;
                            _arrayOfShapes[x3, y].transform.localPosition = new Vector3((position.x - size.x / 2 + 0.035f) + x3 * 0.041f, (position.y - size.y / 2 + 0.045f) + y * 0.0452f, position.z - 0.02f);
                        }
                    }
                }
            }
        }
        return stack;
    }

    // Swap Motion Animation, to animate the switching arrays
    void DoSwapMotion(Transform a, Transform b)
    {
        Vector3 posA = a.localPosition;
        Vector3 posB = b.localPosition;
        TweenParms parms = new TweenParms().Prop("localPosition", posB).Ease(EaseType.EaseOutQuart);
        HOTween.To(a, 0.2f, parms).WaitForCompletion();
        parms = new TweenParms().Prop("localPosition", posA).Ease(EaseType.EaseOutQuart);
        HOTween.To(b, 0.2f, parms).WaitForCompletion();

    }


    // Swap Two Tile, it swaps the position of two objects in the grid array
    void DoSwapTile(GameObject a, GameObject b, ref GameObject[,] cells)
    {
        Vector3 position = gameObject.transform.position;
        Vector3 size = gameObject.GetComponent<BoxCollider>().bounds.size;
        float ax = (a.transform.position.x - (position.x - size.x / 2 + 0.035f)) / 0.041f;
        float ay = (a.transform.position.y - (position.y - size.y / 2 + 0.045f)) / 0.0452f;

        float bx = (b.transform.position.x - (position.x - size.x / 2 + 0.035f)) / 0.041f;
        float by = (b.transform.position.y - (position.y - size.y / 2 + 0.045f)) / 0.0452f;

        GameObject cell = cells[(int)ax, (int)ay];
        cells[(int)ax, (int)ay] = cells[(int)bx, (int)by];
        cells[(int)bx, (int)by] = cell;

    }

    // Do Empty Tile Move Down
    private void DoEmptyDown(ref GameObject[,] cells)
    {
        Vector3 position = gameObject.transform.position;
        Vector3 size = gameObject.GetComponent<BoxCollider>().bounds.size;
        
        //replace the empty tiles with the ones above
        for (int x = 0; x <= cells.GetUpperBound(0); x++)
        {
            for (int y = 0; y <= cells.GetUpperBound(1); y++)
            {

                var thisCell = cells[x, y];
                if (thisCell.name == "Empty(Clone)")
                {

                    for (int y2 = y; y2 <= cells.GetUpperBound(1); y2++)
                    {
                        if (cells[x, y2].name != "Empty(Clone)")
                        {
                            cells[x, y] = cells[x, y2];
                            cells[x, y2] = thisCell;
                            break;
                        }

                    }

                }

            }
        }
        //Instantiate new tiles to replace the ones destroyed
        for (int x = 0; x <= cells.GetUpperBound(0); x++)
        {
            for (int y = 0; y <= cells.GetUpperBound(1); y++)
            {
                if (cells[x, y].name == "Empty(Clone)")
                {
                    Destroy(cells[x, y]);
                    GameObject gb = null;                
                    gb = GameObject.Instantiate(_listOfGems[Random.Range(0, _listOfGems.Length)] as GameObject) as GameObject;
                    gb.transform.position = new Vector3((position.x - size.x/2 + 0.035f)+ x * 0.041f, (position.y - size.y/2 + 0.045f) + (cells.GetUpperBound(1) + 1) * 0.0452f, position.z - 0.02f);
                    cells[x, y] = gb;
                }
            }
        }
          for (int x = 0; x <= cells.GetUpperBound(0); x++)
        {
            for (int y = 0; y <= cells.GetUpperBound(1); y++)
            {
                TweenParms parms = new TweenParms().Prop("localPosition", new Vector3((position.x - size.x / 2 + 0.035f) + x * 0.041f,(position.y  - size.y / 2 + 0.045f) + y * 0.0452f, position.z - 0.02f)).Ease(EaseType.EaseOutQuart);
                HOTween.To(cells[x, y].transform, .4f, parms);
            }
        }

    }
    //Instantiate the star objects
    void DoShapeEffect()
    {
        if (isPaused) return;
        foreach (GameObject row in _currentParticleEffets)
            Destroy(row);

        Vector3 position = gameObject.transform.position;
        Vector3 size = gameObject.GetComponent<BoxCollider>().bounds.size;

        for (int i = 0; i <= 2; i++)
        {
            GameObject gb = GameObject.Instantiate(_particleEffect as GameObject) as GameObject;
            int x = Random.Range(0, _arrayOfShapes.GetUpperBound(0) + 1);
            int y = Random.Range(0, _arrayOfShapes.GetUpperBound(1) + 1);
            gb.transform.localPosition = new Vector3((position.x - size.x / 2 + 0.035f) + x * 0.041f, (position.y - size.y / 2 + 0.045f) + y * 0.0452f, position.z - 0.02f);
            _currentParticleEffets.Add(gb);
        }

    }

    void SetScore(int _scoreTotal)
    {
        PlayerPrefs.SetInt("LastScore", _scoreTotal);
        if (PlayerPrefs.GetInt("HighScore") < _scoreTotal)
        {

            PlayerPrefs.SetInt("HighScore", _scoreTotal);
       
        }
        if (PlayerPrefs.GetInt("HighLevel") < _scoreTotal)
        {

            PlayerPrefs.SetInt("HighLevel", int.Parse((_LevelTextValue.GetComponent(typeof(TextMesh)) as TextMesh).text));

        }
    }

    void AnimateBigSmall(GameObject go, Vector3 position, string s)
    {
        var destroyingParticle = GameObject.Instantiate(go as GameObject, position, transform.rotation) as GameObject;
        (destroyingParticle.GetComponent(typeof(TextMesh)) as TextMesh).text = s;
        Color oldColor2 = destroyingParticle.GetComponent<Renderer>().material.color;
        TweenParms parms2 = new TweenParms().Prop("color", new Color(oldColor2.r, oldColor2.b, oldColor2.g, 0f)).Ease(EaseType.EaseOutQuart);
        HOTween.To((destroyingParticle.GetComponent(typeof(TextMesh)) as TextMesh), 4f, parms2);
        parms2 = new TweenParms().Prop("fontSize", 200).Ease(EaseType.EaseOutQuart);
        HOTween.To((destroyingParticle.GetComponent(typeof(TextMesh)) as TextMesh), 3f, parms2);
        Destroy(destroyingParticle, 4);
    }


    void Selectarget(GameObject target)
    {
        if(_FirstObject == null)
            _FirstObject = target;
             
        else if(_SecondObject == null)
            _SecondObject= target;
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        string name = eventData.selectedObject.transform.name;
        throw new System.NotImplementedException();
    }
}

