# Room Display Project

This project is a custom .NET application designed for a dedicated Raspberry Pi setup in my room. It uses Avalonia with an MVVM architecture. 

## Display Specifications
* **Resolution:** 1024 x 600
* **UI Optimization:** The top settings layer/taskbar is configured to **auto-hide** (move up when not in use) to maximize the vertical real estate of the 600px height.

## Prerequisites
To build this project, you must have the **.NET 10 SDK** installed on your development machine. (Follow instructions online)

## Build and Deployment

### 1. Build
Run the following command to build the project for the Raspberry Pi (ARM64 architecture):
```bash
dotnet publish -r linux-arm64 --self-contained -c Release
```

### 2. Transfer Exec to Pi
```bash
scp -r bin/Release/net10.0/linux-arm64/publish/ rasp_pi 
```

### 3. Run on Pi
```bash
export DISPLAY=:0;cd ~/App_Deployed/;nohup ./TrainApp &
```


## Preview 

![Example Layout](./example.png) 