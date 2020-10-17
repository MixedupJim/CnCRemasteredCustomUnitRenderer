using System.IO;
using UnityEditor;
using UnityEngine;

public class TankRenderer : MonoBehaviour
{
    public Camera unitCamera;

    public GameObject hull;
    public GameObject turret;
    public GameObject shadowPlane;
    public GameObject turretShadowPlane;

    private const float numHullRotations = 32;
    private const float numTurretRotations = 32;
    private const float rotationPerFrame = 11.25f;

    private int frameNumber = 0;
    public string unitName = "mtnk";

    private string extraZeros;
    public string outputPath; // The path of the folder you want to send all of the files to

    void Start()
    {
        RenderFrameSet();
        EditorApplication.isPlaying = false;

        if (outputPath == null)
        {
            outputPath = Application.dataPath;
        }
    }

    private void RenderFrameSet()
    {
        turret.SetActive(false);
        turretShadowPlane.SetActive(false);

        // Render hull frames
        while (frameNumber < numHullRotations)
        {
            SaveScreenShot();
            WriteMetaFile();
            frameNumber++;
            RotateVehicle(rotationPerFrame);
        }

        hull.SetActive(false);
        shadowPlane.SetActive(false);

        turret.SetActive(true);
        turretShadowPlane.SetActive(true);

        // Render turret frames
        while (frameNumber < numHullRotations + numTurretRotations)
        {
            SaveScreenShot();
            WriteMetaFile();
            frameNumber++;
            RotateVehicle(rotationPerFrame);
        }
    }

    private void WriteMetaFile()
    {
        string input = "{\"size\":[" + 192 + "," + 192 + "],\"crop\":[" + 0 + "," + 0 + "," + 192 + "," + 192 + "]}";
        File.WriteAllText(Path.Combine(outputPath, unitName + "-" + extraZeros + frameNumber + ".meta"), input);
    }

    private void RotateVehicle(float rotationAmount)
    {
        transform.rotation *= Quaternion.AngleAxis(-rotationAmount, Vector3.up);
    }

    private void SaveScreenShot()
    {
        //apply number formatting for CnC
        if (frameNumber < 10)
        {
            extraZeros = "000";
        };

        if (frameNumber > 9 && frameNumber < 100)
        {
            extraZeros = "00";
        };

        //save file out to path as TGA
        File.WriteAllBytes(Path.Combine(outputPath, unitName + "-" + extraZeros + frameNumber + ".tga"), TakeScreenShot().EncodeToTGA());
    }

    private Texture2D TakeScreenShot()
    {
        //get camera width and height
        int cameraWidth = unitCamera.pixelWidth;
        int cameraHeight = unitCamera.pixelHeight;

        //set up the render texture to receive the camera image
        var renderTexture = new RenderTexture(cameraWidth, cameraHeight, 32);

        //set camera to use the render texture
        unitCamera.targetTexture = renderTexture;

        //set up the texture
        var screenShot = new Texture2D(cameraWidth, cameraHeight, TextureFormat.ARGB32, false);

        //this handles the alpha channel
        unitCamera.clearFlags = CameraClearFlags.SolidColor;
        unitCamera.backgroundColor = new UnityEngine.Color(0, 0, 0, 0);

        //tell the camera to render immediately
        unitCamera.Render();

        //output texture
        RenderTexture.active = renderTexture;
        screenShot.ReadPixels(new Rect(0, 0, cameraWidth, cameraHeight), 0, 0);
        screenShot.Apply();

        //clean up
        unitCamera.targetTexture = null;
        RenderTexture.active = null;
        DestroyImmediate(renderTexture);

        return screenShot;
    }
}

