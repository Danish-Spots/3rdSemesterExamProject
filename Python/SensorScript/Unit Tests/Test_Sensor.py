import unittest
import sys
sys.path.append("../") 
import Scripts as sens

class TestSensor(unittest.TestCase):

    def test_sensor_data(self):
        sensor = sens.Sensor()
        self.assertGreater(0, sensor.get_temp(), "getTemp should give temperature below 0, unless sensor is plugged in")

    

if __name__ == '__main__':
    unittest.main()
