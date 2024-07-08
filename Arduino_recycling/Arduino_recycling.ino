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

void setup() {
    Serial.begin(9600);
    Serial.println("Arduino starts!");
    pinMode(MOTER_DIRECTION, OUTPUT);       // dc모터의 방향을 제어하는 핀을 output으로 설정
    pinMode(LASER_PIN, OUTPUT);             // 레이저핀을 pinmode_OUTPUT으로 지정
    pinMode(IR_SENSOR_FST, INPUT);          // 적외선 센서 핀을 pinmode_INPUT으로 지정
    pinMode(IR_SENSOR_SEC, INPUT);          // 적외선 센서 핀을 pinmode_INPUT으로 지정
    digitalWrite(MOTER_DIRECTION, LOW);     // 방향은 전진. 의도한 방향과 반대일 경우 HIGH -> LOW로 변경
    analogWrite(MOTOR_SPEED, speed_val);    // 레일 작동 시작
}

void loop() {
    servoWork(90);
    dcWork();
    digitalWrite(LASER_PIN, HIGH);                // 레이저 센서 켜기
    light_val = analogRead(LIGHT_SENSOR);         // 조도 센서
    float metal = analogRead(METAL_SENSOR);       // 금속 감지 센서

    first_ir_val = digitalRead(IR_SENSOR_FST);    // 적외선 센서
    second_ir_val = digitalRead(IR_SENSOR_SEC);   // 적외선 센서
    
    // 첫 번째 검사대에서 플라스틱과 종이/캔 분류
    if (first_ir_val == LOW) {
      Serial.println("detected 1");
      dcStop();
      // 플라스틱이면
      if (light_val > 900) {
        Serial.println("plastic");
        delay(1000);
        servoWork(POS_PST);
        is_plastic = true;
        // 레이저 끄기
        digitalWrite(LASER_PIN, LOW);
        Serial.println("LASER OFF!");
        dcWork();
      }
      // 종이/캔이면
      else {
        Serial.println("Not plastic");
        delay(1000);
      }
      // dcWork();
    }

    // 두 번째 검사대에서 플라스틱과 종이/캔 분류
    if (second_ir_val == LOW) {
      Serial.println("detected 2");
      dcStop();
      // 캔인 경우
      if (metal < 500) {
        Serial.println("can");
        delay(1000);
        servoWork(POS_CAN);
      }
      // 종이인 경우
      else {
        if(is_plastic) {
          Serial.println("plastic");
          delay(1000);
          servoWork(POS_PST);
        }
        else {
          Serial.println("box");
          delay(1000);
          servoWork(POS_BOX);
        }
      }
      dcWork();
    }
}

void dcWork() {
  digitalWrite(MOTER_DIRECTION, LOW);
  analogWrite(MOTOR_SPEED, speed_val);   // 레일 작동 시작
}

void dcStop() {
  analogWrite(MOTOR_SPEED, 0);   // 레일 작동 중지
  delay(1000);
}

// [!] 이 함수 안에 detach() 쓰면 DC 동작 안 함!
void servoWork(int box) {
  delay(1000);
  servo.attach(SERVO_PIN);
  servo.write(box);
  delay(1000);
}