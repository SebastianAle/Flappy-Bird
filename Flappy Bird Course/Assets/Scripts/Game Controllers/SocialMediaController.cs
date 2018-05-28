using UnityEngine;
using System.Collections;
using Soomla;
using Soomla.Profile;

public class SocialMediaController : MonoBehaviour {

	public static SocialMediaController instance;

	void Awake() {
		MakeSingleton ();
	}

	// Use this for initialization
	void Start () {
		SoomlaProfile.Initialize ();
	}

	public void OnEnable() {
		ProfileEvents.OnLoginFinished += onLoginFinished;
		ProfileEvents.OnLoginFailed += onLoginFailed;
		ProfileEvents.OnLoginCancelled += onLoginCancelled;
		ProfileEvents.OnLogoutFinished += onLogoutFinished;
		ProfileEvents.OnLogoutFailed += onLogoutFailed;
		ProfileEvents.OnSocialActionFinished += onSocialActionFinished;
		ProfileEvents.OnSocialActionFailed += onSocialActionFailed;
		ProfileEvents.OnSocialActionCancelled += onSocialActionCancelled;
	}
	
	public void OnDisable() {
		ProfileEvents.OnLoginFinished -= onLoginFinished;
		ProfileEvents.OnLoginFailed -= onLoginFailed;
		ProfileEvents.OnLoginCancelled -= onLoginCancelled;
		ProfileEvents.OnLogoutFinished -= onLogoutFinished;
		ProfileEvents.OnLogoutFailed -= onLogoutFailed;
		ProfileEvents.OnSocialActionFinished -= onSocialActionFinished;
		ProfileEvents.OnSocialActionFailed -= onSocialActionFailed;
		ProfileEvents.OnSocialActionCancelled -= onSocialActionCancelled;
	}
	
	void MakeSingleton() {
		if (instance != null) {
			Destroy (gameObject);
		} else {
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}

	public void LogInOrLogOutTwitter() {
		if (SoomlaProfile.IsLoggedIn (Provider.TWITTER)) {
			SoomlaProfile.Logout (Provider.TWITTER);
		} else {
			SoomlaProfile.Login (Provider.TWITTER);
		}
	}

	public void ShareOnTwitter() {
		if (SoomlaProfile.IsLoggedIn (Provider.TWITTER)) {

			SoomlaProfile.UpdateStory (
				Provider.TWITTER,
				"Im Playing This Awesome Game " + Random.Range(0, 100),
				null,
				null,
				null,
				null,
				"www.Link.com",
				null,
				null
				);
		} else {
			if (Application.loadedLevelName == "MainMenu") {
				MenuController.instance.NotificationMessage("Please Connect In Order To Post");
			}
		}
	}

	void onLoginFinished(UserProfile userProfileJson, string payload){
		if (Application.loadedLevelName == "MainMenu") {
			MenuController.instance.NotificationMessage("Connected");
		}
	}
	
	void onLoginFailed(Provider provider, string message, string payload) {
		if (Application.loadedLevelName == "MainMenu") {
			MenuController.instance.NotificationMessage("Log In Failed");
		}
	}
	
	void onLoginCancelled(Provider provider, string payload) {
		if (Application.loadedLevelName == "MainMenu") {
			MenuController.instance.NotificationMessage("Log In Canceled");
		}
	}
	
	void onLogoutFinished(Provider provider) {
		if (Application.loadedLevelName == "MainMenu") {
			MenuController.instance.NotificationMessage("Disconnected");
		}
	}
	
	void onLogoutFailed(Provider provider, string message) {
		if (Application.loadedLevelName == "MainMenu") {
			MenuController.instance.NotificationMessage("Could Not Disconnect");
		}
	}
	
	void onSocialActionFinished(Provider provider, SocialActionType action, string payload) {
		if (provider == Provider.TWITTER) {
			if(action == SocialActionType.UPDATE_STORY) {
				if (Application.loadedLevelName == "MainMenu") {
					MenuController.instance.NotificationMessage("Thank You For Sharing");
				}
			}
		}
	}
	
	void onSocialActionCancelled(Provider provider, SocialActionType action, string payload) {
		if (provider == Provider.TWITTER) {
			if(action == SocialActionType.UPDATE_STORY) {
				if (Application.loadedLevelName == "MainMenu") {
					MenuController.instance.NotificationMessage("Could Not Post");
				}
			}
		}
	}
	
	void onSocialActionFailed(Provider provider, SocialActionType action, string message, string payload) {
		if (provider == Provider.TWITTER) {
			if(action == SocialActionType.UPDATE_STORY) {
				if (Application.loadedLevelName == "MainMenu") {
					MenuController.instance.NotificationMessage("Could Not Post");
				}
			}
		}
	}

} // class


























































