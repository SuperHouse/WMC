/**
 * Receives messages via CAN bus and drives PWM outputs for servos or ESCs.
 * 
 * Intended for testing of wheelchair control using a RoboClaw motor controller,
 * with commands received over CAN bus from a Chair Breakout Mini attached to
 * a wheelchair joystick.
 * 
 * By Chris Fryer (chris.fryer78@gmail.com) and Jonathan Oxer (jon@oxer.com.au)
 * www.superhouse.tv
 * 
 * To do:
 * - Activate brakes when speed reaches 0. This needs to be checked with the RoboClaw,
 * because the chair may take a while to stop after it has been set to 0 speed.
 * - Check brake status before changing speed from 0 to anything else, and release them.
 */


// Configuration should be done in the included file:
#include "config.h"

#include <CAN.h>             // CAN bus communication: https://github.com/sandeepmistry/arduino-CAN (available in library manager)
#include <SoftwareSerial.h>  // Required for RoboClaw serial connection
#include "RoboClaw.h"        // Library from BasicMicro

// Serial link to RoboClaw
SoftwareSerial serial(RC_S1,RC_S2);
RoboClaw roboclaw(&serial,10000);

String SerialBuffer = ""; // Buffer for incoming serial messages from console

// The "...InputValue" variables are taken from inputs and mixed or used directly
int XInputValue = MID_OUTPUT_VALUE; // Start at the mid-point
int YInputValue = MID_OUTPUT_VALUE; // Start at the mid-point

// The "...MotorSetpoint" variables are the target speed we want to reach
double LeftMotorSetpoint  = MID_OUTPUT_VALUE; // Start at the mid-point
double RightMotorSetpoint = MID_OUTPUT_VALUE; // Start at the mid-point

// The "...MotorCurrentValue" variables are directly used to drive the motors
double LeftMotorCurrentValue  = MID_OUTPUT_VALUE;
double RightMotorCurrentValue = MID_OUTPUT_VALUE;

// Recalculate the motor speed based on current and target
long last_calculation_time = 0;
unsigned int motor_output_calculation_interval = 100; // How often to update the motor speed
int speed_output_increment = 1; // How big the jump should be per interval

// Check whether we need to apply the brakes
long last_brake_check_time = 0;
unsigned int brake_check_interval = 500;

// Serial port reports
long last_report_time = 0;
unsigned int serial_report_interval = 500;

char incoming_message_buffer[12];  // Buffer for incoming CAN messages

// Check temperature of RoboClaw motor driver
float roboclaw_temperature = 0.0;
long last_temperature_check_time = 0;
unsigned int temperature_check_interval = 1000;
float roboclaw_voltage = 0.0;
float roboclaw_left_current = 0.0;
float roboclaw_right_current = 0.0;

/*
 * Setup
 */
void setup() {
  Serial.begin(115200);     // Console
  while (!Serial) {
    ; // wait for serial port to connect. Needed for native USB port only
  }
  
  Serial.println("RoboClaw CAN bus interface v1.2");
  
  Serial.print("Opening serial comms to RoboClaw: ");
  roboclaw.begin(38400);  // RoboClaw packet serial connection
  char version[32];
  if(roboclaw.ReadVersion(ADDRESS,version)){
    Serial.print(version);
    Serial.println(" done");
  } else {
    Serial.println(" FAILED");
  }
  
  Serial.print("Setting brake control outputs and sensors: ");
  pinMode(LEFT_BRAKE_CONTROL_PIN,  OUTPUT);
  pinMode(RIGHT_BRAKE_CONTROL_PIN, OUTPUT);
  pinMode(LEFT_BRAKE_SENSOR_PIN,   INPUT);
  pinMode(RIGHT_BRAKE_SENSOR_PIN,  INPUT);
  digitalWrite(LEFT_BRAKE_CONTROL_PIN,  LOW);
  digitalWrite(RIGHT_BRAKE_CONTROL_PIN, LOW);
  Serial.println("done");

  //Set PID Coefficients
  roboclaw.SetM1VelocityPID(ADDRESS,KD,KP,KI,QPPS);  // Right motor
  roboclaw.SetM2VelocityPID(ADDRESS,KD,KP,KI,QPPS);  // Left motor
  
  Serial.println("Starting CAN Receiver");
  // start the CAN bus at 500 kbps
  if (!CAN.begin(500E3)) {
    Serial.println("Starting CAN failed!");
    //while (1);
  } else {
    Serial.println("Started CAN OK");
  }

  // register the receive callback
  CAN.onReceive(onReceive);

  delay(100);
}

/**
 * Loop
 */
void loop() {
  long current_time = millis(); // Used by various timed events in the loop

  // See if either wheel is stationary, and apply the brake if it is
  if(current_time > (last_brake_check_time + brake_check_interval))
  {
    //long LeftMotorSpeed  = roboclaw.ReadSpeedM2(ADDRESS);
    //if(LeftMotorSpeed == 0)
    if(LeftMotorSetpoint == MID_OUTPUT_VALUE)
    {
      digitalWrite(LEFT_BRAKE_CONTROL_PIN, LOW);
    }
    //long RightMotorSpeed = roboclaw.ReadSpeedM1(ADDRESS);
    //if(RightMotorSpeed == 0)
    if(RightMotorSetpoint == MID_OUTPUT_VALUE)
    {
      digitalWrite(RIGHT_BRAKE_CONTROL_PIN, LOW);
    }
    last_brake_check_time = current_time;
  }

  /**
   * Motor speed updates can be managed in this sketch, or they can be
   * delegated to the RoboClaw if the PID values are set correctly. The
   * first option below applies acceleration control within the sketch,
   * while the second attempts to drive the motors immediately to their
   * target speed and relies on the RoboClaw to take care of smoothing.
   */
  // OPTION 1: Manage acceleration within the sketch.
  /*
  // Update the desired motor setting periodically
  if(current_time > (last_calculation_time + motor_output_calculation_interval))
  {
    // WARNING: the calculations below don't yet account for overshoot, so
    // the increment could take it past the target
    if(LeftMotorCurrentValue < LeftMotorSetpoint)
    {
      LeftMotorCurrentValue += speed_output_increment;
    }
    if(LeftMotorCurrentValue > LeftMotorSetpoint)
    {
      LeftMotorCurrentValue -= speed_output_increment;
    }

    if(RightMotorCurrentValue < RightMotorSetpoint)
    {
      RightMotorCurrentValue += speed_output_increment;
    }
    if(RightMotorCurrentValue > RightMotorSetpoint)
    {
      RightMotorCurrentValue -= speed_output_increment;
    }
    
    last_calculation_time = current_time;
  }
  */

  // Option 2: Allow RoboClaw to control motor response.
  LeftMotorCurrentValue = LeftMotorSetpoint;
  RightMotorCurrentValue = RightMotorSetpoint;


  // Release the brake if speed is set to anything except 0
  if(LeftMotorCurrentValue != 64)
  {
    digitalWrite(LEFT_BRAKE_CONTROL_PIN, HIGH);
  }
  if(RightMotorCurrentValue != 64)
  {
    digitalWrite(RIGHT_BRAKE_CONTROL_PIN, HIGH);
  }
  
  // Set the motors to their currently configured speeds
  roboclaw.ForwardBackwardM2(ADDRESS,LeftMotorCurrentValue);
  roboclaw.ForwardBackwardM1(ADDRESS,RightMotorCurrentValue);
    
  if(OUTPUT_DEBUG)
  {
    if(current_time > (last_report_time + serial_report_interval))
    {
      long LeftMotorSpeedReport  = roboclaw.ReadSpeedM2(ADDRESS);
      long RightMotorSpeedReport = roboclaw.ReadSpeedM1(ADDRESS);
      
      //LeftMotorCurrentValue = LeftMotorOutputValue;
      //RightMotorCurrentValue = RightMotorOutputValue;
      
      Serial.print("Left:");
      Serial.print(LeftMotorSetpoint);
      Serial.print("|");
      Serial.print(LeftMotorCurrentValue);
      Serial.print("|");
      Serial.print(LeftMotorSpeedReport);
      
      Serial.print("   Right:");
      Serial.print(RightMotorSetpoint);
      Serial.print("|");
      Serial.print(RightMotorCurrentValue);
      Serial.print("|");
      Serial.print(RightMotorSpeedReport);

      Serial.print("   Delta:");
      Serial.print(LeftMotorSetpoint - RightMotorSetpoint);
      Serial.print("|");
      Serial.print(LeftMotorCurrentValue - RightMotorCurrentValue);
      Serial.print(" | ");
      Serial.print("V:");
      Serial.print(roboclaw_voltage);
      Serial.print(" | ");
      Serial.print("T:");
      Serial.print(roboclaw_temperature);
      Serial.print("|");
      Serial.print("L:");
      Serial.print(roboclaw_left_current);
      Serial.print("|");
      Serial.print("R:");
      Serial.print(roboclaw_right_current);
      Serial.println("|");
      
      last_report_time = current_time;
    }
  }

  processSerialCommands();

  if(current_time > (last_temperature_check_time + temperature_check_interval))
  {
    last_temperature_check_time = current_time;
    read_roboclaw_temperature();
    read_roboclaw_voltage();
  }

  

  // WARNING: only one of these below should be uncommented
  splitInputChannels(); // Apply X axis to left motor, Y axist to right motor
  //mixInputChannels();  // Merge X and Y axis to derive a speed and direction
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
    //Serial.read(); //next character is comma, so skip it using this
    String YInputString = Serial.readStringUntil('\n');
    int XInputRaw = XInputString.toInt();
    int YInputRaw = YInputString.toInt();
    if(  XInputRaw > MIN_OUTPUT_VALUE 
      && XInputRaw < MAX_OUTPUT_VALUE 
      && YInputRaw > MIN_OUTPUT_VALUE 
      && YInputRaw < MAX_OUTPUT_VALUE )
    {
      XInputValue = XInputRaw;
      YInputValue = YInputRaw;
      //Serial.println("ok!");
    }
    //Serial.println("ok2!");
  }
}

/**
 * Treat X and Y input values as separate, and send them to the motors individually
 */
void splitInputChannels()
{
  LeftMotorSetpoint  = XInputValue;
  RightMotorSetpoint = YInputValue;
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

  LeftMotorSetpoint  = left_speed  + MID_OUTPUT_VALUE;
  RightMotorSetpoint = right_speed + MID_OUTPUT_VALUE;
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

void read_roboclaw_temperature()
{
  uint16_t temp = 0;
  roboclaw.ReadTemp(ADDRESS, temp);
  roboclaw_temperature = temp / 10.0;
}

void read_roboclaw_voltage()
{
  uint8_t temp = 0.0;
  temp = roboclaw.ReadMainBatteryVoltage(ADDRESS);
  roboclaw_voltage = temp / 10.0;
}

void read_roboclaw_current()
{
  int16_t temp_left = 0;
  int16_t temp_right = 0;
  
  roboclaw.ReadCurrents(ADDRESS, temp_right, temp_left);
  roboclaw_left_current = temp_left / 10.0;
  roboclaw_right_current = temp_right / 10.0;
}

//ReadCurrents(uint8_t address, int16_t &current1, int16_t &current2);
