    Public Function CalculaNIF(ByVal NIF As String) As String
        'Calcula el NIF para ver si es correcto
        Dim sCalculaNif As String
        Dim DNI As String
        If Len(NIF.Trim) >= 8 Then
            DNI = Mid(NIF.Trim, 1, 8)
        Else
            DNI = NIF.Trim
        End If
        sCalculaNif = Mid("TRWAGMYFPDXBNJZSQVHLCKE", 1 + DNI Mod 23, 1)
        CalculaNIF = DNI & sCalculaNif      ' Devuelve el NIF con letra incluida
    End Function
