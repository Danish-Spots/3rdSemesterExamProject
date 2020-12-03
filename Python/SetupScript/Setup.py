import re
import requests

measuringSettings = [
    ["cooldown_timer", "3f", r"^[0-9]*(\.?[0-9]*)$",
     "Cooldown between measurements"],
    ["measuring_timer", "2f", r"^[0-9]*(\.?[0-9]*)$",
     "Time taken to measure temperature"],
    ["sensor_polling_rate", "0.1f",
        r"^[0-9]*(\.?[0-9]*)$", "Polling Rate of the sensor"],
    ["lower_bound_temp", "34f",
        r"^[0-9]*(\.?[0-9]*)$", "Lower temperature bound for the range where the measurement activates"],
    ["upper_bound_temp", "43f",
        r"^[0-9]*(\.?[0-9]*)$", "Upper temperature bound for the range where the measurement activates"],
    ["fever_temp", "37.5f", r"^[0-9]*(\.?[0-9]*)$",
     "Temperature at which the measured temperature is recording as being a fever"],
    ["port_number", "42069", r"^[0-9]*$",
        "The port number the script broadcasts on"],
    ["measurement_round", "3", r"^[0-9]*$",
        "The number of decimal places a measurement is rounded to"]
]


def PromptSetting(prompt, regexString, default):
    answer = None
    if (regexString != None):
        pattern = re.compile(regexString)

    while answer == None:
        print(prompt + " [Default: {}]".format(default))
        answer = input()

        if (answer == ""):
            if (default != None):
                return default
            if (regexString != None):
                print("The answer doesnt match the format required:", regexString)
            else:
                print("The answer doesnt match the format required")
        else:
            if (regexString != None):
                if (pattern.match(answer)):
                    if ("." in answer):
                        answer += "f"
                    return answer
            else:
                return answer
        answer = None


def ConcatSettingsFile(settingsList):
    settingsFile = ""

    for setting in settingsList:
        settingsFile += "# {}".format(setting[3])
        settingsFile += "\n"
        settingsFile += "{}: {}".format(setting[0], setting[1])
        settingsFile += "\n\n"
    return settingsFile


def Setup():
    for setting in measuringSettings:
        value = PromptSetting(setting[0], setting[2], setting[1])
        if (value != None):
            setting[1] = value

    location = PromptSetting(
        "Please write the address this pi is being setup on", None, None)
    profileID = int(PromptSetting(
        "Please write the companys profile ID", r"^[0-9]*$", None))
    name = PromptSetting("Please write the name of the pi", None, None)

    response = requests.get("https://nominatim.openstreetmap.org/search?q=" +
                            location.replace(" ", "%20")+"&format=jsonv2")
    jsonResponse = response.json()
    lat = jsonResponse[0]["lat"]
    lon = jsonResponse[0]["lon"]

    post = requests.post("https://fevr.azurewebsites.net/api/RaspberryPis", json={
        "id": 0,
        "location": name,
        "isActive": True,
        "profileID": profileID,
        "longitude": lon,
        "latitude": lat
    })

    #rpiID = post.text["id"]
    rpiID = 0
    settings = [
        ["rpi_id", rpiID, r"^[0-9]*(\.?[0-9]*)$",
         "Raspberry Pi Device ID"]
    ]

    settings += measuringSettings

    fileText = ConcatSettingsFile(settings)

    print(
        "Where should the settings be put? (Relative file path) [Default: settings.txt]")
    filePath = input()
    if (filePath == "" or filePath == None):
        filePath = "settings.txt"

    f = open(filePath, "w")
    f.write(fileText)
    f.close()

    print("settings have been written in the file {}".format(filePath))


if __name__ == "__main__":
    Setup()
