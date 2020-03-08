using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Xml;

namespace RefinanciamientoKon
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
    // NOTE: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.cs en el Explorador de soluciones e inicie la depuración.

    [ServiceContract]
    public interface IService
    {

        //[OperationContract]
        //string GetClientData(string XMLData);


        [OperationContract(Name= "GetClientData")]
        [WebInvoke(Method ="POST", UriTemplate= "GetClientData")]
        String GetClientData(Stream xmlData);



        [OperationContract(Name = "GetCreditType")]
        [WebInvoke(Method = "POST", UriTemplate = "GetCreditType")]
        String GetCreditType(Stream xmlData);


        [OperationContract(Name = "GetRefData")]
        [WebInvoke(Method = "POST", UriTemplate = "GetRefData")]
        String GetRefData(Stream xmlData);

        [OperationContract(Name = "GetNewDap")]
        [WebInvoke(Method = "POST", UriTemplate = "GetNewDap")]
        String GetNewDap(Stream xmlData);


        // TODO: agregue aquí sus operaciones de servicio
    }
    


    [AspNetCompatibilityRequirements(
        RequirementsMode=AspNetCompatibilityRequirementsMode.Required)]
    public class DataReciever : IService
    {


        public string GetClientData(Stream xmlData)
        {

            String ResponseData = "";
            StreamReader lsrReaderXML = new StreamReader(xmlData);
            string lstrDataXML = lsrReaderXML.ReadToEnd();
          //  ConnectionBroker.DataManagerClient lobjDisbursement = null/* TODO Change to default(_) if this is not a reference type */;
            int lintLogId = 0;

            try
            {
                lintLogId = InitDispatch("GetClientData", lstrDataXML);
                // Se inicia la transacción de registro
                if (lintLogId == 0)
                    return "<ROOT><TRANSCODE>0</TRANSCODE><DESCRIPTION>No se pudo generar token de transacción.</DESCRIPTION></ROOT>";


                XmlDocument lxmlDocument = null/* TODO Change to default(_) if this is not a reference type */;
                XmlNode lxmlNode = null/* TODO Change to default(_) if this is not a reference type */;
               // string lstrSQL = "";
                string lstrError = "";
              


                if (lstrDataXML.Length == 0)
                    throw new Exception("El documento XML no puede ir vacío.");
                lxmlDocument = new XmlDocument();
                lxmlDocument.LoadXml(lstrDataXML);


                lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("RFC");

                if (lxmlNode == null)
                    lstrError = "El nodo de RFC (RFC) no está contenido en el XML." + Environment.NewLine;

                lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Distribuidor");
                if (lxmlNode == null)
                    lstrError += "El nodo de Distribuidor (Distribuidor) no está contenido en el XML." + Environment.NewLine;

                lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Sucursal");
                if (lxmlNode == null)
                    lstrError += "El nodo de Sucursal (Sucursal) no está contenido en el XML." + Environment.NewLine;


                // valida Cantidad Errores de Nodos 

                if (lstrError.Length > 0)
                    throw new Exception(lstrError);



                if (lxmlDocument.DocumentElement.SelectSingleNode("RFC").InnerText.Length == 0)
                    lstrError = "El Número de RFC es un dato requerido y no puede ir vacío." + Environment.NewLine;
                
                if (lxmlDocument.DocumentElement.SelectSingleNode("Distribuidor").InnerText.Length == 0)
                    lstrError += "El Distribuidor es un dato requerido y no puede ir vacío." + Environment.NewLine;

                if (lxmlDocument.DocumentElement.SelectSingleNode("Sucursal").InnerText.Length == 0)
                    lstrError += "La Sucursal es un dato requerido y no puede ir vacío." + Environment.NewLine;


                // valida Cantidad Errores de Datos Vacios
                if (lstrError.Length > 0)
                    throw new Exception(lstrError);


                string RFC = lxmlDocument.DocumentElement.SelectSingleNode("RFC").InnerText.Trim().ToUpper();        // "HEVC860614SC6";
                string Sucursal = lxmlDocument.DocumentElement.SelectSingleNode("Sucursal").InnerText.Trim(); //"0079";
                string Distribuidor = lxmlDocument.DocumentElement.SelectSingleNode("Distribuidor").InnerText.Trim();  //"2029";


                string lstrSQL = "DXN_RCvistasolicitudcreditoCorp  '1', '" + RFC + "','3','', '', '" + Sucursal + "', '" + Distribuidor + "'";

               
               ResponseData = GetXMLForQuery(lstrSQL);
                          

                if (ResponseData != "<NewDataSet />")
                {
                    ResponseData = ResponseData.Replace("<NewDataSet>", "");
                    ResponseData = ResponseData.Replace("<Table>", " ");
                    ResponseData = ResponseData.Replace("</NewDataSet>", "");
                    ResponseData = ResponseData.Replace("</Table>", "");
                }
                else
                    ResponseData = "NO DATA";

                ResponseData = ResponseData.ToUpper();

                CloseDispatch(lintLogId, ResponseData);
                return "<ROOT><TRANSCODE>" + lintLogId + "</TRANSCODE><XMLRESPONSE>" + ResponseData + "</XMLRESPONSE></ROOT>";
            }
            catch (Exception ex)
            {
                string ResponseError= "<ROOT><TRANSCODE>0</TRANSCODE><DESCRIPTION>" + ex.Message.ToString().Replace("\"", "").Replace("<", "").Replace(">", "") + "</DESCRIPTION></ROOT>";
                if (lintLogId != 0)
                {
                    if (ResponseError.Length > 79000)
                    {
                        CloseDispatch(lintLogId, ResponseError.Substring(0, 7900));
                    }
                    else
                    {
                        CloseDispatch(lintLogId, ResponseError);
                    }
                }
                 TrackError("GetClientData" + Environment.NewLine + Environment.NewLine + ex.Message.ToString().Replace("\"", "").Replace("<", "").Replace(">", "") + Environment.NewLine + "Id_Dispatch :" + lintLogId + Environment.NewLine + Environment.NewLine + lstrDataXML + "",97);
                return ResponseError;
            }
            finally
            {
                //if (lobjDisbursement != null) { lobjDisbursement = null; }
            }
        }



        public string GetCreditType(Stream xmlData)
        {

            String ResponseData = "";
            StreamReader lsrReaderXML = new StreamReader(xmlData);
            string lstrDataXML = lsrReaderXML.ReadToEnd();
            //  ConnectionBroker.DataManagerClient lobjDisbursement = null/* TODO Change to default(_) if this is not a reference type */;
            int lintLogId = 0;

            try
            {
                lintLogId = InitDispatch("GetCreditType", lstrDataXML);
                // Se inicia la transacción de registro
                if (lintLogId == 0)
                    return "<ROOT><TRANSCODE>0</TRANSCODE><DESCRIPTION>No se pudo generar token de transacción.</DESCRIPTION></ROOT>";


                XmlDocument lxmlDocument = null/* TODO Change to default(_) if this is not a reference type */;
                XmlNode lxmlNode = null/* TODO Change to default(_) if this is not a reference type */;
                // string lstrSQL = "";
                string lstrError = "";



                if (lstrDataXML.Length == 0)
                    throw new Exception("El documento XML no puede ir vacío.");
                lxmlDocument = new XmlDocument();
                lxmlDocument.LoadXml(lstrDataXML);

                
                lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Distribuidor");
                if (lxmlNode == null)
                    lstrError += "El nodo de Distribuidor (Distribuidor) no está contenido en el XML." + Environment.NewLine;

                lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Sucursal");
                if (lxmlNode == null)
                    lstrError += "El nodo de Sucursal (Sucursal) no está contenido en el XML." + Environment.NewLine;


                // valida Cantidad Errores de Nodos 

                if (lstrError.Length > 0)
                    throw new Exception(lstrError);

                
                if (lxmlDocument.DocumentElement.SelectSingleNode("Distribuidor").InnerText.Length == 0)
                    lstrError += "El Distribuidor es un dato requerido y no puede ir vacío." + Environment.NewLine;

                if (lxmlDocument.DocumentElement.SelectSingleNode("Sucursal").InnerText.Length == 0)
                    lstrError += "La Sucursal es un dato requerido y no puede ir vacío." + Environment.NewLine;


                // valida Cantidad Errores de Datos Vacios
                if (lstrError.Length > 0)
                    throw new Exception(lstrError);


                string Sucursal = lxmlDocument.DocumentElement.SelectSingleNode("Sucursal").InnerText.Trim(); //"0079";
                string Distribuidor = lxmlDocument.DocumentElement.SelectSingleNode("Distribuidor").InnerText.Trim();  //"2029";

                
                string lstrSQL = "DXN_Refinanciamientos '6' , '', '" + Distribuidor + "', '" + Sucursal + "'";

                ResponseData = GetXMLForQuery(lstrSQL);
                  

                if (ResponseData != "<NewDataSet />")
                {
                    ResponseData = ResponseData.Replace("<NewDataSet>", "");
                    ResponseData = ResponseData.Replace("<Table>", " ");
                    ResponseData = ResponseData.Replace("</NewDataSet>", "");
                    ResponseData = ResponseData.Replace("</Table>", "");
                }
                else
                    ResponseData = "NO DATA";

                ResponseData = ResponseData.ToUpper();

                CloseDispatch(lintLogId, ResponseData);
                return "<ROOT><TRANSCODE>" + lintLogId + "</TRANSCODE><XMLRESPONSE>" + ResponseData + "</XMLRESPONSE></ROOT>";
            }
            catch (Exception ex)
            {
                string ResponseError = "<ROOT><TRANSCODE>0</TRANSCODE><DESCRIPTION>" + ex.Message.ToString().Replace("\"", "").Replace("<", "").Replace(">", "") + "</DESCRIPTION></ROOT>";
                if (lintLogId != 0)
                {
                    if (ResponseError.Length > 79000)
                    {
                        CloseDispatch(lintLogId, ResponseError.Substring(0, 7900));
                    }
                    else
                    {
                        CloseDispatch(lintLogId, ResponseError);
                    }
                }
                TrackError("GetCreditType" + Environment.NewLine + Environment.NewLine + ex.Message.ToString().Replace("\"", "").Replace("<", "").Replace(">", "") + Environment.NewLine + "Id_Dispatch :" + lintLogId + Environment.NewLine + Environment.NewLine + lstrDataXML + "", 97);
                return ResponseError;
            }
            finally
            {
                //if (lobjDisbursement != null) { lobjDisbursement = null; }
            }
        }



        public string GetRefData(Stream xmlData)
        {

            String ResponseData = "";
            String ResultOperaciones = "";
            String ResultRefData = "";
            StreamReader lsrReaderXML = new StreamReader(xmlData);
            string lstrDataXML = lsrReaderXML.ReadToEnd();
            //  ConnectionBroker.DataManagerClient lobjDisbursement = null/* TODO Change to default(_) if this is not a reference type */;
            int lintLogId = 0;

            try
            {
                lintLogId = InitDispatch("GetCreditType", lstrDataXML);
                // Se inicia la transacción de registro
                if (lintLogId == 0)
                    return "<ROOT><TRANSCODE>0</TRANSCODE><DESCRIPTION>No se pudo generar token de transacción.</DESCRIPTION></ROOT>";


                XmlDocument lxmlDocument = null/* TODO Change to default(_) if this is not a reference type */;
                XmlNode lxmlNode = null/* TODO Change to default(_) if this is not a reference type */;
                // string lstrSQL = "";
                string lstrError = "";



                if (lstrDataXML.Length == 0)
                    throw new Exception("El documento XML no puede ir vacío.");
                lxmlDocument = new XmlDocument();
                lxmlDocument.LoadXml(lstrDataXML);


                lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("RFC");
                if (lxmlNode == null)
                    lstrError = "El nodo de RFC (RFC) no está contenido en el XML." + Environment.NewLine;


                lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Distribuidor");
                if (lxmlNode == null)
                    lstrError += "El nodo de Distribuidor (Distribuidor) no está contenido en el XML." + Environment.NewLine;

                lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Sucursal");
                if (lxmlNode == null)
                    lstrError += "El nodo de Sucursal (Sucursal) no está contenido en el XML." + Environment.NewLine;


                // valida Cantidad Errores de Nodos 

                if (lstrError.Length > 0)
                    throw new Exception(lstrError);

                if (lxmlDocument.DocumentElement.SelectSingleNode("RFC").InnerText.Length == 0)
                    lstrError = "El Número de RFC es un dato requerido y no puede ir vacío." + Environment.NewLine;

                if (lxmlDocument.DocumentElement.SelectSingleNode("Distribuidor").InnerText.Length == 0)
                    lstrError += "El Distribuidor es un dato requerido y no puede ir vacío." + Environment.NewLine;

                if (lxmlDocument.DocumentElement.SelectSingleNode("Sucursal").InnerText.Length == 0)
                    lstrError += "La Sucursal es un dato requerido y no puede ir vacío." + Environment.NewLine;


                // valida Cantidad Errores de Datos Vacios
                if (lstrError.Length > 0)
                    throw new Exception(lstrError);

                string RFC = lxmlDocument.DocumentElement.SelectSingleNode("RFC").InnerText.Trim().ToUpper();// "HEVC860614SC6";
                string Sucursal = lxmlDocument.DocumentElement.SelectSingleNode("Sucursal").InnerText.Trim(); //"0079";
                string Distribuidor = lxmlDocument.DocumentElement.SelectSingleNode("Distribuidor").InnerText.Trim();  //"2029";


                //GetOperations

                string lstrSQLOperations = "EXEC RefConstante_WSKON 1,'" + RFC + "','" + Distribuidor + "','" + Sucursal + "',''";

                ResultOperaciones = GetXMLForQuery(lstrSQLOperations);

                //GetRefData

                string lstrSQLRefData = "EXEC RefConstante_WSKON 2,'" + RFC + "','" + Distribuidor + "','" + Sucursal + "',''";

                ResultRefData = GetXMLForQuery(lstrSQLRefData);


                //Tratamiento de De datos  y cadena XML


                if (ResultOperaciones != "<NewDataSet />")
                {
                    ResultOperaciones = ResultOperaciones.Replace("<NewDataSet>", "");
                    ResultOperaciones = ResultOperaciones.Replace("<Table>", "<ResultOperaciones>");
                    ResultOperaciones = ResultOperaciones.Replace("</NewDataSet>", "");
                    ResultOperaciones = ResultOperaciones.Replace("</Table>", "</ResultOperaciones>");

                }
                else
                    ResultOperaciones = "<ResultOperaciones></ResultOperaciones>";



                if (ResultRefData != "<NewDataSet />")
                {
                    ResultRefData = ResultRefData.Replace("<NewDataSet>", "");
                    ResultRefData = ResultRefData.Replace("<Table>", "<ResultRefData>");
                    ResultRefData = ResultRefData.Replace("</NewDataSet>", "");
                    ResultRefData = ResultRefData.Replace("</Table>", "</ResultRefData>");

                }
                else
                    ResultRefData = "<ResultRefData></ResultRefData>";



                ResponseData = ResultOperaciones + ResultRefData;

                ResponseData = ResponseData.ToUpper();
                int datalength=ResponseData.Length;
                CloseDispatch(lintLogId, ResponseData);
                return "<ROOT><TRANSCODE>" + lintLogId + "</TRANSCODE><XMLRESPONSE>" + ResponseData + "</XMLRESPONSE></ROOT>";
            }
            catch (Exception ex)
            {
                string ResponseError = "<ROOT><TRANSCODE>0</TRANSCODE><DESCRIPTION>" + ex.Message.ToString().Replace("\"", "").Replace("<", "").Replace(">", "") + "</DESCRIPTION></ROOT>";
                if (lintLogId != 0)
                {
                    if (ResponseError.Length > 79000)
                    {
                        CloseDispatch(lintLogId, ResponseError.Substring(0, 7900));
                    }
                    else
                    {
                        CloseDispatch(lintLogId, ResponseError);
                    }
                }
                TrackError("GetCreditType" + Environment.NewLine + Environment.NewLine + ex.Message.ToString().Replace("\"", "").Replace("<", "").Replace(">", "") + Environment.NewLine + "Id_Dispatch :" + lintLogId + Environment.NewLine + Environment.NewLine + lstrDataXML + "", 97);
                return ResponseError;
            }
            finally
            {
                //if (lobjDisbursement != null) { lobjDisbursement = null; }
            }
        }





        public string GetNewDap(Stream xmlData)
        {

            String ResponseData = "";
            StreamReader lsrReaderXML = new StreamReader(xmlData);
            string lstrDataXML = lsrReaderXML.ReadToEnd();
           
            int lintLogId = 0;

            try
            {
                lintLogId = InitDispatch("GetNewDap", lstrDataXML);
                // Se inicia la transacción de registro
                if (lintLogId == 0)
                    return "<ROOT><TRANSCODE>0</TRANSCODE><DESCRIPTION>No se pudo generar token de transacción.</DESCRIPTION></ROOT>";


                XmlDocument lxmlDocument = null;
                XmlNode lxmlNode = null;
                
                string lstrError = "";



                if (lstrDataXML.Length == 0)
                    throw new Exception("El documento XML no puede ir vacío.");
                lxmlDocument = new XmlDocument();
                lxmlDocument.LoadXml(lstrDataXML);


                lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("RFC");

                if (lxmlNode == null)
                    lstrError = "El nodo de RFC (RFC) no está contenido en el XML." + Environment.NewLine;

                lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Distribuidor");
                if (lxmlNode == null)
                    lstrError += "El nodo de Distribuidor (Distribuidor) no está contenido en el XML." + Environment.NewLine;

                lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Sucursal");
                if (lxmlNode == null)
                    lstrError += "El nodo de Sucursal (Sucursal) no está contenido en el XML." + Environment.NewLine;

                lxmlNode = lxmlDocument.DocumentElement.SelectSingleNode("Promocion");
                if (lxmlNode == null)
                    lstrError += "El nodo de Sucursal (Promocion) no está contenido en el XML." + Environment.NewLine;

                // valida Cantidad Errores de Nodos 

                if (lstrError.Length > 0)
                    throw new Exception(lstrError);



                if (lxmlDocument.DocumentElement.SelectSingleNode("RFC").InnerText.Length == 0)
                    lstrError = "El Número de RFC es un dato requerido y no puede ir vacío." + Environment.NewLine;

                if (lxmlDocument.DocumentElement.SelectSingleNode("Distribuidor").InnerText.Length == 0)
                    lstrError += "El Distribuidor es un dato requerido y no puede ir vacío." + Environment.NewLine;

                if (lxmlDocument.DocumentElement.SelectSingleNode("Sucursal").InnerText.Length == 0)
                    lstrError += "La Sucursal es un dato requerido y no puede ir vacío." + Environment.NewLine;

                if (lxmlDocument.DocumentElement.SelectSingleNode("Promocion").InnerText.Length == 0)
                    lstrError += "La Promocion es un dato requerido y no puede ir vacío." + Environment.NewLine;



                // valida Cantidad Errores de Datos Vacios
                if (lstrError.Length > 0)
                    throw new Exception(lstrError);


                string RFC = lxmlDocument.DocumentElement.SelectSingleNode("RFC").InnerText.Trim().ToUpper();        // "HEVC860614SC6";
                string Sucursal = lxmlDocument.DocumentElement.SelectSingleNode("Sucursal").InnerText.Trim(); //"0079";
                string Distribuidor = lxmlDocument.DocumentElement.SelectSingleNode("Distribuidor").InnerText.Trim();  //"2029";
                string Promocion = lxmlDocument.DocumentElement.SelectSingleNode("Promocion").InnerText.Trim();  //"2029";


                //string lstrSQL = "DXN_RCvistasolicitudcreditoCorp  '1', '" + RFC + "','3','', '', '" + Sucursal + "', '" + Distribuidor + "'";
                //string lstrSQL = "EXEC RefConstante_WSKON 3,'AAMJ480802H61','2029','0108','60AG'";
                string lstrSQL = "EXEC RefConstante_WSKON 3,'" + RFC + "','" + Distribuidor + "','" + Sucursal + "','" + Promocion + "'";


                ResponseData = GetXMLForQuery(lstrSQL);


                if (ResponseData != "<NewDataSet />")
                {
                    ResponseData = ResponseData.Replace("<NewDataSet>", "");
                    ResponseData = ResponseData.Replace("<Table>", " ");
                    ResponseData = ResponseData.Replace("<Column1>", "");
                    ResponseData = ResponseData.Replace("<Operacion>", "<NewDap>");
                    ResponseData = ResponseData.Replace("</NewDataSet>", "");
                    ResponseData = ResponseData.Replace("</Table>", "");
                    ResponseData = ResponseData.Replace("</Column1>", "");
                    ResponseData = ResponseData.Replace("</Operacion>", "</NewDap>");
                }
                else
                {
                    ResponseData = "<NewDap>NO DATA</NewDap>";
                }
                
                    ResponseData = ResponseData.ToUpper();


                if (ResponseData == "SIN INFORMACION")
                {

                    ResponseData = "<NewDap>NO DATA</NewDap>";
                }

                CloseDispatch(lintLogId, ResponseData);
                return "<ROOT><TRANSCODE>" + lintLogId + "</TRANSCODE><XMLRESPONSE>" + ResponseData + "</XMLRESPONSE></ROOT>";
            }
            catch (Exception ex)
            {
                string ResponseError = "<ROOT><TRANSCODE>0</TRANSCODE><DESCRIPTION>" + ex.Message.ToString().Replace("\"", "").Replace("<", "").Replace(">", "") + "</DESCRIPTION></ROOT>";
                if (lintLogId != 0)
                {
                    if (ResponseError.Length > 79000)
                    {
                        CloseDispatch(lintLogId, ResponseError.Substring(0, 7900));
                        TrackError("GetNewDap" + Environment.NewLine + Environment.NewLine + ex.Message.ToString().Replace("\"", "").Replace("<", "").Replace(">", "") + Environment.NewLine + "Id_Dispatch :" + lintLogId + Environment.NewLine + Environment.NewLine + lstrDataXML + "", 97);
                        return ResponseError;
                    }
                    else
                    {
                        CloseDispatch(lintLogId, ResponseError);
                        TrackError("GetNewDap" + Environment.NewLine + Environment.NewLine + ex.Message.ToString().Replace("\"", "").Replace("<", "").Replace(">", "") + Environment.NewLine + "Id_Dispatch :" + lintLogId + Environment.NewLine + Environment.NewLine + lstrDataXML + "", 97);
                        return ResponseError;
                    }
                }
                TrackError("GetNewDap" + Environment.NewLine + Environment.NewLine + ex.Message.ToString().Replace("\"", "").Replace("<", "").Replace(">", "") + Environment.NewLine + "Id_Dispatch :" + lintLogId + Environment.NewLine + Environment.NewLine + lstrDataXML + "", 97);
                return ResponseError;
            }
          
        }





        private string GetXMLForQuery(string Query)
        {
            string ResponseData = "";
            try
            {

                using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MainConnection"].ConnectionString))
                {

                    connection.Open();
                    using (SqlCommand CMD = new SqlCommand(Query, connection))
                    {
                        CMD.CommandType = CommandType.Text;
                        using (SqlDataAdapter sqlAdp = new SqlDataAdapter(CMD))
                        {
                            sqlAdp.SelectCommand.CommandTimeout = 300;
                            using (DataSet dtws = new DataSet())
                            {
                                sqlAdp.Fill(dtws);
                                connection.Close();
                                ResponseData = dtws.GetXml();
                            }
                        }


                    }

                }

                return ResponseData;

            }catch(Exception EX)
            {
                throw new Exception(EX.ToString() + " " + EX.StackTrace.ToString());
            }
        }


        private int InitDispatch(string ServiceName, string xmlData)
        {
            ConnectionBroker.DataManagerClient lobjDispatch = null/* TODO Change to default(_) if this is not a reference type */;
            string lstrSQL = "";
            lobjDispatch = new ConnectionBroker.DataManagerClient();
            try
            {
                int lintIdServices = 0;

                lobjDispatch.Open();
                lobjDispatch.setConnectionString(System.Configuration.ConfigurationManager.ConnectionStrings["MainConnection"].ConnectionString);
                lobjDispatch.setReturnIdentity(true);

                lstrSQL = "INSERT INTO WCF_REFI_Log (Service_Name, XML_Data) VALUES ('" + ServiceName + "','" + xmlData.Replace("'", "''") + "')";
                lobjDispatch.ExecuteCommand(lstrSQL);
                lintIdServices = lobjDispatch.getIdentityValue();
                lobjDispatch.Finalize();
                lobjDispatch.Close();
                return lintIdServices;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (lobjDispatch != null){ lobjDispatch = null; }
                    /* TODO Change to default(_) if this is not a reference type */;
            }
        }



        private void CloseDispatch(int intIdDispatch,string ResponseData)
        {
            ConnectionBroker.DataManagerClient lobjDispatch = null/* TODO Change to default(_) if this is not a reference type */;

            string lstrSQL = "";

            try
            {
                int lintDispatchId = 0;

                lobjDispatch = new ConnectionBroker.DataManagerClient();
                lobjDispatch.Open();
                lobjDispatch.setConnectionString(System.Configuration.ConfigurationManager.ConnectionStrings["MainConnection"].ConnectionString);
                lobjDispatch.setReturnIdentity(true);
                lstrSQL = "UPDATE WCF_REFI_Log  SET Process_Status = 1, Response_Date = GETDATE(), Response_Data= '" + ResponseData  + "' WHERE Id_Services = " + intIdDispatch;
                lobjDispatch.ExecuteCommand(lstrSQL);
                lintDispatchId = lobjDispatch.getIdentityValue();
                lobjDispatch.Finalize();
                lobjDispatch.Close();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (lobjDispatch != null) { lobjDispatch = null; }
            }
        }




        private void TrackError(string strEvent,int codeError)
        {
            EventLog.WriteEntry("EventCreate", strEvent, EventLogEntryType.Error, codeError);
        }

      
    }
}
