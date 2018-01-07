using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderShowBool : MonoBehaviour {

		public Text sliderValue;
 		public Slider slider;
 
 	void Update()
	{
		if (slider.value == 1)
 			sliderValue.text = "true";
		else
			sliderValue.text = "false";
 	}
}
