# RebotsProHelpdeskUnitySDK (KR)

<code>Assets/Scenes/Rebots.Unity</code> 파일로 테스트 할 수 있습니다.

##### 헬프데스크 사용을 위한 프리팹
  - <code>Assets/Prefabs/Rebots/Rebots.prefab</code>
  - <code>Assets/Prefabs/Rebots/RebotsSettingManager.prefab</code>

## Rebots Pro Unity SDK – 헬프데스크 설정 도움말

##### 1. 헬프데스크 UI 사용 설정
  - <code>Assets/Prefabs/Rebots/Rebots.prefab</code> :
    - **Hierarchy** 패널 내에서 <code>HelpdeskScreen - GameObject</code>를 선택합니다.
    - **Inspector** 패널 내에서 <code>Rebots Manager</code> 항목에 <code>RebotsSettingManager - GameObject</code>를 지정합니다.

##### 2. 헬프데스크 초기화 설정 (Unity 라이프사이클의 'Awake'에서 실행)
  - <code>Assets/Prefabs/Rebots/RebotsSettingManager.prefab</code> :
    - **Inspector** 패널에 <code>RebotsSettingManager.cs</code> 파일을 필요로 합니다.
    - **Inspector** 패널 내에서 프로젝트 정보 항목을 입력합니다.
    - <code>Project Main Key</code> - *Rebots Pro Workspace에서 설정한 정보입니다.*
    - <code>Rebots Translation File</code> - *<code>Assets/Rebots/Localization/rebots_translation_utf.csv</code> 파일을 필요로 합니다.*
    - <code>Helpdesk Language</code> - *Rebots Pro Workspace에서 설정한 정보입니다.*
  - <code>Check Language Available</code> 버튼을 클릭하여 콘솔 로그에서 헬프데스크 연결을 확인합니다.

##### 3. 사용자 초기화 설정 (Unity 라이프사이클의 'Start'에서 실행)
  - <code>Assets/Prefabs/Rebots/RebotsSettingManager.prefab</code> :
    - **Inspector** 패널에 <code>RebotsParameterDataManager.cs</code> 파일을 필요로 합니다.
    - <code>RebotsParameterDataManager.cs</code> 파일 <code>SetParameterData()</code> 내부에 사용자(player) 값을 할당합니다. - *현재 예시 값이 할당되어 있습니다.*

##### 4. 헬프데스크 호출 메서드
  - <code>RebotsHelpdeskUIManager.cs</code> 파일의 <code>RebotsHelpdeskShow();</code>.

---
# RebotsProHelpdeskUnitySDK (EN)

You can test with <code>Assets/Scenes/Rebots.Unity</code> file.

##### Prefab for Helpdesk use
  - <code>Assets/Prefabs/Rebots/Rebots.prefab</code>
  - <code>Assets/Prefabs/Rebots/RebotsSettingManager.prefab</code>

## Rebots Pro Unity SDK – Helpdesk setup help

##### 1. Settings for using the Helpdesk UI
  - <code>Assets/Prefabs/Rebots/Rebots.prefab</code> :
    - Select <code>HelpdeskScreen - GameObject</code> from the **Hierarchy** panel.
    - Inside the **Inspector** panel, assign the <code>RebotsSettingManager - GameObject</code> to the <code>Rebots Manager</code> property.

##### 2. Helpdesk Initailize Setting (Running in 'Awake' of Unity lifecycle)
  - <code>Assets/Prefabs/Rebots/RebotsSettingManager.prefab</code> :
    - The **Inspector** panel requires the <code>RebotsSettingManager.cs</code> file.
    - Inside the **Inspector** panel, assign the property.
    - <code>Project Main Key</code> - *Information set in Rebots Pro Workspace.*
    - <code>Rebots Translation File</code> - *Requires the <code>Assets/Rebots/Localization/rebots_translation_utf.csv</code> file.*
    - <code>Helpdesk Language</code> - *Information set in Rebots Pro Workspace.*
  - Click the <code>Check Language Available</code> button to check the helpdesk connection in the console log.

##### 3. User Initialize Setting (Running in 'Start' of Unity lifecycle)
  - <code>Assets/Prefabs/Rebots/RebotsSettingManager.prefab</code> :
    - The **Inspector** panel requires the <code>RebotsParameterDataManager.cs</code> file.
    - Assign the User(player) value inside the <code>RebotsParameterDataManager.cs</code> file <code>SetParameterData()</code>. - *An example value is currently assigned.*

##### 4. Helpdesk call method
  - <code>RebotsHelpdeskShow();</code> in the <code>RebotsHelpdeskUIManager.cs</code> file.
