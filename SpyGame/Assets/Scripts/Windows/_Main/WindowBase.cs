using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowBase : MonoBehaviour 
{
    public void Init()
    {
        gameObject.SetActive(false);
        OnInit();
    }

    public void Show()
    {
		OnShowStart();
        gameObject.SetActive(true);
		OnShowEnd();
    }

    public void Hide()
    {
		OnHideStart();
        gameObject.SetActive(false);
		OnHideEnd();
    }

    protected virtual void OnInit()
    {
    }

    protected virtual void OnShowStart()
    {
    }

	protected virtual void OnShowEnd()
	{
	}

    protected virtual void OnHideStart()
    {
    }

	protected virtual void OnHideEnd()
	{
	}
}