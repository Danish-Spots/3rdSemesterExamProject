from Scripts import ReadSettingsFile
from Scripts import BroadCaster 
import asyncio

def StartScript():
    settings_list = ReadSettingsFile()
    print(settings_list[5])
    if settings_list[0] == -1:
        exit()

    b = BroadCaster(settings_list)
    asyncio.run(b.start_broadcast())

StartScript()