/**
 * Receives messages via CAN bus and drives PWM outputs for servos or ESCs.
 * 
 * Intended for testing of wheelchair control using a RoboClaw motor controller,
 * with commands received over CAN bus from a Chair Breakout Mini attached to
 * a wheelchair joystick.
 * 
 * By Chris Fryer (chris.fryer78@gmail.com) and Jonathan Oxer (jon@oxer.com.au)
 * www.superhouse.tv
 */

#include <CAN.h>    // CAN bus communication: https://github.com/sandeepmistry/arduino-CAN (available in library manager)
#include <Servo.h>  // The RoboClaw uses PWM input, so we pretend it's a servo

// Configuration should be done in the included file:
#include "config.h"

Servo LeftMotor;   // We treat the motor controller (ESC) like a servo
Servo RightMotor;  // We treat the motor controller (ESC) like a servo
String SerialBuffer = ""; // Buffer for incoming serial messages

// The "...InputValue" variables are taken from inputs and mixed or used directly
int XInputValue = MID_OUTPUT_VALUE; // Start at the mid-point
int YInputValue = MID_OUTPUT_VALUE; // Start at the mid-point

// The "...MotorValue" variables are directly used to drive the motors
int LeftMotorValue  = MID_OUTPUT_VALUE; // Start at the mid-point
int RightMotorValue = MID_OUTPUT_VALUE; // Start at the mid-point

long last_report_time = 0;
unsigned int serial_report_interval = 500;

char incoming_message_buffer[12];  // Buffer for incoming CAN messages

/*
 * Setup
 */
void setup() {
  Serial.begin(9600);

  Serial.println("RoboClaw CAN bus interface v1.0");
  
  Serial.println("Starting CAN Receiver");
  // start the CAN bus at 500 kbps
  if (!CAN.begin(500E3)) {
    Serial.println("Starting CAN failed!");
    while (1);
  } else {
    Serial.println("Started CAN OK");
  }

  // register the receive callback
  CAN.onReceive(onReceive);

  // Set up the motors and make sure they aren't spinning
  LeftMotor.attach(LEFT_MOTOR_PIN);
  RightMotor.attach(RIGHT_MOTOR_PIN);
  LeftMotor.write(LeftMotorValue);
  RightMotor.write(RightMotorValue);

  delay(100);
}

/**
 * Loop
 */
void loop() {
  // Set the motors to their currently configured speeds
  LeftMotor.write(LeftMotorValue);
  RightMotor.write(RightMotorValue);
    
  if(OUTPUT_DEBUG)
  {
    long current_time = millis();
    if(current_time > (last_report_time + serial_report_interval))
    {
      Serial.print("Left:");
      Serial.print(LeftMotorValue);
      
      Serial.print(" |Right:");
      Serial.print(RightMotorValue);

      Serial.print(" |Delta:");
      Serial.print(LeftMotorValue - RightMotorValue);
      
      Serial.println(" |");
      last_report_time = current_time;
    }
  }

  processSerialCommands();

  // WARNING: only one of these below should be uncommented
  //splitInputChannels();
  mixInputChannels();
}

/**
 * Check the serial buffer for any messages. The format is a pair of numbers
 * that represent the X and Y joystick positions, with "90" as the mid point:
 * "90,90" is joystick centered.
 * "80,90" is joystick slightly left.
 * "100,90" is joystick slightly right.
 * "100,120" is joystick slightly right and forward.
 */
void processSerialCommands()
{
  /*
  while (Serial.available() > 0) {
    int inChar = Serial.read();
    if (isDigit(inChar)) {
      // Convert the incoming byte to a char and add it to the string:
      SerialBuffer += (char)inChar;
    }
    if (inChar == '\n') {
      int InputValue = SerialBuffer.toInt();
      if((InputValue >= MIN_OUTPUT_VALUE) && (InputValue <= MAX_OUTPUT_VALUE))
      {
        XInputValue = InputValue;
        YInputValue = InputValue;
        Serial.print("Setting speed: ");
        Serial.println(InputValue, DEC);
      }

      // clear the string for new input:
      SerialBuffer = "";
    }
  }
  */
  while (Serial.available() > 0) {
    String XInputString  = Serial.readStringUntil(',');
    Serial.read(); //next character is comma, so skip it using this
    String YInputString = Serial.readStringUntil('\n');
    int XInputRaw = XInputString.toInt();
    int YInputRaw = YInputString.toInt();
    if(XInputRaw > MIN_OUTPUT_VALUE && XInputRaw < MAX_OUTPUT_VALUE && YInputRaw > MIN_OUTPUT_VALUE && YInputRaw < MAX_OUTPUT_VALUE)
    {
      XInputValue = XInputRaw;
      YInputValue = YInputRaw;
    }
  }
}

/**
 * Treat X and Y input values as separate, and send them to the motors individually
 */
void splitInputChannels()
{
  LeftMotorValue  = XInputValue;
  RightMotorValue = YInputValue;
}

/**
 * Mix the X and Y input values together to calculate a vector, and drive the
 * motors to match that vector
 */
void mixInputChannels()
{
  int Speed    = MID_OUTPUT_VALUE;  // Forward / reverse speed of the chair
  int TurnRate = MID_OUTPUT_VALUE;  // Lower values turn left, higher turn right

  /*
   * NOTE: Scaling the turn rate below is critical to making the chair behave in
   * an intuitive way. If this is not scaled, the turn rate can cancel out the speed
   * of the chair and make it work in a surprising way. For example, without turn
   * rate scaling, if the joystick is moved diagonally to the front right (such as
   * position 100,100) the chair won't move forward at all, it will turn on the spot
   * with the right motor stopped and left motor at speed 100.
   */
  Speed = YInputValue - MID_OUTPUT_VALUE;
  TurnRate = (XInputValue - MID_OUTPUT_VALUE) / 4;

  // Determine left and right motor values
  int left_speed  = Speed + TurnRate;
  //int left_magnitude = abs(left_speed);
  int right_speed = Speed - TurnRate;
  //int right_magnitude = abs(right_speed);

  LeftMotorValue  = left_speed  + MID_OUTPUT_VALUE;
  RightMotorValue = right_speed + MID_OUTPUT_VALUE;
}


/**
 * Callback when a CAN packet arrives
 */
void onReceive(int packetSize) {
  // received a packet
    int integerValue = 0;    // throw away previous integerValue
    char incomingByte;
    byte is_negative = 0;
    int i = 0;
    while (CAN.available()) {
      //Serial.print((char)CAN.read());
      incomingByte = (char)CAN.read();
      incoming_message_buffer[i] = incomingByte;
      i++;
      if(incomingByte == '-')
      {
        is_negative = true;
      } else {
        integerValue *= 10;  // shift left 1 decimal place
        // convert ASCII to integer, add, and shift left 1 decimal place
        integerValue = ((incomingByte - 48) + integerValue);
      }
    }
  
    if(is_negative)
    {
      integerValue = integerValue * -1;
    }

    int axis_output = 0;
    // Only print packet data for non-RTR packets
    if(CAN.packetId() == 0x12)  // Packet ID 0x12 for X axis
    {
      if(CAN_DEBUG)
      {
        Serial.print("X=");
        Serial.print(integerValue, DEC);
      }

      axis_output = map(integerValue, -75, +75, MIN_OUTPUT_VALUE, MAX_OUTPUT_VALUE);
      XInputValue = constrain(axis_output, MIN_OUTPUT_VALUE, MAX_OUTPUT_VALUE);
    }
    if(CAN.packetId() == 0x13)  // Packet ID 0x13 for Y axis
    {
      if(CAN_DEBUG)
      {
        Serial.print("     Y=");
        Serial.println(integerValue, DEC);
      }
      axis_output = map(integerValue, -75, +75, MIN_OUTPUT_VALUE, MAX_OUTPUT_VALUE);
      YInputValue = constrain(axis_output, MIN_OUTPUT_VALUE, MAX_OUTPUT_VALUE);
    }
}
