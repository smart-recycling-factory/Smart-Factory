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

int speed_val = 70;  
int light_val; 

void setup() {
    Serial.begin(9600);
    Serial.println("Arduino starts!");
    pinMode(IR_SENSOR_FST, INPUT);          // 적외선 센서 핀을 pinmode_INPUT으로 지정
    pinMode(IR_SENSOR_SEC, INPUT);          // 적외선 센서 핀을 pinmode_INPUT으로 지정
}

void loop() {
    int first_ir_val = digitalRead(IR_SENSOR_FST);
    int second_ir_val = digitalRead(IR_SENSOR_SEC);
    // 첫 번째 검사대에서 플라스틱과 종이/캔 분류
    if (first_ir_val == LOW) {
      Serial.println("detected 1");
      delay(1000);
    }
    // 두 번째 검사대에서 플라스틱과 종이/캔 분류
    if (second_ir_val == LOW) {
      Serial.println("detected 2");
      delay(1000);
    }
}

