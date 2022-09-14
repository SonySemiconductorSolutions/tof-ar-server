/*
 * SPDX-License-Identifier: (Apache-2.0 OR GPL-2.0-only)
 *
 * Copyright 2018,2019,2020,2021 Sony Semiconductor Solutions Corporation.
 *
 */
using TofAr.V0;
using TofAr.V0.Slam;
using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;
using TofAr.V0.Body;
using TofAr.V0.Segmentation;
using TofAr.V0.Color;
using TofAr.V0.Face;
using TofAr.V0.Tof;
using UnityEngine.SceneManagement;

public class DebugServerController : MonoBehaviour
{
    public delegate void OnDebugServerStartedDelegate();
    public OnDebugServerStartedDelegate OnDebugServerStarted;

    public delegate void OnDebugServerStoppedDelegate();
    public OnDebugServerStoppedDelegate OnDebugServerStopped;

    public Text titleText;
    public Text ipText;
    public InputField portInput;
    public Text statusText;
    public TextAsset testJson;

    public GameObject depthViewNotePanel;

    private string ipAddress = "";
    private string port = "";

    private string locale;

    [Serializable]
    class ServerStatus
    {
        public string key = "";
        public int opendCount = 0;
    }

    [Serializable]
    class ServerStatusList
    {
        public ServerStatus[] values = null;
    }

    public void SetPort(string port)
    {
        PlayerPrefs.SetString("Port", port);
        PlayerPrefs.Save();

        // TODO message box for restart

        string msg = "Changes to the port will take effect after restart. Exit application now?";

        if (locale.Equals("ja_JP"))
            msg = "ポート番号の変更は再起動時に有効になります。\nアプリケーションを終了しますか？";


        ShowMessageBox(msg);

    }

    public static string GetIP()
    {
        string output = "127.0.0.1";

        foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
        {
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
            NetworkInterfaceType _type1 = NetworkInterfaceType.Wireless80211;
            NetworkInterfaceType _type2 = NetworkInterfaceType.Ethernet;

            if ((item.NetworkInterfaceType == _type1 || item.NetworkInterfaceType == _type2) && item.OperationalStatus == OperationalStatus.Up)
#endif 
            {
                foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                {
                    string ipStr = ip.Address.ToString();

                    if (ip.Address.AddressFamily == AddressFamily.InterNetwork && !ipStr.Equals("127.0.0.1"))
                    {
                        output = ip.Address.ToString();
                    }

                }
            }
        }
        return output;
    }



    bool isRequesting;
    IEnumerator RequestUserPermission(string permission)
    {
        isRequesting = true;
        Permission.RequestUserPermission(permission);
        float timeElapsed = 0;
        while (isRequesting)
        {
            if (timeElapsed > 0.5f)
            {
                isRequesting = false;
                yield break;
            }
            timeElapsed += Time.deltaTime;

            yield return null;
        }
        yield break;
    }

    private bool isServerRunning = false;
    private bool isAppStarted = false;
    private string GetLocale()
    {
#if UNITY_ANDROID
        using (AndroidJavaClass locale = new AndroidJavaClass("java.util.Locale"))
        {
            AndroidJavaObject defaultLocale = locale.CallStatic<AndroidJavaObject>("getDefault");

            string lang = defaultLocale.Call<string>("toString");

            return lang;
        }
#else
        return "en_US";
#endif
    }

    void Awake()
    {
        this.titleText.text = $"ToF AR Server : v{TofArManager.Instance.Version}";

        if (depthViewNotePanel != null)
        {
            depthViewNotePanel.SetActive(false);
        }
    }

    IEnumerator Start()
    {
        /**/
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        string playerPrefPort = PlayerPrefs.GetString("Port", "");
        if (playerPrefPort.Equals(""))
        {
            playerPrefPort = "8080";
            PlayerPrefs.SetString("Port", playerPrefPort);
        }

        this.port = playerPrefPort;

        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        this.ipAddress = GetIP();

        yield return SetIpUI();

        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            yield return RequestUserPermission(Permission.Camera);
        }



        this.UpdateServerSettingsFile();

        locale = GetLocale();



        yield return StartServerCoroutine();
    }

    private void UpdateServerSettingsFile()
    {
        var core = TofArManager.Instance.SensCordCore;
        if (!this.ipAddress.Equals("127.0.0.1") || !this.port.Equals("8080"))
        {
            var filePath = $"{Application.temporaryCachePath}{Path.DirectorySeparatorChar}TofAr{Path.DirectorySeparatorChar}senscord_server.xml";
            if (System.IO.File.Exists(filePath))
            {
                var doc = XDocument.Load(filePath);

                var instances = doc.Root.Elements("listeners");
                {
                    if (instances.Count() < 2)
                    {
                        var newListener = new XElement("listener");
                        newListener.Add(new XAttribute("connection", "tcp"));
                        newListener.Add(new XAttribute("address", this.ipAddress + ":" + this.port));
                        doc.Root.Element("listeners").Add(newListener);
                        instances = doc.Root.Elements("listeners");
                    }
                    var instance = instances.FirstOrDefault();
                    // debug server listening
                    instance.SetAttributeValue("address", "127.0.0.1:8080");
                    instance = instances.LastOrDefault();
                    // get own ip address
                    instance.SetAttributeValue("address", this.ipAddress + ":" + this.port);

                }
                doc.Save(filePath);
            }
        }
    }

    private void OnDestroy()
    {
        if (TofArSlamManager.Instance != null)
        {
            TofArSlamManager.Instance.StopStream();
        }
        
        this.StopServer();
    }

    private IEnumerator SetIpUI()
    {
        this.ipText.text = "IP: " + this.ipAddress + ":";

        this.portInput.text = this.port;

        yield return null;
    }

    public void Quit()
    {
        // TODO open dialog



        ShowMessageBox("アプリケーションを終了しますか？");
    }

    private void ShowMessageBox(string msg)
    {

        using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {

            AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

            activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                // Create an AlertDialog.Builder object
                using (AndroidJavaObject alertDialogBuilder = new AndroidJavaObject("android/app/AlertDialog$Builder", activity))
                {
                    alertDialogBuilder.Call<AndroidJavaObject>("setMessage", msg);
                    alertDialogBuilder.Call<AndroidJavaObject>("setCancelable", false);
                    alertDialogBuilder.Call<AndroidJavaObject>("setPositiveButton", this.locale.Equals("ja_JP") ? "終了" : "Exit", new PositiveButtonListener());
                    alertDialogBuilder.Call<AndroidJavaObject>("setNegativeButton", this.locale.Equals("ja_JP") ? "キャンセル" : "Cancel", new NegativeButtonListener());

                    AndroidJavaObject dialog = alertDialogBuilder.Call<AndroidJavaObject>("create");
                    dialog.Call("show");
                }
            }));
        }
    }

    /// <summary>
    /// Positive button listner.
    /// </summary>
    private class PositiveButtonListener : AndroidJavaProxy
    {

        public PositiveButtonListener() : base("android.content.DialogInterface$OnClickListener")
        {
        }

        public void onClick(AndroidJavaObject obj, int value)
        {
            Application.Quit(0);

            /*using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                const int FLAG_ACTIVITY_SINGLE_TOP = 0x20000000;
                const int FLAG_CANCEL_CURRENT = 0x10000000;
                const int RTC = 0x1;
                const int pendingId = 12345;

                var classSystem = new AndroidJavaClass("java.lang.System");
                var classPendingIntent = new AndroidJavaClass("android.app.PendingIntent");
                var classProcess = new AndroidJavaClass("android.os.Process");

                var currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
                var alarmService = currentActivity.Call<AndroidJavaObject>("getSystemService", "alarm");

                var packageManager = currentActivity.Call<AndroidJavaObject>("getPackageManager");
                var launchIntent = packageManager.Call<AndroidJavaObject>("getLaunchIntentForPackage", Application.identifier);
                launchIntent.Call<AndroidJavaObject>("setFlags", FLAG_ACTIVITY_SINGLE_TOP);

                var activityPendingIntent = classPendingIntent.CallStatic<AndroidJavaObject>("getActivity", currentActivity, pendingId, launchIntent, FLAG_CANCEL_CURRENT);
                
                long currentTimeMillis = classSystem.CallStatic<long>("currentTimeMillis") + 100;
                alarmService.Call("set", RTC, currentTimeMillis, activityPendingIntent); 
                currentActivity.Call("finish");

                int pid = classProcess.CallStatic<int>("myPid");
                classProcess.CallStatic("killProcess", pid);
            }*/
        }
    }

    /// <summary>
    /// Negative button listner.
    /// </summary>
    private class NegativeButtonListener : AndroidJavaProxy
    {

        public NegativeButtonListener() : base("android.content.DialogInterface$OnClickListener")
        {
        }

    }

    public void DepthViewButton()
    {
        if (depthViewNotePanel != null)
        {
            depthViewNotePanel.SetActive(!depthViewNotePanel.activeSelf);
        }
    }

    public void ChangeDepthScene()
    {
        TofArBodyManager.Instance?.StopStream();
        TofArSlamManager.Instance?.StopStream();
        TofArFaceManager.Instance?.StopStream();
        TofArSegmentationManager.Instance?.StopStream();
        TofArColorManager.Instance?.StopStream();
        TofArTofManager.Instance?.StopStream();

        Destroy(TofArManager.Instance.gameObject);
        Destroy(TofArColorManager.Instance.gameObject);
        Destroy(TofArTofManager.Instance.gameObject);
        Destroy(TofArSlamManager.Instance.gameObject);
        Destroy(TofArBodyManager.Instance.gameObject);
        Destroy(TofArSegmentationManager.Instance.gameObject);
        Destroy(TofArFaceManager.Instance.gameObject);

        SceneManager.LoadSceneAsync("Depth");
    }

    void Update()
    {
        if (!this.isServerRunning)
        {
            this.statusText.text = "Starting server...";
            return;
        }
        int bufferSize = 4096;
        var buffer = new StringBuilder(bufferSize);
#if UNITY_EDITOR
        {
            var json = this.testJson.text;
#else
        if (GetTofArServerStatus(buffer, (uint)buffer.Capacity) == 0)
        {
            var json = buffer.ToString();
#endif
            //TofArManager.Logger.WriteLog(LogLevel.Debug, json);

            var statusList = JsonUtility.FromJson<ServerStatusList>(json);
            var message = "[Opened] / Stream Key\n\n";
            foreach (var value in statusList.values)
            {
                message += $"[{value.opendCount,4}] / {value.key}\n";
            }
            this.statusText.text = message;
        }
    }

    private void OnApplicationPause(bool pause)
    {
        if (!this.isAppStarted) // don't call on startup
            return;

        if (pause)
        {
            this.StopServer();
            // Cache cleanuped by TofArManager.OnApplicationPause().
        }
        else
        {
            this.UpdateServerSettingsFile();
            this.StartServer();
        }
    }

    private void StartServer()
    {
        TofArManager.Logger.WriteLog(LogLevel.Debug, "StartServer");
        this.StartCoroutine(StartServerCoroutine());


    }

    private void StopServer()
    {
        this.StopAllCoroutines();

        OnDebugServerStopped?.Invoke();
        TofArManager.Logger.WriteLog(LogLevel.Debug, "StopServer");
        if (this.isServerRunning)
        {
            int status = StopTofArServer();
            if (status != 0)
            {
                TofArManager.Logger.WriteLog(LogLevel.Debug, "Failed to stop server");
            }
            else
            {
                TofArManager.Logger.WriteLog(LogLevel.Debug, "Server stopped");
                this.isServerRunning = false;
            }
        }


    }

    private IEnumerator StartServerCoroutine()
    {
        TofAr.AppLicense.AppLicense appLicense = this.GetComponent<TofAr.AppLicense.AppLicense>();

        if (appLicense != null)
        {
            while (!appLicense.GetAgreeState())
            {
                yield return null;
            }
        }

        int success = 1;

        while (success != 0)
        {

            success = StartTofArServer();
            yield return null;
        }

        TofArManager.Logger.WriteLog(LogLevel.Debug, "Server started");

        this.isServerRunning = true;
        this.isAppStarted = true;

        // isRemoteServer flag apply for naitive plgins
        var runtimeSettings = TofArManager.Instance.GetProperty<RuntimeSettingsProperty>();
        runtimeSettings.runMode = RunMode.Default;
        runtimeSettings.isRemoteServer = true;
        TofArManager.Instance.SetProperty(runtimeSettings);
        TofArManager.Logger.WriteLog(LogLevel.Debug, "Update RuntimeSettings isRemoteServer -> true");

        OnDebugServerStarted?.Invoke();
        TofArSlamManager.Instance.StartStream();

    }

    public void GyroToggleValueChanged(bool value)
    {
        if (value)
        {
            TofArSlamManager.Instance.StartStream();
        }
        else
        {
            TofArSlamManager.Instance.StopStream();
        }
    }
#if UNITY_ANDROID
    [DllImport("tofar_debug_server", EntryPoint = "StartTofArServer", CallingConvention = CallingConvention.StdCall)]
#else
    [DllImport("__Internal", EntryPoint = "StartTofArServer", CallingConvention = CallingConvention.StdCall)]
#endif
    internal static extern int StartTofArServer();

#if UNITY_ANDROID
    [DllImport("tofar_debug_server", EntryPoint = "StopTofArServer", CallingConvention = CallingConvention.StdCall)]
#else
    [DllImport("__Internal", EntryPoint = "StopTofArServer", CallingConvention = CallingConvention.StdCall)]
#endif
    internal static extern int StopTofArServer();

#if UNITY_ANDROID
    [DllImport("tofar_debug_server", EntryPoint = "GetTofArServerStatus", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
#else
    [DllImport("__Internal", EntryPoint = "GetTofArServerStatus", CallingConvention = CallingConvention.StdCall)]
#endif
    internal static extern int GetTofArServerStatus(StringBuilder outputJsonBffer, uint bufferSize);
}
