using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_de_Gestion.Modelo
{
    public class Mensajes
    {
        public static void MensajeError(Exception ex)
        {
            string titulo = "Error";
            string mensaje = $"Mensaje de error: {ex.Message}\n\n" +
                             $"Origen: {ex.Source}\n\n" +
                             $"Seguimiento de la pila:\n{ex.StackTrace}";

            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void MostrarMensaje(string _texto, string titulo, MessageBoxIcon _icono, MessageBoxButtons _botones)
        {
            MessageBox.Show(_texto, titulo, _botones, _icono);
        }

    }
}
