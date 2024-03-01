#include <Joystick.h>
Joystick_ Joystick;
void setup()
{
  pinMode(A0, INPUT);
  Joystick.begin();
}
void loop()
{
  int pot = analogRead(A0);
  Joystick.setZAxis(pot);
}
