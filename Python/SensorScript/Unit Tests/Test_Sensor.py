import unittest
import sys
sys.path.append("../") 
import Scripts as sens

class TestSensor(unittest.TestCase):

    def test_sensor_data(self):
        sensor = sens.Sensor()
        self.assertGreater(sensor.get_temp(), 0, "getTemp should give temperature above 0, unless sensor is plugged in")

    

if __name__ == '__main__':
    unittest.main()
