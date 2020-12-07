from Scripts import ReadSettingsFile
from Scripts import BroadCaster 
import Setup
import requests
import asyncio

def StartScript():
    settings_list = ReadSettingsFile()
    #print(settings_list[5])
    if settings_list == -1:
        Setup.Setup()       
        print("\nRerun the application to use the new settings")
        print("The application will now exit")
        exit()

    rpi_id = settings_list[0]
    response = requests.get("https://fevr.azurewebsites.net/api/RaspberryPis/" + str(rpi_id))
    if not response.ok:
        print("Unable to get device data from server\nThe application will now exit\nPlease try again later")
        exit()
    json_response = response.json()
    is_confirmed = json_response["isAccountConfirmed"]
    if not is_confirmed:
        print("This device has not been confirmed on the website yet\nThe application will now exit\nPlease rerun when you have confirmed the device on the website")
        exit()
    b = BroadCaster(settings_list)
    asyncio.run(b.start_broadcast())

StartScript()