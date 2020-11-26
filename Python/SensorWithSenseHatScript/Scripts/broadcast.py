import socket
import time
from .sensor import Sensor
from .TestData import Test
from .Animator import Animator
import json



class BroadCaster():
    def __init__(self, rpi_id):
        self.rpi_id = rpi_id
        self.animator = Animator((0, 0, 0))

    async def start_broadcast(self):
        server = socket.socket(socket.AF_INET, socket.SOCK_DGRAM, socket.IPPROTO_UDP)
        server.setsockopt(socket.SOL_SOCKET, socket.SO_BROADCAST, 1)
        server.settimeout(0.2)
        #sens = Sensor()
        #---------REENABLE FOR WHEN SENSOR IS AVAILABLE
        
        while True:
            temp = 40#sens.get_temp()
            #---------REENABLE FOR WHEN SENSOR IS AVAILABLE
            if (temp > 34 and temp < 43):
                has_fever = False
                if temp > 37.5:
                    has_fever = True
                    self.animator.displayImage(self.animator.crossImage)
                else:
                    self.animator.displayImage(self.animator.checkmarkImage)
                test_object = Test(self.rpi_id, temp, has_fever)
                json_str = json.dumps(test_object.__dict__)
                message = bytes(str(json_str).encode())
                server.sendto(message, ("<broadcast>", 42069))
                time.sleep(.5)


