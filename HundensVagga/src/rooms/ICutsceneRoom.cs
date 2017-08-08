namespace HundensVagga {
    internal interface ICutsceneRoom : IRoom {
        string ExitRoomName { get; }

        bool ShouldGoToExit();

        void Stop();
    }
}