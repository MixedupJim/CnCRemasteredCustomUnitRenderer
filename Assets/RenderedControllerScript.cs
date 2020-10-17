using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class RenderedControllerScript : MonoBehaviour
{
	public string unitName;
	public int startingImageIndex = 0;
	public Camera camera;
	
	private GameObject cameraPivot;
	private int shotNumber = -1;
	private int maxShotNumber = 32;
	private string extraZeros;

	// Script example is from here.
    // https://entitycrisis.blogspot.com/2017/02/take-unity-screenshot-with-alpha.html
    void Start()
    {
        cameraPivot = GameObject.Find("CameraPivot");
		maxShotNumber = maxShotNumber + startingImageIndex;
		shotNumber = startingImageIndex;
	
    }

    // Update is called once per frame
    void Update()
    {
			
		if (shotNumber < maxShotNumber) {
			takeScreenShot();
			saveScreenshot();
			shotNumber++;
			rotateCamera(11.25f);
		};
		
    }
			
	void rotateCamera(float stepSize)
	{
		//rotates the camera by the stepSize angle
		cameraPivot.transform.localEulerAngles = cameraPivot.transform.localEulerAngles + new Vector3(0.0f,stepSize,0.0f);
		
	}
	
	Texture2D takeScreenShot()
    {
        //get camera width and height
        int cameraWidth = camera.pixelWidth;
        int cameraHeight = camera.pixelHeight;
		
		//set up the render texture to receive the camera image
        var renderTexture = new RenderTexture (cameraWidth, cameraHeight, 32);
		
		//set camera to use the render texture
        camera.targetTexture = renderTexture;
		
		//set up the texture
        var screenShot = new Texture2D (cameraWidth, cameraHeight, TextureFormat.ARGB32, false);
        var clearFlags = camera.clearFlags;

		//this handles the alpha channel
        camera.clearFlags = CameraClearFlags.SolidColor;
        camera.backgroundColor = new Color (0, 0, 0, 0);

		//tell the camera to render immediately
        camera.Render();
		
		//output texture
        RenderTexture.active = renderTexture;
        screenShot.ReadPixels (new Rect (0, 0, cameraWidth, cameraHeight), 0, 0);
        screenShot.Apply();
		
		//clean up
        camera.targetTexture = null;
        RenderTexture.active = null;
        DestroyImmediate(renderTexture);
        camera.clearFlags = clearFlags;
        return screenShot;
    }
	
	public void saveScreenshot()
    {
			//apply number formatting for CnC
			if (shotNumber < 10) {
				
				extraZeros = "000";
				
			};
			
			if (shotNumber > 9 && shotNumber < 100) {
				
				extraZeros = "00";
				
			};
			
		//save file out to application root folder as TGA
        File.WriteAllBytes (Path.Combine (Application.dataPath + "/../", unitName + "-" + extraZeros + shotNumber + ".tga"), takeScreenShot().EncodeToTGA());
    }
}
