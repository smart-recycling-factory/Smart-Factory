# 2024_miniproject
  ## smart factory

## 공지사항
- 깃 작성 방법
  - [깃 컨벤션 참조1](https://velog.io/@shin6403/Git-git-%EC%BB%A4%EB%B0%8B-%EC%BB%A8%EB%B2%A4%EC%85%98-%EC%84%A4%EC%A0%95%ED%95%98%EA%B8%B0)
  - [깃 컨벤션 참조2](https://hyunjun.kr/21)
  - [리드미 작성 참조](https://annajin.tistory.com/189)

## 1일차 (2024-07-03)
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

