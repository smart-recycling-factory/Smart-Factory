# 2024_miniproject
  ## smart factory

## 공지사항
- 깃 작성 방법
  - [깃 컨벤션 참조1](https://velog.io/@shin6403/Git-git-%EC%BB%A4%EB%B0%8B-%EC%BB%A8%EB%B2%A4%EC%85%98-%EC%84%A4%EC%A0%95%ED%95%98%EA%B8%B0)
  - [깃 컨벤션 참조2](https://hyunjun.kr/21)
  - [리드미 작성 참조](https://annajin.tistory.com/189)

## 1일차 (2024-07-02)
### 목표와 소자별 기능 소개
- 목표 
  - 다양한 센서를 통한 제품의 분류

- 동작 방식
  - 적외선 센서를 통해 컨베이어 벨트에 물품이 올려진 경우 이를 인식, DC모터의 동작을 진행
  
  - 아두이노에 연결된 센서를 통하여 분류를 진행
    - 1차 분류 : 조도 센서를 통하여 플라스틱, 금속, 종이 중 플라스틱을 분류
    - 2차 분류 : 금속감지 센서릍 통하여 금속, 종이 중 금속을 분류

  - 분류 결과에 따라 서보모터의 각도를 조절하여 원하는 위치로 이동

- 사용하는 소자
<br>
- 적외선 센서<br>
<img src="https://raw.githubusercontent.com/c9yu/Smart-Factory/dev/imgs/img001.jpg"  width="300" height="300"/><br>
사물을 인식하여 DC 모터 동작을 제어한다.
<br>
<br>
- 레이저 모듈<br>
<img src="https://raw.githubusercontent.com/c9yu/Smart-Factory/dev/imgs/img003.jpg"  width="300" height="300"/><br> 
조도 센서와 함께 사용될 광원의 역할을 한다.
<br>
<br>
- 조도 센서<br>
<img src="https://raw.githubusercontent.com/c9yu/Smart-Factory/dev/imgs/img002.jpg"  width="300" height="300"/><br>
광원에서 쏘아낸 빛이 물체를 투과하여 얼마나 조도센서에 들어오는지를 통해 사물을 구분
<br>
<br>
- 금속 감지 센서<br>
<img src="https://raw.githubusercontent.com/c9yu/Smart-Factory/dev/imgs/img004.jpg"  width="300" height="300"/><br>   
금속에 접촉할 경우 전기가 흘러 금속과 비금속을 구분
<br>
<br>
- DC 모터<br>
<img src="https://raw.githubusercontent.com/c9yu/Smart-Factory/dev/imgs/img005.jpg"  width="300" height="300"/><br>
적외선 센서를 기반으로 컨베이어 벨트를 동작시킴
<br>
<br>
- 서보 모터<br>
<img src="https://raw.githubusercontent.com/c9yu/Smart-Factory/dev/imgs/img006.jpg"  width="300" height="300"/><br>   
조도 센서와 금속 감지 센서에 의해 제어되며 각도의 조절을 통해 사물의 최종 분류를 수행<br>

## 2일차 (2024-07-03)
- 각 소자별 주요 코드 확인
  - 적외선 센서
  ```c
  #define IR_SENSOR A0
  int IR_val;

  void setup(){
    pinMode(IR_SENSOR, INPUT); // 센서를 입력으로 사용
  }

  void loop(){
    IR_val = digitalRead(IR_SENSOR); // 적외선 센서의 값을 읽어온다.
  }
  ```

  - 레이저 모듈
  ```c
  void setup(){
    pinMode(LASER_PIN, OUTPUT); // 레이저 모듈을 출력으로 사용
  }

  void loop(){
    digitalWrite(LASER_PIN,OUTPUT); // 레이저가 작동
  }
  ```

  - 조도 센서
  ```c
  int light_val;
  
  void loop(){
    light_val = analogRead(A1); // 연결한 핀을 할당
    Serial.println(light_val); // 
    delay(10); // 값을 불러올 때 딜레이를 추가
  }
  ```

  - 금속 감지 센서
  ```c
  void loop(){
    float metal = analogRead(A3);
    Serial.println(metal);
    if(metal < 950) // 출력되는 값이 974~973 정도로 균일하게 출력되므로 950정도로 오차값을 고려하여 구분을 진행
        Serial.println(" - touch"); // 금속과 접촉시 '- touch'를 출력
    prev = metal;
    Serial.println();
    delay(100);
  }
  ```

  - DC 모터
  ```c
  // 단순히 DC 모터를 동작시키는 것이 아닌 다른 적외선 센서의 값에 따라 동작을 수행

  int motorSpeedPin = 10;      // 1번(A) 모터 회전속도(speed)
  int motorDirectionPin = 12;  // 1번(A) 모터 방향제어(forward, backward)
  int motor_speed_DC;  

  void setup(){
    pinMode(motorDirectionPin, HIGH);  // 방향제어핀을 pinmode_HIGH으로 지정
  }
  
  void loop(){
    if (IR_val == LOW) {                    // IR_val센서는 LOW ACTIVE로 탐지 시 LOW값을 전송함
       Serial.println("Detected");
      delay(300);
      digitalWrite(motorDirectionPin, LOW);  
      motor_speed_DC = 70;
      analogWrite(motorSpeedPin, motor_speed_DC);
    } 
    else {
      Serial.println("Not detected");
      delay(300);
      digitalWrite(motorDirectionPin, LOW);  
      motor_speed_DC = 0;
      analogWrite(motorSpeedPin, motor_speed_DC);
    }
  }
  ```

- 서보 모터
  ```c
  #include <Servo.h>
  #define SERVO_PIN 9

  Servo servo;

  void setup(){
    servo.attach(SERVO_PIN);                // 서보모터를 아두이노와 연결
    servo.write(0);                         // 최초 서보모터 각도는 0도

  void loop(){
    servo.write(180);
    delay(30);
    }
  }
  ```

## 3일차 (2024-07-04)
- 각 소자간 연동

- wpf 디자인 시작

## 4일차 (2024-07-05)
- 각 센서의 연동

## 5일차 (2024-07-08)
<img src="https://raw.githubusercontent.com/smart-recycling-factory/Smart-Factory/dev/imgs/img007.png"/><br>

- 각 센서의 연동
  - 위치 ① 동작 순서
    - 적외선 센서 : 물체 감지 &rarr; dc모터 정지
    - 조도 센서 : 900 이상 &rarr; 플라스틱으로 판단
    - 서보 모터 : 각도 2°(플라스틱) 조절
    - ~~레이저 모듈 : off~~
    
      ```c
      /* 로그 출력, 딜레이는 표시생략 */
      if (first_ir_val == LOW) {        // 적외선 센서 감지 시
        dcStop();                       // dc모터 정지
        // 플라스틱
        if (light_val > 900) {          // 조도센서의 값이 900보다 크면
          servoWork(POS_PST);           // 서보모터 각도 2° 조절
          is_plastic = true;            // false로 선언된 플래그변수를 true로 설정

          digitalWrite(LASER_PIN, LOW); // 레이저 off (동작안함)
          dcWork();                     // dc모터 시작 (동작안함)
        }
        // 종이 | 캔
        else { 
          // 플라스틱이 아님을 출력 후 continue
        }
      }
      ```

  - 위치 ② 동작 순서
    - 적외선 센서 : 물체 감지 &rarr; dc모터 정지
    - 금속 센서 : 감지되면(값이 떨어지면) &rarr; 캔으로 판단, 아닐경우 종이
    - 서보 모터 : 각도 35°(캔) | 57°(종이) 조절

      ```c
      /* 로그 출력, 딜레이는 표시생략 */
      if (second_ir_val == LOW) {     // 적외선 센서 감지 시
        dcStop();                     // dc모터 정지
        // 경우1 : 캔
        if (metal < 500) {            // 금속센서의 값이 500 아래로 떨어지면
          servoWork(POS_CAN);         // 서보모터 각도 35° 조절
        }
        // 경우2 : 종이
        else {
        if(is_plastic) {              // is_plastic == true, 즉 ①에서 플라스틱이면
            servoWork(POS_PST);       // 서보모터 각도 2° 조절
          }
          else {
            servoWork(POS_BOX);       // 서보모터 각도 57° 조절
          }
        }
      }
      ```

- 문제점❗❗
  - ①의 위치
    - ❌ 하나의 물체(종이, 캔)가 이동함에 따라 값의 변동 발생 ❌
      <p>하나의 물체에 대한 센서값은 물체가 분류완료될 때까지 해당 값을 가지고 있어야 한다.<br>
      하지만 dc모터가 재가동됨에 따라 물체가 ①에서 벗어날 때 값이 재입력된다.(분류결과가 달라짐)<br>
      => 900이하(종이,캔) &rarr; 900이상(플라스틱)❗❗
      </p>
  
    - ❌ 적외선 센서 감지 이후 dc모터 동작x ❌
      <p>적외선 센서에 물체가 감지되면 dc모터를 멈추고 조도센서 값을 읽어낸다. <br>
      값을 읽은 후 dc모터가 재가동되어 ②의 위치로 이동해야 한다. <br>
      하지만 적외선 센서에 물체가 감지되고 있는 상태에서 물체를 감지범위 밖으로 이동시키지 않는 한 dc모터가 재가동되지 않는다.(무한정지상태)❗❗
      </p> 

  - ②의 위치
    - ❌ 금속센서 : 종이/캔 분류 &rarr; 예외발생 ❌
      <p>종이/캔 중 금속센서에 감지되지 않는 것은 종이로 분류한다. <br>
      하지만 ①에서 플라스틱으로 분류된 물체 또한 ②에서 금속센서에 감지되지 않아 플라스틱이 종이로 분류되게 된다❗❗
      </p>

## 6일차 (2024-07-09)
- 시리얼 통신
  - go 입력 시 프로그램 실행
  - stop 입력 시 동작 중지

  ```c
  void loop() {
    String str = "";
    if (Serial.available() > 0) {
      // 줄바꿈 문자(\n)가 나타날 때까지의 모든 문자열을 읽어와 str에 저장
      str = Serial.readStringUntil('\n');
    }
    // go 입력 시 분류 시작
    if (str == "go") {
      // 컨베이어 벨트 동작, 레이저 on, 적외선 센서 값 읽기
      // 첫 번째 검사대에서 조도센서로 플라스틱과 종이/캔 분류
      if (first_ir_val == LOW) {
        // ...
      }

      // 두 번째 검사대에서 조도센서로 종이/캔 분류 및 물체별 서보모터 동작
      if (second_ir_val == LOW) {
        // ...
      }
    } 
    // stop 입력 시 컨베이어 벨트 동작 중지
    else if (str == "stop") {
      dcStop();
    }
  }
  ```

- 문제점❗❗
  - ❌ 물체가 감지된 후에도 적외선 센서 값이 바뀌지 않음 ❌
    <p>loop()가 돌면서 str 값이 공백으로 초기화되어 조건문이 성립될 수 없음❗❗</p>

  ```c
  String str;
  void loop() {
    if (Serial.available() > 0) {
      str = Serial.readStringUntil('\n');
    }
    if (str == "go") {
      if (first_ir_val == LOW) {
        // ...
      }
      if (second_ir_val == LOW) {
        // ...
      }
    } 
    else if (str == "stop") {
      dcStop();
    }
  }
  ```
  - ✅ str 변수를 전역변수로 선언함으로써 루프를 한 번 도는 동안 시리얼 모니터에서 입력한 값이 유지됨
  
## 7일차 (2024-07-10)
