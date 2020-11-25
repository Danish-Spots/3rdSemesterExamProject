import unittest
import sys
import time
from socket import *
import asyncio




class TestBroadcast(unittest.TestCase):
    def test_broadcaster(self):
        client = socket(AF_INET, SOCK_DGRAM, IPPROTO_UDP)
        client.setsockopt(SOL_SOCKET, SO_BROADCAST, 1)
        
        client.bind(("", 42069))
        
        while True:
            
            data, addr = client.recvfrom(1024)
            returnedData = data.decode('utf-8') 
            
            print(returnedData)
            # if (float(returnedData) > 22.0):
            #     break
        self.assertGreater(float(returnedData), 22.0, "recieved data should be above 22.0")



if __name__ == '__main__':
    unittest.main()

