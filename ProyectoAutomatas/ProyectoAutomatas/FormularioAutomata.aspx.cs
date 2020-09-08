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
            String EstadosJson = "";
            String StringResult = "";

            TxtResultado.Text = "";
            EstadosJson = @"";

            for (int estadoRecorre = 0; estadoRecorre < estadoArray.Length; estadoRecorre++) {
                concatenacion = concatenacion + estadoArray[estadoRecorre] + ",";
                EstadosJson = EstadosJson + "{'id':" + (estadoRecorre + 1) + ", 'loc':'" + (150 * (estadoRecorre + 1)) + " 0', 'text':'"+ estadoArray[estadoRecorre].ToString() + "'},\n";
            }



            String ResultadoCadena = "";
            String RutasJson = @"";



            for (int aceptacion = 0; aceptacion < charArr.Length; aceptacion++) {
                for (int evalua = 0; evalua <= 1; evalua++)
                {
                    if (charArr[aceptacion].ToString() == evalua.ToString())
                    {
                        ResultadoCadena =  ResultadoCadena + "δ(" + estadoArray[aceptacion].ToString() + "," + evalua.ToString() + ") = " + estadoArray[ aceptacion + 1] + "\n";
                        RutasJson = RutasJson + @"{ 'from': " + (aceptacion + 1) + ", 'to': " + (aceptacion + 2) + ", 'text': '" + evalua.ToString() + "' },\n";
                    }
                    else {
                        if (aceptacion == 0 || aceptacion == 1)
                        {
                            ResultadoCadena = ResultadoCadena + "δ(" + estadoArray[aceptacion].ToString() + "," + evalua.ToString() + ") = " + estadoArray[aceptacion] + "\n";
                            RutasJson = RutasJson + @"{ 'from': " + (aceptacion + 1) + ", 'to': " + (aceptacion + 1) + ", 'text': '" + evalua.ToString() + "' },\n";
                        }
                        else {
                            ResultadoCadena = ResultadoCadena + "δ(" + estadoArray[aceptacion].ToString() + "," + evalua.ToString() + ") = " + estadoArray[0] + "\n";
                            RutasJson = RutasJson + @"{ 'from': " + (aceptacion + 1) + ", 'to': " + (1) + ", 'text': '" + evalua.ToString() + "' },\n";
                        }
                    }
                }
            }

            ResultadoCadena = ResultadoCadena + "δ(" + estadoArray[charArr.Length].ToString() + ",0) = " + estadoArray[charArr.Length] + "\n";
            RutasJson = RutasJson + @"{ 'from': " + (largo + 1).ToString() + ", 'to': " + (largo + 1).ToString() + ", 'text': '" + 0 + "' },\n";
            ResultadoCadena = ResultadoCadena + "δ(" + estadoArray[charArr.Length].ToString() + ",1) = " + estadoArray[charArr.Length] + "\n";
            RutasJson = RutasJson + @"{ 'from': " + (largo + 1).ToString() + ", 'to': " + (largo + 1).ToString() + ", 'text': '" + 1 + "' },\n";
            TxtCadenas.Text = ResultadoCadena;

            //Estados del Automata
            TxtResultado.Text = concatenacion.Substring(0, concatenacion.Length - 1); 

            StringResult = @"{ 'class': 'go.GraphLinksModel',
                               'nodeKeyProperty': 'id',
                               'nodeDataArray': [ "
                               + EstadosJson.Substring(0, EstadosJson.Length - 2) +                         
                               @"],
                               'linkDataArray': [ "
                               + RutasJson.Substring(0, RutasJson.Length - 2 ) + 
                               @"]
                             }";

            String withDoubleQuotes = StringResult.Replace("'", "\"");
            mySavedModel.InnerText = withDoubleQuotes;

            /*
            https://gojs.net/latest/samples/stateChart.html
            { "class": "go.GraphLinksModel",
                "nodeKeyProperty": "id",
                "nodeDataArray": [ 
                {"id":1, "loc":"150 0", "text":"q0"},
                {"id":2, "loc":"300 0", "text":"q1"},
                {"id":3, "loc":"450 0", "text":"q2"},
                {"id":4, "loc":"600 0", "text":"q3"},
                {"id":5, "loc":"750 0", "text":"q4"}
                ],
                "linkDataArray": [ 
                { "from": 1, "to": 1, "text": "0" },
                { "from": 1, "to": 2, "text": "1" },
                { "from": 2, "to": 3, "text": "0" },
                { "from": 2, "to": 2, "text": "1" },
                { "from": 3, "to": 1, "text": "0" },
                { "from": 3, "to": 4, "text": "1" },
                { "from": 4, "to": 5, "text": "0" },
                { "from": 4, "to": 1, "text": "1" }
                ]
                } 
            */


        }
    }
}