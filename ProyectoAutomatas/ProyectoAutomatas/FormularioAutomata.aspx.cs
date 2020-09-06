using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoAutomatas
{
    public partial class FormularioAutomata : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnEvaluaCadena_Click(object sender, EventArgs e)
        {
            String Cadena = "";
            Int64 largo = 0;
            String[] estados;
            Cadena = TxtCadena.Text.Trim();
            largo = Cadena.Length;

            for (int i = 0; i<= largo; i++) { 
                    
            }



       
            char[] charArr = Cadena.ToCharArray();
            foreach (char ch in charArr)
            {
                Console.WriteLine(ch);
            }

            for 


        }
    }
}