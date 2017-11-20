using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMoveScript : MonoBehaviour {
    public OSC osc;
	public float fovDegrees=30;//horizontal fanning. Determining this accurately is important in correct measurements
	public float screenWidth=30;//in cm
	public float screenHeight = 17;//in cm
	public float numerator = 200;
    private int numOfScenes = 3;
	public float[] captureSize = { 640, 320 };
    private bool isAutoMove=false;
    private Vector3 origin;
    
    private bool isReceiving = false;
	Vector3 bottomLeft;
	Vector3 bottomRight;
	Vector3 topLeft;
    private float theta;
    private float rawX, rawY, rawScale;
    private Vector3 facePos = new Vector3(0, 0, -50);

    private float focus;

    private bool wasOpen = false; //to check if in previous data, mouth was open.
    private float openStartTime;

    private float speed = 30f;

    public GameObject cameraBox;

    GUIStyle style = new GUIStyle(GUIStyle.none);


    // Use this for initialization
    void Start () {

        osc.SetAddressHandler("/pose/position", OnReceiveFace);
        osc.SetAddressHandler("/pose/scale", onReceiveScale);

        theta = fovDegrees * (3.14f / 180);//horizontal fanning of camera in radians


		bottomLeft = new Vector3 (-screenWidth / 2, -screenHeight / 2, 0);
		bottomRight = new Vector3 (screenWidth / 2, -screenHeight / 2, 0);
		topLeft = new Vector3 (-screenWidth / 2, screenHeight / 2, 0);

        focus = captureSize[0] / 2 / Mathf.Tan(theta / 2);

        // remembers the original position of the camera box, so it can be reset later
        origin = cameraBox.transform.position;
    }
    
    private void OnGUI()
    {
        //TODO: show "settings" button
    }


    // Update is called once per frame
    void Update()
    {
        UpdateFacePos();
        Camera cam = Camera.main;

        Matrix4x4 pm = GeneralizedPerspectiveProjection(bottomLeft, bottomRight, topLeft, facePos, cam.nearClipPlane, cam.farClipPlane);
        transform.localPosition = facePos;
        cam.projectionMatrix = pm;

        if (Input.GetKeyDown(KeyCode.M))
            isAutoMove = !isAutoMove;

        if (Input.GetKeyDown(KeyCode.Space))
            cameraBox.transform.position = origin;

        if (isAutoMove)
        {
            if (facePos.x > 50)
                cameraBox.transform.Translate(cameraBox.transform.right * Time.deltaTime * speed);
            else if (facePos.x < -50)
                cameraBox.transform.Translate(-1 * cameraBox.transform.right * Time.deltaTime * speed);
            if (facePos.z > (-70))
                cameraBox.transform.Translate(cameraBox.transform.forward * Time.deltaTime * speed);
            else if (facePos.z < (-120))
                cameraBox.transform.Translate(-1 * cameraBox.transform.forward * Time.deltaTime * speed);
        }

        if (Input.GetKey(KeyCode.RightArrow))
            cameraBox.transform.Translate(cameraBox.transform.right*Time.deltaTime*speed);
        if (Input.GetKey(KeyCode.LeftArrow))
            cameraBox.transform.Translate(-1* cameraBox.transform.right* Time.deltaTime * speed);
        if (Input.GetKey(KeyCode.UpArrow))
            cameraBox.transform.Translate(cameraBox.transform.forward * Time.deltaTime * speed);
        if (Input.GetKey(KeyCode.DownArrow))
            cameraBox.transform.Translate(-1 * cameraBox.transform.forward * Time.deltaTime * speed);

        //マウス
        float mouseWheelScroll = Input.GetAxis("Mouse ScrollWheel");
        cameraBox.transform.Translate(this.transform.forward * (20) * mouseWheelScroll);

        if (Input.GetMouseButton(1))
        {
            cameraBox.transform.Translate(transform.right * speed * Time.deltaTime);
        }
        else if (Input.GetMouseButton(0))
        {
            cameraBox.transform.Translate(-1*transform.right * speed * Time.deltaTime);
        }
    }


        void OnReceiveFace(OscMessage message)
    {
        rawX = message.GetFloat(0);
        rawY = message.GetFloat(1);
    }

    void onReceiveScale(OscMessage message)
    {
        rawScale = message.GetFloat(0);

        //make this code smarter
        //to determine if it's actually been receiving data for a while
        isReceiving = true;
    }
    
    void UpdateFacePos()
    {
        if (!isReceiving)
        {
            print("is not receiving face data from faceOSC");
            return;
        }

        //isAutoMove = true;
        float distance = numerator / rawScale;
        facePos.x = -(rawX - captureSize[0] / 2) * distance / focus;
        facePos.y = -(rawY - captureSize[1] / 2) * distance / focus+screenHeight/2;
        facePos.z = -distance;

        print(facePos);
    }
	public static Matrix4x4 GeneralizedPerspectiveProjection(Vector3 pa, Vector3 pb, Vector3 pc, Vector3 pe, float near, float far){
		Vector3 va, vb, vc;
		Vector3 vr, vu, vn;

		float left, right, bottom, top, eyedistance;

		Matrix4x4 transformMatrix;
		Matrix4x4 projectionM;
		Matrix4x4 eyeTranslateM;
		Matrix4x4 finalProjection;
		vr = pb - pa;
		vr.Normalize ();
		vu = pc - pa;
		vu.Normalize ();
		vn = Vector3.Cross (vr, vu);
		vn.Normalize ();

		va = pa - pe;
		vb = pb - pe;
		vc = pc - pe;
		eyedistance = (Vector3.Dot (va, vn));

		left = (Vector3.Dot (vr, va) * near) / eyedistance;
		right = (Vector3.Dot (vr, vb) * near) / eyedistance;
		bottom = (Vector3.Dot (vu, va) * near) / eyedistance;
		top = (Vector3.Dot (vu, vc) * near) / eyedistance;
		projectionM = PerspectiveOffCenter (left, right, bottom, top, near, far);

		transformMatrix = new Matrix4x4 ();
		transformMatrix [0, 0] = vr.x;
		transformMatrix [0, 1] = vr.y;
		transformMatrix [0, 2] = vr.z;
		transformMatrix [0, 3] = 0;
		transformMatrix [1, 0] = vu.x;
		transformMatrix [1, 1] = vu.y;
		transformMatrix [1, 2] = vu.z;
		transformMatrix [1, 3] = 0;
		transformMatrix [2, 0] = vn.x;
		transformMatrix [2, 1] = vn.y;
		transformMatrix [2, 2] = vn.z;
		transformMatrix [2, 3] = 0;
		transformMatrix [3, 0] = 0;
		transformMatrix [3, 1] = 0;
		transformMatrix [3, 2] = 0;
		transformMatrix [3, 3] = 1f;

		eyeTranslateM = new Matrix4x4 ();
		eyeTranslateM [0, 0] = 1;
		eyeTranslateM [0, 1] = 0;
		eyeTranslateM [0, 2] = 0;
		eyeTranslateM [0, 3] = -pe.x;
		eyeTranslateM [1, 0] = 0;
		eyeTranslateM [1, 1] = 1;
		eyeTranslateM [1, 2] = 0;
		eyeTranslateM [1, 3] = -pe.y;
		eyeTranslateM [2, 0] = 0;
		eyeTranslateM [2, 1] = 0;
		eyeTranslateM [2, 2] = 1;
		eyeTranslateM [2, 3] = -pe.z;
		eyeTranslateM [3, 0] = 0;
		eyeTranslateM [3, 1] = 0;
		eyeTranslateM [3, 2] = 0;
		eyeTranslateM [3, 3] = 1f;

		finalProjection = new Matrix4x4 ();
		finalProjection = Matrix4x4.identity * projectionM * transformMatrix;//* eyeTranslateM;

		//return finalProjection;
		return finalProjection;

	}

	static Matrix4x4 PerspectiveOffCenter(float left, float right, float bottom, float top, float near, float far)
	{
		float x = 2.0F * near / (right - left);
		float y = 2.0F * near / (top - bottom);
		float a = (right + left) / (right - left);
		float b = (top + bottom) / (top - bottom);
		float c = -(far + near) / (far - near);
		float d = -(2.0F * far * near) / (far - near);
		float e = -1.0F;
		Matrix4x4 m = new Matrix4x4();
		m[0, 0] = x;
		m[0, 1] = 0;
		m[0, 2] = a;
		m[0, 3] = 0;
		m[1, 0] = 0;
		m[1, 1] = y;
		m[1, 2] = b;
		m[1, 3] = 0;
		m[2, 0] = 0;
		m[2, 1] = 0;
		m[2, 2] = c;
		m[2, 3] = d;
		m[3, 0] = 0;
		m[3, 1] = 0;
		m[3, 2] = e;
		m[3, 3] = 0;
		return m;
	}

    //tried to have the window settings to be user-definable, but I don't have time now.
    //    private Rect windowrect = new Rect(0, 0, 120, 120);
    //   private int fov;
    //    private int captureWidth;
    //    private int captureHeight;
    //    private int screenWidth2;
    //    private int screenHeight2;
    //    private int numerator2;
    //    void OnGUI()
    //    {
    //       windowrect = GUI.Window(0, windowrect, DoMyWindow, "configuration");
    //
    //    }

    //    void DoMyWindow()
    //    {
    //        GUI.Label(new Rect(0, 0, 60, 20), "fov");
    //        GUI.TextField(new Rect(60,0,60,20), )
    //    }



}
