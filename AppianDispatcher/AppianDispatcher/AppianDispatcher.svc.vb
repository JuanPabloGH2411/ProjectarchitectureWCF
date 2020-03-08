Imports System.Xml
Imports AppianDispatcher.ConnectionBroker.DataManagerClient
Imports System.ServiceModel.Activation
Imports System.IO
Imports System.Diagnostics
Imports System.Data.SqlClient

<ServiceContract()>
Public Interface IService
    ' <OperationContract()> _
    '<WebGet(UriTemplate:="/sendDisbursement?xmlData={xmlData}", ResponseFormat:=WebMessageFormat.Xml)> _
    ' Function SendDisbursement(ByVal xmlData As String) As String

    <OperationContract(Name:="sendCustomer")>
    <WebInvoke(Method:="POST", UriTemplate:="sendCustomer")>
    Function SendCustomer(ByVal xmlData As Stream) As String

    <OperationContract(Name:="sendAddress")>
    <WebInvoke(Method:="POST", UriTemplate:="sendAddress")>
    Function SendAddress(ByVal xmlData As Stream) As String

    <OperationContract(Name:="sendJob")>
    <WebInvoke(Method:="POST", UriTemplate:="sendJob")>
    Function SendJob(ByVal xmlData As Stream) As String

    <OperationContract(Name:="sendReference")>
    <WebInvoke(Method:="POST", UriTemplate:="sendReference")>
    Function SendReference(ByVal xmlData As Stream) As String

    <OperationContract(Name:="sendRequest")>
    <WebInvoke(Method:="POST", UriTemplate:="sendRequest")>
    Function SendRequest(ByVal xmlData As Stream) As String

    <OperationContract(Name:="SendPreAuthorization")>
    <WebInvoke(Method:="POST", UriTemplate:="SendPreAuthorization")>
    Function SendPreAuthorization(ByVal xmlData As Stream) As String

    <OperationContract(Name:="SendFile")>
    <WebInvoke(Method:="POST", UriTemplate:="SendFile")>
    Function SendFile(ByVal imgDataB64 As Stream) As String

    <OperationContract(Name:="SendAdditionalData")>
    <WebInvoke(Method:="POST", UriTemplate:="SendAdditionalData")>
    Function SendAdditionalData(ByVal xmlData As Stream) As String

    <OperationContract(Name:="GetAutomaticCustomerNumber")>
    <WebInvoke(Method:="POST", UriTemplate:="GetAutomaticCustomerNumber")>
    Function GetAutomaticCustomerNumber(ByVal xmlData As Stream) As String

    '<OperationContract(Name:="GetAutomaticCustomerNumberTest")>
    '<WebInvoke(Method:="POST", UriTemplate:="GetAutomaticCustomerNumberTest")>
    'Function GetAutomaticCustomerNumberTest(ByVal xmlData As Stream) As String

    <OperationContract(Name:="GetAutomaticCustomerNumb")>
    <WebInvoke(Method:="POST", UriTemplate:="GetAutomaticCustomerNumb")>
    Function GetAutomaticCustomerNumb(ByVal xmlData As Stream) As String

    '<OperationContract(Name:="GetBranchConsultation")>
    '<WebInvoke(Method:="POST", UriTemplate:="GetBranchConsultation")>
    'Function GetBranchConsultation(ByVal xmlData As Stream) As String


End Interface

<AspNetCompatibilityRequirements(RequirementsMode:=Activation.AspNetCompatibilityRequirementsMode.Required)>
Public Class DataReciever
    Implements IService

    'Public Function SendDisbursement(ByVal xmlData As String) As String Implements IService.SendDisbursement
    '    Dim lobjDisbursement As ConnectionBroker.DataManagerClient = Nothing
    '    Dim lintDispatch As Integer = 0

    '    Try
    '        lintDispatch = InitDispatch("SendDisbursement", xmlData)
    '        'Se inicia la transacción de registro
    '        If lintDispatch = 0 Then
    '            Return "<ROOT><TRANSCODE>0</TRANSCODE><DESCRIPTION>No se pudo generar token de transacción.</DESCRIPTION><RETRY>1</RETRY></ROOT>"
    '        End If

    '        Dim lxmlDocument As XmlDocument = Nothing
    '        Dim lxmlNode As XmlNode = Nothing
    '        Dim lstrSQL As String = ""
    '        Dim lstrError As String = ""
    '        Dim lintLogId As Integer = 0

    '        If xmlData.Length = 0 Then Throw New Exception("[ServiceDispatcherError]" & vbCrLf & "El documento XML no puede ir vacío.")
    '        lxmlDocument = New XmlDocument
    '        lxmlDocument.LoadXml(xmlData)
    '        lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Customer_Number")
    '        If lxmlNode Is Nothing Then lstrError = "El nodo de Número de Cliente (Customer_Number) no está contenido en el XML." & vbCrLf
    '        lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Loan_Reference")
    '        If lxmlNode Is Nothing Then lstrError += "El nodo de Número de Operación (Loan_Reference) no está contenido en el XML." & vbCrLf
    '        lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Disbursement_Code")
    '        If lxmlNode Is Nothing Then lstrError += "El nodo de Código de Dispersión (Disbursement_Code) no está contenido en el XML." & vbCrLf
    '        lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("RFC")
    '        If lxmlNode Is Nothing Then lstrError += "El nodo de RFC no está contenido en el XML." & vbCrLf
    '        lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Disbursement_Date")
    '        If lxmlNode Is Nothing Then lstrError += "El nodo de Fecha de Dispersión (Disbursement_Date) no está contenido en el XML." & vbCrLf
    '        lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Status_Date")
    '        If lxmlNode Is Nothing Then lstrError += "El nodo de Fecha Estatus (Status_Date) no está contenido en el XML." & vbCrLf
    '        lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Disbursement_Status")
    '        If lxmlNode Is Nothing Then lstrError += "El nodo de Estatus de Dispersión (Disbursement_Status) no está contenido en el XML." & vbCrLf
    '        lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Company_Code")
    '        If lxmlNode Is Nothing Then lstrError += "El nodo de Clave de Distribuidor (Company_Code) no está contenido en el XML." & vbCrLf
    '        lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Request_Number")
    '        If lxmlNode Is Nothing Then lstrError += "El nodo de Número de Consecutivo (Request_Number) no está contenido en el XML." & vbCrLf
    '        lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Celular_Phone")
    '        If lxmlNode Is Nothing Then lstrError += "El nodo de Teléfono Celular (Celular_Phone) no está contenido en el XML." & vbCrLf
    '        lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("eMail")
    '        If lxmlNode Is Nothing Then lstrError += "El nodo de Correo Electrónico (eMail) no está contenido en el XML." & vbCrLf
    '        lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Bank_Account")
    '        If lxmlNode Is Nothing Then lstrError += "El nodo de Cuenta Bancaria (Bank_Account) no está contenido en el XML." & vbCrLf
    '        lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("CLABE")
    '        If lxmlNode Is Nothing Then lstrError += "El nodo de Cuenta CLABE (CLABE) no está contenido en el XML." & vbCrLf
    '        lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Bank_Code")
    '        If lxmlNode Is Nothing Then lstrError += "El nodo de Código de Banco (Bank_Code) no está contenido en el XML." & vbCrLf
    '        lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Comments")
    '        If lxmlNode Is Nothing Then lstrError += "El nodo de Comentarios (Comments) no está contenido en el XML."

    '        If lstrError.Length > 0 Then Throw New Exception("[ServiceDispatcherError]" & vbCrLf & lstrError)
    '        If lxmlDocument.DocumentElement.SelectSingleNode("Customer_Number").InnerText.Length = 0 Then lstrError = "El Número de Cliente es un dato requerido y no puede ir vacío." & vbCrLf
    '        If lxmlDocument.DocumentElement.SelectSingleNode("Loan_Reference").InnerText.Length = 0 Then lstrError += "El Número de Operacion es un dato requerido y no puede ir vacío." & vbCrLf
    '        If lxmlDocument.DocumentElement.SelectSingleNode("Disbursement_Code").InnerText.Length = 0 Then lstrError += "El Código de Dispersión es un dato requerido y no puede ir vacío." & vbCrLf
    '        If lxmlDocument.DocumentElement.SelectSingleNode("Company_Code").InnerText.Length = 0 Then lstrError += "La Clave de Distribuidor es un dato requerido y no puede ir vacío." & vbCrLf
    '        If lxmlDocument.DocumentElement.SelectSingleNode("Request_Number").InnerText.Length = 0 Then lstrError += "El Número de Consecutivo es un dato requerido y no puede ir vacío." & vbCrLf
    '        If lstrError.Length > 0 Then Throw New Exception("[ServiceDispatcherError]" & vbCrLf & lstrError)

    '        'validacion 
    '        'If Not Request_Exists(Mid(lxmlDocument.DocumentElement.SelectSingleNode("Request_Number").InnerText.Trim.Replace("'", "").ToUpper, 1, 15).ToString) Then lstrError += "El consecutivo no cuenta con registro de Solicitud" & vbCrLf
    '        'If lstrError.Length > 0 Then Throw New Exception("[ServiceDispatcherError]" & vbCrLf & lstrError)

    '        lobjDisbursement = New ConnectionBroker.DataManagerClient
    '        lobjDisbursement.Open()
    '        lobjDisbursement.setConnectionString(System.Configuration.ConfigurationManager.ConnectionStrings("MainConnection").ConnectionString)
    '        lobjDisbursement.setReturnIdentity(True)

    '        lstrSQL = "INSERT INTO BPM_Disbursements_Queue (Id_Dispatch, Customer_Number, Loan_Reference, Disbursement_Code, RFC, Disbursement_Date, Status_Date, Disbursement_Status, Company_Code," & vbCrLf & _
    '        "Request_Number, Celular_Phone, eMail, Bank_Account, CLABE, Bank_Code, Transfer_Status, Comments, Created_Date) VALUES (" & lintDispatch & ", '" & _
    '        "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Customer_Number").InnerText.Trim.Replace("'", "").ToUpper, 1, 14) & "', '" & _
    '        "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Loan_Reference").InnerText.Trim.Replace("'", "").ToUpper, 1, 12) & "', " & _
    '        "" & lxmlDocument.DocumentElement.SelectSingleNode("Disbursement_Code").InnerText.Trim & ", '" & _
    '        "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("RFC").InnerText.Trim.Replace("'", "").ToUpper, 1, 13) & "', '" & _
    '        "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Disbursement_Date").InnerText.Trim.Replace("'", "").ToUpper, 1, 10) & "', '" & _
    '        "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Status_Date").InnerText.Trim.Replace("'", "").ToUpper, 1, 10) & "', '" & _
    '        "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Disbursement_Status").InnerText.Trim.Replace("'", "").ToUpper, 1, 2) & "', '" & _
    '        "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Company_Code").InnerText.Trim.Replace("'", "").ToUpper, 1, 5) & "', '" & _
    '        "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Request_Number").InnerText.Trim.Replace("'", "").ToUpper, 1, 15) & "', '" & _
    '        "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Celular_Phone").InnerText.Trim.Replace("'", "").ToUpper, 1, 10) & "', '" & _
    '        "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("eMail").InnerText.Trim.Replace("'", "").ToUpper, 1, 250) & "', '" & _
    '        "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Bank_Account").InnerText.Trim.Replace("'", "").ToUpper, 1, 15) & "', '" & _
    '        "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("CLABE").InnerText.Trim.Replace("'", "").ToUpper, 1, 18) & "', '" & _
    '        "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Bank_Code").InnerText.Trim.Replace("'", "").ToUpper, 1, 5) & "', " & _
    '        "0, '" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Comments").InnerText.Trim.Replace("'", "").ToUpper, 1, 500) & "', " & _
    '        "GETDATE())"
    '        lobjDisbursement.ExecuteCommand(lstrSQL)
    '        lintLogId = lobjDisbursement.getIdentityValue
    '        lobjDisbursement.Finalize()
    '        lobjDisbursement.Close()
    '        CloseDispatch(lintDispatch)
    '        Return "<ROOT><TRANSCODE>" & lintDispatch & "</TRANSCODE><DESCRIPTION>Transacción exitosa</DESCRIPTION><RETRY>0</RETRY></ROOT>"
    '    Catch ex As Exception
    '        SendDisbursement = 0
    '        TrackError("SendDisbursement" & vbCrLf & vbCrLf & ex.Message.ToString.Replace("""", "").Replace("<", "").Replace(">", "").Replace("[ServiceDispatcherError]", "") & vbCrLf & "Id_Dispatch :" & lintDispatch & vbCrLf & vbCrLf & xmlData)
    '        Return "<ROOT><TRANSCODE>0</TRANSCODE><DESCRIPTION>" & ex.Message.ToString.Replace("""", "").Replace("<", "").Replace(">", "").Replace("[ServiceDispatcherError]", "") & "</DESCRIPTION>" & IIf(ex.Message.ToString.Contains("[ServiceDispatcherError]"), "<RETRY>0</RETRY>", "<RETRY>1</RETRY>") & "</ROOT>"
    '    Finally
    '        If Not lobjDisbursement Is Nothing Then lobjDisbursement = Nothing
    '    End Try
    'End Function

    'Public Function GetBranchConsultation(ByVal xmlData As Stream) As String Implements IService.GetBranchConsultation
    '    Dim lsrReaderXML As New StreamReader(xmlData)
    '    Dim lstrDataXML As String = lsrReaderXML.ReadToEnd
    '    Dim lintDispatch As Integer = 0

    '    Dim lstrSQL As String = ""

    '    Try
    '        lintDispatch = InitPublicDispatch("GetAutomaticCustomer", lstrDataXML)
    '        'Se inicia la transacción de registro
    '        If lintDispatch = 0 Then
    '            Return "<ROOT><TRANSCODE>0</TRANSCODE><DESCRIPTION>No se pudo generar token de transacción.</DESCRIPTION><RETRY>1</RETRY></ROOT>"
    '        End If

    '        Dim lxmlDocument As XmlDocument = Nothing
    '        Dim lxmlNode As XmlNode = Nothing
    '        Dim lstrError As String = ""
    '        Dim lintLogId As Integer = 0

    '        If lstrDataXML.Length = 0 Then Throw New Exception("[ServiceDispatcherError]" & vbCrLf & "El documento XML no puede ir vacío.")
    '        lxmlDocument = New XmlDocument
    '        lxmlDocument.LoadXml(lstrDataXML)


    '        lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Company_Code")
    '        If lxmlNode Is Nothing Then lstrError = "El nodo de Distribuidor (Company_Code) no está contenido en el XML." & vbCrLf
    '        lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Branch_Code")
    '        If lxmlNode Is Nothing Then lstrError = "El nodo de Sucursal (Branch_Code) no está contenido en el XML." & vbCrLf


    '        If lstrError.Length > 0 Then Throw New Exception("[ServiceDispatcherError]" & vbCrLf & lstrError)

    '        If lxmlDocument.DocumentElement.SelectSingleNode("Company_Code").InnerText.Length = 0 Then lstrError += "El Distribuidor es un dato requerido y no puede ir vacío." & vbCrLf
    '        If lxmlDocument.DocumentElement.SelectSingleNode("Branch_Code").InnerText.Length = 0 Then lstrError += "La Sucursal es un dato requerido y no puede ir vacío." & vbCrLf

    '        If lstrError.Length > 0 Then Throw New Exception("[ServiceDispatcherError]" & vbCrLf & lstrError)

    '        Dim Company_Code As String,
    '                Branch_Code As String


    '        Company_Code = Mid(lxmlDocument.DocumentElement.SelectSingleNode("Company_Code").InnerText.Trim.Replace("'", "").ToUpper, 1, 25)
    '        Branch_Code = Mid(lxmlDocument.DocumentElement.SelectSingleNode("Branch_Code").InnerText.Trim.Replace("'", "").ToUpper, 1, 25)

    '        Dim CRMXKIOVMBD09 As String = ConfigurationManager.ConnectionStrings("CRMXKIOVMBD09").ConnectionString
    '        Dim CRMXKIOVMBD11DXN As String = ConfigurationManager.ConnectionStrings("CRMXKIOVMBD11DXN").ConnectionString
    '        Dim CRMXKIOVMBD11CA As String = ConfigurationManager.ConnectionStrings("CRMXKIOVMBD11CA").ConnectionString

    '        ''''''''''''''''''''''''''

    '        Dim Opcon11DXN As New SqlConnection(CRMXKIOVMBD11)

    '        Dim cmd As SqlCommand = New SqlCommand("Cliente_sp_ObtenId", Opcon11DXN)


    '        cmd.CommandType = CommandType.StoredProcedure
    '        cmd.Parameters.AddWithValue("@apellido_paterno", Apellido_Paterno)
    '        cmd.Parameters.AddWithValue("@apellido_materno", Apellido_Materno)
    '        cmd.Parameters.AddWithValue("@primer_nombre", Primer_Nombre)
    '        cmd.Parameters.AddWithValue("@segundo_nombre", Segundo_Nombre)
    '        cmd.Parameters.AddWithValue("@rfc", RFC)
    '        cmd.Parameters.AddWithValue("@fechanac", Fecha_Nacimiento)
    '        cmd.Parameters.Add("@numero_solicitud", SqlDbType.VarChar, 30)
    '        cmd.Parameters("@numero_solicitud").Direction = ParameterDirection.Output

    '        Opcon11DXN.Open()
    '        cmd.ExecuteNonQuery()

    '        resultado = cmd.Parameters("@numero_solicitud").Value.ToString()
    '        cmd.Dispose()
    '        Opcon11DXN.Close()
    '        Opcon11DXN.Dispose()


    '        If resultado = "0" Then



    '            Dim Opcon09 As New SqlConnection(CRMXKIOVMBD09)
    '            Dim cmd2 As SqlCommand = New SqlCommand("Cliente_sp_ObtenId_TEST_JP", Opcon09)


    '            cmd2.CommandType = CommandType.StoredProcedure
    '            cmd2.Parameters.AddWithValue("@apellido_paterno", Apellido_Paterno)
    '            cmd2.Parameters.AddWithValue("@apellido_materno", Apellido_Materno)
    '            cmd2.Parameters.AddWithValue("@primer_nombre", Primer_Nombre)
    '            cmd2.Parameters.AddWithValue("@segundo_nombre", Segundo_Nombre)
    '            cmd2.Parameters.AddWithValue("@rfc", RFC)
    '            cmd2.Parameters.AddWithValue("@fechanac", Fecha_Nacimiento)
    '            cmd2.Parameters.Add("@numero_solicitud", SqlDbType.VarChar, 30)
    '            cmd2.Parameters("@numero_solicitud").Direction = ParameterDirection.Output

    '            Opcon09.Open()
    '            cmd2.ExecuteNonQuery()

    '            resultado2 = cmd2.Parameters("@numero_solicitud").Value.ToString()
    '            cmd2.Dispose()
    '            resultado = resultado2
    '            Opcon09.Close()
    '            Opcon09.Dispose()
    '        End If


    '        ClosePublicDispatch(lintDispatch, resultado)
    '        Return "<ROOT><TRANSCODE>" & lintDispatch & "</TRANSCODE><RESPONSE>" & resultado & "</RESPONSE><RETRY>0</RETRY></ROOT>"
    '    Catch ex As Exception
    '        GetBranchConsultation = 0
    '        TrackError("SendCustomer" & vbCrLf & vbCrLf & ex.Message.ToString.Replace("""", "").Replace("<", "").Replace(">", "").Replace("[ServiceDispatcherError]", "") & vbCrLf & "Id_Dispatch :" & lintDispatch & vbCrLf & vbCrLf & lstrDataXML)
    '        Return "<ROOT><TRANSCODE>0</TRANSCODE><RESPONSE>" & ex.Message.ToString.Replace("""", "").Replace("<", "").Replace(">", "").Replace("[ServiceDispatcherError]", "") & "</RESPONSE>" & IIf(ex.Message.ToString.Contains("[ServiceDispatcherError]"), "<RETRY>0</RETRY>", "<RETRY>1</RETRY>") & "</ROOT>"


    '    Finally
    '    End Try
    'End Function


    'Public Function GetAutomaticCustomerNumberTest(ByVal xmlData As Stream) As String Implements IService.GetAutomaticCustomerNumberTest
    '    Dim lsrReaderXML As New StreamReader(xmlData)
    '    Dim lstrDataXML As String = lsrReaderXML.ReadToEnd
    '    Dim lintDispatch As Integer = 0

    '    Dim lstrSQL As String = ""

    '    Try
    '        lintDispatch = InitPublicDispatch("GetAutomaticCustomer", lstrDataXML)
    '        'Se inicia la transacción de registro
    '        If lintDispatch = 0 Then
    '            Return "<ROOT><TRANSCODE>0</TRANSCODE><DESCRIPTION>No se pudo generar token de transacción.</DESCRIPTION><RETRY>1</RETRY></ROOT>"
    '        End If

    '        Dim lxmlDocument As XmlDocument = Nothing
    '        Dim lxmlNode As XmlNode = Nothing
    '        Dim lstrError As String = ""
    '        Dim lintLogId As Integer = 0

    '        If lstrDataXML.Length = 0 Then Throw New Exception("[ServiceDispatcherError]" & vbCrLf & "El documento XML no puede ir vacío.")
    '        lxmlDocument = New XmlDocument
    '        lxmlDocument.LoadXml(lstrDataXML)


    '        lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("First_Name")
    '        If lxmlNode Is Nothing Then lstrError += "El nodo de Fecha de Primer Nombre (First_Name) no está contenido en el XML." & vbCrLf
    '        lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Middle_Name")
    '        If lxmlNode Is Nothing Then lstrError += "El nodo de Segundo Nombre (Middle_Name) no está contenido en el XML." & vbCrLf
    '        lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Paternal_Name")
    '        If lxmlNode Is Nothing Then lstrError += "El nodo de Apellido Paterno (Paternal_Name) no está contenido en el XML." & vbCrLf
    '        lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Maternal_Name")
    '        If lxmlNode Is Nothing Then lstrError += "El nodo de Apellido Materno (Maternal_Name) no está contenido en el XML." & vbCrLf
    '        lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Birth_date")
    '        If lxmlNode Is Nothing Then lstrError += "El nodo de Fecha de Nacimiento (Birth_date) no está contenido en el XML." & vbCrLf
    '        lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("RFC")
    '        If lxmlNode Is Nothing Then lstrError += "El nodo de RFC (RFC) no está contenido en el XML." & vbCrLf
    '        lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("check_Disp")
    '        If lxmlNode Is Nothing Then lstrError += "El nodo check_Disp (check_Disp) no está contenido en el XML." & vbCrLf

    '        If lstrError.Length > 0 Then Throw New Exception("[ServiceDispatcherError]" & vbCrLf & lstrError)

    '        If lxmlDocument.DocumentElement.SelectSingleNode("Paternal_Name").InnerText.Length = 0 Then
    '            ClosePublicDispatch(lintDispatch, "vacio")
    '            Return "<ROOT><TRANSCODE>" & lintDispatch & "</TRANSCODE><RESPONSE>" & " " & "</RESPONSE><RETRY>0</RETRY></ROOT>"
    '        End If

    '        If lxmlDocument.DocumentElement.SelectSingleNode("First_Name").InnerText.Length = 0 Then lstrError += "El Primer Nombre es un dato requerido y no puede ir vacío." & vbCrLf
    '        'If lxmlDocument.DocumentElement.SelectSingleNode("Paternal_Name").InnerText.Length = 0 Then lstrError += "El Apellido Paterno es un dato requerido y no puede ir vacío." & vbCrLf
    '        'If lxmlDocument.DocumentElement.SelectSingleNode("Maternal_Name").InnerText.Length = 0 Then lstrError += "El Apellido Materno es un dato requerido y no puede ir vacío." & vbCrLf
    '        If lxmlDocument.DocumentElement.SelectSingleNode("Birth_date").InnerText.Length = 0 Then lstrError += "La Fecha de Nacimiento es un dato requerido y no puede ir vacío." & vbCrLf
    '        If lxmlDocument.DocumentElement.SelectSingleNode("RFC").InnerText.Length = 0 Then lstrError += "El RFC es un dato requerido y no puede ir vacío." & vbCrLf
    '        If lxmlDocument.DocumentElement.SelectSingleNode("check_Disp").InnerText.Length = 0 Then lstrError += "El check_Disp es un dato requerido y no puede ir vacío." & vbCrLf

    '        If lstrError.Length > 0 Then Throw New Exception("[ServiceDispatcherError]" & vbCrLf & lstrError)

    '        Dim Apellido_Paterno As String,
    '                Apellido_Materno As String,
    '                Primer_Nombre As String,
    '                Segundo_Nombre As String,
    '                RFC As String,
    '                Fecha_Nacimiento As String,
    '                resultado As String,
    '                resultado2 As String,
    '                check_Disp As String


    '        Apellido_Paterno = Mid(lxmlDocument.DocumentElement.SelectSingleNode("Paternal_Name").InnerText.Trim.Replace("'", "").ToUpper, 1, 25)
    '        Apellido_Materno = Mid(lxmlDocument.DocumentElement.SelectSingleNode("Maternal_Name").InnerText.Trim.Replace("'", "").ToUpper, 1, 25)
    '        Primer_Nombre = Mid(lxmlDocument.DocumentElement.SelectSingleNode("First_Name").InnerText.Trim.Replace("'", "").ToUpper, 1, 20)
    '        Segundo_Nombre = Mid(lxmlDocument.DocumentElement.SelectSingleNode("Middle_Name").InnerText.Trim.Replace("'", "").ToUpper, 1, 20)
    '        RFC = Mid(lxmlDocument.DocumentElement.SelectSingleNode("RFC").InnerText.Trim.Replace("'", "").ToUpper, 1, 15)
    '        Fecha_Nacimiento = Mid(lxmlDocument.DocumentElement.SelectSingleNode("Birth_date").InnerText.Trim.Replace("'", "").ToUpper, 1, 15)
    '        check_Disp = Mid(lxmlDocument.DocumentElement.SelectSingleNode("check_Disp").InnerText.Trim.Replace("'", "").ToUpper, 1, 1)

    '        Dim CRMXKIOVMBD09 As String = ConfigurationManager.ConnectionStrings("CRMXKIOVMBD09").ConnectionString
    '        Dim CRMXKIOVMBD11DXN As String = ConfigurationManager.ConnectionStrings("CRMXKIOVMBD11DXN").ConnectionString


    '        Dim Opcon11DXN As New SqlConnection(CRMXKIOVMBD11DXN)

    '        Dim cmd As SqlCommand = New SqlCommand("Cliente_sp_ObtenId_TEST", Opcon11DXN)


    '        cmd.CommandType = CommandType.StoredProcedure
    '        cmd.Parameters.AddWithValue("@apellido_paterno", Apellido_Paterno)
    '        cmd.Parameters.AddWithValue("@apellido_materno", Apellido_Materno)
    '        cmd.Parameters.AddWithValue("@primer_nombre", Primer_Nombre)
    '        cmd.Parameters.AddWithValue("@segundo_nombre", Segundo_Nombre)
    '        cmd.Parameters.AddWithValue("@rfc", RFC)
    '        cmd.Parameters.AddWithValue("@fechanac", Fecha_Nacimiento)
    '        cmd.Parameters.AddWithValue("@IsKD", check_Disp)
    '        cmd.Parameters.Add("@numero_solicitud", SqlDbType.VarChar, 30)
    '        cmd.Parameters("@numero_solicitud").Direction = ParameterDirection.Output

    '        Opcon11DXN.Open()
    '        cmd.ExecuteNonQuery()

    '        resultado = cmd.Parameters("@numero_solicitud").Value.ToString()
    '        cmd.Dispose()
    '        Opcon11DXN.Close()
    '        Opcon11DXN.Dispose()


    '        If resultado = "0" Then



    '            Dim Opcon09 As New SqlConnection(CRMXKIOVMBD09)
    '            Dim cmd2 As SqlCommand = New SqlCommand("Cliente_sp_ObtenId_TEST", Opcon09)


    '            cmd2.CommandType = CommandType.StoredProcedure
    '            cmd2.Parameters.AddWithValue("@apellido_paterno", Apellido_Paterno)
    '            cmd2.Parameters.AddWithValue("@apellido_materno", Apellido_Materno)
    '            cmd2.Parameters.AddWithValue("@primer_nombre", Primer_Nombre)
    '            cmd2.Parameters.AddWithValue("@segundo_nombre", Segundo_Nombre)
    '            cmd2.Parameters.AddWithValue("@rfc", RFC)
    '            cmd2.Parameters.AddWithValue("@fechanac", Fecha_Nacimiento)
    '            cmd2.Parameters.Add("@numero_solicitud", SqlDbType.VarChar, 30)
    '            cmd2.Parameters("@numero_solicitud").Direction = ParameterDirection.Output

    '            Opcon09.Open()
    '            cmd2.ExecuteNonQuery()

    '            resultado2 = cmd2.Parameters("@numero_solicitud").Value.ToString()
    '            cmd2.Dispose()
    '            resultado = resultado2
    '            Opcon09.Close()
    '            Opcon09.Dispose()
    '        End If


    '        ClosePublicDispatch(lintDispatch, resultado)
    '        Return "<ROOT><TRANSCODE>" & lintDispatch & "</TRANSCODE><RESPONSE>" & resultado & "</RESPONSE><RETRY>0</RETRY></ROOT>"
    '    Catch ex As Exception
    '        GetAutomaticCustomerNumberTest = 0
    '        TrackError("GetAutomaticCustomerNumber" & vbCrLf & vbCrLf & ex.Message.ToString.Replace("""", "").Replace("<", "").Replace(">", "").Replace("[ServiceDispatcherError]", "") & vbCrLf & "Id_Dispatch :" & lintDispatch & vbCrLf & vbCrLf & lstrDataXML)
    '        Return "<ROOT><TRANSCODE>0</TRANSCODE><RESPONSE>" & ex.Message.ToString.Replace("""", "").Replace("<", "").Replace(">", "").Replace("[ServiceDispatcherError]", "") & "</RESPONSE>" & IIf(ex.Message.ToString.Contains("[ServiceDispatcherError]"), "<RETRY>0</RETRY>", "<RETRY>1</RETRY>") & "</ROOT>"


    '    Finally
    '    End Try
    'End Function
    Public Function GetAutomaticCustomerNumb(ByVal xmlData As Stream) As String Implements IService.GetAutomaticCustomerNumb
        Dim lsrReaderXML As New StreamReader(xmlData)
        Dim lstrDataXML As String = lsrReaderXML.ReadToEnd
        Dim lintDispatch As Integer = 0

        Dim lstrSQL As String = ""

        Try
            lintDispatch = InitPublicDispatch("GetAutomaticCustomer", lstrDataXML)
            'Se inicia la transacción de registro
            If lintDispatch = 0 Then
                Return "<ROOT><TRANSCODE>0</TRANSCODE><DESCRIPTION>No se pudo generar token de transacción.</DESCRIPTION><RETRY>1</RETRY></ROOT>"
            End If

            Dim lxmlDocument As XmlDocument = Nothing
            Dim lxmlNode As XmlNode = Nothing
            Dim lstrError As String = ""
            Dim lintLogId As Integer = 0

            If lstrDataXML.Length = 0 Then Throw New Exception("[ServiceDispatcherError]" & vbCrLf & "El documento XML no puede ir vacío.")
            lxmlDocument = New XmlDocument
            lxmlDocument.LoadXml(lstrDataXML)


            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("First_Name")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Fecha de Primer Nombre (First_Name) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Middle_Name")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Segundo Nombre (Middle_Name) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Paternal_Name")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Apellido Paterno (Paternal_Name) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Maternal_Name")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Apellido Materno (Maternal_Name) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Birth_date")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Fecha de Nacimiento (Birth_date) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("RFC")
            If lxmlNode Is Nothing Then lstrError += "El nodo de RFC (RFC) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("check_Disp")
            If lxmlNode Is Nothing Then lstrError += "El nodo check_Disp (check_Disp) no está contenido en el XML." & vbCrLf

            If lstrError.Length > 0 Then Throw New Exception("[ServiceDispatcherError]" & vbCrLf & lstrError)

            If lxmlDocument.DocumentElement.SelectSingleNode("Paternal_Name").InnerText.Length = 0 Then
                ClosePublicDispatch(lintDispatch, "vacio")
                Return "<ROOT><TRANSCODE>" & lintDispatch & "</TRANSCODE><RESPONSE>" & " " & "</RESPONSE><RETRY>0</RETRY></ROOT>"
            End If

            If lxmlDocument.DocumentElement.SelectSingleNode("First_Name").InnerText.Length = 0 Then lstrError += "El Primer Nombre es un dato requerido y no puede ir vacío." & vbCrLf
            'If lxmlDocument.DocumentElement.SelectSingleNode("Paternal_Name").InnerText.Length = 0 Then lstrError += "El Apellido Paterno es un dato requerido y no puede ir vacío." & vbCrLf
            'If lxmlDocument.DocumentElement.SelectSingleNode("Maternal_Name").InnerText.Length = 0 Then lstrError += "El Apellido Materno es un dato requerido y no puede ir vacío." & vbCrLf
            If lxmlDocument.DocumentElement.SelectSingleNode("Birth_date").InnerText.Length = 0 Then lstrError += "La Fecha de Nacimiento es un dato requerido y no puede ir vacío." & vbCrLf
            If lxmlDocument.DocumentElement.SelectSingleNode("RFC").InnerText.Length = 0 Then lstrError += "El RFC es un dato requerido y no puede ir vacío." & vbCrLf
            If lxmlDocument.DocumentElement.SelectSingleNode("check_Disp").InnerText.Length = 0 Then lstrError += "El check_Disp es un dato requerido y no puede ir vacío." & vbCrLf

            If lstrError.Length > 0 Then Throw New Exception("[ServiceDispatcherError]" & vbCrLf & lstrError)

            Dim Apellido_Paterno As String,
                    Apellido_Materno As String,
                    Primer_Nombre As String,
                    Segundo_Nombre As String,
                    RFC As String,
                    Fecha_Nacimiento As String,
                    resultado As String,
                    resultado2 As String,
                    check_Disp As String


            Apellido_Paterno = Mid(lxmlDocument.DocumentElement.SelectSingleNode("Paternal_Name").InnerText.Trim.Replace("'", "").ToUpper, 1, 25)
            Apellido_Materno = Mid(lxmlDocument.DocumentElement.SelectSingleNode("Maternal_Name").InnerText.Trim.Replace("'", "").ToUpper, 1, 25)
            Primer_Nombre = Mid(lxmlDocument.DocumentElement.SelectSingleNode("First_Name").InnerText.Trim.Replace("'", "").ToUpper, 1, 20)
            Segundo_Nombre = Mid(lxmlDocument.DocumentElement.SelectSingleNode("Middle_Name").InnerText.Trim.Replace("'", "").ToUpper, 1, 20)
            RFC = Mid(lxmlDocument.DocumentElement.SelectSingleNode("RFC").InnerText.Trim.Replace("'", "").ToUpper, 1, 15)
            Fecha_Nacimiento = Mid(lxmlDocument.DocumentElement.SelectSingleNode("Birth_date").InnerText.Trim.Replace("'", "").ToUpper, 1, 15)
            check_Disp = Mid(lxmlDocument.DocumentElement.SelectSingleNode("check_Disp").InnerText.Trim.Replace("'", "").ToUpper, 1, 1)

            Dim CRMXKIOVMBD09 As String = ConfigurationManager.ConnectionStrings("CRMXKIOVMBD09").ConnectionString
            Dim CRMXKIOVMBD11DXN As String = ConfigurationManager.ConnectionStrings("CRMXKIOVMBD11DXN").ConnectionString


            Dim Opcon11DXN As New SqlConnection(CRMXKIOVMBD11DXN)

            Dim cmd As SqlCommand = New SqlCommand("Cliente_sp_ObtenId_Prod", Opcon11DXN)


            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@apellido_paterno", Apellido_Paterno)
            cmd.Parameters.AddWithValue("@apellido_materno", Apellido_Materno)
            cmd.Parameters.AddWithValue("@primer_nombre", Primer_Nombre)
            cmd.Parameters.AddWithValue("@segundo_nombre", Segundo_Nombre)
            cmd.Parameters.AddWithValue("@rfc", RFC)
            cmd.Parameters.AddWithValue("@fechanac", Fecha_Nacimiento)
            cmd.Parameters.AddWithValue("@IsKD", check_Disp)
            cmd.Parameters.Add("@numero_solicitud", SqlDbType.VarChar, 30)
            cmd.Parameters("@numero_solicitud").Direction = ParameterDirection.Output

            Opcon11DXN.Open()
            cmd.ExecuteNonQuery()

            resultado = cmd.Parameters("@numero_solicitud").Value.ToString()
            cmd.Dispose()
            Opcon11DXN.Close()
            Opcon11DXN.Dispose()


            If resultado = "0" Then



                Dim Opcon09 As New SqlConnection(CRMXKIOVMBD09)
                Dim cmd2 As SqlCommand = New SqlCommand("Cliente_sp_ObtenId_Prod", Opcon09)


                cmd2.CommandType = CommandType.StoredProcedure
                cmd2.Parameters.AddWithValue("@apellido_paterno", Apellido_Paterno)
                cmd2.Parameters.AddWithValue("@apellido_materno", Apellido_Materno)
                cmd2.Parameters.AddWithValue("@primer_nombre", Primer_Nombre)
                cmd2.Parameters.AddWithValue("@segundo_nombre", Segundo_Nombre)
                cmd2.Parameters.AddWithValue("@rfc", RFC)
                cmd2.Parameters.AddWithValue("@fechanac", Fecha_Nacimiento)
                cmd2.Parameters.Add("@numero_solicitud", SqlDbType.VarChar, 30)
                cmd2.Parameters("@numero_solicitud").Direction = ParameterDirection.Output

                Opcon09.Open()
                cmd2.ExecuteNonQuery()

                resultado2 = cmd2.Parameters("@numero_solicitud").Value.ToString()
                cmd2.Dispose()
                resultado = resultado2
                Opcon09.Close()
                Opcon09.Dispose()
            End If


            ClosePublicDispatch(lintDispatch, resultado)
            Return "<ROOT><TRANSCODE>" & lintDispatch & "</TRANSCODE><RESPONSE>" & resultado & "</RESPONSE><RETRY>0</RETRY></ROOT>"
        Catch ex As Exception
            GetAutomaticCustomerNumb = 0
            TrackError("GetAutomaticCustomerNumb" & vbCrLf & vbCrLf & ex.Message.ToString.Replace("""", "").Replace("<", "").Replace(">", "").Replace("[ServiceDispatcherError]", "") & vbCrLf & "Id_Dispatch :" & lintDispatch & vbCrLf & vbCrLf & lstrDataXML)
            Return "<ROOT><TRANSCODE>0</TRANSCODE><RESPONSE>" & ex.Message.ToString.Replace("""", "").Replace("<", "").Replace(">", "").Replace("[ServiceDispatcherError]", "") & "</RESPONSE>" & IIf(ex.Message.ToString.Contains("[ServiceDispatcherError]"), "<RETRY>0</RETRY>", "<RETRY>1</RETRY>") & "</ROOT>"


        Finally
        End Try
    End Function

    Public Function GetAutomaticCustomerNumber(ByVal xmlData As Stream) As String Implements IService.GetAutomaticCustomerNumber
        Dim lsrReaderXML As New StreamReader(xmlData)
        Dim lstrDataXML As String = lsrReaderXML.ReadToEnd
        Dim lintDispatch As Integer = 0

        Dim lstrSQL As String = ""

        Try
            lintDispatch = InitPublicDispatch("GetAutomaticCustomer", lstrDataXML)
            'Se inicia la transacción de registro
            If lintDispatch = 0 Then
                Return "<ROOT><TRANSCODE>0</TRANSCODE><DESCRIPTION>No se pudo generar token de transacción.</DESCRIPTION><RETRY>1</RETRY></ROOT>"
            End If

            Dim lxmlDocument As XmlDocument = Nothing
            Dim lxmlNode As XmlNode = Nothing
            Dim lstrError As String = ""
            Dim lintLogId As Integer = 0

            If lstrDataXML.Length = 0 Then Throw New Exception("[ServiceDispatcherError]" & vbCrLf & "El documento XML no puede ir vacío.")
            lxmlDocument = New XmlDocument
            lxmlDocument.LoadXml(lstrDataXML)


            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("First_Name")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Fecha de Primer Nombre (First_Name) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Middle_Name")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Segundo Nombre (Middle_Name) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Paternal_Name")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Apellido Paterno (Paternal_Name) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Maternal_Name")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Apellido Materno (Maternal_Name) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Birth_date")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Fecha de Nacimiento (Birth_date) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("RFC")
            If lxmlNode Is Nothing Then lstrError += "El nodo de RFC (RFC) no está contenido en el XML." & vbCrLf

            If lstrError.Length > 0 Then Throw New Exception("[ServiceDispatcherError]" & vbCrLf & lstrError)

            If lxmlDocument.DocumentElement.SelectSingleNode("Paternal_Name").InnerText.Length = 0 Then
                ClosePublicDispatch(lintDispatch, "vacio")
                Return "<ROOT><TRANSCODE>" & lintDispatch & "</TRANSCODE><RESPONSE>" & " " & "</RESPONSE><RETRY>0</RETRY></ROOT>"
            End If

            If lxmlDocument.DocumentElement.SelectSingleNode("First_Name").InnerText.Length = 0 Then lstrError += "El Primer Nombre es un dato requerido y no puede ir vacío." & vbCrLf
            'If lxmlDocument.DocumentElement.SelectSingleNode("Paternal_Name").InnerText.Length = 0 Then lstrError += "El Apellido Paterno es un dato requerido y no puede ir vacío." & vbCrLf
            'If lxmlDocument.DocumentElement.SelectSingleNode("Maternal_Name").InnerText.Length = 0 Then lstrError += "El Apellido Materno es un dato requerido y no puede ir vacío." & vbCrLf
            If lxmlDocument.DocumentElement.SelectSingleNode("Birth_date").InnerText.Length = 0 Then lstrError += "La Fecha de Nacimiento es un dato requerido y no puede ir vacío." & vbCrLf
            If lxmlDocument.DocumentElement.SelectSingleNode("RFC").InnerText.Length = 0 Then lstrError += "El RFC es un dato requerido y no puede ir vacío." & vbCrLf

            If lstrError.Length > 0 Then Throw New Exception("[ServiceDispatcherError]" & vbCrLf & lstrError)

            Dim Apellido_Paterno As String,
                    Apellido_Materno As String,
                    Primer_Nombre As String,
                    Segundo_Nombre As String,
                    RFC As String,
                    Fecha_Nacimiento As String,
                    resultado As String,
                    resultado2 As String


            Apellido_Paterno = Mid(lxmlDocument.DocumentElement.SelectSingleNode("Paternal_Name").InnerText.Trim.Replace("'", "").ToUpper, 1, 25)
            Apellido_Materno = Mid(lxmlDocument.DocumentElement.SelectSingleNode("Maternal_Name").InnerText.Trim.Replace("'", "").ToUpper, 1, 25)
            Primer_Nombre = Mid(lxmlDocument.DocumentElement.SelectSingleNode("First_Name").InnerText.Trim.Replace("'", "").ToUpper, 1, 20)
            Segundo_Nombre = Mid(lxmlDocument.DocumentElement.SelectSingleNode("Middle_Name").InnerText.Trim.Replace("'", "").ToUpper, 1, 20)
            RFC = Mid(lxmlDocument.DocumentElement.SelectSingleNode("RFC").InnerText.Trim.Replace("'", "").ToUpper, 1, 15)
            Fecha_Nacimiento = Mid(lxmlDocument.DocumentElement.SelectSingleNode("Birth_date").InnerText.Trim.Replace("'", "").ToUpper, 1, 15)

            Dim CRMXKIOVMBD09 As String = ConfigurationManager.ConnectionStrings("CRMXKIOVMBD09").ConnectionString
            Dim CRMXKIOVMBD11DXN As String = ConfigurationManager.ConnectionStrings("CRMXKIOVMBD11DXN").ConnectionString


            Dim Opcon11DXN As New SqlConnection(CRMXKIOVMBD11DXN)

            Dim cmd As SqlCommand = New SqlCommand("Cliente_sp_ObtenId", Opcon11DXN)


            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@apellido_paterno", Apellido_Paterno)
            cmd.Parameters.AddWithValue("@apellido_materno", Apellido_Materno)
            cmd.Parameters.AddWithValue("@primer_nombre", Primer_Nombre)
            cmd.Parameters.AddWithValue("@segundo_nombre", Segundo_Nombre)
            cmd.Parameters.AddWithValue("@rfc", RFC)
            cmd.Parameters.AddWithValue("@fechanac", Fecha_Nacimiento)
            cmd.Parameters.Add("@numero_solicitud", SqlDbType.VarChar, 30)
            cmd.Parameters("@numero_solicitud").Direction = ParameterDirection.Output

            Opcon11DXN.Open()
            cmd.ExecuteNonQuery()

            resultado = cmd.Parameters("@numero_solicitud").Value.ToString()
            cmd.Dispose()
            Opcon11DXN.Close()
            Opcon11DXN.Dispose()


            If resultado = "0" Then



                Dim Opcon09 As New SqlConnection(CRMXKIOVMBD09)
                Dim cmd2 As SqlCommand = New SqlCommand("Cliente_sp_ObtenId", Opcon09)


                cmd2.CommandType = CommandType.StoredProcedure
                cmd2.Parameters.AddWithValue("@apellido_paterno", Apellido_Paterno)
                cmd2.Parameters.AddWithValue("@apellido_materno", Apellido_Materno)
                cmd2.Parameters.AddWithValue("@primer_nombre", Primer_Nombre)
                cmd2.Parameters.AddWithValue("@segundo_nombre", Segundo_Nombre)
                cmd2.Parameters.AddWithValue("@rfc", RFC)
                cmd2.Parameters.AddWithValue("@fechanac", Fecha_Nacimiento)
                cmd2.Parameters.Add("@numero_solicitud", SqlDbType.VarChar, 30)
                cmd2.Parameters("@numero_solicitud").Direction = ParameterDirection.Output

                Opcon09.Open()
                cmd2.ExecuteNonQuery()

                resultado2 = cmd2.Parameters("@numero_solicitud").Value.ToString()
                cmd2.Dispose()
                resultado = resultado2
                Opcon09.Close()
                Opcon09.Dispose()
            End If


            ClosePublicDispatch(lintDispatch, resultado)
            Return "<ROOT><TRANSCODE>" & lintDispatch & "</TRANSCODE><RESPONSE>" & resultado & "</RESPONSE><RETRY>0</RETRY></ROOT>"
        Catch ex As Exception
            GetAutomaticCustomerNumber = 0
            TrackError("GetAutomaticCustomerNumber" & vbCrLf & vbCrLf & ex.Message.ToString.Replace("""", "").Replace("<", "").Replace(">", "").Replace("[ServiceDispatcherError]", "") & vbCrLf & "Id_Dispatch :" & lintDispatch & vbCrLf & vbCrLf & lstrDataXML)
            Return "<ROOT><TRANSCODE>0</TRANSCODE><RESPONSE>" & ex.Message.ToString.Replace("""", "").Replace("<", "").Replace(">", "").Replace("[ServiceDispatcherError]", "") & "</RESPONSE>" & IIf(ex.Message.ToString.Contains("[ServiceDispatcherError]"), "<RETRY>0</RETRY>", "<RETRY>1</RETRY>") & "</ROOT>"


        Finally
        End Try
    End Function

    Public Function SendCustomer(ByVal xmlData As Stream) As String Implements IService.SendCustomer
        Dim lsrReaderXML As New StreamReader(xmlData)
        Dim lstrDataXML As String = lsrReaderXML.ReadToEnd
        Dim lintDispatch As Integer = 0
        Dim lobjCustomer As ConnectionBroker.DataManagerClient = Nothing

        Dim lstrSQL As String = ""

        Try
            lintDispatch = InitDispatch("SendCustomer", lstrDataXML)
            'Se inicia la transacción de registro
            If lintDispatch = 0 Then
                Return "<ROOT><TRANSCODE>0</TRANSCODE><DESCRIPTION>No se pudo generar token de transacción.</DESCRIPTION><RETRY>1</RETRY></ROOT>"
            End If

            Dim lxmlDocument As XmlDocument = Nothing
            Dim lxmlNode As XmlNode = Nothing
            Dim lstrError As String = ""
            Dim lintLogId As Integer = 0

            If lstrDataXML.Length = 0 Then Throw New Exception("[ServiceDispatcherError]" & vbCrLf & "El documento XML no puede ir vacío.")
            lxmlDocument = New XmlDocument
            lxmlDocument.LoadXml(lstrDataXML)

            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Customer_Number")
            If lxmlNode Is Nothing Then lstrError = "El nodo de Número de Cliente (Customer_Number) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Customer_Type")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Tipo de Cliente (Customer_Type) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Record_Date")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Fecha Alta (Record_Date) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("User_Name")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Clave Capturista no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("First_Name")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Fecha de Primer Nombre (First_Name) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Middle_Name")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Segundo Nombre (Middle_Name) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Paternal_Name")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Apellido Paterno (Paternal_Name) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Maternal_Name")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Apellido Materno (Maternal_Name) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Company_Name")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Razon_Social (Company_Name) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Birth_date")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Fecha de Nacimiento (Birth_date) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Gender")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Genero (Gender) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Marital_Status")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Estado Civil (Marital_Status) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Dependents")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Dependencias (Dependents) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Scholarship")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Escolaridad (Scholarship) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("RFC")
            If lxmlNode Is Nothing Then lstrError += "El nodo de RFC (RFC) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Main_Phone")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Télefono Principal (Main_Phone) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Secondary_Phone")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Telefono secundario (Secondary_Phone) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Nationality")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Nacionalidad (Nationality) no está contenido en el XML." & vbCrLf

            If lstrError.Length > 0 Then Throw New Exception("[ServiceDispatcherError]" & vbCrLf & lstrError)
            If lxmlDocument.DocumentElement.SelectSingleNode("Customer_Number").InnerText.Length = 0 Then lstrError = "El Número de Cliente es un dato requerido y no puede ir vacío." & vbCrLf
            If lxmlDocument.DocumentElement.SelectSingleNode("Customer_Type").InnerText.Length = 0 Then lstrError += "El Tipo de Cliente es un dato requerido y no puede ir vacío." & vbCrLf
            If lxmlDocument.DocumentElement.SelectSingleNode("User_Name").InnerText.Length = 0 Then lstrError += "La Clave Capturista es un dato requerido y no puede ir vacío." & vbCrLf
            If lxmlDocument.DocumentElement.SelectSingleNode("First_Name").InnerText.Length = 0 Then lstrError += "El Primer Nombre es un dato requerido y no puede ir vacío." & vbCrLf
            If lxmlDocument.DocumentElement.SelectSingleNode("Birth_date").InnerText.Length _
                > 0 Then If Not DateCheck(lxmlDocument.DocumentElement.SelectSingleNode("Birth_date").InnerText.Trim) Then lstrError += "La fecha de nacimiento no puede ir vacio o el formato de Fecha es incorrecto" & vbCrLf
            'If lxmlDocument.DocumentElement.SelectSingleNode("Nationality").InnerText.Length = 0 Then lstrError += "La Nacionalidad es un dato requerido y no puede ir vacío." & vbCrLf
            'If lxmlDocument.DocumentElement.SelectSingleNode("Birth_date").InnerText.Length = 0 Then lstrError += "La Fecha de Nacimiento es un dato requerido y no puede ir vacío." & vbCrLf
            'If lxmlDocument.DocumentElement.SelectSingleNode("RFC").InnerText.Length = 0 Then lstrError += "El RFC es un dato requerido y no puede ir vacío." & vbCrLf
            'If lxmlDocument.DocumentElement.SelectSingleNode("Nationality").InnerText.Length = 0 Then lstrError += "La Nacionalidad es un dato requerido y no puede ir vacío." & vbCrLf
            If lstrError.Length > 0 Then Throw New Exception("[ServiceDispatcherError]" & vbCrLf & lstrError)

            'If lxmlDocument.DocumentElement.SelectSingleNode("Paternal_Name").InnerText.Length = 0 Then lxmlDocument.DocumentElement.SelectSingleNode("Paternal_Name").InnerText = "NO PROPORCIONADO"
            'If lxmlDocument.DocumentElement.SelectSingleNode("Maternal_Name").InnerText.Length = 0 Then lxmlDocument.DocumentElement.SelectSingleNode("Maternal_Name").InnerText = "NO PROPORCIONADO"
            lobjCustomer = New ConnectionBroker.DataManagerClient
            lobjCustomer.Open()
            lobjCustomer.setConnectionString(System.Configuration.ConfigurationManager.ConnectionStrings("MainConnection").ConnectionString)
            lobjCustomer.setReturnIdentity(True)

            lstrSQL = "INSERT INTO BPM_Customers_Queue (Id_Dispatch, Customer_Number, Customer_Type, Record_Date, User_Name, First_Name, Middle_Name, Paternal_Name, Maternal_Name," & vbCrLf &
                "Company_Name, Birth_date, Gender, Marital_Status, Dependents, Scholarship, RFC, Main_Phone, Secondary_Phone, Nationality, Transfer_Status, Created_Date)" & vbCrLf &
            " VALUES (" & lintDispatch & ", '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Customer_Number").InnerText.Trim.Replace("'", "").ToUpper, 1, 14) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Customer_Type").InnerText.Trim.Replace("'", "").ToUpper, 1, 1) & "', " &
            "GETDATE(), '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("User_Name").InnerText.Trim.Replace("'", "").ToUpper, 1, 6) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("First_Name").InnerText.Trim.Replace("'", "").ToUpper, 1, 20) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Middle_Name").InnerText.Trim.Replace("'", "").ToUpper, 1, 20) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Paternal_Name").InnerText.Trim.Replace("'", "").ToUpper.Replace("NO PROPORCIONADO", ""), 1, 25) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Maternal_Name").InnerText.Trim.Replace("'", "").ToUpper.Replace("NO PROPORCIONADO", ""), 1, 25) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Company_Name").InnerText.Trim.Replace("'", "").ToUpper, 1, 10) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Birth_date").InnerText.Trim.Replace("'", "").ToUpper, 1, 10) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Gender").InnerText.Trim.Replace("'", "").ToUpper, 1, 9) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Marital_Status").InnerText.Trim.Replace("'", "").ToUpper, 1, 15) & "', " &
            "" & Val("" & lxmlDocument.DocumentElement.SelectSingleNode("Dependents").InnerText.Trim.Replace("'", "")) & ", '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Scholarship").InnerText.Trim.Replace("'", "").ToUpper, 1, 20) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("RFC").InnerText.Trim.Replace("'", "").ToUpper, 1, 15) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Main_Phone").InnerText.Trim.Replace("'", "").ToUpper, 1, 10) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Secondary_Phone").InnerText.Trim.Replace("'", "").ToUpper, 1, 10) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Nationality").InnerText.Trim.Replace("'", "").ToUpper, 1, 15) & "', " &
            "0, GETDATE())"
            lobjCustomer.ExecuteCommand(lstrSQL)
            lintLogId = lobjCustomer.getIdentityValue
            lobjCustomer.Finalize()
            lobjCustomer.Close()
            CloseDispatch(lintDispatch)
            Return "<ROOT><TRANSCODE>" & lintDispatch & "</TRANSCODE><DESCRIPTION>Transacción exitosa</DESCRIPTION><RETRY>0</RETRY></ROOT>"
        Catch ex As Exception
            SendCustomer = 0
            TrackError("SendCustomer" & vbCrLf & vbCrLf & ex.Message.ToString.Replace("""", "").Replace("<", "").Replace(">", "").Replace("[ServiceDispatcherError]", "") & vbCrLf & "Id_Dispatch :" & lintDispatch & vbCrLf & vbCrLf & lstrDataXML)
            Return "<ROOT><TRANSCODE>0</TRANSCODE><DESCRIPTION>" & ex.Message.ToString.Replace("""", "").Replace("<", "").Replace(">", "").Replace("[ServiceDispatcherError]", "") & "</DESCRIPTION>" & IIf(ex.Message.ToString.Contains("[ServiceDispatcherError]"), "<RETRY>0</RETRY>", "<RETRY>1</RETRY>") & "</ROOT>"


        Finally
            If Not lobjCustomer Is Nothing Then lobjCustomer = Nothing
        End Try
    End Function



    Public Function SendAddress(ByVal xmlData As Stream) As String Implements IService.SendAddress
        Dim lsrReaderXML As New StreamReader(xmlData)
        Dim lstrDataXML As String = lsrReaderXML.ReadToEnd
        Dim lintDispatch As Integer = 0
        Dim lobjAddress As ConnectionBroker.DataManagerClient = Nothing

        Dim lstrSQL As String = ""

        Try
            lintDispatch = InitDispatch("SendAddress", lstrDataXML)
            'Se inicia la transacción de registro
            If lintDispatch = 0 Then
                Return "<ROOT><TRANSCODE>0</TRANSCODE><DESCRIPTION>No se pudo generar token de transacción.</DESCRIPTION><RETRY>1</RETRY></ROOT>"
            End If

            Dim lxmlDocument As XmlDocument = Nothing
            Dim lxmlNode As XmlNode = Nothing
            Dim lstrError As String = ""
            Dim lintLogId As Integer = 0

            If lstrDataXML.Length = 0 Then Throw New Exception("[ServiceDispatcherError]" & vbCrLf & "El documento XML no puede ir vacío.")
            lxmlDocument = New XmlDocument
            lxmlDocument.LoadXml(lstrDataXML)

            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Customer_Number")
            If lxmlNode Is Nothing Then lstrError = "El nodo de Número de Cliente (Customer_Number) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Request_Number")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Consecutivo (Request_Number) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Record_Date")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Fecha Alta (Record_Date) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Property_Type")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Tipo de Propiedad  (Property_Type) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Street_Name")
            If lxmlNode Is Nothing Then lstrError += "El nodo Calle (Street_Name) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Outdoor_Number")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Numero Exterior (Outdoor_Number) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Internal_Number")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Numero Interior (Internal_Number) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Between_Streets")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Entre Calles (Between_Streets) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Settlement")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Colonia (Settlement) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("City")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Ciudad (City) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Municipality")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Delegacion o Municipio (Municipality) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("State_Key")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Clave de Estado (State_Key) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("CP")
            If lxmlNode Is Nothing Then lstrError += "El nodo de CP (CP) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Antiquity")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Antiguedad (Antiquity) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Phone")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Telefono (Phone) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Comments")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Comments (Comments) no está contenido en el XML." & vbCrLf

            If lstrError.Length > 0 Then Throw New Exception("[ServiceDispatcherError]" & vbCrLf & lstrError)
            If lxmlDocument.DocumentElement.SelectSingleNode("Customer_Number").InnerText.Length = 0 Then lstrError = "El Número de Cliente es un dato requerido y no puede ir vacío." & vbCrLf
            If lxmlDocument.DocumentElement.SelectSingleNode("Request_Number").InnerText.Length = 0 Then lstrError += "El Consecutivo es un dato requerido y no puede ir vacío." & vbCrLf
            If lxmlDocument.DocumentElement.SelectSingleNode("CP").InnerText.Length > 0 Then
                If Not IsNumeric(lxmlDocument.DocumentElement.SelectSingleNode("CP").InnerText) Then
                    lstrError += "El CP debe ser un dato tipo entero." & vbCrLf
                End If
            End If

            'If lxmlDocument.DocumentElement.SelectSingleNode("Street_Name").InnerText.Length = 0 Then lstrError += "El nombre de la calle es un dato requerido y no puede ir vacío." & vbCrLf
            'If lxmlDocument.DocumentElement.SelectSingleNode("Settlement").InnerText.Length = 0 Then lstrError += "La Colonia es un dato requerido y no puede ir vacío." & vbCrLf
            'If lxmlDocument.DocumentElement.SelectSingleNode("City").InnerText.Length = 0 Then lstrError += "La ciudad es un dato requerido y no puede ir vacío." & vbCrLf
            'If lxmlDocument.DocumentElement.SelectSingleNode("Municipality").InnerText.Length = 0 Then lstrError += "El municiopio o Delegación es un dato requerido y no puede ir vacío." & vbCrLf
            'If lxmlDocument.DocumentElement.SelectSingleNode("State_Key").InnerText.Length = 0 Then lstrError += "LA Clave de Estado es un dato requerido y no puede ir vacío." & vbCrLf
            If lstrError.Length > 0 Then Throw New Exception("[ServiceDispatcherError]" & vbCrLf & lstrError)

            'If lxmlDocument.DocumentElement.SelectSingleNode("Paternal_Name").InnerText.Length = 0 Then lxmlDocument.DocumentElement.SelectSingleNode("Paternal_Name").InnerText = "NO PROPORCIONADO"
            'If lxmlDocument.DocumentElement.SelectSingleNode("Maternal_Name").InnerText.Length = 0 Then lxmlDocument.DocumentElement.SelectSingleNode("Maternal_Name").InnerText = "NO PROPORCIONADO"
            lobjAddress = New ConnectionBroker.DataManagerClient
            lobjAddress.Open()
            lobjAddress.setConnectionString(System.Configuration.ConfigurationManager.ConnectionStrings("MainConnection").ConnectionString)
            lobjAddress.setReturnIdentity(True)

            lstrSQL = "INSERT INTO BPM_Address_Queue(Id_Dispatch, Customer_Number, Request_Number, Record_Date, Property_Type, Street_Name, Outdoor_Number, Internal_Number," & vbCrLf &
                "Between_Streets, Settlement, City, Municipality, State_Key, CP, Antiquity, Phone, Comments, Transfer_Status, Created_Date)" & vbCrLf &
            " VALUES (" & lintDispatch & ", '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Customer_Number").InnerText.Trim.Replace("'", "").ToUpper, 1, 14) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Request_Number").InnerText.Trim.Replace("'", "").ToUpper, 1, 15) & "', " &
            "GETDATE(), '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Property_Type").InnerText.Trim.Replace("'", "").ToUpper, 1, 25) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Street_Name").InnerText.Trim.Replace("'", "").ToUpper, 1, 40) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Outdoor_Number").InnerText.Trim.Replace("'", "").ToUpper, 1, 20) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Internal_Number").InnerText.Trim.Replace("'", "").ToUpper, 1, 20) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Between_Streets").InnerText.Trim.Replace("'", "").ToUpper, 1, 26) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Settlement").InnerText.Trim.Replace("'", "").ToUpper, 1, 40) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("City").InnerText.Trim.Replace("'", "").ToUpper, 1, 40) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Municipality").InnerText.Trim.Replace("'", "").ToUpper, 1, 36) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("State_Key").InnerText.Trim.Replace("'", "").ToUpper, 1, 4) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("CP").InnerText.Trim.Replace("'", "").ToUpper, 1, 5) & "', " &
            "" & Val("" & lxmlDocument.DocumentElement.SelectSingleNode("Antiquity").InnerText.Trim.Replace("'", "")) & " , '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Phone").InnerText.Trim.Replace("'", "").ToUpper, 1, 10) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Comments").InnerText.Trim.Replace("'", "").ToUpper, 1, 500) & "', " &
            "0, GETDATE())"
            lobjAddress.ExecuteCommand(lstrSQL)
            lintLogId = lobjAddress.getIdentityValue
            lobjAddress.Finalize()
            lobjAddress.Close()
            CloseDispatch(lintDispatch)
            Return "<ROOT><TRANSCODE>" & lintDispatch & "</TRANSCODE><DESCRIPTION>Transacción exitosa</DESCRIPTION><RETRY>0</RETRY></ROOT>"
        Catch ex As Exception
            SendAddress = 0
            TrackError("SendAddress" & vbCrLf & vbCrLf & ex.Message.ToString.Replace("""", "").Replace("<", "").Replace(">", "").Replace("[ServiceDispatcherError]", "") & vbCrLf & "Id_Dispatch :" & lintDispatch & vbCrLf & vbCrLf & lstrDataXML)
            Return "<ROOT><TRANSCODE>0</TRANSCODE><DESCRIPTION>" & ex.Message.ToString.Replace("""", "").Replace("<", "").Replace(">", "").Replace("[ServiceDispatcherError]", "") & "</DESCRIPTION>" & IIf(ex.Message.ToString.Contains("[ServiceDispatcherError]"), "<RETRY>0</RETRY>", "<RETRY>1</RETRY>") & "</ROOT>"
        Finally
            If Not lobjAddress Is Nothing Then lobjAddress = Nothing
        End Try
    End Function


    Public Function SendJob(ByVal xmlData As Stream) As String Implements IService.SendJob
        Dim lsrReaderXML As New StreamReader(xmlData)
        Dim lstrDataXML As String = lsrReaderXML.ReadToEnd
        Dim lintDispatch As Integer = 0
        Dim lobjJob As ConnectionBroker.DataManagerClient = Nothing

        Dim lstrSQL As String = ""

        Try
            lintDispatch = InitDispatch("SendJob", lstrDataXML)
            'Se inicia la transacción de registro
            If lintDispatch = 0 Then
                Return "<ROOT><TRANSCODE>0</TRANSCODE><DESCRIPTION>No se pudo generar token de transacción.</DESCRIPTION><RETRY>1</RETRY></ROOT>"
            End If

            Dim lxmlDocument As XmlDocument = Nothing
            Dim lxmlNode As XmlNode = Nothing
            Dim lstrError As String = ""
            Dim lintLogId As Integer = 0

            If lstrDataXML.Length = 0 Then Throw New Exception("[ServiceDispatcherError]" & vbCrLf & "El documento XML no puede ir vacío.")
            lxmlDocument = New XmlDocument
            lxmlDocument.LoadXml(lstrDataXML)

            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Customer_Number")
            If lxmlNode Is Nothing Then lstrError = "El nodo de Número de Cliente (Customer_Number) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Request_Number")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Consecutivo (Request_Number) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Record_Date")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Fecha Alta (Record_Date) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Position")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Posición Laboral (Position) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Salary")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Sueldo (Salary) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Company_Name")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Empresa (Company_Name) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Main_Activity")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Giro de la Empresa (Main_Activity) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Street_Name")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Fecha de Calle (Street_Name) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Outdoor_Number")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Numero Exterior (Outdoor_Number) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Internal_Number")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Numero Interior (Internal_Number) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Between_Streets")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Entre Calles (Between_Streets) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Settlement")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Colonia (Settlement) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("City")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Ciudad (City) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Municipality")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Delegacion o Municipio (Municipality) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("State_Key")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Clave de Estado (State_Key) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("CP")
            If lxmlNode Is Nothing Then lstrError += "El nodo de CP (CP) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Antiquity")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Antiguedad (Antiquity) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Phone")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Telefono (Phone) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Phone_Extension")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Extension Telefonica (Phone_Extension) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Immediate_Boss")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Jefe Inmediato (Immediate_Boss) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Comments")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Comments (Comments) no está contenido en el XML." & vbCrLf

            If lstrError.Length > 0 Then Throw New Exception("[ServiceDispatcherError]" & vbCrLf & lstrError)
            If lxmlDocument.DocumentElement.SelectSingleNode("Customer_Number").InnerText.Length = 0 Then lstrError = "El Número de Cliente es un dato requerido y no puede ir vacío." & vbCrLf
            If lxmlDocument.DocumentElement.SelectSingleNode("Request_Number").InnerText.Length = 0 Then lstrError += "El Consecutivo es un dato requerido y no puede ir vacío." & vbCrLf
            'If lxmlDocument.DocumentElement.SelectSingleNode("Position").InnerText.Length = 0 Then lstrError += "La Posición Laboral es un dato requerido y no puede ir vacío." & vbCrLf
            'If lxmlDocument.DocumentElement.SelectSingleNode("Company_Name").InnerText.Length = 0 Then lstrError += "El Nombre de la Empresa es un dato requerido y no puede ir vacío." & vbCrLf
            'If lxmlDocument.DocumentElement.SelectSingleNode("Main_Activity").InnerText.Length = 0 Then lstrError += "El Giro de la Empresa es un dato requerido y no puede ir vacío." & vbCrLf
            If lstrError.Length > 0 Then Throw New Exception("[ServiceDispatcherError]" & vbCrLf & lstrError)

            lobjJob = New ConnectionBroker.DataManagerClient
            lobjJob.Open()
            lobjJob.setConnectionString(System.Configuration.ConfigurationManager.ConnectionStrings("MainConnection").ConnectionString)
            lobjJob.setReturnIdentity(True)

            lstrSQL = "INSERT INTO BPM_Jobs_Queue(Id_Dispatch, Customer_Number, Request_Number, Record_Date, Position, Salary, Company_Name, Main_Activity, Street_Name, Outdoor_Number, Internal_Number," & vbCrLf &
                "Between_Streets, Settlement, City, Municipality, State_Key, CP, Antiquity, Phone, Phone_Extension, Immediate_Boss, Comments, Transfer_Status, Created_Date)" & vbCrLf &
            " VALUES (" & lintDispatch & ", '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Customer_Number").InnerText.Trim.Replace("'", "").ToUpper, 1, 14) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Request_Number").InnerText.Trim.Replace("'", "").ToUpper, 1, 15) & "', " &
            "GETDATE(), '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Position").InnerText.Trim.Replace("'", "").ToUpper, 1, 40) & "', " &
            "" & Val("" & lxmlDocument.DocumentElement.SelectSingleNode("Salary").InnerText.Trim.Replace("'", "")) & " , '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Company_Name").InnerText.Trim.Replace("'", "").ToUpper, 1, 40) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Main_Activity").InnerText.Trim.Replace("'", "").ToUpper, 1, 40) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Street_Name").InnerText.Trim.Replace("'", "").ToUpper, 1, 40) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Outdoor_Number").InnerText.Trim.Replace("'", "").ToUpper, 1, 20) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Internal_Number").InnerText.Trim.Replace("'", "").ToUpper, 1, 20) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Between_Streets").InnerText.Trim.Replace("'", "").ToUpper, 1, 26) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Settlement").InnerText.Trim.Replace("'", "").ToUpper, 1, 40) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("City").InnerText.Trim.Replace("'", "").ToUpper, 1, 40) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Municipality").InnerText.Trim.Replace("'", "").ToUpper, 1, 36) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("State_Key").InnerText.Trim.Replace("'", "").ToUpper, 1, 4) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("CP").InnerText.Trim.Replace("'", ""), 1, 5).ToUpper & "', " &
            "" & Val("" & lxmlDocument.DocumentElement.SelectSingleNode("Antiquity").InnerText.Trim.Replace("'", "")) & " , '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Phone").InnerText.Trim.Replace("'", "").ToUpper, 1, 10) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Phone_Extension").InnerText.Trim.Replace("'", "").ToUpper, 1, 10) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Immediate_Boss").InnerText.Trim.Replace("'", "").ToUpper, 1, 10) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Comments").InnerText.Trim.Replace("'", "").ToUpper, 1, 500) & "', " &
            "0, GETDATE())"
            lobjJob.ExecuteCommand(lstrSQL)
            lintLogId = lobjJob.getIdentityValue
            lobjJob.Finalize()
            lobjJob.Close()
            CloseDispatch(lintDispatch)
            Return "<ROOT><TRANSCODE>" & lintDispatch & "</TRANSCODE><DESCRIPTION>Transacción exitosa</DESCRIPTION><RETRY>0</RETRY></ROOT>"
        Catch ex As Exception
            SendJob = 0
            TrackError("SendJob" & vbCrLf & vbCrLf & ex.Message.ToString.Replace("""", "").Replace("<", "").Replace(">", "").Replace("[ServiceDispatcherError]", "") & vbCrLf & "Id_Dispatch :" & lintDispatch & vbCrLf & vbCrLf & lstrDataXML)
            Return "<ROOT><TRANSCODE>0</TRANSCODE><DESCRIPTION>" & ex.Message.ToString.Replace("""", "").Replace("<", "").Replace(">", "").Replace("[ServiceDispatcherError]", "") & "</DESCRIPTION>" & IIf(ex.Message.ToString.Contains("[ServiceDispatcherError]"), "<RETRY>0</RETRY>", "<RETRY>1</RETRY>") & "</ROOT>"
        Finally
            If Not lobjJob Is Nothing Then lobjJob = Nothing
        End Try
    End Function


    Public Function SendReference(ByVal xmlData As Stream) As String Implements IService.SendReference
        Dim lsrReaderXML As New StreamReader(xmlData)
        Dim lstrDataXML As String = lsrReaderXML.ReadToEnd
        Dim lintDispatch As Integer = 0
        Dim lobjRefence As ConnectionBroker.DataManagerClient = Nothing

        Dim lstrSQL As String = ""

        Try
            lintDispatch = InitDispatch("SendReference", lstrDataXML)
            'Se inicia la transacción de registro
            If lintDispatch = 0 Then
                Return "<ROOT><TRANSCODE>0</TRANSCODE><DESCRIPTION>No se pudo generar token de transacción.</DESCRIPTION><RETRY>1</RETRY></ROOT>"
            End If

            Dim lxmlDocument As XmlDocument = Nothing
            Dim lxmlNode As XmlNode = Nothing
            Dim lstrError As String = ""
            Dim lintLogId As Integer = 0

            If lstrDataXML.Length = 0 Then Throw New Exception("[ServiceDispatcherError]" & vbCrLf & "El documento XML no puede ir vacío.")
            lxmlDocument = New XmlDocument
            lxmlDocument.LoadXml(lstrDataXML)

            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Customer_Number")
            If lxmlNode Is Nothing Then lstrError = "El nodo de Número de Cliente (Customer_Number) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Request_Number")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Consecutivo (Request_Number) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Record_Date")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Fecha Alta (Record_Date) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Reference_Type")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Tipo de Referencia (Reference_Type) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Person_Name")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Nombre de la refencia (Person_Name) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Paternal_Name")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Apellido Paterno (Paternal_Name) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Maternal_Name")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Apellido Materno (Maternal_Name) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Street_Name")
            If lxmlNode Is Nothing Then lstrError += "El nodo Calle (Street_Name) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Outdoor_Number")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Numero Exterior (Outdoor_Number) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Internal_Number")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Numero Interior (Internal_Number) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Settlement")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Colonia (Settlement) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("City")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Ciudad (City) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Municipality")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Delegacion o Municipio (Municipality) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("State_Key")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Clave de Estado (State_Key) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("CP")
            If lxmlNode Is Nothing Then lstrError += "El nodo de CP (CP) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Antiquity")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Antiguedad (Antiquity) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Phone")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Telefono (Phone) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Comments")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Comments (Comments) no está contenido en el XML." & vbCrLf

            If lstrError.Length > 0 Then Throw New Exception("[ServiceDispatcherError]" & vbCrLf & lstrError)
            If lxmlDocument.DocumentElement.SelectSingleNode("Customer_Number").InnerText.Length = 0 Then lstrError = "El Número de Cliente es un dato requerido y no puede ir vacío." & vbCrLf
            If lxmlDocument.DocumentElement.SelectSingleNode("Request_Number").InnerText.Length = 0 Then lstrError += "El Consecutivo es un dato requerido y no puede ir vacío." & vbCrLf
            If lxmlDocument.DocumentElement.SelectSingleNode("Reference_Type").InnerText.Length = 0 Then lstrError += "El Tipo de Referencia es un dato requerido y no puede ir vacío." & vbCrLf
            If lxmlDocument.DocumentElement.SelectSingleNode("Person_Name").InnerText.Length = 0 Then lstrError += "El Nombre de la referencia es un dato requerido y no puede ir vacío." & vbCrLf
            If lstrError.Length > 0 Then Throw New Exception("[ServiceDispatcherError]" & vbCrLf & lstrError)

            'If lxmlDocument.DocumentElement.SelectSingleNode("Paternal_Name").InnerText.Length = 0 Then lxmlDocument.DocumentElement.SelectSingleNode("Paternal_Name").InnerText = "NO PROPORCIONADO"
            'If lxmlDocument.DocumentElement.SelectSingleNode("Maternal_Name").InnerText.Length = 0 Then lxmlDocument.DocumentElement.SelectSingleNode("Maternal_Name").InnerText = "NO PROPORCIONADO"
            lobjRefence = New ConnectionBroker.DataManagerClient
            lobjRefence.Open()
            lobjRefence.setConnectionString(System.Configuration.ConfigurationManager.ConnectionStrings("MainConnection").ConnectionString)
            lobjRefence.setReturnIdentity(True)

            lstrSQL = "INSERT INTO BPM_References_Queue(Id_Dispatch, Customer_Number, Request_Number, Record_Date, Reference_Type, Person_Name, Paternal_Name, Maternal_Name, Street_Name, Outdoor_Number, Internal_Number," & vbCrLf &
                " Settlement, City, Municipality, State_Key, CP, Antiquity, Phone, Comments, Transfer_Status, Created_Date)" & vbCrLf &
            " VALUES (" & lintDispatch & ", '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Customer_Number").InnerText.Trim.Replace("'", "").ToUpper, 1, 14) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Request_Number").InnerText.Trim.Replace("'", "").ToUpper, 1, 15) & "', " &
            "GETDATE(), '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Reference_Type").InnerText.Trim.Replace("'", "").ToUpper, 1, 25) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Person_Name").InnerText.Trim.Replace("'", "").ToUpper, 1, 25) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Paternal_Name").InnerText.Trim.Replace("'", "").ToUpper.Replace("NO PROPORCIONADO", ""), 1, 25) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Maternal_Name").InnerText.Trim.Replace("'", "").ToUpper.Replace("NO PROPORCIONADO", ""), 1, 25) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Street_Name").InnerText.Trim.Replace("'", "").ToUpper, 1, 40) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Outdoor_Number").InnerText.Trim.Replace("'", "").ToUpper, 1, 20) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Internal_Number").InnerText.Trim.Replace("'", "").ToUpper, 1, 20) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Settlement").InnerText.Trim.Replace("'", "").ToUpper, 1, 40) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("City").InnerText.Trim.Replace("'", "").ToUpper, 1, 40) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Municipality").InnerText.Trim.Replace("'", "").ToUpper, 1, 36) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("State_Key").InnerText.Trim.Replace("'", "").ToUpper, 1, 4) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("CP").InnerText.Trim.Replace("'", "").ToUpper, 1, 5) & "', " &
            "" & Val("" & lxmlDocument.DocumentElement.SelectSingleNode("Antiquity").InnerText.Trim.Replace("'", "")) & " , '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Phone").InnerText.Trim.Replace("'", "").ToUpper, 1, 10) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Comments").InnerText.Trim.Replace("'", "").ToUpper, 1, 500) & "', " &
            "0, GETDATE())"
            lobjRefence.ExecuteCommand(lstrSQL)
            lintLogId = lobjRefence.getIdentityValue
            lobjRefence.Finalize()
            lobjRefence.Close()
            CloseDispatch(lintDispatch)
            Return "<ROOT><TRANSCODE>" & lintDispatch & "</TRANSCODE><DESCRIPTION>Transacción exitosa</DESCRIPTION><RETRY>0</RETRY></ROOT>"
        Catch ex As Exception
            SendReference = 0
            TrackError("SendReference" & vbCrLf & vbCrLf & ex.Message.ToString.Replace("""", "").Replace("<", "").Replace(">", "").Replace("[ServiceDispatcherError]", "") & vbCrLf & "Id_Dispatch :" & lintDispatch & vbCrLf & vbCrLf & lstrDataXML)
            Return "<ROOT><TRANSCODE>0</TRANSCODE><DESCRIPTION>" & ex.Message.ToString.Replace("""", "").Replace("<", "").Replace(">", "").Replace("[ServiceDispatcherError]", "") & "</DESCRIPTION>" & IIf(ex.Message.ToString.Contains("[ServiceDispatcherError]"), "<RETRY>0</RETRY>", "<RETRY>1</RETRY>") & "</ROOT>"
        Finally
            If Not lobjRefence Is Nothing Then lobjRefence = Nothing
        End Try
    End Function


    Public Function SendRequest(ByVal xmlData As Stream) As String Implements IService.SendRequest
        Dim lsrReaderXML As New StreamReader(xmlData)
        Dim lstrDataXML As String = lsrReaderXML.ReadToEnd
        Dim lintDispatch As Integer = 0
        Dim lobjRequest As ConnectionBroker.DataManagerClient = Nothing

        Dim lstrSQL As String = ""

        Try
            lintDispatch = InitDispatch("SendRequest", lstrDataXML)
            'Se inicia la transacción de registro
            If lintDispatch = 0 Then
                Return "<ROOT><TRANSCODE>0</TRANSCODE><DESCRIPTION>No se pudo generar token de transacción.</DESCRIPTION><RETRY>1</RETRY></ROOT>"
            End If

            Dim lxmlDocument As XmlDocument = Nothing
            Dim lxmlNode As XmlNode = Nothing
            Dim lstrError As String = ""
            Dim lintLogId As Integer = 0

            If lstrDataXML.Length = 0 Then Throw New Exception("[ServiceDispatcherError]" & vbCrLf & "El documento XML no puede ir vacío.")
            lxmlDocument = New XmlDocument
            lxmlDocument.LoadXml(lstrDataXML)


            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Request_Number")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Consecutivo (Request_Number) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Customer_Number")
            If lxmlNode Is Nothing Then lstrError = "El nodo de Número de Cliente (Customer_Number) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Company_Code")
            If lxmlNode Is Nothing Then lstrError = "El nodo de Distribuidor (Company_Code) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Branch_Code")
            If lxmlNode Is Nothing Then lstrError = "El nodo de Sucursal (Branch_Code) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Record_Date")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Fecha Alta (Record_Date) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("User_Name")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Usuario (User_Name) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Requested_Amount")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Monto Solicitado (Requested_Amount) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Authorized_Amount")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Monto Autorizado (Authorized_Amount) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Observations")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Observaciones (Observations) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Status")
            If lxmlNode Is Nothing Then lstrError += "El nodo Status (Status) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Result_Code")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Motivo (Result_Code) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("First_Name")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Fecha de Primer Nombre (First_Name) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Middle_Name")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Segundo Nombre (Middle_Name) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Paternal_Name")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Apellido Paterno (Paternal_Name) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Maternal_Name")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Apellido Materno (Maternal_Name) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Seller")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Vendedor (Seller) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Promotion")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Promoción (Promotion) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Loan_Reference")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Referencia (Loan_Reference) no está contenido en el XML." & vbCrLf

            If lstrError.Length > 0 Then Throw New Exception("[ServiceDispatcherError]" & vbCrLf & lstrError)
            If lxmlDocument.DocumentElement.SelectSingleNode("Customer_Number").InnerText.Length = 0 Then lstrError = "El Número de Cliente es un dato requerido y no puede ir vacío." & vbCrLf
            If lxmlDocument.DocumentElement.SelectSingleNode("Request_Number").InnerText.Length = 0 Then lstrError += "El Consecutivo es un dato requerido y no puede ir vacío." & vbCrLf
            If lxmlDocument.DocumentElement.SelectSingleNode("Company_Code").InnerText.Length = 0 Then lstrError += "El Distribuidor es un dato requerido y no puede ir vacío." & vbCrLf
            If lxmlDocument.DocumentElement.SelectSingleNode("Branch_Code").InnerText.Length = 0 Then lstrError += "La Sucursal es un dato requerido y no puede ir vacío." & vbCrLf
            If lxmlDocument.DocumentElement.SelectSingleNode("Record_Date").InnerText.Length = 0 Then lstrError += "La Fecha Alta es un dato requerido y no puede ir vacío." & vbCrLf
            If lxmlDocument.DocumentElement.SelectSingleNode("User_Name").InnerText.Length = 0 Then lstrError += "El Usuario es un dato requerido y no puede ir vacío." & vbCrLf
            If lxmlDocument.DocumentElement.SelectSingleNode("Requested_Amount").InnerText.Length = 0 Then lstrError += "El Monto Solicitado  es un dato requerido y no puede ir vacío." & vbCrLf
            If Val("" & lxmlDocument.DocumentElement.SelectSingleNode("Requested_Amount").InnerText.Replace(",", "").Replace("$", "")) <= 1000 _
                And lxmlDocument.DocumentElement.SelectSingleNode("Status").InnerText.Trim.ToUpper = "AUTORIZADA" Then lstrError += "El Monto Solicitado no puede ser menor o igual a 1,000.00." & vbCrLf
            If lxmlDocument.DocumentElement.SelectSingleNode("Promotion").InnerText.Length = 0 _
                And lxmlDocument.DocumentElement.SelectSingleNode("Status").InnerText.Trim.ToUpper = "AUTORIZADA" Then lstrError += "La Promoción es un dato requerido y no puede ir vacío." & vbCrLf
            If lstrError.Length > 0 Then Throw New Exception("[ServiceDispatcherError]" & vbCrLf & lstrError)
            lobjRequest = New ConnectionBroker.DataManagerClient
            lobjRequest.Open()
            lobjRequest.setConnectionString(System.Configuration.ConfigurationManager.ConnectionStrings("MainConnection").ConnectionString)
            lobjRequest.setReturnIdentity(True)

            lstrSQL = "INSERT INTO BPM_Requests_Queue(Id_Dispatch, Request_Number, Customer_Number, Record_Date, Company_Code, Branch_Code, User_Name, Requested_Amount, Authorized_Amount, Observations," & vbCrLf &
                " Status, Result_Code,First_Name,Middle_Name,Paternal_Name,Maternal_Name,Seller,Promotion,Loan_Reference,Transfer_Status, Created_Date)" & vbCrLf &
            " VALUES (" & lintDispatch & ", '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Request_Number").InnerText.Trim.Replace("'", "").ToUpper, 1, 14) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Customer_Number").InnerText.Trim.Replace("'", "").ToUpper, 1, 15) & "', " &
            "GETDATE(), '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Company_Code").InnerText.Trim.Replace("'", "").ToUpper, 1, 5) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Branch_Code").InnerText.Trim.Replace("'", "").ToUpper, 1, 5) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("User_Name").InnerText.Trim.Replace("'", "").ToUpper, 1, 6) & "', " &
            "" & Val("" & lxmlDocument.DocumentElement.SelectSingleNode("Requested_Amount").InnerText.Trim.Replace("'", "").Replace(",", "").Replace("$", "")) & " , " &
            "" & Val("" & lxmlDocument.DocumentElement.SelectSingleNode("Authorized_Amount").InnerText.Trim.Replace("'", "").Replace(",", "").Replace("$", "")) & " , '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Observations").InnerText.Trim.Replace("'", "").ToUpper, 1, 255) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Status").InnerText.Trim.Replace("'", "").ToUpper, 1, 20) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Result_Code").InnerText.Trim.Replace("'", "").ToUpper, 1, 5) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("First_Name").InnerText.Trim.Replace("'", "").ToUpper, 1, 20) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Middle_Name").InnerText.Trim.Replace("'", "").ToUpper, 1, 20) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Paternal_Name").InnerText.Trim.Replace("'", "").ToUpper.Replace("NO PROPORCIONADO", ""), 1, 25) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Maternal_Name").InnerText.Trim.Replace("'", "").ToUpper.Replace("NO PROPORCIONADO", ""), 1, 25) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Seller").InnerText.Trim.Replace("'", "").ToUpper, 1, 60) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Promotion").InnerText.Trim.Replace("'", "").ToUpper, 1, 6) & "' , '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Loan_Reference").InnerText.Trim.Replace("'", "").ToUpper, 1, 20) & "', " &
            "0, GETDATE())"
            lobjRequest.ExecuteCommand(lstrSQL)
            lintLogId = lobjRequest.getIdentityValue
            lobjRequest.Finalize()
            lobjRequest.Close()
            CloseDispatch(lintDispatch)
            Return "<ROOT><TRANSCODE>" & lintDispatch & "</TRANSCODE><DESCRIPTION>Transacción exitosa</DESCRIPTION><RETRY>0</RETRY></ROOT>"
        Catch ex As Exception
            SendRequest = 0
            TrackError("SendRequest" & vbCrLf & vbCrLf & ex.Message.ToString.Replace("""", "").Replace("<", "").Replace(">", "").Replace("[ServiceDispatcherError]", "") & vbCrLf & "Id_Dispatch :" & lintDispatch & vbCrLf & vbCrLf & lstrDataXML)
            Return "<ROOT><TRANSCODE>0</TRANSCODE><DESCRIPTION>" & ex.Message.ToString.Replace("""", "").Replace("<", "").Replace(">", "").Replace("[ServiceDispatcherError]", "") & "</DESCRIPTION>" & IIf(ex.Message.ToString.Contains("[ServiceDispatcherError]"), "<RETRY>0</RETRY>", "<RETRY>1</RETRY>") & "</ROOT>"
        Finally
            If Not lobjRequest Is Nothing Then lobjRequest = Nothing
        End Try
    End Function

    Public Function SendPreAuthorization(ByVal xmlData As Stream) As String Implements IService.SendPreAuthorization
        Dim lsrReaderXML As New StreamReader(xmlData)
        Dim lstrDataXML As String = lsrReaderXML.ReadToEnd
        Dim lintDispatch As Integer = 0
        Dim lobjPreAuthorizationEX As ConnectionBroker.DataManagerClient = Nothing

        Dim lstrSQL As String = ""

        Try
            lintDispatch = InitDispatch("SendPreAuthorization", lstrDataXML)
            'Se inicia la transacción de registro
            If lintDispatch = 0 Then
                Return "<ROOT><TRANSCODE>0</TRANSCODE><DESCRIPTION>No se pudo generar token de transacción.</DESCRIPTION><RETRY>1</RETRY></ROOT>"
            End If

            Dim lxmlDocument As XmlDocument = Nothing
            Dim lxmlNode As XmlNode = Nothing
            Dim lstrError As String = ""
            Dim lintLogId As Integer = 0

            If lstrDataXML.Length = 0 Then Throw New Exception("[ServiceDispatcherError]" & vbCrLf & "El documento XML no puede ir vacío.")
            lxmlDocument = New XmlDocument
            lxmlDocument.LoadXml(lstrDataXML)


            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Request_Number")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Consecutivo (Request_Number) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Company_Code")
            If lxmlNode Is Nothing Then lstrError = "El nodo de Distribuidor (Company_Code) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Branch_Code")
            If lxmlNode Is Nothing Then lstrError = "El nodo de Sucursal (Branch_Code) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Seller")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Vendedor (Seller) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Paternal_Name")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Apellido Paterno (Paternal_Name) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Maternal_Name")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Apellido Materno (Maternal_Name) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("First_Name")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Fecha de Primer Nombre (First_Name) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Middle_Name")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Segundo Nombre (Middle_Name) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("User_Name")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Usuario (User_Name) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("User_Name")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Usuario (User_Name) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("User_Name")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Usuario (User_Name) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("NIP")
            If lxmlNode Is Nothing Then lstrError += "El nodo de NIP (NIP) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Query_type")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Tipo de Consulta(Query_type) no está contenido en el XML." & vbCrLf


            If lstrError.Length > 0 Then Throw New Exception("[ServiceDispatcherError]" & vbCrLf & lstrError)
            If lxmlDocument.DocumentElement.SelectSingleNode("Request_Number").InnerText.Length = 0 Then lstrError += "El Consecutivo es un dato requerido y no puede ir vacío." & vbCrLf
            If lxmlDocument.DocumentElement.SelectSingleNode("Company_Code").InnerText.Length = 0 Then lstrError += "El Distribuidor es un dato requerido y no puede ir vacío." & vbCrLf
            If lxmlDocument.DocumentElement.SelectSingleNode("Branch_Code").InnerText.Length = 0 Then lstrError += "La Sucursal es un dato requerido y no puede ir vacío." & vbCrLf
            If lstrError.Length > 0 Then Throw New Exception("[ServiceDispatcherError]" & vbCrLf & lstrError)
            lobjPreAuthorizationEX = New ConnectionBroker.DataManagerClient
            lobjPreAuthorizationEX.Open()
            lobjPreAuthorizationEX.setConnectionString(System.Configuration.ConfigurationManager.ConnectionStrings("AutosMainConnection").ConnectionString)
            lobjPreAuthorizationEX.setReturnIdentity(True)


            lstrSQL = "INSERT INTO BPM_PreAuthorizations_Queue(Id_Dispatch, Company_Code, Branch_Code, Seller, Paternal_Name, Maternal_Name, First_Name, Middle_Name, User_Name, " & vbCrLf &
                "NIP, Query_type, Request_Number,Transfer_Status, Created_Date)" & vbCrLf &
            " VALUES (" & lintDispatch & ", '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Company_Code").InnerText.Trim.Replace("'", "").ToUpper, 1, 5) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Branch_Code").InnerText.Trim.Replace("'", "").ToUpper, 1, 5) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Seller").InnerText.Trim.Replace("'", "").ToUpper, 1, 60) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Paternal_Name").InnerText.Trim.Replace("'", "").ToUpper, 1, 25) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Maternal_Name").InnerText.Trim.Replace("'", "").ToUpper, 1, 25) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("First_Name").InnerText.Trim.Replace("'", "").ToUpper, 1, 20) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Middle_Name").InnerText.Trim.Replace("'", "").ToUpper, 1, 20) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("User_Name").InnerText.Trim.Replace("'", "").ToUpper, 1, 6) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("NIP").InnerText.Trim.Replace("'", "").ToUpper, 1, 180) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Query_type").InnerText.Trim.Replace("'", "").ToUpper, 1, 2) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Request_Number").InnerText.Trim.Replace("'", "").ToUpper, 1, 14) & "', " &
            "0, GETDATE())"
            lobjPreAuthorizationEX.ExecuteCommand(lstrSQL)
            lintLogId = lobjPreAuthorizationEX.getIdentityValue
            lobjPreAuthorizationEX.Finalize()
            lobjPreAuthorizationEX.Close()
            CloseDispatch(lintDispatch)
            Return "<ROOT><TRANSCODE>" & lintDispatch & "</TRANSCODE><DESCRIPTION>Transacción exitosa</DESCRIPTION><RETRY>0</RETRY></ROOT>"
        Catch ex As Exception
            SendPreAuthorization = 0
            TrackError("SendPreAuthorization" & vbCrLf & Date.Now().ToString() & vbCrLf & vbCrLf & ex.Message.ToString.Replace("""", "").Replace("<", "").Replace(">", "").Replace("[ServiceDispatcherError]", "") & vbCrLf & "Id_Dispatch :" & lintDispatch & vbCrLf & vbCrLf & lstrDataXML)
            Return "<ROOT><TRANSCODE>0</TRANSCODE><DESCRIPTION>" & ex.Message.ToString.Replace("""", "").Replace("<", "").Replace(">", "").Replace("[ServiceDispatcherError]", "") & "</DESCRIPTION>" & IIf(ex.Message.ToString.Contains("[ServiceDispatcherError]"), "<RETRY>0</RETRY>", "<RETRY>1</RETRY>") & "</ROOT>"
        Finally
            If Not lobjPreAuthorizationEX Is Nothing Then lobjPreAuthorizationEX = Nothing
        End Try
    End Function

    Public Function SendFile(ByVal xmlData As Stream) As String Implements IService.SendFile
        Dim lintDispatch As Integer = 0
        Dim lsrReader As New StreamReader(xmlData)
        Dim lstrDataXML As String = lsrReader.ReadToEnd
        Dim stringb64 As String = Nothing
        Dim File_Extension As String = Nothing
        Dim File_Type As String = Nothing
        Dim Request_Number As String = Nothing
        Dim DocName As String = Nothing
        Dim arrbytes As Byte() = Nothing
        Dim Path_Base As String = System.Configuration.ConfigurationManager.AppSettings("path")
        Dim path_File As String = Nothing
        Dim lobjSendFile As ConnectionBroker.DataManagerClient = Nothing

        Try


            lintDispatch = InitDispatch("SendFile", Mid(lstrDataXML, 1, 300))
            'Se inicia la transacción de registro
            If lintDispatch = 0 Then
                Return "<ROOT><TRANSCODE>0</TRANSCODE><DESCRIPTION>No se pudo generar token de transacción.</DESCRIPTION><RETRY>1</RETRY></ROOT>"
            End If


            Dim lxmlDocument As XmlDocument = Nothing
            Dim lxmlNode As XmlNode = Nothing
            Dim lstrSQL As String = ""
            Dim lstrError As String = ""
            Dim lintLogId As Integer = 0

            If lstrDataXML.Length = 0 Then Throw New Exception("[ServiceDispatcherError]" & vbCrLf & "El documento XML no puede ir vacío.")
            lxmlDocument = New XmlDocument
            lxmlDocument.LoadXml(lstrDataXML)

            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Company_Code")
            If lxmlNode Is Nothing Then lstrError = "El nodo de Distribuidor (Company_Code) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Branch_Code")
            If lxmlNode Is Nothing Then lstrError = "El nodo de Sucursal (Branch_Code) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Request_Number")
            If lxmlNode Is Nothing Then lstrError = "El nodo de Numero de Consecutivo(Request_Number) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Seller")
            If lxmlNode Is Nothing Then lstrError = "El nodo de Vendedor(Seller) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("User_Name")
            If lxmlNode Is Nothing Then lstrError = "El nodo de Usuario(User_Name) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("File_Type")
            If lxmlNode Is Nothing Then lstrError = "El nodo de Extensión de archivo(File_Type) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("File_Extension")
            If lxmlNode Is Nothing Then lstrError = "El nodo de Extensión de archivo(File_Extension) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("File_B64")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Archivo Base64 (File_B64) no está contenido en el XML." & vbCrLf


            If lstrError.Length > 0 Then Throw New Exception("[ServiceDispatcherError]" & vbCrLf & lstrError)
            If lxmlDocument.DocumentElement.SelectSingleNode("Request_Number").InnerText.Length = 0 Then lstrError = "El Numero de Consecutivo es un dato requerido y no puede ir vacío." & vbCrLf
            If lxmlDocument.DocumentElement.SelectSingleNode("File_Type").InnerText.Length = 0 Then lstrError = "El tipo de Documento es un dato requerido y no puede ir vacío." & vbCrLf
            If Not (lxmlDocument.DocumentElement.SelectSingleNode("File_Type").InnerText.Trim.ToLower = "prsc" Or
                 lxmlDocument.DocumentElement.SelectSingleNode("File_Type").InnerText.Trim.ToLower = "sc" Or
                 lxmlDocument.DocumentElement.SelectSingleNode("File_Type").InnerText.Trim.ToLower = "pa" Or
                 lxmlDocument.DocumentElement.SelectSingleNode("File_Type").InnerText.Trim.ToLower = "iof" Or
                 lxmlDocument.DocumentElement.SelectSingleNode("File_Type").InnerText.Trim.ToLower = "docad") Then lstrError = "El tipo de Documento no es Correcto solo se admiten los Siguientes tipos de Documentos prsc,sc,pa,iof,docad." & vbCrLf
            If lxmlDocument.DocumentElement.SelectSingleNode("File_Extension").InnerText.Length = 0 Then lstrError = "La Extensión del Documento es un dato requerido y no puede ir vacío." & vbCrLf
            If Not (lxmlDocument.DocumentElement.SelectSingleNode("File_Extension").InnerText.Trim.ToLower = "pdf" Or
                lxmlDocument.DocumentElement.SelectSingleNode("File_Extension").InnerText.Trim.ToLower = "jpeg" Or
                lxmlDocument.DocumentElement.SelectSingleNode("File_Extension").InnerText.Trim.ToLower = "jpg" Or
                lxmlDocument.DocumentElement.SelectSingleNode("File_Extension").InnerText.Trim.ToLower = "png") Then lstrError = "El tipo de Documento no es Correcto solo se admiten los Siguientes tipos de Documentos pdf,jpeg,jpg,png." & vbCrLf
            If lxmlDocument.DocumentElement.SelectSingleNode("File_B64").InnerText.Length = 0 Then lstrError += "El Documento en Base64 es un dato requerido y no puede ir vacío." & vbCrLf
            If lstrError.Length > 0 Then Throw New Exception("[ServiceDispatcherError]" & vbCrLf & lstrError)


            stringb64 = lxmlDocument.DocumentElement.SelectSingleNode("File_B64").InnerText.Trim()
            File_Extension = lxmlDocument.DocumentElement.SelectSingleNode("File_Extension").InnerText.Trim.ToLower
            Request_Number = lxmlDocument.DocumentElement.SelectSingleNode("Request_Number").InnerText.Trim.ToUpper
            File_Type = lxmlDocument.DocumentElement.SelectSingleNode("File_Type").InnerText.Trim.ToLower
            arrbytes = ConvertBase64ToByteArray(stringb64)

            If Not System.IO.Directory.Exists(Path_Base + Request_Number.Substring(0, 4) + "\" + Date.Today.Year.ToString + Date.Today.Month.ToString("D2")) Then
                System.IO.Directory.CreateDirectory(Path_Base + Request_Number.Substring(0, 4) + "\" + Date.Today.Year.ToString + Date.Today.Month.ToString("D2"))
            End If

            path_File = Path_Base + Request_Number.Substring(0, 4) + "\" + Date.Today.Year.ToString + Date.Today.Month.ToString("D2") + "\" + lintDispatch.ToString + "_" + File_Type + "." + File_Extension
            File.WriteAllBytes(path_File, arrbytes)


            lobjSendFile = New ConnectionBroker.DataManagerClient
            lobjSendFile.Open()
            lobjSendFile.setConnectionString(System.Configuration.ConfigurationManager.ConnectionStrings("AutosMainConnection").ConnectionString)
            lobjSendFile.setReturnIdentity(True)
            lstrSQL = "INSERT INTO BPM_Files_Queue(Id_Dispatch, Company_Code, Branch_Code, Seller, User_Name, " & vbCrLf &
               "Request_Number, File_Type, File_Path,Transfer_Status, Created_Date)" & vbCrLf &
           " VALUES (" & lintDispatch & ", '" &
           "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Company_Code").InnerText.Trim.Replace("'", "").ToUpper, 1, 5) & "', '" &
           "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Branch_Code").InnerText.Trim.Replace("'", "").ToUpper, 1, 5) & "', '" &
           "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Seller").InnerText.Trim.Replace("'", "").ToUpper, 1, 60) & "', '" &
           "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("User_Name").InnerText.Trim.Replace("'", "").ToUpper, 1, 6) & "', '" &
           "" & Request_Number & "', '" &
           "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("File_Type").InnerText.Trim.Replace("'", "").ToUpper, 1, 5) & "', '" &
           "" & path_File.Replace(Path_Base, "") & "', " &
           "0, GETDATE())"
            lobjSendFile.ExecuteCommand(lstrSQL)
            lintLogId = lobjSendFile.getIdentityValue
            lobjSendFile.Finalize()
            lobjSendFile.Close()

            CloseDispatch(lintDispatch)

            Return "<ROOT><TRANSCODE>" & lintDispatch & "</TRANSCODE><DESCRIPTION>Transacción exitosa</DESCRIPTION><RETRY>0</RETRY></ROOT>"

        Catch ex As Exception
            SendFile = 0
            TrackError("SendFile" & vbCrLf & Date.Now().ToString() & vbCrLf & vbCrLf & ex.Message.ToString.Replace("""", "").Replace("<", "").Replace(">", "").Replace("[ServiceDispatcherError]", "") & vbCrLf & "Id_Dispatch :" & lintDispatch & vbCrLf & vbCrLf & Mid(lstrDataXML, 1, 300))
            Return "<ROOT><TRANSCODE>0</TRANSCODE><DESCRIPTION>" & ex.Message.ToString.Replace("""", "").Replace("<", "").Replace(">", "").Replace("[ServiceDispatcherError]", "") & "</DESCRIPTION>" & IIf(ex.Message.ToString.Contains("[ServiceDispatcherError]"), "<RETRY>0</RETRY>", "<RETRY>1</RETRY>") & "</ROOT>"
        Finally
            If Not arrbytes Is Nothing Then arrbytes = Nothing
            If Not lsrReader Is Nothing Then lsrReader = Nothing
            If Not xmlData Is Nothing Then xmlData = Nothing
            If Not lstrDataXML Is Nothing Then lstrDataXML = Nothing
            If Not stringb64 Is Nothing Then stringb64 = Nothing
            If Not lobjSendFile Is Nothing Then lobjSendFile = Nothing
        End Try

    End Function


    Public Function SendAdditionalData(ByVal xmlData As Stream) As String Implements IService.SendAdditionalData
        Dim lsrReaderXML As New StreamReader(xmlData)
        Dim lstrDataXML As String = lsrReaderXML.ReadToEnd
        Dim lintDispatch As Integer = 0
        Dim lobjAdditionalData As ConnectionBroker.DataManagerClient = Nothing
        Dim IsFailError As Integer = 0

        Dim lstrSQL As String = ""

        Try
            lintDispatch = InitDispatch("SendAdditionalData", lstrDataXML)
            'Se inicia la transacción de registro
            If lintDispatch = 0 Then
                Return "<ROOT><TRANSCODE>0</TRANSCODE><DESCRIPTION>No se pudo generar token de transacción.</DESCRIPTION><RETRY>1</RETRY></ROOT>"
            End If

            Dim lxmlDocument As XmlDocument = Nothing
            Dim lxmlNode As XmlNode = Nothing
            Dim lstrError As String = ""
            Dim lintLogId As Integer = 0

            If lstrDataXML.Length = 0 Then Throw New Exception("[ServiceDispatcherError]" & vbCrLf & "El documento XML no puede ir vacío.")
            lxmlDocument = New XmlDocument
            lxmlDocument.LoadXml(lstrDataXML)


            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Request_Number")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Consecutivo (Request_Number) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Customer_Number")
            If lxmlNode Is Nothing Then lstrError = "El nodo de Número de Cliente (Customer_Number) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Monthly_Amount")
            If lxmlNode Is Nothing Then lstrError = "El nodo de Importe Mensual (Monthly_Amount) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Term")
            If lxmlNode Is Nothing Then lstrError = "El nodo de Plazo (Term) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Curp")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Curp (Curp) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Profession")
            If lxmlNode Is Nothing Then lstrError += "El nodo de profesión (Profession) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Mail")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Mail (Mail) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Work_Mail")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Mail de Trabajo (Work_Mail) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("cell_Phone")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Celular (cell_Phone) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Profile")
            If lxmlNode Is Nothing Then lstrError += "El nodo Perfil (Profile) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Activity")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Actividad (Activity) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Birth_Entity")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Entidad de Nacimiento (Birth_Entity) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("End_Resources")
            If lxmlNode Is Nothing Then lstrError += "El nodo de FinRecursos (End_Resources) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Origin_Resources")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Origen de Recursos (Origin_Resources) no está contenido en el XML." & vbCrLf
            lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Country_of_Birth")
            If lxmlNode Is Nothing Then lstrError += "El nodo de Pais de Nacimiento (Country_of_Birth) no está contenido en el XML." & vbCrLf

            If lxmlDocument.DocumentElement.SelectSingleNode("Customer_Number").InnerText.Length = 0 And
                lxmlDocument.DocumentElement.SelectSingleNode("Monthly_Amount").InnerText.Length = 0 And
                lxmlDocument.DocumentElement.SelectSingleNode("Term").InnerText.Length = 0 And
                lxmlDocument.DocumentElement.SelectSingleNode("Curp").InnerText.Length = 0 And
                lxmlDocument.DocumentElement.SelectSingleNode("Profession").InnerText.Length = 0 And
                lxmlDocument.DocumentElement.SelectSingleNode("Mail").InnerText.Length = 0 And
                lxmlDocument.DocumentElement.SelectSingleNode("Work_Mail").InnerText.Length = 0 And
                lxmlDocument.DocumentElement.SelectSingleNode("cell_Phone").InnerText.Length = 0 And
                lxmlDocument.DocumentElement.SelectSingleNode("Activity").InnerText.Length = 0 And
                lxmlDocument.DocumentElement.SelectSingleNode("Birth_Entity").InnerText.Length = 0 And
                lxmlDocument.DocumentElement.SelectSingleNode("Country_of_Birth").InnerText.Length = 0 Then
                IsFailError = 1
            End If



            If lstrError.Length > 0 Then Throw New Exception("[ServiceDispatcherError]" & vbCrLf & lstrError)
            If lxmlDocument.DocumentElement.SelectSingleNode("Customer_Number").InnerText.Length = 0 Then lstrError = "El Número de Cliente es un dato requerido y no puede ir vacío." & vbCrLf
            If lxmlDocument.DocumentElement.SelectSingleNode("Request_Number").InnerText.Length = 0 Then lstrError += "El Consecutivo es un dato requerido y no puede ir vacío." & vbCrLf

            If lstrError.Length > 0 Then Throw New Exception("[ServiceDispatcherError]" & vbCrLf & lstrError)
            lobjAdditionalData = New ConnectionBroker.DataManagerClient
            lobjAdditionalData.Open()
            lobjAdditionalData.setConnectionString(System.Configuration.ConfigurationManager.ConnectionStrings("MainConnection").ConnectionString)
            lobjAdditionalData.setReturnIdentity(True)



            lstrSQL = "INSERT INTO dbo.BPM_Additional_Data_Queue(Id_Dispatch,Customer_Number,Request_Number,Monthly_Amount,Term,Curp,Profession,Mail,Work_Mail" & vbCrLf &
                ",cell_Phone,Profile,Activity,Birth_Entity,End_Resources,Origin_Resources,Country_of_Birth,Transfer_Status,Created_Date)" & vbCrLf &
            " VALUES (" & lintDispatch & ", '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Customer_Number").InnerText.Trim.Replace("'", "").ToUpper, 1, 14) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Request_Number").InnerText.Trim.Replace("'", "").ToUpper, 1, 15) & "', " &
            "" & Val("" & lxmlDocument.DocumentElement.SelectSingleNode("Monthly_Amount").InnerText.Trim.Replace("'", "").Replace(",", "").Replace("$", "")) & " , " &
            "" & Val("" & lxmlDocument.DocumentElement.SelectSingleNode("Term").InnerText.Trim) & " , '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Curp").InnerText.Trim.Replace("'", "").ToUpper, 1, 20) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Profession").InnerText.Trim.Replace("'", "").ToUpper, 1, 20) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Mail").InnerText.Trim.Replace("'", "").ToUpper, 1, 20) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Work_Mail").InnerText.Trim.Replace("'", "").ToUpper, 1, 20) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("cell_Phone").InnerText.Trim.Replace("'", "").ToUpper, 1, 10) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Profile").InnerText.Trim.Replace("'", "").ToUpper, 1, 5) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Activity").InnerText.Trim.Replace("'", "").ToUpper, 1, 20) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Birth_Entity").InnerText.Trim.Replace("'", "").ToUpper, 1, 15) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("End_Resources").InnerText.Trim.Replace("'", "").ToUpper, 1, 15) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Origin_Resources").InnerText.Trim.Replace("'", "").ToUpper, 1, 15) & "', '" &
            "" & Mid(lxmlDocument.DocumentElement.SelectSingleNode("Country_of_Birth").InnerText.Trim.Replace("'", "").ToUpper, 1, 15) & "', " &
            "0, GETDATE())"
            lobjAdditionalData.ExecuteCommand(lstrSQL)
            lintLogId = lobjAdditionalData.getIdentityValue
            lobjAdditionalData.Finalize()
            lobjAdditionalData.Close()
            CloseDispatch(lintDispatch)
            Return "<ROOT><TRANSCODE>" & lintDispatch & "</TRANSCODE><DESCRIPTION>Transacción exitosa</DESCRIPTION><RETRY>0</RETRY></ROOT>"
        Catch ex As Exception
            SendAdditionalData = 0
            If IsFailError = 0 Then
                TrackError("SendAdditionalData" & vbCrLf & vbCrLf & ex.Message.ToString.Replace("""", "").Replace("<", "").Replace(">", "").Replace("[ServiceDispatcherError]", "") & vbCrLf & "Id_Dispatch :" & lintDispatch & vbCrLf & vbCrLf & lstrDataXML)

            End If

            Return "<ROOT><TRANSCODE>0</TRANSCODE><DESCRIPTION>" & ex.Message.ToString.Replace("""", "").Replace("<", "").Replace(">", "").Replace("[ServiceDispatcherError]", "") & "</DESCRIPTION>" & IIf(ex.Message.ToString.Contains("[ServiceDispatcherError]"), "<RETRY>0</RETRY>", "<RETRY>1</RETRY>") & "</ROOT>"
        Finally
            If Not lobjAdditionalData Is Nothing Then lobjAdditionalData = Nothing
        End Try
    End Function


    ' recursos de servicios privados para los servicios

    'Private Function Request_Exists(Request_Number As String) As Boolean
    '    Dim lobjValidate As ConnectionBroker.DataManagerClient = Nothing
    '    Dim dts As DataSet = Nothing
    '    Dim lstrRes As String = Nothing

    '    Dim lstrSQL As String = ""

    '    Try
    '        Dim lintDispatchId As Integer = 0

    '        lobjValidate = New ConnectionBroker.DataManagerClient
    '        lobjValidate.Open()
    '        lobjValidate.setConnectionString(System.Configuration.ConfigurationManager.ConnectionStrings("MainConnection").ConnectionString)
    '        lobjValidate.setReturnIdentity(True)
    '        lstrSQL = "EXEC BPM_Verify_Request_Number '" & Request_Number & "'"
    '        dts = lobjValidate.GetDataSet(lstrSQL, "Data")
    '        lstrRes = dts.Tables(0).Rows(0).ItemArray(0).ToString()
    '        lobjValidate.Finalize()
    '        lobjValidate.Close()
    '        Request_Exists = IIf((lstrRes = "1"), True, False)
    '        Return Request_Exists
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    Finally
    '        If Not lobjValidate Is Nothing Then lobjValidate = Nothing
    '        If Not dts Is Nothing Then dts = Nothing
    '    End Try
    'End Function

    Function DateCheck(fecha As String) As Boolean
        Dim result As Boolean = False

        If fecha.Length > 7 Then


            fecha = Mid(fecha, 1, 2)
            fecha = fecha.Replace("/", "")
            If (fecha < 13) Then

                result = True

            Else
                result = False
            End If

        End If

        Return result

    End Function

    Public Function ConvertBase64ToByteArray(base64 As String) As Byte()
        Return Convert.FromBase64String(base64) 'Convert the base64 back to byte array.
    End Function

    Private Function InitDispatch(ByVal ServiceName As String, ByVal xmlData As String) As Integer
        Dim lobjDispatch As ConnectionBroker.DataManagerClient = Nothing
        Dim lstrSQL As String = ""
        Try
            Dim lintDispatchId As Integer = 0

            lobjDispatch = New ConnectionBroker.DataManagerClient
            lobjDispatch.Open()
            lobjDispatch.setConnectionString(System.Configuration.ConfigurationManager.ConnectionStrings("MainConnection").ConnectionString)
            lobjDispatch.setReturnIdentity(True)

            lstrSQL = "INSERT INTO BPM_Dispatcher_Log (Service_Name, XML_Data) VALUES ('" & ServiceName & "','" & xmlData.Replace("'", "''") & "')"
            lobjDispatch.ExecuteCommand(lstrSQL)
            lintDispatchId = lobjDispatch.getIdentityValue
            lobjDispatch.Finalize()
            lobjDispatch.Close()
            Return lintDispatchId
        Catch ex As Exception
            InitDispatch = 0
            Throw New Exception(ex.Message)
        Finally
            If Not lobjDispatch Is Nothing Then lobjDispatch = Nothing
        End Try
    End Function



    Private Sub CloseDispatch(ByVal intIdDispatch As Integer)
        Dim lobjDispatch As ConnectionBroker.DataManagerClient = Nothing

        Dim lstrSQL As String = ""

        Try
            Dim lintDispatchId As Integer = 0

            lobjDispatch = New ConnectionBroker.DataManagerClient
            lobjDispatch.Open()
            lobjDispatch.setConnectionString(System.Configuration.ConfigurationManager.ConnectionStrings("MainConnection").ConnectionString)
            lobjDispatch.setReturnIdentity(True)
            lstrSQL = "UPDATE BPM_Dispatcher_Log SET Process_Status = 1 WHERE Id_Dispatch = " & intIdDispatch
            lobjDispatch.ExecuteCommand(lstrSQL)
            lintDispatchId = lobjDispatch.getIdentityValue
            lobjDispatch.Finalize()
            lobjDispatch.Close()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not lobjDispatch Is Nothing Then lobjDispatch = Nothing
        End Try
    End Sub

    Private Function InitPublicDispatch(ByVal ServiceName As String, ByVal xmlData As String) As Integer
        Dim lobjDispatch As ConnectionBroker.DataManagerClient = Nothing
        Dim lstrSQL As String = ""
        Try
            Dim lintDispatchId As Integer = 0

            lobjDispatch = New ConnectionBroker.DataManagerClient

            lobjDispatch.Open()
            DirectCast(lobjDispatch.Endpoint.Binding, System.ServiceModel.WSHttpBinding).ReaderQuotas.MaxStringContentLength = 2147483647
            lobjDispatch.setConnectionString(System.Configuration.ConfigurationManager.ConnectionStrings("CRMXKIOVMBD09").ConnectionString)
            lobjDispatch.setReturnIdentity(True)

            lstrSQL = "INSERT INTO Public_Services_Dispatcher_Log (Service_Name, XML_Data) VALUES ('" & ServiceName & "','" & xmlData.Replace("'", "''") & "')"
            lobjDispatch.ExecuteCommand(lstrSQL)
            lintDispatchId = lobjDispatch.getIdentityValue
            lobjDispatch.Finalize()
            lobjDispatch.Close()
            Return lintDispatchId
        Catch ex As Exception
            InitPublicDispatch = 0
            Throw New Exception(ex.Message)
        Finally
            If Not lobjDispatch Is Nothing Then lobjDispatch = Nothing
        End Try
    End Function


    Private Sub ClosePublicDispatch(ByVal intIdDispatch As Integer, ByVal ProcessData As String)
        Dim lobjDispatch As ConnectionBroker.DataManagerClient = Nothing

        Dim lstrSQL As String = ""

        Try
            Dim lintDispatchId As Integer = 0

            lobjDispatch = New ConnectionBroker.DataManagerClient
            lobjDispatch.Open()
            lobjDispatch.setConnectionString(System.Configuration.ConfigurationManager.ConnectionStrings("CRMXKIOVMBD09").ConnectionString)
            lobjDispatch.setReturnIdentity(True)
            lstrSQL = "UPDATE Public_Services_Dispatcher_Log SET Process_Status = 1, Process_Data = '" & ProcessData & "', Process_date= getdate() WHERE Id_Dispatch = " & intIdDispatch
            lobjDispatch.ExecuteCommand(lstrSQL)
            lintDispatchId = lobjDispatch.getIdentityValue
            lobjDispatch.Finalize()
            lobjDispatch.Close()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not lobjDispatch Is Nothing Then lobjDispatch = Nothing
        End Try
    End Sub


    Private Sub TrackError2(ByVal sEvent As String)
        'Dim cmdComand As String
        Dim sSource As String
        Dim sLogName As String
        Dim sMachine As String

        sSource = "EventCreate"
        sLogName = "Application"
        sMachine = "."

        Dim dtsource As New EventSourceCreationData(sSource, sLogName)

        If Not EventLog.SourceExists(sSource, sMachine) Then
            EventLog.CreateEventSource(dtsource)
        End If

        Dim ELog As New EventLog(sLogName, sMachine, sSource)
        ELog.WriteEntry(sEvent, EventLogEntryType.Error, 99)


    End Sub


    Private Sub TrackError(ByVal strEvent As String)
        'If Not EventLog.SourceExists("Application") Then
        '    Dim lobjEvent As New EventSourceCreationData("AppianDispatcher", "Application")
        'End If

        EventLog.WriteEntry("EventCreate", strEvent, EventLogEntryType.Error, 99)
    End Sub




End Class
