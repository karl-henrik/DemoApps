

#define DATA            12 //While pin is high transmitter is sending, connect transmitter data pin here.
#define VCC             13 //VCC pin for transmitter, it's job is to give +5V to the transmitter. Connect VCC on transmitter here
#define MY_HOMECODE     0x9394ff80

#define TSHORT		240		//   250 us
#define TLONG		(TSHORT * 5)	//  1250 us
#define TSTART		(TSHORT*10)	//  2500 us
#define TSTOP		(TSHORT*40)	// 10000 us

#define REPEATS         3



void setup() {
  pinMode(VCC,OUTPUT);
  pinMode(DATA, OUTPUT);
  Serial.begin(57600);
  
  digitalWrite(VCC,HIGH);

}

void loop() {  
  

  String test = Serial.readString();
  
  
  test.trim();
  
  if(test.equals("OFF"))
  {		
    send_data(MY_HOMECODE, 0, 0, 0);
    Serial.print("Lamp is: OFF\n");
  }
  else if(test.equals("ON"))
  {
    	send_data(MY_HOMECODE, 0, 1, 0);
        Serial.print("Lamp is: ON\n");
  }
  else if(test.length() > 0) {
    Serial.print("Please issue either of the commands ON or OFF\n");
    }
  
}


void twait(uint32_t us)
{
        delayMicroseconds(us);
}

//
// set a level on the output pin and delay
//
void set_level(uint8_t polarity, uint16_t time)
{
  if(polarity)
  {
    digitalWrite(DATA,HIGH);
  }
  else
  {
    digitalWrite(DATA,LOW);    
  }
  twait(time);
}

//
// output a '1' bit
//
void one_bit(void)
{
	set_level(1, TSHORT);
	set_level(0, TLONG);
}

//
// output a '0' bit
//
void zero_bit(void)
{
	set_level(1, TSHORT);
	set_level(0, TSHORT);
}

//
// send a 32-bit sequence, including start and stop bits
//
void shift_out(uint32_t data)
{
	uint8_t bit;

	// send lead-in
	set_level(1, TSHORT);
	set_level(0, TSTART);

	// send data
	for (bit=0;bit<32;bit++)
	{
		if (data & 0x80000000)	// one-bit
		{
			one_bit();
			zero_bit();
		}
		else
		{
			zero_bit();
			one_bit();
		}
		data <<= 1;
	}

	// stop bit
	set_level(1, TSHORT);
	set_level(0, TSTOP);
}

//
// send an RF  command
//
void send_data(uint32_t id, int group, int onoff, int channel)
{
	int i;
	for (i=0;i<REPEATS;i++)
	{
		id &= 0xFFFFFFC0;		// mask off ID part
		id |= ((group & 1) << 5);	// add group bit
		id |= ((onoff & 1) << 4);	// add on/off bit
		id |= ((~channel) & 15);	// add channel addressing	
		shift_out(id);			// send the pattern
	}
}

void delayms(uint16_t v)
{
	while (v--)
		twait(1000);
}

