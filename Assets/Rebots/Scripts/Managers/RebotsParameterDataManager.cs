using UnityEngine;

namespace Rebots.HelpDesk
{
    public class RebotsParameterDataManager : MonoBehaviour
    {
        [SerializeField] RebotsParameterData m_ParameterData;
        public RebotsParameterData ParameterData { get => m_ParameterData; private set => m_ParameterData = value; }

        public void SetParameterData()
        {
            Debug.Log("Game data is loaded and starting user login.");
            ParameterData = new();

            /// ---필수(Required)--------------------------------------------------
            /// 'UserInitialize' 실행을 위한 사용자 식별 값을 지정합니다.
            /// 이미 할당된 값은 샘플 프로젝트의 예시입니다.
            /// 
            /// en) Assign user identification value for running 'UserInitialize'.
            /// Already assigned value is example of sample project.
            ParameterData.UserAuthKey = SystemInfo.deviceUniqueIdentifier.Substring(0, 14);
            ParameterData.UserName = "userNameTest";
            ParameterData.UserEmail = "userEmail@Test.com";
            /// -------------------------------------------------------------------

            /// ---선택(Option)----------------------------------------------------
            /// 문의 파라미터 Key-Value Dictionary.
            /// 문의 입력 화면에서 key-value를 설정한 파라미터 항목에 value를 자동 할당합니다.
            /// Key: RebotsPro 웹 솔루션(Workspace) 'CS 티켓 설정 > 공통 카테고리' 메뉴에서 추가한 '문의 항목'의 파라미터 key입니다.
            /// 이미 추가된 컬렉션은 샘플 프로젝트의 예시입니다.
            /// 
            /// en) Inquiry Parameter Key-Value Dictionary.
            /// The value is automatically assigned to the parameter item for which the key-value is set in the inquiry create page.
            /// Key: This is the parameter key of 'inquiry items' added in the 'CS Ticket Settings > Common Category' menu of RebotsPro Web Solution (Workspace).
            /// Already added collection is example of sample project.
            ParameterData.parameters.Add("p2myxpm3ff5yx7jr", "v1.2.08");
#if UNITY_ANDROID
            ParameterData.parameters.Add("9f4qnlta9h07zxzv", "Google Play");
#elif UNITY_IOS
            ParameterData.parameters.Add("9f4qnlta9h07zxzv", "App Store");
#endif
            /// -------------------------------------------------------------------
        }
    }
}
