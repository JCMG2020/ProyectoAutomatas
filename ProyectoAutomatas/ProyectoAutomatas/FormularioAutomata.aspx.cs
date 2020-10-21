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
            myDiagramDiv.Visible = false;
            BtnGrafico.Visible = false;
        }

        protected void BtnEvaluaCadena_Click(object sender, EventArgs e)
        {
            String Cadena = "";
            Int64 largo = 0;
            List<String> estados = new List<String>();
            Cadena = TxtCadena.Text.Trim();

            


            char[] charArr = Cadena.ToCharArray();
            largo = Cadena.Length;

            for (int i = 0; i < largo; i++) {

                if (charArr[i].ToString() == "0" || charArr[i].ToString() == "1")
                {
                    Console.WriteLine("La cadena es valida");
                }
                else {
                    Msgbox("Adevertencia", "Solo pueden agrearse cadenas con 0 y 1, El valor [" + charArr[i].ToString() + "] dentro de la cadena, no es valido", "warning", this.Page, this, 1);
                    return;
                }
                
            }


                for (int i = 0; i <= largo; i++)
            {
                estados.Add("q" + i.ToString());
            }

            String[] estadoArray = estados.ToArray();
            String concatenacion = "";
            String EstadosJson = "";
            String StringResult = "";
            EstadosJson = @"";

            for (int estadoRecorre = 0; estadoRecorre < estadoArray.Length; estadoRecorre++)
            {
                concatenacion = concatenacion +  @"<tr>
                                                  <th scope='row'>" + (estadoRecorre + 1) + @"</th>
                                                  <td>" + estadoArray[estadoRecorre] + @"</td>
                                                </tr>";
                                        
                EstadosJson = EstadosJson + "{'id':" + (estadoRecorre + 1) + ", 'loc':'" + (150 * (estadoRecorre + 1)) + " 0', 'text':'" + estadoArray[estadoRecorre].ToString() + "'},\n";
            }

            TxtTabla.InnerHtml = @"<br>
                                   <h4>Estados</h4>
                                   <br>
                                    <table class='table table-striped'>
                                      <thead>
                                        <tr>
                                          <th scope='col'>#</th>
                                          <th scope='col'>Estado</th>
                                        </tr>
                                      </thead>
                                      <tbody>
                                       " + concatenacion + @"
                                      </tbody>
                                    </table>";

            String CadenaEstados = "";
            String RutasJson = @"";

            for (int aceptacion = 0; aceptacion < charArr.Length; aceptacion++)
            {
                for (int evalua = 0; evalua <= 1; evalua++)
                {
                    if (charArr[aceptacion].ToString() == evalua.ToString())
                    {
                        CadenaEstados = CadenaEstados  + @"<tr>
                                                              <th scope='row'>δ(" + estadoArray[aceptacion].ToString() + "," + evalua.ToString() + @")</th>
                                                              <td>" + estadoArray[aceptacion + 1] + @"</td>
                                                           </tr>";
                        RutasJson = RutasJson + @"{ 'from': " + (aceptacion + 1) + ", 'to': " + (aceptacion + 2) + ", 'text': '" + evalua.ToString() + "' },\n";
                    }
                    else
                    {
                        if (aceptacion == 0 || aceptacion == 1)
                        {
                            CadenaEstados = CadenaEstados + @"<tr>
                                                              <th scope='row'>δ(" + estadoArray[aceptacion].ToString() + "," + evalua.ToString() + @")</th>
                                                              <td>" + estadoArray[aceptacion] + @"</td>
                                                              </tr>";
                            RutasJson = RutasJson + @"{ 'from': " + (aceptacion + 1) + ", 'to': " + (aceptacion + 1) + ", 'text': '" + evalua.ToString() + "' },\n";
                        }
                        else
                        {
                            CadenaEstados = CadenaEstados + @"<tr>
                                                              <th scope='row'>δ(" + estadoArray[aceptacion].ToString() + "," + evalua.ToString() + @")</th>
                                                              <td>" + estadoArray[0] + @"</td>
                                                              </tr>";
                            RutasJson = RutasJson + @"{ 'from': " + (aceptacion + 1) + ", 'to': " + (1) + ", 'text': '" + evalua.ToString() + "' },\n";
                        }
                    }
                }
            }

            CadenaEstados = CadenaEstados + @"<tr>
                                              <th scope='row'>δ(" + estadoArray[charArr.Length].ToString() + @",0)</th>
                                              <td>" + estadoArray[charArr.Length] + @"</td>
                                              </tr>";
            RutasJson = RutasJson + @"{ 'from': " + (largo + 1).ToString() + ", 'to': " + (largo + 1).ToString() + ", 'text': '" + 0 + "' },\n";
            CadenaEstados = CadenaEstados + @"<tr>
                                              <th scope='row'>δ(" + estadoArray[charArr.Length].ToString() + @",1)</th>
                                              <td>" + estadoArray[charArr.Length] + @"</td>
                                              </tr>";
            RutasJson = RutasJson + @"{ 'from': " + (largo + 1).ToString() + ", 'to': " + (largo + 1).ToString() + ", 'text': '" + 1 + "' },\n";
            TxtEstados.InnerHtml = @" <br>
                                      <h4>Transición</h4>
                                      <br>
                                      <table class='table table-striped'>
                                      <thead>
                                        <tr>
                                          <th scope='col'>Evalua</th>
                                          <th scope='col'>Resultado</th>
                                        </tr>
                                      </thead>
                                      <tbody>
                                       " + CadenaEstados + @"
                                      </tbody>
                                    </table>";


            StringResult = @"{ 'class': 'go.GraphLinksModel',
                               'nodeKeyProperty': 'id',
                               'nodeDataArray': [ "
                               + EstadosJson.Substring(0, EstadosJson.Length - 2) +
                               @"],
                               'linkDataArray': [ "
                               + RutasJson.Substring(0, RutasJson.Length - 2) +
                               @"]
                             }";

            String withDoubleQuotes = StringResult.Replace("'", "\"");
            mySavedModel.InnerText = withDoubleQuotes;
            myDiagramDiv.Visible = true;
            BtnGrafico.Visible = true;

            Msgbox("Listo", "Se han generado los estados, estados de transicion y el grafo", "success", this.Page, this, 1);

        }

        protected void BtnBorrarRegistro_Click(object sender, EventArgs e)
        {
            Response.Redirect("/FormularioAutomata.aspx", false);
        }

        public void Msgbox(String titulo, String mensaje, String tipo_mensaje, Page pg, Object obj, int tipo)
        {
            string s = "<script> ";   // Se Utiliza el componente Sweet Alert, se realiza funcion para diferentes tipos de mensaje

            if (tipo == 1)
            {
                s += " swal.fire({";
                s += " title: '" + titulo + "',";
                s += " text: '" + mensaje + "',";
                s += " icon: '" + tipo_mensaje + "',";
                s += " showCancelButton: false,";
                s += " confirmButtonColor: '#3085d6',";
                s += " cancelButtonColor: '#d33',";
                s += " confirmButtonText: 'Ok'";
                s += " }).then(function () {";
                s += " ";
                s += "";
                s += "   }) ";
            }
            else
            {
                s += " swal.fire({";
                s += " title: '" + titulo + "',";
                s += " text: '" + mensaje + "',";
                s += " type: '" + tipo_mensaje + "',";
                s += " showCancelButton: false,";
                s += " confirmButtonColor: '#3085d6',";
                s += " cancelButtonColor: '#d33',";
                s += " confirmButtonText: 'Ok'";
                s += " }).then(function () {";
                s += "  window.location.replace('/Administrador/NuevaCategoria.aspx'); ";
                s += "   }) ";
            }



            s += "</script>";

            Type cstype = obj.GetType();
            ClientScriptManager cs = pg.ClientScript;
            cs.RegisterClientScriptBlock(cstype, s.ToString(), s.ToString());
        }


    }
}
