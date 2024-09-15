﻿// Copyright (c) 2015 - 2019 Imba
// Author: Kaka
// Created: 2019/08

using DG.Tweening;
using Imba.Utils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Imba.UI
{
    /// <summary>
    /// UIManager: Manage all UI element include Popups, Views, Notices, Alert ...
    /// </summary>
    public class UIManager : ManualSingletonMono<UIManager>
    {
        public Camera         UICamera;
        public UIViewManager  ViewManager;
        public UIPopupManager PopupManager;
        public RectTransform  LoadingObject;
        public UIAlertManager AlertManager;

        public RectTransform CanvasRect;

        //public CanvasScaler mainCanvas;
        //public static float ratioCustomizeItem = 170 / (16 / 9);
        //public LevelManager LevelSystemManager;
        //public RectTransform PopupContainer;
        //public UITooltip tooltip;
        //public GameObject objBugReport;
        [Header("Transition")]
        public RectTransform transitionRect;

        public RectTransform transitionRender;

        public bool IsShowingLoading
        {
            get
            {
                if (LoadingObject)
                {
                    return LoadingObject.gameObject.activeSelf;
                }

                return false;
            }
        }

        public override void Awake()
        {
            base.Awake();
            if (!ViewManager)
            {
                ViewManager = GetComponentInChildren<UIViewManager>();
            }

            if (!PopupManager)
            {
                PopupManager = GetComponentInChildren<UIPopupManager>();
            }

            if (!AlertManager)
            {
                AlertManager = GetComponentInChildren<UIAlertManager>();
            }

            if (LoadingObject)
            {
                LoadingObject.gameObject.SetActive(false);
            }

            // mainCanvas.referenceResolution = new Vector2(Screen.width, Screen.height);


#if DISABLE_LOG
			Debug.unityLogger.filterLogType = LogType.Error;
#endif
        }

        void Start()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
        }

        public void ShowLoading(float timeToHide = 0)
        {
            //Debug.Log ("ShowLoading");
            LoadingObject.gameObject.SetActive(true);
            if (timeToHide <= 0) CancelInvoke("HideLoadingCallback");
            else Invoke("HideLoadingCallback", timeToHide);
        }

        public void HideLoading()
        {
            CancelInvoke("HideLoadingCallback");
            LoadingObject.gameObject.SetActive(false);
        }

        public void ShowTransition(UnityAction onDone)
        {
            transitionRect.SetActive(true);
            transitionRender.localScale = Vector3.zero;
            transitionRender.DOScale(Vector3.one * 8, 0.5f).OnComplete(() => { onDone?.Invoke(); }).SetEase(Ease.Linear);
        }

        public void HideTransition(UnityAction onDone)
        {
            transitionRender.DOScale(Vector3.zero, 0.5f).OnComplete(() =>
            {
                transitionRect.SetActive(false);
                onDone?.Invoke();
            }).SetEase(Ease.Linear);
        }

        void HideLoadingCallback()
        {
            LoadingObject.gameObject.SetActive(false);
        }

        public bool ShowDebugLog = true;

        public static void DebugLog<T>(string message, T com)
        {
#if UNITY_EDITOR
            if (!Instance || !Instance.ShowDebugLog) return;

            string msg = string.Format("[{0}] {1}", com != null ? com.GetType().ToString() : "", message);
            Debug.Log(string.Format("<color=blue>[UIManager][{0}] {1}</color>", com.GetType(), message));
#endif
        }
    }
}