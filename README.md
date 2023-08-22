# RebotsProHelpdeskUnitySDK
**Rebots Pro Unity SDK – 헬프데스크 설정 도움말**

Scenes 폴더에서 Rebots 씬으로 테스트할 수 있습니다.

##### 1. 헬프데스크 사용을 위한 Prefabs
  - Rebots
  - RebotsSettingManager


##### 2. 헬프데스크 초기화 설정 (Unity 라이프사이클의 'Awake'에서 실행)

  - Prefabs 'RebotsSettingManager'에 아래 인스펙터 값을 입력합니다.
    - Project Public Name
    - Project Main Key
    - Project Helpdesk Language
   
  - 'Check Language Available'을 클릭하여 콘솔 로그에서 헬프데스크 연결을 확인합니다.

##### 3. 사용자 초기화 설정 (Unity 라이프사이클의 'Start'에서 실행)

  - RebotsParameterDataManager.cs 파일의 <code>SetParameterData()</code> 내부에 필드 값을 할당합니다.


##### 4. 헬프데스크 호출 메서드

  - RebotsHelpdeskUIManager.cs 파일의 <code>RebotsHelpdeskShow()</code>
