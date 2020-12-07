
def ReadSettingsFile():
    try:
        return_list = []
        f = open("../Settings/settings.txt", "r")
        print("test")
        entire_text = f.read()
        split_text = entire_text.split("\n")
        #split_text = [i for i in split_text if i]
        split_text = list(filter(None, split_text))
        f.close()
        for s in split_text:
            if(s[0] != '#'):
                return_list.append(ReturnValue(s))
        return return_list
    except:
        print("No Settings File Found!")
        print("-----------------------")
        print("(To be developed)")
        return -1

def ReturnValue(file_readline):
    text = file_readline.split(":")[1].strip()
    if text[len(text)-1] == 'f':
        text = text[0:len(text)-1]
        return float(text)
    return int(text)

ReadSettingsFile()

