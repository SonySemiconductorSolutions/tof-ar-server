## Contents

* [Download the App](#download)
* [About ToF AR](#about)
* [Overview of ToF AR Server](#overview)
* [Development environment](#environment)
* [Notes](#notes)
* [Contributing](#contributing)


<a name="download"></a>
# Download the App

Use tools to debug your ToF AR development.  
Download ToF AR Server now from App Store and Google Play and try it out!

[<img alt="Get it on the App Store" src="/Docs/images/App_Store_Badge_US-UK_092917.svg" height="60">](https://apps.apple.com/us/developer/id1601362415)
&nbsp;&nbsp;&nbsp;&nbsp;
[<img alt="Get it on Google Play" src="/Docs/images/google-play-badge_us.png" height="70">](https://play.google.com/store/apps/developer?id=Sony+Semiconductor+Solutions+Corporation)


<a name="about"></a>
# About ToF AR

ToF AR, Time of Flight Augmented Reality, is a toolkit library for Unity providing application developers with tools to start building immersive worlds for iOS and Android devices.
ToF AR mainly targets iOS and Android smartphones with a ToF sensor from Sony, but it also works with other depth sensors, such as structured light method sensors.

As well as ToF AR, Unity and compatible devices with ToF sensors are required to build and execute ToF AR Server.

Please see [the ToF AR Site on Developer World](https://developer.sony.com/develop/tof-ar) for ToF AR downloads and development guides, sample applications, and a list of compatible devices.


<a name="overview"></a>
# Overview of ToF AR Server

ToF AR Server is a program to debug applications using ToF AR.

You can debug your application executed on Unity Editor using the data captured from the ToF camera, when ToF AR Server is executed on a smartphone.


<a name="environment"></a>
# Development environment

## Build library

ToF AR is required for build. Download the ToF AR toolkit from [the ToF AR Site on Developer World](https://developer.sony.com/develop/tof-ar), then import it and use it in a Unity project. 
Note that both Base and Hand components are required by ToF AR Server. 

If the project is opened before ToF AR is set up, a confirmation message to enter safe mode may appear, depending on your settings. 
If you start Unity in safe mode, exit safe mode and then import the ToF AR package.


## Documents

To use ToF AR Server, see [Debug with TofARServer](https://developer.sony.com/develop/tof-ar/development-guides/docs/ToF_AR_User_Manual_en.html#_debug_with_tofarserver) in the [ToF AR user manual](https://developer.sony.com/develop/tof-ar/development-guides/docs/ToF_AR_User_Manual_en.html).

For known issues and platform limitations, see [Restrictions and limitations](https://developer.sony.com/develop/tof-ar/development-guides/docs/ToF_AR_User_Manual_en.html#_restrictions_and_limitations) in the [ToF AR user manual](https://developer.sony.com/develop/tof-ar/development-guides/docs/ToF_AR_User_Manual_en.html).


## Verification environment

Operation was verified in the following environment:

* Unity Version  : 2021.3.11f1
* ToF AR Version : 1.2.0


<a name="notes"></a>
# Notes

Be aware that recognizable hand gestures may have different meaning in countries/areas.  
Prior cultural checks are advisable.


<a name="contributing"></a>
# Contributing

**We cannot accept any Pull Request (PR) at this time.** 
However, you are always welcome to report bugs and request new features by creating issues.

We have released this program as a sample app with a goal of making ToF AR widely available. 
So please feel free to create issues for reporting bugs and requesting features, and we may update this program or add new features after getting feedback.
