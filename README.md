To view video of working application see next video:
https://www.youtube.com/watch?v=cTRfOLWLcho

Additional task:
1) Task description:
User in the settings provide the JSON url (GET). 
By clicking on the button secured by the PIN code you have the ability to change the URL as well.  
The PIN for verification will be always the same. Let's say 90021213.

A corresponding payload is sent to the same url POST, identifying the print task details / printout / status. 
Once you save the settings the application checks periodically the url and when it comes a new order / receipt appears, 
Example of JSON could be found in /examples/print.json.txt.
It should print out (ideally, if you avoid cyclical server polling, and for example use the socket that automatically recognized the appearance of a new order)

In the application settings, you should be able to assign a task to a specific device.  
For the test task, consider 2 devices - the order.kitchen task, and the other order.bar
You can assign the specific printer from discovered list of devices.

On the separate view User has the ability to view printed + waiting orders (if any) or even cancel it as well.

2) Was implemented:

2.1) I avoided cyclical server polling, and have used the easiest way by using long-polling technique: client will make a request and will be waiting until server will send a response and only after response will came, then will handle it and make next request for getting new orders.

2.2)  I have implemented the following:
Preconditions: user does not link task type (e.g Kitchen) to the device in settings dialog and there are pending (not printed) orders.
Scenario: If the user links the task to the device, then all pending orders will be printed.

2.3) I have realized that order could be canceled.

2.4) Order history is persisted only during application run (as it was easiest and faster way)

2.5) Orders are auto generated each 30 seconds. I have created my own endpoint for an API:
http://test.devitrust.com/order/

=====================================================================================

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

=====================================================================================

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
