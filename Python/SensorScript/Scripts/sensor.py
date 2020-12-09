import board
import busio as io
import adafruit_mlx90614

class Sensor():
    def __init__(self):
        try:
            self.i2c = io.I2C(board.SCL, board.SDA, frequency=100000)
            self.mlx = adafruit_mlx90614.MLX90614(self.i2c)
        except:
            print("The sensor was not plugged in correctly")
            print("Please plug the sensor into the GPIO pins SDA, SCL, GND and 3v3 power and run again")
            print("Make sure to only use a 3 volt version of the MLX90614 sensor")
            print("This software has only been tested to work with a 3 volt sensor")
            print("The application will now exit")
            exit()
        

    def get_temp(self):
        return self.mlx.object_temperature

    def get_ambient_temp(self):
        return self.mlx.ambient_temperature
