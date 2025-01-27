﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.42000
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Namespace ConnectionBroker
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ServiceModel.ServiceContractAttribute(ConfigurationName:="ConnectionBroker.IDataManager")>  _
    Public Interface IDataManager
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/IDataManager/OpenConnection", ReplyAction:="http://tempuri.org/IDataManager/OpenConnectionResponse")>  _
        Function OpenConnection() As Boolean
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/IDataManager/ExecuteCommand", ReplyAction:="http://tempuri.org/IDataManager/ExecuteCommandResponse")>  _
        Function ExecuteCommand(ByVal SQLStatement As String) As Boolean
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/IDataManager/GetDataSet", ReplyAction:="http://tempuri.org/IDataManager/GetDataSetResponse")>  _
        Function GetDataSet(ByVal lstrSQLStatement As String, ByVal lstrDataSetName As String) As System.Data.DataSet
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/IDataManager/setConnectionString", ReplyAction:="http://tempuri.org/IDataManager/setConnectionStringResponse")>  _
        Sub setConnectionString(ByVal ConnectionString As String)
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/IDataManager/setApplicationName", ReplyAction:="http://tempuri.org/IDataManager/setApplicationNameResponse")>  _
        Sub setApplicationName(ByVal ApplicationName As String)
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/IDataManager/setReturnIdentity", ReplyAction:="http://tempuri.org/IDataManager/setReturnIdentityResponse")>  _
        Sub setReturnIdentity(ByVal ReturnIdentity As Boolean)
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/IDataManager/getIdentityValue", ReplyAction:="http://tempuri.org/IDataManager/getIdentityValueResponse")>  _
        Function getIdentityValue() As Integer
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/IDataManager/Finalize", ReplyAction:="http://tempuri.org/IDataManager/FinalizeResponse")>  _
        Sub Finalize()
    End Interface
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")>  _
    Public Interface IDataManagerChannel
        Inherits ConnectionBroker.IDataManager, System.ServiceModel.IClientChannel
    End Interface
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")>  _
    Partial Public Class DataManagerClient
        Inherits System.ServiceModel.ClientBase(Of ConnectionBroker.IDataManager)
        Implements ConnectionBroker.IDataManager
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal endpointConfigurationName As String)
            MyBase.New(endpointConfigurationName)
        End Sub
        
        Public Sub New(ByVal endpointConfigurationName As String, ByVal remoteAddress As String)
            MyBase.New(endpointConfigurationName, remoteAddress)
        End Sub
        
        Public Sub New(ByVal endpointConfigurationName As String, ByVal remoteAddress As System.ServiceModel.EndpointAddress)
            MyBase.New(endpointConfigurationName, remoteAddress)
        End Sub
        
        Public Sub New(ByVal binding As System.ServiceModel.Channels.Binding, ByVal remoteAddress As System.ServiceModel.EndpointAddress)
            MyBase.New(binding, remoteAddress)
        End Sub
        
        Public Function OpenConnection() As Boolean Implements ConnectionBroker.IDataManager.OpenConnection
            Return MyBase.Channel.OpenConnection
        End Function
        
        Public Function ExecuteCommand(ByVal SQLStatement As String) As Boolean Implements ConnectionBroker.IDataManager.ExecuteCommand
            Return MyBase.Channel.ExecuteCommand(SQLStatement)
        End Function
        
        Public Function GetDataSet(ByVal lstrSQLStatement As String, ByVal lstrDataSetName As String) As System.Data.DataSet Implements ConnectionBroker.IDataManager.GetDataSet
            Return MyBase.Channel.GetDataSet(lstrSQLStatement, lstrDataSetName)
        End Function
        
        Public Sub setConnectionString(ByVal ConnectionString As String) Implements ConnectionBroker.IDataManager.setConnectionString
            MyBase.Channel.setConnectionString(ConnectionString)
        End Sub
        
        Public Sub setApplicationName(ByVal ApplicationName As String) Implements ConnectionBroker.IDataManager.setApplicationName
            MyBase.Channel.setApplicationName(ApplicationName)
        End Sub
        
        Public Sub setReturnIdentity(ByVal ReturnIdentity As Boolean) Implements ConnectionBroker.IDataManager.setReturnIdentity
            MyBase.Channel.setReturnIdentity(ReturnIdentity)
        End Sub
        
        Public Function getIdentityValue() As Integer Implements ConnectionBroker.IDataManager.getIdentityValue
            Return MyBase.Channel.getIdentityValue
        End Function
        
        Public Sub Finalize() Implements ConnectionBroker.IDataManager.Finalize
            MyBase.Channel.Finalize
        End Sub
    End Class
End Namespace
