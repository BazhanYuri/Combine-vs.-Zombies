using System;
using System.Collections;
using System.IO;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using ShadowResolution = UnityEngine.ShadowResolution;
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine.Rendering.Universal;
#endif

public class ScreenshotsEditor : ScriptableObject
{
#if UNITY_EDITOR
    [Title("General")]
    [SerializeField] private bool m_ShowLogOnCreation = true;
    [SerializeField] private bool m_OpenOnFinish = true;

    [Title("Camera")]
    [SerializeField] private Camera m_Camera;

    [Title("Screen Size")]
    [SerializeField] private int m_ScreenWidth;
    [SerializeField] private int m_ScreenHeight;
    [Range(1, 5)]
    [SerializeField] private int m_ScreenSizeMultiplier = 1;

    [BoxGroup("Save Path"), ShowInInspector] private string SavePath { get => EditorPrefs.GetString(k_SavingPathId, Application.dataPath); set => EditorPrefs.SetString(k_SavingPathId, value); }

    [BoxGroup("Save Path"), Button]
    private void Browse()
    {
        BrowseSavePath();
    }

    [BoxGroup("Save Path"), PropertyOrder(2)]
    [SerializeField] private bool m_SeperateToResolutionFolders = true;

    private const string k_SavingPathId = "ScreenshotEditorId";

    [Button(ButtonSizes.Medium)]
    public void SetAsGameScreenSize()
    {
        m_ScreenWidth = (int)Handles.GetMainGameViewSize().x;
        m_ScreenHeight = (int)Handles.GetMainGameViewSize().y;
    }

    [Button(ButtonSizes.Medium, Name = "Set As 1080x1920")]
    public void SetAs1080x1920()
    {
        GameViewSize.SelectSize(GameViewSize.SetCustomSize(1080, 1920));

        m_ScreenWidth = (int)Handles.GetMainGameViewSize().x;
        m_ScreenHeight = (int)Handles.GetMainGameViewSize().y;
    }

    public void BrowseSavePath()
    {
        SavePath = EditorUtility.SaveFolderPanel("Path to Save Images", SavePath, Application.dataPath);
    }

    private IEnumerator m_ScreenshotCoroutine;

    // Android
    private readonly Tuple<int, int, string> r_Android7Inch = new Tuple<int, int, string>(600, 1024, "GooglePlay/600x1024");
    private readonly Tuple<int, int, string> r_AndroidFullHD = new Tuple<int, int, string>(1080, 1920, "GooglePlay/1080x1920");
    private readonly Tuple<int, int, string> r_Android10Inch = new Tuple<int, int, string>(1200, 1920, "GooglePlay/1200x1920");

    [Button(ButtonSizes.Medium)]
    public void TakeScreenshot()
    {
        TakeScreenShot(new Tuple<int, int, string>[] { new Tuple<int, int, string>(m_ScreenWidth, m_ScreenHeight, $"{m_ScreenWidth}x{m_ScreenHeight}") });
    }

    [HorizontalGroup("2"), Button("600x1024 (7'')", ButtonSizes.Gigantic)]
    public void TakeScreenshotGP1()
    {
        TakeScreenShot(new Tuple<int, int, string>[] { r_Android7Inch });
    }

    [HorizontalGroup("2"), Button("1080x1920 (FullHD)", ButtonSizes.Gigantic)]
    public void TakeScreenshotGP2()
    {
        TakeScreenShot(new Tuple<int, int, string>[] { r_AndroidFullHD });
    }

    [HorizontalGroup("2"), Button("1200x1920 (10'')", ButtonSizes.Gigantic)]
    public void TakeScreenshotGP3()
    {
        TakeScreenShot(new Tuple<int, int, string>[] { r_Android10Inch });
    }

    [Button("Take All Screenshots At Once (Android)", ButtonSizes.Large)]
    public void TakeAllScreenshotsAtOnceAndroid()
    {
        TakeScreenShot(new Tuple<int, int, string>[] { r_Android7Inch, r_AndroidFullHD, r_Android10Inch });
    }

    private void TakeScreenShot(Tuple<int, int, string>[] i_Resolutions)
    {
        if (SavePath == string.Empty)
            BrowseSavePath();

        if (SavePath != string.Empty)
        {
            if (EditorApplication.isPaused || !EditorApplication.isPlaying)
            {
                EditorApplication.update += coroutineHandler;

                m_ScreenshotCoroutine = TakeScreenShotCo(i_Resolutions);
            }
            else
            {
                Managers.Instance.StartCoroutine(TakeScreenShotCo(i_Resolutions));
            }
        }
    }

    void coroutineHandler()
    {
        m_ScreenshotCoroutine.MoveNext();
    }

    private IEnumerator TakeScreenShotCo(Tuple<int, int, string>[] i_Resolutions)
    {
        string path = string.Empty;
        EventSystem eventSystem = GameObject.FindObjectOfType<EventSystem>();

        //Original values
        bool eventSystemStateOrg = eventSystem != null ? eventSystem.gameObject.activeSelf : true;
        //Standard Render Pipeline
        ShadowResolution shadowResolutionOrg = QualitySettings.shadowResolution;
        int antiAliasingOrg = QualitySettings.antiAliasing;
        //URP
        var renderPipeline = ((UniversalRenderPipelineAsset)QualitySettings.renderPipeline);
        var renderPipelineHQ = getURPAssetHQ();

        //Best Values
        Time.timeScale = 0;
        eventSystem?.gameObject.SetActive(false);
        //Standard Render Pipeline
        QualitySettings.antiAliasing = 8;
        QualitySettings.shadowResolution = ShadowResolution.VeryHigh;
        //URP
        if (renderPipeline != null)
            renderPipelineHQ.shadowDistance = renderPipeline.shadowDistance;
        QualitySettings.renderPipeline = renderPipelineHQ;

        foreach (Tuple<int, int, string> resolution in i_Resolutions)
        {
            path = ScreenShotName(resolution, SavePath, m_SeperateToResolutionFolders);
            GameViewSize.BackupCurrentSize();
            GameViewSize.SelectSize(GameViewSize.SetCustomSize(resolution.Item1 * m_ScreenSizeMultiplier, resolution.Item2 * m_ScreenSizeMultiplier));

            yield return new WaitForSecondsRealtime(.1f);

            if (Application.isPlaying && EditorApplication.isPaused)
                EditorApplication.Step();

            ScreenCapture.CaptureScreenshot(path);

            yield return new WaitForSecondsRealtime(.1f);

            if (Application.isPlaying && EditorApplication.isPaused)
                EditorApplication.Step();

            yield return new WaitForSecondsRealtime(.1f);

            GameViewSize.RestoreSize();
        }

        if (m_ShowLogOnCreation)
            Debug.LogErrorFormat(string.Format("Finished taking {0} screenshots", i_Resolutions.Length));

        if (m_OpenOnFinish && i_Resolutions.Length == 1)
            Application.OpenURL(path);

        EditorApplication.update -= coroutineHandler;


        //Retrieving original values
        eventSystem?.gameObject.SetActive(eventSystemStateOrg);
        //Standard Render Pipeline
        QualitySettings.antiAliasing = antiAliasingOrg;
        QualitySettings.shadowResolution = shadowResolutionOrg;
        //URP
        QualitySettings.renderPipeline = renderPipeline;


        Time.timeScale = 1;
    }

    private UniversalRenderPipelineAsset getURPAssetHQ()
    {
        var guids = AssetDatabase.FindAssets("t:UniversalRenderPipelineAsset", new[] { "Assets" });

        foreach (string guid in guids)
        {
            var assetPath = AssetDatabase.GUIDToAssetPath(guid);
            if (Path.GetFileNameWithoutExtension(assetPath) == "URP_Asset HQ")
                return AssetDatabase.LoadAssetAtPath<UniversalRenderPipelineAsset>(assetPath);
        }

        Debug.LogError("Couldn't find HQ version of URP");
        return null;
    }

    public string ScreenShotName(Tuple<int, int, string> i_Resolution, string i_Path, bool i_SeperateToResolutionFolders)
    {
        string strPath = string.Empty;
        string filename = string.Format("{0}x{1}_{2}.png", i_Resolution.Item1, i_Resolution.Item2, DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));

        if (i_SeperateToResolutionFolders)
        {
            strPath = string.Format("{0}/{1}", i_Path, i_Resolution.Item3);
        }
        else
        {
            strPath = i_Path;
        }

        if (!Directory.Exists(strPath)) Directory.CreateDirectory(strPath);

        strPath = string.Format("{0}/{1}", strPath, filename);

        return strPath;
    }

    [OnInspectorGUI]
    void OnInspector()
    {
        if (m_Camera == null) m_Camera = Camera.main;

        if (m_ScreenWidth == 0 || m_ScreenHeight == 0)
            SetAsGameScreenSize();
    }
#endif
}