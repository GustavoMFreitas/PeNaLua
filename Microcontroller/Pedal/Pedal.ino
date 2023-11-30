#include <Joystick.h>
Joystick_ Joystick;
void setup()
{
  pinMode(A0, INPUT);
  Joystick.begin();
}

const int pinMap = A0;
void loop()
{
  int pot = analogRead(A0);
  Joystick.setZAxis(pot);
}
