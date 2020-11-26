import unittest
import sys
sys.path.append("../") 
import Scripts as sens

class TestSensor(unittest.TestCase):

    def test_sensor_data(self):
        sensor = sens.Sensor()
        self.assertGreater(sensor.get_temp(), 0, "getTemp should give temperature above 0, throw error if no sensor is plugged in or the RPI is not used to test")

    def test_sensor_data_ambient(self):
        sensor = sens.Sensor()
        self.assertGreater(sensor.get_ambient_temp(), 0, "get_ambient_temp should give temperature above 0, throw error if no sensor is plugged in or the RPI is not used to test")
    

if __name__ == '__main__':
    unittest.main()
