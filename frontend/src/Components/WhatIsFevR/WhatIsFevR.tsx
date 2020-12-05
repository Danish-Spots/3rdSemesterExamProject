import React from 'react'
import "../../css/whatisfevr.scss";

interface WhatIsFevRProps {

}

export const WhatIsFevR: React.FC<WhatIsFevRProps> = ({}) => {
        return (
            <div className="main-container">
            <h1 style={{ marginTop: "100px" }}>What is FevR?</h1>
            <div className="description-container">
            <p>FevR is a high-temperature detecting system that is able to take the temperature of people without human-contact. 
              With the implementation of our devices in public places the spread of COVID and other diseases could be prevented. </p>

              <p>For our third semester project, we were tasked to create a system that makes use of a webpage, a raspberry pi, a console server and an azure hosted database and api. The exact architecture our project implemented will be discussed in our report. </p>


                <p>Throughout the year, the coronavirus has taken a hit on everyone’s lives. As a result, we came up with the idea of a temperature monitoring system called FevR. 
The idea is that the device can be installed in the entrance of public places (e.g. schools, libraries). The device checks the temperature of people who enter the place and gives immediate feedback if one has fever or not. This way, the chance of spreading corona virus and other diseases can radically be reduced.
It works by a Raspberry Pi getting temperatures using a sensor, and then uploading that to a database and then that data being accessed via a web page.</p>

<p>The sensor used was a MLX90614 BAA IR Thermometer sensor by Melaxis. This allows us to measure surface temperature of whatever object the sensor is being pointed at. It also has the ability to measure the ambient temperature, however we don't make use of this feature in our current iteration.</p>

<p>We also use a Sense Hat provided by the school to give immediate feedback on the temperatures measured. This is done by lighting up the 8x8 grid of LEDs with different colours depending on what kind of feedback needs to be given.

However, the usage of the Sense Hat is not a requirement. We have two programs that run on the Raspberry Pi, one makes use of the Sense Hat to provide immediate feedback, whereas the other one does not make use of it, and is for a more passive installation, that just gets temperature data.</p>

<p>We have also developed a web page that can be used to see all of this gathered temperature data, which include a map view displaying pins on it to places where the Raspberry Pi devices are installed with the program running.

The development methodology we used in order to develop such a project was XP, and we used some XP practices along with other practices we have used in the past, this will be discussed in more detail in a later section.
</p>
<p>This project was created as our 3rd semester project in Computer Science at Zealand Academy, Denmark.</p>

<p>Made with love by Andreas, Dominik, Justin</p>
</div>
            </div>
        );
}

export default WhatIsFevR