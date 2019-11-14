// Debug options
#define CAN_DEBUG false
#define OUTPUT_DEBUG true

// Don't output more than this using PWM. Scale to make this 100% output
// ludicrous mode!!
// RoboClaw speed range is 0-127, with 64 as the mid-point (stop). Lower
// numbers are reverse, higher numbers are forward.
#define MAX_OUTPUT_VALUE 127
#define MID_OUTPUT_VALUE 64
#define MIN_OUTPUT_VALUE 0

// Pin assignments
//#define LEFT_MOTOR_PIN  9
//#define RIGHT_MOTOR_PIN 6
#define RC_S1    8
#define RC_S2    9

#define LEFT_BRAKE_CONTROL_PIN   4
#define RIGHT_BRAKE_CONTROL_PIN  5
#define LEFT_BRAKE_SENSOR_PIN    6
#define RIGHT_BRAKE_SENSOR_PIN   7

// RoboClaw Config
#define ADDRESS 0x80

//Velocity PID coefficients.
#define KP 1.0
#define KI 0.5
#define KD 0.25
#define QPPS 44000
