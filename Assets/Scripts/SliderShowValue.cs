using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderShowValue : MonoBehaviour {

		public Text sliderValue;
 		public Slider slider;
 
 	void Update()
	{
 		sliderValue.text = slider.value.ToString();
 	}
}
