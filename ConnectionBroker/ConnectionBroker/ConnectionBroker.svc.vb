Imports System.Data.SqlClient

<ServiceContract()> _
Public Interface IDataManager
    <OperationContract()> _
    Function OpenConnection() As Boolean

    <OperationContract()> _
    Function ExecuteCommand(ByVal SQLStatement As String) As Boolean

    <OperationContract()> _
    Function GetDataSet(ByVal lstrSQLStatement As String, Optional ByVal lstrDataSetName As String = "DATA") As DataSet

    <OperationContract()> _
    Sub setConnectionString(ByVal ConnectionString As String)

    <OperationContract()> _
    Sub setApplicationName(ByVal ApplicationName As String)

    <OperationContract()> _
    Sub setReturnIdentity(ByVal ReturnIdentity As Boolean)

    <OperationContract()> _
    Function getIdentityValue() As Integer

    <OperationContract()> _
    Sub Finalize()



End Interface


<DataContract()> _
Public Class ConnectionManager
    Implements IDataManager

    Private strConnectionString As String = ""                  'Cadena de Conexión
    Private strApplicationName As String = "ConnectionManager"  'Nombre aplicación que se conectará a SQL
    Private adodbMain As SqlConnection = Nothing                'Objeto de Conexión
    Private idbTransaccion As IDbTransaction = Nothing          'Objeto para manejar transacciones de base de datos
    Private sdrActiveRecordset As SqlDataReader = Nothing       'Contiene el objeto donde se almacenan tablas, renglones y columnas
    Private blnReturnIdentity As Boolean = False                'Indica si al ejecutar un comando de inserción se debe regresar el nuevo id = @@identity
    Private intIdentityValue As Integer = 0                     'Valor a regresar cuando se ejecuta un comando de inserción id = @@identity

    'Propiedad con la cadena de Conexión
    Public Sub setConnectionString(ByVal ConnectionString As String) Implements IDataManager.setConnectionString
        strConnectionString = ConnectionString
    End Sub

    'Propiedad con la cadena de Conexión
    Public Sub setApplicationName(ByVal ApplicationName As String) Implements IDataManager.setApplicationName
        strApplicationName = ApplicationName
    End Sub

    'Propiedad Indica si al ejecutar un comando de inserción se debe regresar el nuevo id = @@identity
    Public Sub setReturnIdentity(ByVal ReturnIdentity As Boolean) Implements IDataManager.setReturnIdentity
        blnReturnIdentity = ReturnIdentity
    End Sub

    'Propiedad Valor a regresar cuando se ejecuta un comando de inserción id = @@identity
    Public Function getIdentityValue() As Integer Implements IDataManager.getIdentityValue
        Return intIdentityValue
    End Function

    'Propiedad con el objeto de conexión
    <DataMember()> _
    Public Property ActiveConnection As SqlConnection
        Get
            Return adodbMain
        End Get
        Set(ladodbMain As SqlConnection)
            adodbMain = ladodbMain
        End Set
    End Property

    'Método para abrir la base de datos
    Public Function OpenConnection() As Boolean Implements IDataManager.OpenConnection
        Try
            Dim lstrError As String = ""
            OpenConnection = False
            If strConnectionString.Trim.Length = 0 Then Throw New Exception("ConnectionManager.OpenConnection : " & vbCrLf & "La cadena de conexión no puede ir vacía.")
            adodbMain = New SqlConnection
            With adodbMain
                .ConnectionString = strConnectionString
                .Open()
            End With
            Return True
        Catch ex As Exception
            Throw New Exception("ConnectionManager.OpenConnection : " & vbCrLf & ex.Message)
        End Try
    End Function

    'Método que indica si está abierta la conexión a la base de datos
    Public Function IsOpen() As Boolean
        strApplicationName = IIf(strApplicationName = "", "ConnectionManager", strApplicationName)
        Try
            IsOpen = False
            If adodbMain Is Nothing Then
                IsOpen = False
            Else
                IsOpen = CBool(IIf(adodbMain.State = ConnectionState.Open, True, False))
            End If

            'Verifica si el status de la base de datos está abierto
            Return IsOpen
        Catch ex As Exception
            Throw New Exception(strApplicationName + ".IsOpen" + vbCrLf + ex.Message + vbCrLf + ex.StackTrace)
        End Try
    End Function

    'Ejecuta un sentencia SQL
    Public Function ExecuteCommand(ByVal lstrSQLStatement As String) As Boolean Implements IDataManager.ExecuteCommand
        Dim lscCommand As SqlCommand = Nothing
        Try
            If lstrSQLStatement.Trim = "" Then Throw New Exception("La sentencia SQL no puede ir vacía.")
            If Not IsOpen() Then OpenConnection()
            idbTransaccion = adodbMain.BeginTransaction

            If Not sdrActiveRecordset Is Nothing Then
                If Not sdrActiveRecordset.IsClosed Then sdrActiveRecordset.Close()
            End If
            If blnReturnIdentity Then
                lstrSQLStatement = "SET NOCOUNT ON" & vbCrLf & lstrSQLStatement & vbCrLf & "SET NOCOUNT OFF" & vbCrLf & "SELECT @@Identity"
            End If
            lscCommand = adodbMain.CreateCommand
            With lscCommand
                .CommandTimeout = 60
                .Connection = adodbMain
                .CommandText = lstrSQLStatement
                .CommandType = CommandType.Text
                If Not idbTransaccion Is Nothing Then .Transaction = idbTransaccion
                sdrActiveRecordset = lscCommand.ExecuteReader
            End With
            If sdrActiveRecordset.HasRows Then
                sdrActiveRecordset.Read()
                If blnReturnIdentity Then intIdentityValue = CInt("0" & sdrActiveRecordset.Item(0).ToString)
                sdrActiveRecordset.Close()
            End If
            idbTransaccion.Commit()
            Return True ' Si se logra llegar a este punto la ejecución es exitosa
        Catch ex As Exception
            intIdentityValue = 0
            If Not idbTransaccion Is Nothing Then idbTransaccion.Rollback()
            Throw New Exception(strApplicationName + ".ExecuteCommand : " + vbCrLf + ex.Message + vbCrLf + ex.StackTrace)
        Finally
            If Not lscCommand Is Nothing Then lscCommand.Dispose()
            lscCommand = Nothing
            If Not idbTransaccion Is Nothing Then idbTransaccion.Dispose()
            If Not sdrActiveRecordset Is Nothing Then sdrActiveRecordset.Close()
            sdrActiveRecordset = Nothing
        End Try
    End Function


    'Método que devuelve un Dataset
    Public Function GetDataSet(ByVal lstrSQLStatement As String, Optional ByVal lstrDataSetName As String = "DATA") As DataSet Implements IDataManager.GetDataSet
        Dim lsdaAdapter As SqlDataAdapter = Nothing
        Dim lscCommand As SqlCommand = Nothing
        Dim ldsDataSet As DataSet = Nothing
        Try
            Dim lstrError As String = ""
            strApplicationName = "Connector"

            'Verifica si la conexión está abierta
            If Not IsOpen() Then OpenConnection()
            lscCommand = New SqlCommand()
            With lscCommand
                .CommandTimeout = 100
                .Connection = adodbMain
                .CommandText = lstrSQLStatement
                .CommandType = CommandType.Text
            End With
            lsdaAdapter = New SqlDataAdapter(lscCommand)
            ldsDataSet = New DataSet
            lsdaAdapter.FillLoadOption = LoadOption.Upsert
            lsdaAdapter.Fill(ldsDataSet)
            If ldsDataSet.Tables.Count > 0 Then
                ldsDataSet.Tables(0).TableName = ldsDataSet.Tables(0).TableName.ToUpper
                Dim lcColumn As DataColumn = Nothing
                For Each lcColumn In ldsDataSet.Tables(0).Columns
                    lcColumn.ColumnName = lcColumn.ColumnName.ToUpper
                Next lcColumn
            End If
            Return ldsDataSet
        Catch ex As Exception
            Throw New Exception(strApplicationName + ".GetDataSet()" + vbCrLf + ex.Message + vbCrLf + ex.StackTrace)
        Finally
            If Not lscCommand Is Nothing Then lscCommand.Dispose()
            lscCommand = Nothing
        End Try
    End Function








    'Evento que se dispara cuando se finaliza el objeto
    Protected Overrides Sub Finalize() Implements IDataManager.Finalize
        Try
            If Not adodbMain Is Nothing Then
                If Not adodbMain.State = ConnectionState.Closed Then
                    adodbMain.Close()
                End If
                SqlConnection.ClearPool(adodbMain)
                adodbMain.Dispose()
            End If
            If Not idbTransaccion Is Nothing Then idbTransaccion.Dispose()
            idbTransaccion = Nothing
            adodbMain = Nothing
            MyBase.Finalize()
        Catch ex As Exception
            Throw New Exception("ConnectionManager.OpenConnection : " & vbCrLf & ex.Message)
        End Try
    End Sub

End Class


