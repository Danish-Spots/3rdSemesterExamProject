# 3rdSemesterExamProject

Installation Instructions
These are the required steps to get this solution to work
Raspberry Pi - Python
To run any of the python based programs in this solution (RunScripts.py in both SensorScripts and SensorWithSenseHatScript), you will need to install some package dependencies. These are the following commands to install the dependencies:
sudo pip3 install adafruit-circuitpython-mlx90614
sudo pip3 install adafruit-circuitpython-busdevice
Bus device should be installed off of the mlx90614 installation, however it is listed here in case it doesn't get installed.
To use the version of the program that makes use of the SenseHat, you will need to run the following command as well.
sudo apt install sense-hat
After running those commands, especially the SenseHat installation, it is recommended that you reboot the Raspberry Pi by doing the following command.
sudo reboot
After that ensure that the I2C interface is enabled and then everything should work. (you can check by using a graphical interface and clicking the following buttons Raspberry Pi start logo → Preferences → Raspberry Pi Configuration → Interfaces. You can then see if it is enabled or not by looking in the list, if not click the enable radio button and restart the pi.) 
Console Server - C#
To run the console server, you will need Visual Studio to compile and run it. You can either turn it into a release and run the .exe that is created from that, or you can run it from directly inside of Visual Studio.
Front End - React with Typescript
Make sure node.js is installed. If not, go here to download and install the downloaded file (pick either version): 
https://nodejs.org/en/

Open a command prompt in the front end folder and type “npm install”.
After that task is completed type “npm start” to start the development server.S
API and Database
Both are hosted on Azure and don't require any setup or installation, however the files for our API are in the release, but not the scripts to make the database.
