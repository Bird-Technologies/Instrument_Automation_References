Imports NationalInstruments.Visa

Module Module1

    Sub Main()
        ' Initialize the resource manager and open a VISA session
        Dim rmSession As New ResourceManager()

        Dim devices() As String = rmSession.Find("(ASRL|GPIB|TCPIP|USB)?*")
        For Each device In devices
            Console.WriteLine(device)
        Next

        Dim mbSession As MessageBasedSession = rmSession.Open("TCPIP0::127.0.0.1::inst0::INSTR")

        mbSession.RawIO.Write("*RST\n")

        mbSession.RawIO.Write("*IDN?\n")
        Dim temp2 As String = mbSession.RawIO.ReadString()
        Console.WriteLine("{0}", temp2)

        ' Use the query function
        Console.Write("{0}", Query(mbSession, "*IDN?"))


        mbSession.Dispose()

    End Sub

    Function Query(ByVal sess As MessageBasedSession, ByVal command As String) As String
        sess.RawIO.Write(command + "\n")
        Return sess.RawIO.ReadString()
    End Function
End Module
