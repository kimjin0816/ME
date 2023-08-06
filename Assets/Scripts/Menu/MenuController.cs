using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject Panel_menu = null;                // 메뉴 패널을 나타내는 게임 오브젝트
    public GameObject Toggle1 = null;                   // SFX 토글을 나타내는 게임 오브젝트
    public GameObject Toggle2 = null;                   // BGM 토글을 나타내는 게임 오브젝트
    public Image[] OnImage;                             // 활성화 이미지 배열
    public Image[] OffImage;                            // 비활성화 이미지 배열

    private bool isMuted1 = false;                      // SFX 토글의 음소거 여부
    private bool isMuted2 = false;                      // BGM 토글의 음소거 여부

    public SceneFader sceneFader; // SceneFader 스크립트에 대한 참조

    public void Click_Menu()     // 메뉴 버튼을 클릭했을 경우
    {
        Time.timeScale = 0;                             // 게임 시간을 일시적으로 멈춥니다.
        Panel_menu.SetActive(true);                     // 메뉴 패널을 활성화합니다.
    }

    public void Click_Continue()  // 메뉴패널의 CONTINUE 버튼 클릭했을 경우
    {
        Time.timeScale = 1;                             // 게임 시간을 다시 시작합니다.
        Panel_menu.SetActive(false);                    // 메뉴 패널을 비활성화합니다.
    }

    public void Click_Exit() // 메뉴패널의 QUIT 버튼 클릭했을 경우
    {
        Scenechange();                                  // Scenechange 함수를 호출하여 타이틀 씬으로 전환합니다.
    }

    private void Scenechange() // 씬 전환 함수
    {
        StartCoroutine(ChangeSceneWithFade("TitleScene"));           // "TitleScene"을 로드하여 씬을 전환합니다.
    }

    void Start()
    {
        Panel_menu.SetActive(false);                    // 메뉴 패널을 처음에는 비활성화합니다.
        UpdateMuteImages();                             // 음소거 이미지를 업데이트합니다.
    }

    public void ToggleMute1() // SFX토글의 음소거 클릭했을경우
    {
        isMuted1 = !isMuted1;                           // SFX 토글의 음소거 이미지를 반전시킵니다.
        UpdateMuteImages();                             // 음소거 이미지를 업데이트합니다.
    }

    public void ToggleMute2() // BGM토글의 음소거 클릭했을경우
    {
        isMuted2 = !isMuted2;                           // BGM 토글의 음소거 이미지를 반전시킵니다.
        UpdateMuteImages();                             // 음소거 이미지를 업데이트합니다.
    }

    private void UpdateMuteImages() // 음소거 이미지 변경 함수
    {
        OnImage[0].gameObject.SetActive(!isMuted1);     // SFX 토글의 활성화 이미지를 설정합니다.
        OffImage[0].gameObject.SetActive(isMuted1);     // SFX 토글의 비활성화 이미지를 설정합니다.
        OnImage[1].gameObject.SetActive(!isMuted2);     // BGM 토글의 활성화 이미지를 설정합니다.
        OffImage[1].gameObject.SetActive(isMuted2);     // BGM 토글의 비활성화 이미지를 설정합니다.
    }

    private IEnumerator ChangeSceneWithFade(string sceneName)
    {
        yield return StartCoroutine(FadeOut());
        SceneManager.LoadScene(sceneName);
    }

    // 페이드 아웃 효과를 위한 코루틴
    private IEnumerator FadeOut()
    {
        sceneFader.StartFadeOut("TitleScene");
        yield return new WaitForSeconds(sceneFader.fadeDuration);
    }
}