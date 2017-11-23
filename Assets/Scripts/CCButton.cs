using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCButton : MonoBehaviour {

	public Material[] materials;//Allows input of material colors in a set size of array;
	public Renderer Rend; //What are we rendering? Input object(Sphere,Cylinder,...) to render.

	private int index = 1;//Initialize at 1, otherwise you have to press the ball twice to change colors at first.

	public void buttonPressed(){
		if (materials.Length == 0)//If there are no materials nothing happens.
			return;

		index += 1;//When button is pressed down we increment up to the next index location

		if (index == materials.Length + 1)//When it reaches the end of the materials it starts over.
			index = 1;

		print (index);//used for debugging 

		Rend.sharedMaterial = materials [index - 1]; //This sets the material color values inside the index
	}
}
