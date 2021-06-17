Additional task:
The ticket below should be send directly to the print device from the list. 
By double click you should get the separate view with print button along with a preview of the ticket.
<p align="center">
  <img width="320" src="https://raw.githubusercontent.com/omahost/SystemMonitor/main/screenshots/test_ticket.png">
</p>

Notes: Printing was tested localy and on remotely own 3-inch Direct Thermal POS Printer.

<p align="center">
  <img width="320" src="https://raw.githubusercontent.com/omahost/SystemMonitor/main/screenshots/print_result.jpg">
</p>

Original Task:
1) Task description:
Write a windows desktop application that checks devices connected via ports (ethernet, usb), display parameters, statuses (on / off, error, at work).

2) Was implemented:
- getting USB and Printer devices connected via ports (ethernet, usb)

- displaying basic information for devices (Device ID, Description etc)

- displaying statuses like (on / off, error, at work).
Note: was not tested/implemented all scenarios (handling and mapping custom device status into our type).
Note: Main purpose of code and implementation was to show how are used design patterns and architecture layers.

- event detection for inserting/removing devices, so inserting new devices will automatically appear in list.

- architecture/foundation is made flexible, so it will be easier to add other kind of devices to analyze

3) Left to implement:
- unit tests for main parts and classes of application
- event handling for device property change, to auto update item in grid
