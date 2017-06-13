using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoginDevExpress
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtUsuario.Properties.NullText = "Usuario";
            txtPassword.Properties.NullText = "Contraseña";
            
        }

        private void textEdit2_EditValueChanged(object sender, EventArgs e)
        {
            txtPassword.Properties.PasswordChar = '*';
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                MessageHeader header;
                OperationContextScope scope;

                String userToken = String.Empty;


                LoginWebService.LoginClient login = new LoginWebService.LoginClient();


                userToken = login.UsuarioLogin(txtUsuario.Text, txtPassword.Text);

                scope = new OperationContextScope(login.InnerChannel);

                header = MessageHeader.CreateHeader("TokenHeader", "TokenNameSpace", userToken);

                OperationContext.Current.OutgoingMessageHeaders.Add(header);

                DatosLibrary.Usuario_ usuario = new DatosLibrary.Usuario_();

                usuario = login.GetUsuarioData(txtUsuario.Text, txtPassword.Text);

                if (usuario != null && usuario.IdUsuario > 0)
                    MessageBox.Show("Usuario logueado con éxito");
                else
                    MessageBox.Show("Usuario incorrecto");
            }
            catch(System.ServiceModel.FaultException es)
            {

            }

        }
    }
}
