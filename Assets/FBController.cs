// Include Facebook namespace
using Facebook.Unity;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FBController: MonoBehaviour {
	List<string> perms = new List<string>(){"public_profile", "email", "user_friends"};
	// Awake function from Unity's MonoBehavior
	void Awake () {
		if (!FB.IsInitialized) {
			// Initialize the Facebook SDK
			FB.Init(InitCallback, OnHideUnity);
		} else {
			// Already initialized, continue with callback directly
			this.InitCallback();
		}
	}

	private void InitCallback () {
		if (FB.IsInitialized) {
			// Continue with Facebook SDK
			Debug.Log("FB init done.");
			FB.LogInWithReadPermissions(perms, AuthCallback);
		} else {
			Debug.Log("Failed to Initialize the Facebook SDK");
		}
	}

	private void OnHideUnity (bool isGameShown) {
		if (!isGameShown) {
			// Pause the game - we will need to hide
			Time.timeScale = 0;
		} else {
			// Resume the game - we're getting focus again
			Time.timeScale = 1;
		}
	}

	private void AuthCallback (ILoginResult result) {
		if (FB.IsLoggedIn) {
			// AccessToken class will have session details
			var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
			// Print current access token's User ID
			Debug.Log(aToken.UserId);
			// Print current access token's granted permissions
			foreach (string perm in aToken.Permissions) {
				Debug.Log(perm);
			}
		} else {
			Debug.Log("User cancelled login");
		}
	}
}