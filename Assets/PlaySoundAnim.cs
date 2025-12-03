using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Audio;

public class PlaySoundAnim : MonoBehaviour
{

        [SerializeField] private AudioSource source;
		[SerializeField] private ClipObject clip;


    public void PlaySound(){

        if (!source || !clip) return;
			clip.Settings.Apply(source);
			source.clip = clip.Clip;
			source.Play();

    }

}
