import unittest
import sys
import time
from socket import *
sys.path.append("../") 
import Scripts

class TestBroadcast(unittest.TestCase):
    def test_broadcaster(self):
        bc = Scripts.Broadcaster()
        bc.startBroadcaster()
        time.sleep(5)
        
        client = socket(AF_INET, SOCK_DGRAM, IPPROTO_UDP)
        client.setsockopt(SOL_SOCKET, SO_REUSEPORT, 1)
        client.setsockopt(SOL_SOCKET, SO_BROADCAST, 1)

        client.bind(("", 42069))
        while True:
            data, addr = client.recvfrom(1024)
            returnedData = 0
            self.assertGreater(returnedData, 0, "recieved data should be above 0")



if __name__ == '__main__':
    unittest.main()

