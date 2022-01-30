using UnityEngine;

//使い方↓
//https://nn-hokuson.hatenablog.com/entry/2016/12/08/200133
public class SmoothPaint : MonoBehaviour
{
    Texture2D drawTexture;
    Color[] buffer;

    [SerializeField] GameObject board;

    private Vector3 _prevPosition;

    private GameObject raycaster;

    private int size;

    void Start()
    {
        Texture2D mainTexture = (Texture2D)board.GetComponent<Renderer>().material.mainTexture;
        size =  (int)board.GetComponent<Renderer>().material.mainTexture.width;
        Color[] pixels = mainTexture.GetPixels();

        buffer = new Color[pixels.Length];
        pixels.CopyTo(buffer, 0);

        drawTexture = new Texture2D(mainTexture.width, mainTexture.height, TextureFormat.RGBA32, false);
        drawTexture.filterMode = FilterMode.Point;
        raycaster = this.transform.Find("raycaster").gameObject;
    }

	public void Draw(Vector2 p)
	{
		for (int x = 0; x < size; x++){
			for (int y = 0; y < size; y++){
				if ((p - new Vector2 (x, y)).magnitude < 5){
					buffer.SetValue (Color.black, x + size * y);
				}
			}
		}
    }

    void OnTriggerEnter(Collider other){
        Debug.Log("exit");
        _prevPosition = Vector3.zero;
    }
    void OnTriggerStay(Collider other)
    {
        #region Use_textureCoord
        if (other.gameObject.tag == "Board") 
		{
            if (_prevPosition == Vector3.zero)
            {
                _prevPosition = raycaster.transform.position;
            }

            Vector3 endPosition = raycaster.transform.position;
            //1フレームの線の距離
            float lineLength = Vector3.Distance(_prevPosition, endPosition);
            //線の長さに応じて変わる補間値　CeilToIntは小数点以下を切り上げ
            float lerpCountAdjustNum = 0.05f;
            int lerpCount = Mathf.CeilToInt(lineLength / lerpCountAdjustNum);
            Debug.Log(lerpCount);
            for (int i = 1; i <= lerpCount; i++)
            {
                //Lerpの割合値を "現在の回数/合計回数" で出す
                float lerpWeight = (float) i / lerpCount;

                //前回の入力座標、現在の入力座標、割合を渡して補間する座標を算出
                Vector3 lerpPosition = Vector3.Lerp(_prevPosition, endPosition, lerpWeight);
                Debug.DrawRay(lerpPosition ,transform.up, Color.red, 0.5f);
                RaycastHit hit;
                int mask = 1 << 7;
                if (Physics.Raycast(lerpPosition, transform.up, out hit, 0.5f, mask)) {
                    Debug.Log(hit.textureCoord);
				    Draw (hit.textureCoord * size);
			    }
                drawTexture.SetPixels(buffer);
                drawTexture.Apply();
                board.GetComponent<Renderer>().material.mainTexture = drawTexture;
            }

            //前回の入力座標を記録
            _prevPosition = endPosition;

            
		}
        
        #endregion Use_textureCoord
    }

    
    
    
}