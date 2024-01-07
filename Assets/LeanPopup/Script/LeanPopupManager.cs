using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeanPopupManager : MonoBehaviour
{
    public static LeanPopupManager intance
    {
        get{
            if(_intance==null)
            {
                GameObject temp=new GameObject();
                temp.name="LeanPopupManager";   
                _intance=temp.AddComponent<LeanPopupManager>();
            }
            return _intance;
        }
    }
    static LeanPopupManager _intance=null;
    public List<LeanPopup> allPopup;
    private void OnEnable() {
        if(_intance==null)
        {
            _intance=this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    public void RegisterPopup(LeanPopup popup)
    {
        if(allPopup==null)
        {
            allPopup=new List<LeanPopup>();
        }
        
        if(!allPopup.Contains(popup))
            allPopup.Add(popup);
        allPopup.RemoveAll(x=>x==null);
    }
}
