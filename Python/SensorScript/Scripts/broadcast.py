import socket
import time
import sensor


class BroadCaster():
    async def start_broadcast(self):
        server = socket.socket(socket.AF_INET, socket.SOCK_DGRAM, socket.IPPROTO_UDP)
        server.setsockopt(socket.SOL_SOCKET, socket.SO_BROADCAST, 1)
        server.settimeout(0.2)
        sens = sensor.Sensor()
        while True:
            message = bytes(sens.get_temp())
            server.sendto(message, ("<broadcast>", 42069))
            time.sleep(1)


BroadCaster().start_broadcast()
