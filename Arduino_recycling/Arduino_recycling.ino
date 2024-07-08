#include <Servo.h>
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
    pinMode(MOTER_DIRECTION, OUTPUT);      // dc모터의 방향을 제어하는 핀을 output으로 설정
    pinMode(LASER_PIN, OUTPUT);             // 레이저핀을 pinmode_OUTPUT으로 지정
    digitalWrite(MOTER_DIRECTION, LOW);    // 방향은 전진. 의도한 방향과 반대일 경우 HIGH -> LOW로 변경
    analogWrite(MOTOR_SPEED, speed_val);   // 레일 작동 시작
}

void loop() {
    dcWork();
    // 레이저 (광원)
    digitalWrite(LASER_PIN, OUTPUT); 

    // 조도 센서
    light_val = analogRead(LIGHT_SENSOR);
    Serial.print("light - ");
    Serial.println(light_val); // 값을 정수로 출력
    delay(1000);

    // 금속 감지 센서
    float metal = analogRead(METAL_SENSOR);  
    Serial.print("metal - ");
    Serial.println(metal);
    if(metal < 500) 
        Serial.println("금속 감지");
    Serial.println();
    delay(100);

    // 플라스틱일 경우
    if(light_val < 900) {
      dcStop();
      // Serial.println("Plastic");
      servoWork(POS_PST);
      dcWork();
    }

    else {
      // 금속일 경우
      if (metal < 500) {
        dcStop();
        Serial.println("can");
        servoWork(POS_CAN);
        dcWork();
      }
      // 종이일 경우
      else {
        dcStop();
        Serial.println("box");
        servoWork(POS_BOX);
        dcWork();
      }
    }
}

void dcWork() {
  digitalWrite(MOTER_DIRECTION, LOW);
  analogWrite(MOTOR_SPEED, speed_val);   // 레일 작동 시작
}

void dcStop() {
  analogWrite(MOTOR_SPEED, 0);   // 레일 작동 중지
}

// [!] 이 함수 안에 detach() 쓰면 DC 동작 안 함!
void servoWork(int box) {
  delay(1000);
  servo.attach(SERVO_PIN);
  servo.write(box);
  delay(1000);
}