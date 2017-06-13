using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WCFLoginWebService
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface ILogin
    {

        [OperationContract]
        DatosLibrary.Usuario_ GetUsuarioData(string usuario,string password);

        [OperationContract]
        String UsuarioLogin(String usuario,String password);

        
    }

    
}
