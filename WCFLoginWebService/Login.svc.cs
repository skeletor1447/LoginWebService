using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WCFLoginWebService
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
    // NOTE: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.cs en el Explorador de soluciones e inicie la depuración.

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class Service1 : ILogin
    {

        private String UserToken = String.Empty;

        public DatosLibrary.Usuario_ GetUsuarioData(string usuario1,string password1)
        {
            
                dbcabEntities db = new dbcabEntities();

                DatosLibrary.Usuario_ usuario = new DatosLibrary.Usuario_();

                try
                {
                    usuarios data = db.usuarios.Where(x => x.Usuario == usuario1 && x.Contrasenia == password1).Single();

                    usuario.IdUsuario = (int)data.IdUsuario;
                    usuario.Usuario = data.Usuario;


                    return usuario;
                }
                catch
                {
                    return null;
                }
            
        }
        
        public string UsuarioLogin(string usuario, string password)
        {
            dbcabEntities db = new dbcabEntities();

            try
            {
                db.usuarios.Where(x => x.Usuario == usuario).Single();

                UserToken = OperationContext.Current.SessionId;
            }
            catch
            {
                UserToken = OperationContext.Current.SessionId;
            }



            return UserToken;
        }

        public bool IsValidoUsuario()
        {
            //Getting the user token from client request
            if (OperationContext.Current.IncomingMessageHeaders.FindHeader("TokenHeader", "TokenNameSpace") == -1)
            {
                return false;
            }

            string userIdentityToken = Convert.ToString(OperationContext.Current.IncomingMessageHeaders.GetHeader<string>("TokenHeader", "TokenNameSpace"));

            //Authenticating user with token, if it is validated then returning employee data
            if (userIdentityToken == UserToken)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
