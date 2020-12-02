from .sensor import Sensor
from sense_hat import SenseHat

class Animator:
    def __init__(self, background):
        self.sense = SenseHat()
        self.background = background
        B = background
        G = (0, 255, 0)
        R = (255, 0, 0)
        Y = (255, 255, 0)
        BL = (0, 0, 255)
        self.checkmarkImage = [
            B, B, B, B, B, B, B, B,
            B, B, B, B, B, B, B, G,
            B, B, B, B, B, B, G, B,
            B, B, B, B, B, G, B, B,
            G, B, B, B, G, B, B, B,
            B, G, B, G, B, B, B, B,
            B, B, G, B, B, B, B, B,
            B, B, B, B, B, B, B, B
        ]
        self.crossImage = [
            R, B, B, B, B, B, B, R,
            B, R, B, B, B, B, R, B,
            B, B, R, B, B, R, B, B,
            B, B, B, R, R, B, B, B,
            B, B, B, R, R, B, B, B,
            B, B, R, B, B, R, B, B,
            B, R, B, B, B, B, R, B,
            R, B, B, B, B, B, B, R
        ]
        self.errorImage = [
            B, B, B, Y, Y, B, B, B,
            B, B, Y, B, B, Y, B, B,
            B, B, Y, B, B, Y, B, B,
            B, B, B, B, Y, B, B, B,
            B, B, B, B, Y, B, B, B,
            B, B, B, B, Y, B, B, B,
            B, B, B, B, B, B, B, B,
            B, B, B, B, Y, B, B, B
        ]
        self.measuringImage = [
            B, BL, BL, BL, BL, BL, BL, B,
            B, B, BL, B, B, BL, B, B,
            B, B, BL, B, B, BL, B, B,
            B, B, B, BL, BL, B, B, B,
            B, B, B, BL, BL, B, B, B,
            B, B, BL, B, B, BL, B, B,
            B, B, BL, B, B, BL, B, B,
            B, BL, BL, BL, BL, BL, BL, B
        ]

    def displayImage(self, image):
        self.sense.set_pixels(image)

    def clearImage(self):
        self.sense.clear()