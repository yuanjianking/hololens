using UnityEngine;
using Holoville.HOTween;

public class MainMenu : MonoBehaviour
{
		public GameObject _Logo;//Your Game Logo
		public GameObject _BestScore;//The Best Score
	    public GameObject _BestLevel;//The  Best Level
		public AudioClip MenuSound;//The Sound Played when you click on the play button

		//void Awake ()
		//{
		//		Time.timeScale = 1; //Setting the timescale to the standard value of 1
		//}

		void Start ()
		{
				AnimateLogo ();
				(_BestScore.GetComponent (typeof(TextMesh))as TextMesh).text = "" + PlayerPrefs.GetInt ("HighScore");
				(_BestLevel.GetComponent (typeof(TextMesh))as TextMesh).text = "" + PlayerPrefs.GetInt ("HighLevel");

		}

	    //Animating the logo in loop
		void AnimateLogo ()
		{
            Sequence mySequence = new Sequence(new SequenceParms().Loops(-1));
            TweenParms parms;

            Color oldColor = _Logo.GetComponent<Renderer>().material.color;
            parms = new TweenParms().Prop("color", new Color(oldColor.r, oldColor.b, oldColor.g, 0.4f)).Ease(EaseType.EaseInQuart);

            parms = new TweenParms().Prop("localScale", new Vector3(0.11f, 0.11f, 1)).Ease(EaseType.EaseOutElastic);
            mySequence.Append(HOTween.To(_Logo.transform, 6f, parms));

            parms = new TweenParms().Prop("localScale", new Vector3(0.09f, 0.09f, 1)).Ease(EaseType.EaseOutElastic);
            mySequence.Append(HOTween.To(_Logo.transform, 5f, parms));

            mySequence.Play();
    }
}