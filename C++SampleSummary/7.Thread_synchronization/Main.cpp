#include <iostream>     // - 입력 & 출력을 위한 헤더
#include <thread>       // - 쓰레드 사용을 위한 헤더
#include <mutex>        // - 동기화방법 중 하나인 뮤텍스를 사용하기 위한 헤더

using namespace std;    // - 표준형식 클래스 사용

// - 뮤텍스 선언
mutex g_Mutex;

// - 함수A 선언
void NiceToMeetYou()
{
    // - 해당 스레드를 뮤텍스로 잠금처리함
    g_Mutex.lock();

    // - 작업 수행( 예시 - 반복문 )
    for (int i = 0; i < 100; i++)
    {
        cout << "반갑습니다" << endl;
    }

    // - 잠금 해제
    g_Mutex.unlock();
}
// - 함수B 선언
void Hello()
{
    // - 해당 스레드를 뮤텍스로 잠금처리함
    g_Mutex.lock();

    // - 작업 수행( 예시 - 반복문 )
    for (int i = 0; i < 100; i++)
    {
        cout << "안녕하세요" << endl;
    }

    // - 잠금 해제
    g_Mutex.unlock();
}

int main()
{
    // - 함수를 스레드로 생성
    thread t1(NiceToMeetYou);
    thread t2(Hello);

    // - 스레드가 종료될 떄 까지 대기
    t1.join();
    t2.join();

    // - 프로그램 종료
    return 0;
}