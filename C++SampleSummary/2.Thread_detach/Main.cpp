#include <iostream>     // - 입력 & 출력을 위한 헤더
#include <thread>       // - 쓰레드 사용을 위한 헤더

using namespace std;    // - 표준형식 클래스 사용

// - 함수 선언
void NiceToMeetYou()
{
    // - 작업 수행( 예시 - 반복문 )
    for (int i = 0; i < 10; ++i)
    {
        // - 결과값 출력
        cout << "반갑습니다" << endl;
    }
}

int main()
{
    // - 함수를 스레드로 생성
    thread t1(NiceToMeetYou);

    // - 메인 프로그램이 종료면 스레드도 종료
    t1.detach();

    // - 메인 작업 수행
    for (int i = 0; i < 10; i++)
    {
        cout << "안녕하세요" << endl;
    }

    // - 프로그램 종료
    cout << "메인 작업 종료" << endl;
    return 0;
}