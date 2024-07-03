#include <Servo.h>
#define IR_sensor A0                          // a0적외선 수신 센서를 연결한 핀
#define SERVO_PIN 9
#define Laser_PIN 5

Servo servo;

int IR_val;
int motorSpeedPin = 10;      // 1번(A) 모터 회전속도(speed)
int motorDirectionPin = 12;  // 1번(A) 모터 방향제어(forward, backward)
int motor_speed_DC;  
int light_val; 

void setup() {
  Serial.begin(9600);
  pinMode(IR_sensor, INPUT);  // 센서값을 입력으로 설정
  Serial.println("arduino starts");
  pinMode(motorDirectionPin, OUTPUT);  // 방향제어핀을 pinmode_OUTPUT으로 지정
  noTone(4);
  pinMode(Laser_PIN, OUTPUT);
  servo.attach(SERVO_PIN);                // 서보모터를 아두이노와 연결
  servo.write(0);                         // 최초 서보모터 각도는 0도
}

float prev = 0;

void loop() {
  // IR_val = digitalRead(IR_sensor);  // 센서값 읽어옴

  // if (digitalRead(light_val) >= 1000) {
  //   servo.write(180);
  //   delay(30);

  //   if (digitalRead(light_val))
  // }

  // 조도 센서
  light_val = analogRead(A1);
  digitalWrite(Laser_PIN, OUTPUT); // 레이저 (광원)
  Serial.println(light_val);
  delay(10);

  // 금속 감지 센서
  float metal = analogRead(3);//*5./1024.; ///IR_val함수에 = A3번 핀의 입력값을 받아 입력값을 변환시킵니다.
  Serial.println(metal);
  if(metal < 990)   ///IR_val - prev 값이 0.2보다 크면 "touch"라고 출력합니다. 
    Serial.println(" - touch");
  prev = metal;
  Serial.println();
  delay(100);

  IR_val = digitalRead(IR_sensor); 

  if (IR_val == LOW) {           // IR_val센서는 LOW ACTIVE로 탐지 시 LOW값을 전송함
    // Serial.println("Detected");
    delay(300);

    digitalWrite(motorDirectionPin, LOW);        // 역방향으로 모터 제어
    motor_speed_DC = 70;
    analogWrite(motorSpeedPin, motor_speed_DC);
  } 
  else {
    // Serial.println("Not detected");
    delay(300);

    digitalWrite(motorDirectionPin, HIGH);  
    motor_speed_DC = 0;
    analogWrite(motorSpeedPin, motor_speed_DC);
  }


}
