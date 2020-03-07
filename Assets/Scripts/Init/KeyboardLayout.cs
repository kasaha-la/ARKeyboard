using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
// json設定ファイルから各キーの配置に変換するためのクラスです。
// 任意のタイミング（起動時）などに１度実行してください。
//
// キー情報はstatic変数layoutから取得します。
// layout[keyname<string>,<keyPosition<Vector3>,keySize<Vector3>>]
// の様に格納されているので、keyにkeynameを指定すると位置とサイズを取得できます。(単位:mm）
//
// キーボード自体の大きさはstatic変数bodyから取得します。
// float型のsizeX、sizeZメンバから値を取得できます。（単位:mm）
/// </summary>
public class KeyboardLayout : MonoBehaviour
{
    // <value>
    // キーの配置
    // </value>
    public static Dictionary<string, ArrayList> layout = new Dictionary<string, ArrayList>();
    public static Body body = new Body();

    // <value>
    // キーボード自体のサイズ
    // <value>
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
                // 反映
                layout.Add(column.text, key);

                //次の位置更新
                v3Nowpos += new Vector3(v3Size.x + fMargin_x, 0, 0) ;
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

