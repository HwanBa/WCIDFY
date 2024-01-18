#include <iostream>     // - 입력 & 출력을 위한 헤더
#include <thread>       // - 쓰레드 사용을 위한 헤더

using namespace std;    // - 표준형식 클래스 사용

// - 함수 선언
void PrintTreadID()
{
    // - 결과값 출력
    cout << "스레드 ID : " << this_thread::get_id() << endl;
}

int main()
{
    // - 함수를 스레드로 생성
    thread t1(PrintTreadID);

    // - 스레드가 종료될 떄 까지 대기
    t1.join();

    // - 프로그램 종료
    return 0;
}