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
            List<String> estados = new List<String>();
            Cadena = TxtCadena.Text.Trim();
            char[] charArr = Cadena.ToCharArray();
            largo = Cadena.Length;

            for (int i = 0; i <= largo; i++) {
                estados.Add("q" + i.ToString());
            }

            String[] estadoArray = estados.ToArray();
            String concatenacion = "";
            TxtResultado.Text = "";

            for (int estadoRecorre = 0; estadoRecorre < estadoArray.Length; estadoRecorre++) {
                concatenacion = concatenacion + estadoArray[estadoRecorre] + ",";




                //for (int evalua = 0; evalua <= 1; evalua++)
                //{
                //    if (estadoArray[estadoRecorre] == evalua.ToString()) { 

                //    }
                //}

            }

            String ResultadoCadena = "";

            for (int aceptacion = 0; aceptacion < charArr.Length; aceptacion++) {
                for (int evalua = 0; evalua <= 1; evalua++)
                {
                    if (charArr[aceptacion].ToString() == evalua.ToString())
                    {
                        ResultadoCadena =  ResultadoCadena + "δ(" + estadoArray[aceptacion].ToString() + "," + evalua.ToString() + ") = " + estadoArray[ aceptacion + 1] + "\n";
                    }
                    else {
                        if (aceptacion == 0 || aceptacion == 1)
                        {
                            ResultadoCadena = ResultadoCadena + "δ(" + estadoArray[aceptacion].ToString() + "," + evalua.ToString() + ") = " + estadoArray[aceptacion] + "\n";
                        }
                        else {
                            ResultadoCadena = ResultadoCadena + "δ(" + estadoArray[aceptacion].ToString() + "," + evalua.ToString() + ") = " + estadoArray[0] + "\n";
                        }
                    }
                }
            }

            ResultadoCadena = ResultadoCadena + "δ(" + estadoArray[charArr.Length].ToString() + ",0) = " + estadoArray[charArr.Length] + "\n";
            ResultadoCadena = ResultadoCadena + "δ(" + estadoArray[charArr.Length].ToString() + ",1) = " + estadoArray[charArr.Length] + "\n";
            TxtCadenas.Text = ResultadoCadena;

            //Estados del Automata
            TxtResultado.Text = concatenacion.Substring(0, concatenacion.Length - 1); ;

            
            foreach (char ch in charArr)
            {
                Console.WriteLine(ch);
            }



        }
    }
}