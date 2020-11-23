import board
import busio as io
import adafruit_mlx90614

class Sensor():
    def __init__():
        self.i2c = io.I2C(board.SCL, board.SDA, frequency=100000)
        self.mlx = adafruit_mlx90614.MLX90614(i2c)

    def get_temp():
        return mlx.object_temperature()

