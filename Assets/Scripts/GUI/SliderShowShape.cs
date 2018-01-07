using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderShowShape : MonoBehaviour {

		public Text sliderValue;
 		public Slider slider;
 
 	void Update()
	{
		if (slider.value == 0)
 			sliderValue.text = "Cone";
		if (slider.value == 1)
 			sliderValue.text = "Donut";
		if (slider.value == 2)
 			sliderValue.text = "Edge";
		if (slider.value == 3)
 			sliderValue.text = "Circle";
 	}
}
