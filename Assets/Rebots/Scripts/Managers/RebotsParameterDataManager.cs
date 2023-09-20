﻿using UnityEngine;

namespace Rebots.HelpDesk
{
    public class RebotsParameterDataManager : MonoBehaviour
    {
        public RebotsSettingManager rebotsSettingManager;

        [SerializeField] RebotsParameterData m_ParameterData;
        public RebotsParameterData ParameterData { get => m_ParameterData; private set => m_ParameterData = value; }

        void Awake()
        {
        }

        void Start()
        {
            Debug.Log("Game data is loaded and starting user login.");
            this.SetParameterData();

            Debug.Log("Helpdesk UserInitialize start.");
            StartCoroutine(this.rebotsSettingManager.HelpdeskUserInitialize(
                ParameterData.UserAuthKey,
                ParameterData.UserEmail,
                ParameterData.UserName));
        }

        public void PublicCallUserInitialize()
        {
            StartCoroutine(this.rebotsSettingManager.HelpdeskUserInitialize(
                ParameterData.UserAuthKey,
                ParameterData.UserEmail,
                ParameterData.UserName));
        }


        public void SetParameterData()
        {
            ParameterData = new();

            /// Assign user identification value of 'UserInitialize'.
            /// Already assigned value is example of sample project.
            ParameterData.UserAuthKey = SystemInfo.deviceUniqueIdentifier.Substring(0, 14);
            ParameterData.UserName = "userNameTest";
            ParameterData.UserEmail = "userEmail@Test.com";

            /// Inquiry Parameter Key Value Dictionary.
            /// Key: Parameter name of Inquiry category item set in 'RebotsPro Workspase'.
            /// Already added collection is example of sample project.
            ParameterData.parameters.Add("p2myxpm3ff5yx7jr", "v1.12.308");
#if UNITY_ANDROID
            ParameterData.parameters.Add("9f4qnlta9h07zxzv", "OneStore");
            ParameterData.parameters.Add("u8hq7mhcxr07xb29", "Smart Phone");
            ParameterData.parameters.Add("g6qpoi8pbl8o84vj", "Item2");
#elif UNITY_IOS
            ParameterData.parameters.Add("social", "social2");
            ParameterData.parameters.Add("platform", "platform2");
#endif
        }
    }
}
