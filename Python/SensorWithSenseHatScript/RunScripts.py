from Scripts import ReadSettingsFile
from Scripts import BroadCaster 
import asyncio

def StartScript():
    rpi_id = ReadSettingsFile()
    if rpi_id == -1:
        exit()

    b = BroadCaster(rpi_id)
    asyncio.run(b.start_broadcast())

StartScript()