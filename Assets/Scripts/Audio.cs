using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Audio : MonoBehaviour
{
	public AudioMixer AM;
	public void SetVolume(float volume)
	{
		AM.SetFloat("Volume", volume);
	}
}
