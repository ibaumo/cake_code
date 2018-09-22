Private Function Importa_excel(ByVal ruta As String) As Boolean
        Importa_excel = True
        Dim ERR As String

        'Variable de tipo Aplicación de Excel   
        Dim objExcel As Excel.Application

        'Una variable de tipo Libro de Excel   
        Dim xLibro As Excel.Workbook

        'creamos un nuevo objeto excel   
        objExcel = New Excel.Application

        'Usamos el método open para abrir el archivo que está _   
        'en el directorio del programa llamado archivo.xls   
        xLibro = objExcel.Workbooks.Open(ruta)

        'Hacemos el Excel No Visible   
        objExcel.Visible = False


        'Abrir la conexion con la base de datos para escribir cada registro.
        Estado_conexion(conexion, "ABRIR")

        Dim da As New OleDb.OleDbDataAdapter("", conexion)
        da.InsertCommand = New OleDb.OleDbCommand("", conexion)

        'Se define la linea que se escribira en cada linea del fichero de texto.
        Dim linea As String = ""
        Dim valor As String = ""
        Dim fecha As String

        If (Me.txtMes.SelectedIndex + 1) > 9 Then
            fecha = "01/" & Me.txtMes.SelectedIndex + 1 & "/" & Me.txtAño.Value
        Else
            fecha = "01/0" & Me.txtMes.SelectedIndex + 1 & "/" & Me.txtAño.Value
        End If


        With xLibro
            ' Hacemos referencia a la Hoja   
            With .Sheets(1)

                'Se carga la linea de cada trabajador
                Dim i As Integer = 2

                Do While Not IsDBNull(.cells(i, 1).value) And .cells(i, 1).value <> Nothing

                    'Se hará un bucle para recorrer en cada fila todas las columnas y preparar la linea que se va a escribir. Total: 16 columnas
                    Dim col As Integer
                    linea = ""
                    For col = 1 To 4
                        'Asignamos el valor de la celda a la variable, y así trabajamos con ella en lugar de con la celda.
                        valor = .cells(i, col).value

                        linea &= valor & "','"

                    Next

                    col = 5 : valor = .cells(i, col).value

                    Select Case UCase(valor)
                        Case ("W1")
                            linea &= "Indefinido (TC)','"
                        Case ("W2")
                            linea &= "Indefinido (TP)','"
                        Case ("W3")
                            linea &= "Temporal (TC)','"
                        Case ("W4")
                            linea &= "Temporal (TP)','"
                        Case ("W5")
                            linea &= "Form/Aprend (TC)','"
                        Case ("W6")
                            linea &= "Form/Aprend (TP)','"
                        Case ("W7")
                            linea &= "Becario (TC)','"
                        Case ("W8")
                            linea &= "Becario (TP)','"
                    End Select

                    For col = 7 To 10
                        'Asignamos el valor de la celda a la variable, y así trabajamos con ella en lugar de con la celda.
                        valor = .cells(i, col).value

                        linea &= valor & "','"

                    Next

                    'Guardar el registro
                    da.InsertCommand.CommandText = "INSERT INTO DATOS([Nº PERS],NOMBRE,SOC,SOCIEDAD,[RELACIÓN LABORAL],[CLAVE DE ORGANIZACIÓN]," & _
                        "[CLAVE DE SEXO],[EDAD DEL EMPLEADO],FORMACIÓN,PERIODO,FECHA_ORDEN) VALUES('" & linea & _
                        Me.txtMes.Text & " " & Me.txtAño.Text & "','" & fecha & "')"

                    Try
                        da.InsertCommand.ExecuteNonQuery()
                    Catch ex As Exception
                        ERR = ex.Message
                        Importa_excel = False
                    End Try

                    i += 1

                Loop
            End With
        End With

        Estado_conexion(conexion, "CERRAR")

        'Eliminamos los objetos si ya no los usamos   
        objExcel.Quit()
        'objExcel = Nothing
        xLibro = Nothing


    End Function