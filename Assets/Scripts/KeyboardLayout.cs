using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

// json設定ファイルから各キーの配置に変換するためのクラスです。
// 任意のタイミング（起動時）などに１度実行してください。
public class KeyboardLayout : MonoBehaviour
{
    // キーの配置
    // -> [Text<String>,coord[Position<Vector3>,size<Vector2>]<Object>]
    // public static
    public static Dictionary<string, ArrayList> layout = new Dictionary<string, ArrayList>();
    public static Body body = new Body();

    public class Body
    {
        public float sizeX;
        public float sizeZ;
    }


    void Start()
    {
        // 設定ファイル(json読み込み)
        FileInfo info = new FileInfo(Application.streamingAssetsPath + "/KeyboardLayout.json");
        StreamReader reader = new StreamReader(info.OpenRead());
        string json = reader.ReadToEnd();
        // 構造体に変換
        KeyboardLayoutList data = JsonUtility.FromJson<KeyboardLayoutList>(json);
        // 構造体からキーの配置に格納
        setKeyLayout(data);
    }

    void Update()
    {

    }

    // jsonから取得した構造体を各キーの配置座標に変換します
    void setKeyLayout(KeyboardLayoutList data)
    {
        Dictionary<string, int> dicSize = new Dictionary<string, int>();

        // 本体サイズ設定取得
        body.sizeX = data.bodySize.sizeX;
        body.sizeZ = data.bodySize.sizeZ;

        // キーサイズ設定取得
        foreach (Sizes size in data.sizes)
        {
            dicSize.Add(size.name, size.size);
        }

        // 各キーの処理
        foreach (Row row in data.rows)
        {
            string[] saOffset = row.offset.Split(',');
            Vector3 v3Offset = new Vector3(float.Parse(saOffset[0]), float.Parse(saOffset[1]), -1 * float.Parse(saOffset[2]) -10f);
            Vector3 v3Nowpos = v3Offset;
            float fMargin_x = row.margin_x;

            foreach (Column column in row.columns)
            {
                ArrayList key = new ArrayList();

                string[] saSize = column.size.Split(',');
                // サイズ変換
                Vector3 v3Size = new Vector3(dicSize[saSize[0]], 1,dicSize[saSize[1]]);

                key.Add(v3Nowpos + v3Size/2);
                key.Add(v3Size);

                v3Nowpos += new Vector3(v3Size.x + fMargin_x, 0, 0) ;

                // 反映
                layout.Add(column.text, key);
            }
        }
    }

    // 構造体代わりのクラスここから
    [System.Serializable]
    class KeyboardLayoutList
    {
        public BodySize bodySize;
        public Sizes[] sizes;
        public Row[] rows;
    }
    [System.Serializable]
    class BodySize
    {
        public float sizeX;
        public float sizeZ;
    }
    [System.Serializable]
    class Sizes
    {
        public string name;
        public int size;
    }
    [System.Serializable]
    class Column
    {
        public string text;
        public string size;
    }
    [System.Serializable]
    class Row
    {
        public string num;
        public string offset;
        public float margin_x;
        public Column[] columns;
    }
    // ここまで

}

