using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardUp : MonoBehaviour
{
    [Tooltip("Add this script on Rect Transform")]
    //이 스크립트가 포함된 오브젝트가 키보드 보일 때 위로올라갑니다.
    private RectTransform scrollView;
    private Vector3 pose;
    private float height;

    private void Start()
    {
        scrollView = transform.GetComponent<RectTransform>();
    }

    void Update()
    {
        height = GetKeyboardSize();
        pose = Input.mousePosition;

        if (TouchScreenKeyboard.visible)
        {
            Debug.Log(pose.y);
            if (pose.y < height)
            {
                Debug.Log("height" + height);
                scrollView.anchoredPosition = new Vector3(scrollView.anchoredPosition.x, height);
            }
        }
        else
        {
            scrollView.anchoredPosition = new Vector3(scrollView.anchoredPosition.x, 0);
        }
    }
    public int GetKeyboardSize()
    {
#if UNITY_EDITOR
        return 0;
#else
        using (AndroidJavaClass UnityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            AndroidJavaObject View = UnityClass.GetStatic<AndroidJavaObject>("currentActivity").Get<AndroidJavaObject>("mUnityPlayer").Call<AndroidJavaObject>("getView");

            using (AndroidJavaObject Rct = new AndroidJavaObject("android.graphics.Rect"))
            {
                View.Call("getWindowVisibleDisplayFrame", Rct);

                //return Screen.height - Rct.Call<int>("height");
                return Display.main.systemHeight - Rct.Call<int>("height");
            }
        }
#endif
    }
}
