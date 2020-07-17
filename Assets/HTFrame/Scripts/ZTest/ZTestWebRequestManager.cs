using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZTestWebRequestManager : MonoBehaviour
{
    // Start is called before the first frame update
    WebRequestTexture webRequestTexture;
    private string operationName;
    private string path = "https://note.youdao.com/yws/public/resource/484dd6cee942bbc4fce5878b1c773adb/xmlnote/WEBRESOURCE5725321e271841a52f8f6be1bfd0824d/26355";
    void Start()
    {
        operationName = "a1.png";
        webRequestTexture = new WebRequestTexture(WebRequestType.Texture, path, operationName, texture =>
        {
            if (texture != null)
            {
                Debug.Log(texture.GetType().ToString());
            }
        });
        Main.webRequestManager.Register(webRequestTexture);

        Main.netWorkManager.ResgisterMessage(1,message=>{
            TCPNetworkMessage data = message as TCPNetworkMessage;
        });

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Main.webRequestManager.SendRequest(webRequestTexture.webRequestType, operationName);
        }
    }
}
