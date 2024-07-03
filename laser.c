int Sensor = A0;
int S_Value;
int Led = 3;
int Laser = 5;

void setup() {
  Serial.begin(9600);
  pinMode(Led, OUTPUT);
  pinMode(Laser, OUTPUT);
}

void loop() {
  S_Value = analogRead(A1);
  digitalWrite(Laser, OUTPUT);
  Serial.println(S_Value);
  delay(10);

  // delay(200);

  if(S_Value<=1000){
    digitalWrite(Led, HIGH);  // LED 꺼짐
  }
  else if(S_Value>=1000){
    digitalWrite(Led, LOW); // LED 켜짐
  }
}