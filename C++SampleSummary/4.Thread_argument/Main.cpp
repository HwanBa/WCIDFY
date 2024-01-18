#include <iostream>     // - 입력 & 출력을 위한 헤더
#include <thread>       // - 쓰레드 사용을 위한 헤더

using namespace std;    // - 표준형식 클래스 사용

// - 함수 선언
void NiceToMeetYou(int repeat)
{
    // - 작업 수행( 예시 - 반복문 )
    for (int i = 0; i < repeat; i++)
    {
        // - 결과값 출력
        cout << "반갑습니다" << endl;
    }
}

int main()
{
    // - 인자값 생성
    int repeat = 5;

    // - 함수를 스레드로 생성
    thread t1(NiceToMeetYou, repeat);

    // - 스레드가 종료될 떄 까지 대기
    t1.join();

    // - 프로그램 종료
    return 0;
}