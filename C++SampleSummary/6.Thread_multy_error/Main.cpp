#include <iostream>     // - 입력 & 출력을 위한 헤더
#include <thread>       // - 쓰레드 사용을 위한 헤더

using namespace std;    // - 표준형식 클래스 사용

// - 전역 변수 선언
int g_sum = 0;

// - 함수A 선언
void Add()
{
    // - 작업 수행( 예시 - 반복문 )
    for (int i = 0; i < 100000; i++)
    {
        g_sum++;
    }
}
// - 함수B 선언
void Sub()
{
    // - 작업 수행( 예시 - 반복문 )
    for (int i = 0; i < 100000; i++)
    {
        g_sum--;
    }
}

int main()
{
    // - 함수를 스레드로 생성
    thread t1(Add);
    thread t2(Sub);

    // - 스레드가 종료될 떄 까지 대기
    t1.join();
    t2.join();

    // - 결과물 출력
    cout << g_sum << endl;

    // - 프로그램 종료
    return 0;
}