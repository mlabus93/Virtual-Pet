using UnityEngine;

namespace com.aeksaekhow.androidnativeplugin
{
	public static class AndroidNativePlugin
	{

		/// <summary>
		/// Show toast message to user.
		/// </summary>
		/// <param name="message"></param>
		/// <param name="timelength"></param>
		public static void ShowToastMessage(string message, ToastTimeLength timelength = ToastTimeLength.Short)
		{
			#if UNITY_ANDROID
			using (AndroidJavaClass nativeAndroidPlugin = new AndroidJavaClass("com.aeksaekhow.nativeandroidlib.NativeAndroidPlugin"))
			{
				using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
				{
					// Get context object.
					AndroidJavaObject context = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
					// Call showToastMessage() method from Android script.
                    nativeAndroidPlugin.CallStatic("showToastMessage", context, message, (int)timelength);
				}
            }
			#endif
		}

		/// <summary>
		/// Send local notification to user.
		/// </summary>
		/// <param name="title"></param>
		/// <param name="content"></param>
		public static void SendLocalNotification(string title, string content)
		{
			#if UNITY_ANDROID
			using (AndroidJavaClass nativeAndroidPlugin = new AndroidJavaClass("com.aeksaekhow.nativeandroidlib.NativeAndroidPlugin"))
			{
				using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
				{
					// Get context object.
					AndroidJavaObject context = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
					// Call sendLocalNotification() method from Android script.
					nativeAndroidPlugin.CallStatic("sendLocalNotification", context, title, content);
				}
			}
			#endif
		}

		/// <summary>
		/// Check internet connectivity.
		/// </summary>
		/// <returns><c>true</c> if internet is connected; otherwise, <c>false</c>.</returns>
		public static bool IsInternetConnected()
		{
			#if UNITY_ANDROID
			using (AndroidJavaClass nativeAndroidPlugin = new AndroidJavaClass("com.aeksaekhow.nativeandroidlib.NativeAndroidPlugin"))
			{
				using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
				{
					// Get context object.
					AndroidJavaObject context = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
					// Call isInternetConnected() method from Android script.
					return nativeAndroidPlugin.CallStatic<bool>("isInternetConnected", context);
				}
			}
			#else
			return false;
			#endif
		}

		/// <summary>
		/// Check WIFI connectivity.
		/// </summary>
		/// <returns><c>true</c> if wifi is connected; otherwise, <c>false</c>.</returns>
		public static bool IsWifiConnected()
		{
			#if UNITY_ANDROID
			using (AndroidJavaClass nativeAndroidPlugin = new AndroidJavaClass("com.aeksaekhow.nativeandroidlib.NativeAndroidPlugin"))
			{
				using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
				{
					// Get context object.
					AndroidJavaObject context = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
					// Call isWifiConnected() method from Android script.
					return nativeAndroidPlugin.CallStatic<bool>("isWifiConnected", context);
				}
			}
			#else
			return false;
			#endif
		}

		/// <summary>
		/// Check mobile connectivity (3G, 4G).
		/// </summary>
		/// <returns><c>true</c> if mobile connected; otherwise, <c>false</c>.</returns>
		public static bool IsMobileConnected()
		{
			#if UNITY_ANDROID
			using (AndroidJavaClass nativeAndroidPlugin = new AndroidJavaClass("com.aeksaekhow.nativeandroidlib.NativeAndroidPlugin"))
			{
				using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
				{
					// Get context object.
					AndroidJavaObject context = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
					// Call isMobileConnected() method from Android script.
					return nativeAndroidPlugin.CallStatic<bool>("isMobileConnected", context);
				}
			}
			#else
			return false;
			#endif
		}

	}
}