using System.Collections;
using System.Collections.Generic;
//#if UNITY_EDITOR 
//using UnityEditor;
//#endif
using UnityEngine;
using UnityEngine.Rendering;

public class GroundItem : MonoBehaviour, ISerializationCallbackReceiver //every time you make an editor change, serialisation happens and calls the before and after methods
{
    public ItemObject item;

    public void OnAfterDeserialize()
    {
        
    }

    public void OnBeforeSerialize()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = item.UIDisplay;
        //EditorUtility.SetDirty(GetComponentInChildren<SpriteRenderer>());
    }
}