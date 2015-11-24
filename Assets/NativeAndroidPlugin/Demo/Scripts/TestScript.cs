using UnityEngine;

namespace com.aeksaekhow.androidnativeplugin
{
	public class TestScript : MonoBehaviour
	{

		public float buttonHeight = 60;

		private bool isInternetConnected;

		private bool isWifiConnected;

		private bool isMobileConnected;

		//--------------------------
		// mono methods
		//--------------------------

		private void OnGUI()
		{
			if (Button("Show toast"))
			{
				AndroidNativePlugin.ShowToastMessage("Hello World!", ToastTimeLength.Short);
            }

			if (Button("Send local notification"))
			{
				AndroidNativePlugin.SendLocalNotification("Notification Title", "Notification Content");
            }

			GUILayout.Label ("isInternetConnected = " + isInternetConnected);

			if (Button("Check internet connectivity"))
			{
				isInternetConnected = AndroidNativePlugin.IsInternetConnected();
			}

			GUILayout.Label ("isWifiConnected = " + isWifiConnected);

			if (Button("Check wifi connectivity"))
			{
				isWifiConnected = AndroidNativePlugin.IsWifiConnected();
			}

			GUILayout.Label ("isMobileConnected = " + isMobileConnected);

			if (Button("Check mobile connectivity (3G)"))
			{
				isMobileConnected = AndroidNativePlugin.IsMobileConnected();
			}
		}

		//--------------------------
		// private methods
		//--------------------------

		private bool Button(string text)
		{
			return GUILayout.Button(text, GUILayout.Width(Screen.width), GUILayout.Height(buttonHeight));
		}

	}
}