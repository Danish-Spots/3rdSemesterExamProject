import unittest
import sys
sys.path.append("../") 
import Scripts as sens

class TestMain(unittest.TestCase):
    def test_main(self):
        mainMethod = sens.MainScript()
        