import socket
import time
from .sensor import Sensor
from .TestData import Test
import json


class BroadCaster():
    def __init__(self, settings_list):
        self.raspberryPiID = settings_list[0]
        self.cooldown_timer = settings_list[1]
        self.measurement_timer = settings_list[2]
        self.sensor_polling_rate = settings_list[3]
        self.lower_bound_temp = settings_list[4]
        self.upper_bound_temp = settings_list[5]
        self.fever_temp = settings_list[6]
        self.port_number = settings_list[7]
        self.measurement_round = settings_list[8]

    async def start_broadcast(self):
        server = socket.socket(socket.AF_INET, socket.SOCK_DGRAM, socket.IPPROTO_UDP)
        server.setsockopt(socket.SOL_SOCKET, socket.SO_BROADCAST, 1)
        server.settimeout(0.2)
        sens = Sensor()
        
        while True:
            temps = []
            temp = sens.get_temp()
            if (temp > self.lower_bound_temp and temp < self.upper_bound_temp):
                measurement_time_end = time.time() + self.measurement_timer
                while True:
                    if time.time() > measurement_time_end:
                        break
                    temps.append(sens.get_temp())
                    time.sleep(self.sensor_polling_rate)
                measured_temp = sum(temps)/len(temps)
                has_fever = False
                if measured_temp > self.fever_temp:
                    has_fever = True
                
                measured_temp_f = (measured_temp * 9/5) + 32
                #print(round(measured_temp,self.measurement_round))
                #print(round(measured_temp_f,self.measurement_round))
                test_object = Test(self.raspberryPiID, round(measured_temp,self.measurement_round), round(measured_temp_f,self.measurement_round),  has_fever)
                json_str = json.dumps(test_object.__dict__)
                message = bytes(str(json_str).encode())
                server.sendto(message, ("<broadcast>", self.port_number))
                time.sleep(self.cooldown_timer)
            

