import socket
import time
from .sensor import Sensor
from .TestData import Test
import json


class BroadCaster():
    def __init__(self, rpi_id):
        self.rpi_id = rpi_id

    async def start_broadcast(self):
        server = socket.socket(socket.AF_INET, socket.SOCK_DGRAM, socket.IPPROTO_UDP)
        server.setsockopt(socket.SOL_SOCKET, socket.SO_BROADCAST, 1)
        server.settimeout(0.2)
        sens = Sensor()
        
        while True:
            temp = sens.get_temp()
            if (temp > 33):
                has_fever = False
                if temp > 37.5:
                    has_fever = True
                test_object = Test(self.rpi_id, temp, has_fever)
                json_str = json.dumps(test_object.__dict__)
                message = bytes(str(json_str).encode())
                server.sendto(message, ("<broadcast>", 42069))
                time.sleep(.1)


