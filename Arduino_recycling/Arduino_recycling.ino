#include <Servo.h>
#define IR_SENSOR_FST A0      // 조도센서 영역 IR 센서
#define IR_SENSOR_SEC A2      // 금속 탐지 센서 영역 IR 센서
#define LIGHT_SENSOR A1       // 조도 센서를 연결한 핀
#define METAL_SENSOR A3       // 금속 탐지 센서를 연결한 핀
#define MOTOR_SPEED 11
#define MOTER_DIRECTION 13
#define SERVO_PIN 9
#define LASER_PIN 5
#define POS_BOX 57  // 종이 제품을 분류할 서보모터의 각도(1번째 상자)
#define POS_CAN 35   // 캔 제품을 분류할 서보모터의 각도(2번째 상자)
#define POS_PST 2    // 플라스틱 분류할 서보모터의 각도(3번째 상자)
Servo servo;

int first_ir_val;
int second_ir_val;
int speed_val = 70;  
int light_val;
bool is_plastic = false;
String str;     // 시리얼 통신 입력값을 전역변수로 선언!

void setup() {
    Serial.begin(9600);
    // Serial.println("Arduino starts!");
    pinMode(MOTER_DIRECTION, OUTPUT);       // dc모터의 방향을 제어하는 핀을 output으로 설정
    pinMode(LASER_PIN, OUTPUT);             // 레이저핀을 pinmode_OUTPUT으로 지정
    pinMode(LIGHT_SENSOR, INPUT);           // 조도 센서 핀을 INPUT으로 지정
    pinMode(METAL_SENSOR, INPUT);           // 금속 탐지 센서 핀을 INPUT으로 지정
    pinMode(IR_SENSOR_FST, INPUT);          // 적외선 센서 핀을 pinmode_INPUT으로 지정
    pinMode(IR_SENSOR_SEC, INPUT);          // 적외선 센서 핀을 pinmode_INPUT으로 지정
    digitalWrite(MOTER_DIRECTION, LOW);     // 방향은 전진. 의도한 방향과 반대일 경우 HIGH -> LOW로 변경
    servo.attach(SERVO_PIN);                // 서보모터 초기화
}

void loop() {
  if (Serial.available() > 0) {
    str = Serial.readStringUntil('\n');
  }
  
  if (str == "go") { 
    servoWork(90);
    dcWork();
    digitalWrite(LASER_PIN, HIGH);                // 레이저 센서 켜기
    first_ir_val = digitalRead(IR_SENSOR_FST);    // 적외선 센서
    second_ir_val = digitalRead(IR_SENSOR_SEC);   // 적외선 센서

    // DB에 맞추기 위해 매번 초기화 해주는 걸로 바꾸기
    int pst_count = 0;
    int box_count = 0;
    int can_count = 0;
    
    // 첫 번째 검사대에서 조도센서로 플라스틱과 종이/캔 분류
    if (first_ir_val == LOW) {
      is_plastic = false;
      // Serial.println("detected 1");
      dcStop();
      // 조도 센서
      light_val = analogRead(LIGHT_SENSOR);  

      // 플라스틱이면
      if (light_val > 900) {
        // Serial.println("plastic");
        delay(1000);
        is_plastic = true;
      }
      // 종이/캔이면
      else {
        // Serial.println("Not plastic!");
        delay(1000);
      }
      dcWork();
    }

    // 두 번째 검사대에서 조도센서로 종이/캔 분류 및 물체별 서보모터 동작
    if (second_ir_val == LOW) {
      // Serial.println("detected 2");
      dcStop();
      // 금속 감지 센서
      float metal = analogRead(METAL_SENSOR);

      // 캔인 경우
      if (metal < 500) {
        // Serial.println("can");
        delay(1000);
        servoWork(POS_CAN);
        can_count += 1;
      }
      // 플라스틱인 경우
      else if(is_plastic) {
          // Serial.println("plastic");
          // Serial.println(is_plastic);
          delay(1000);
          servoWork(POS_PST);
          pst_count += 1;
      }
      // 종이인 경우
      else {
        // Serial.println("box");
        // Serial.println(is_plastic);
        delay(1000);
        servoWork(POS_BOX);
        box_count += 1;
      }
      dcWork();
      delay(2000);    // 서보모터 초기화까지의 시간을 벌기 위함
      Serial.print("pst-");
        Serial.println(pst_count);
      Serial.print("can-");
      Serial.println(can_count);
      Serial.print("box-");
      Serial.println(box_count);
    }
  } 

  else if (str == "stop") {
    dcStop();
  }
}

void dcWork() {
  digitalWrite(MOTER_DIRECTION, LOW);
  analogWrite(MOTOR_SPEED, speed_val);    // 레일 작동 시작
}

void dcStop() {
  analogWrite(MOTOR_SPEED, 0);            // 레일 작동 중지
  delay(1500);
}

// [!] 이 함수 안에 detach() 쓰면 DC 동작 안 함!
void servoWork(int box) {
  delay(500);
  // servo.attach(SERVO_PIN);
  servo.write(box);
  delay(500);
}