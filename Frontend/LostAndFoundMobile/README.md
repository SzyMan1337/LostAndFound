# How to run Mobile App

First make sure you have installed following tools:
- Node.js
- Android Studio

Now you need to launch emulator:
1. Start Android Studio and open Virtual Device Manager
2. From there you can create and run emulator. Make sure to choose Android v.10.0 or higher

Mobile app needs a constant connection to the backend. Run it by following steps shown in Backend/README.md
When calling backend on the localhost, emulator will try to connect with it's own localhost not the one of your device.
That is why you need to redirect it. All of the requests are made to the gateway, we don't need to redirect ports of other services.
Open Windows PowerShell or other terminal and run (gateway is run on port 5000 by deafult)
> adb reverse tcp:5000 tcp:5000 

Now build and run mobile app:
1. Open Windows PowerShell or standard terminal depending on your Operating system
2. Go to Frontend\LostAndFoundMobile directory and run command
> npm install
3. Now you should be able to run app using
> npx react-native run-android


# How to run Mobile App tests

First install detox cli
> npm install detox-cli --global

Now you should configure detox to use your local emulator. Define it's name in the .detoxrc.js file.
For more details see the officiall detox site (step 3): 
https://wix.github.io/Detox/docs/introduction/getting-started

Run tests using the following command (it's recommended to start the emulator before running tests)
> detox test --configuration android.emu.debug
