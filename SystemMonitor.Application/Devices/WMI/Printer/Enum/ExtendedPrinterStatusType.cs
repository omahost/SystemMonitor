namespace SystemMonitor.Application.Devices
{
    // https://docs.microsoft.com/en-us/windows/win32/cimwin32prov/win32-printer
    public enum ExtendedPrinterStatusType
    {
        Undefined = 0,
        Other = 1,
        Unknown = 2,
        Idle = 3,
        Printing = 4,
        WarmingUp = 5,
        StoppedPrinting = 6,
        Offline = 7,
        Paused = 8,
        Error = 9,
        Busy = 10,
        NotAvailable = 11,
        Waiting = 12,
        Processing = 13,
        Initialization = 14,
        PowerSave = 15,
        PendingDeletion = 16,
        IOActive = 17,
        ManualFeed = 18
    }
}
