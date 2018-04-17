using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Facebook.Unity;

public class FacebookController : MonoBehaviour {

	public Button shareBtn;

	// Use this for initialization
	void Awake () {
		if (!FB.IsInitialized) {
			FB.Init (InitCallBack, OnHideUnity);
		} else {
			FB.ActivateApp ();
		}
			
	}

	void Start(){
		if (!FB.IsInitialized) {
			FB.Init (InitCallBack, OnHideUnity);
		} else {
			FB.ActivateApp ();
		}
		shareBtn.onClick.AddListener (OnClickShare);
	}

	private void InitCallBack(){
		if (FB.IsInitialized) {
			FB.ActivateApp ();
		} else {
			Debug.Log ("Failed to Initialize Fb SDK");
		}
	}

	private void OnHideUnity(bool isGameShown){
		if (!isGameShown) {
			Time.timeScale = 0;
		} else {
			Time.timeScale = 1;
		}
	}

	public void ShareLink(){
		FB.ShareLink (new System.Uri ("https://developer.facebook.com/"), callback: ShareCallback);
	}

	private void ShareCallback(IShareResult result){
		if (result.Cancelled || !string.IsNullOrEmpty (result.Error)) {
			Debug.Log ("ShareLink Error: " + result.Error);
		} else if (!string.IsNullOrEmpty (result.PostId)) {
			Debug.Log (result.PostId);
		} else {
			Debug.Log ("ShareLink Success!");
		}
	}

	void OnClickShare(){
		ShareLink ();
	}
}
