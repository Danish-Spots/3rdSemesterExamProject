
def ReadSettingsFile():
    try:
        f = open("Settings/settings.txt", "r")
        rpi_id_text = f.readline()
        f.close()
        rpi_id = rpi_id_text.split("=")[1].strip()
        return rpi_id
    except:
        print("No Settings File Found!")
        print("-----------------------")
        print("(To be developed)")
        return -1
