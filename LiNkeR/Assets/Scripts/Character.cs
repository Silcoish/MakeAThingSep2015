using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Character : MonoBehaviour {

	public List<AudioClip> taunt;
	public List<AudioClip> win;
	public List<AudioClip> select;
	public List<AudioClip> die;
	public List<AudioClip> bee;
	public List<AudioClip> hurt;

	public float speed;
	public float acc;
	public float health;
	public float guns;

	public Sprite characterSprite;
	public Sprite characterIcon;
	public Sprite carSprite;

	public AudioClip PlayTaunt()
	{
		return taunt[Random.Range(0, taunt.Count)];
	}

	public AudioClip PlayWin()
	{
		return win[Random.Range(0, win.Count)];
	}

	public AudioClip PlaySelect()
	{
		return select[Random.Range(0, select.Count)];
	}

	public AudioClip PlayDie()
	{
		return die[Random.Range(0, die.Count)];
	}

	public AudioClip PlayBee()
	{
		return bee[Random.Range(0, bee.Count)];
	}

	public AudioClip PlayHurt()
	{
		return hurt[Random.Range(0, hurt.Count)];
	}
}
