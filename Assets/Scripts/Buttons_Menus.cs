using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttons_Menus : MonoBehaviour {

	public CanvasRenderer Gold_Up;
	public CanvasRenderer Rum_Up;
	public CanvasRenderer Stats_Up;
	public Button G_Button;
	public Button R_Button;
	public Button S_Button;

	public void Active_Stats() {
		Stats_Up.gameObject.SetActive(true);
		Gold_Up.gameObject.SetActive(false);
		Rum_Up.gameObject.SetActive(false);
		R_Button.gameObject.SetActive(false);
		G_Button.gameObject.SetActive(false);
		S_Button.gameObject.SetActive(false);

	}

	public void Deactive_Stats() {
		R_Button.gameObject.SetActive(true);
		S_Button.gameObject.SetActive(true);
		G_Button.gameObject.SetActive(true);
		Stats_Up.gameObject.SetActive(false);
	}


	public void Active_Rum() {
		Rum_Up.gameObject.SetActive(true);
		Stats_Up.gameObject.SetActive(false);
		Gold_Up.gameObject.SetActive(false);
		S_Button.gameObject.SetActive(false);
		G_Button.gameObject.SetActive(false);
		R_Button.gameObject.SetActive(false);

	}

	public void Deactive_Rum() {
		R_Button.gameObject.SetActive(true);
		S_Button.gameObject.SetActive(true);
		G_Button.gameObject.SetActive(true);
		Rum_Up.gameObject.SetActive(false);
	}

	public void Active_Gold() {
		Gold_Up.gameObject.SetActive(true);
		Stats_Up.gameObject.SetActive(false);
		Rum_Up.gameObject.SetActive(false);
		R_Button.gameObject.SetActive(false);
		S_Button.gameObject.SetActive(false);
		G_Button.gameObject.SetActive(false);

	}

	public void Deactive_Gold() {
		R_Button.gameObject.SetActive(true);
		S_Button.gameObject.SetActive(true);
		G_Button.gameObject.SetActive(true);
		Gold_Up.gameObject.SetActive(false);
	}
}
