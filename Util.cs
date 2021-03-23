using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using RubSitio.Entidades;


public class Util
    {

        #region Modulo - Modulo 11 de un numero
        private static double Modulo(double numero)
        {
            double resul = Math.IEEERemainder(numero, 11);
            return resul;
        }
        #endregion Modulo 11


        #region Validaciones

        #region Validacion Mes Anio
        public static Retorno ValidarMesAnio(DateTime? fBaja)
        {
            var respuesta = new Retorno();

            if (fBaja > DateTime.Today)
            {
                respuesta.CodigoError = 999;
                respuesta.DescripcionMensaje = "Fecha ingresada invalida";            
            }

            return respuesta;
        }
        #endregion Validacion Mes Anio
        
        #region Valida cuil/cuit
        /// <summary>
        /// Validacion con algoritmo de CUIL
        /// </summary>
        /// <param name="CUIL">CUIL</param>
        /// <returns></returns>
        public static bool ValidoCUIT(string CUIL)
        {
            string FACTORES = "54327654321";
            double dblSuma = 0;
            bool resul = false;

            if (!(CUIL.Substring(0, 1).ToString() != "3" && CUIL.Substring(0, 1).ToString() != "2"))
            {
                for (int i = 0; i < 11; i++)
                    dblSuma = dblSuma + int.Parse(CUIL.Substring(i, 1).ToString()) * int.Parse(FACTORES.Substring(i, 1).ToString());
            }
            resul = Modulo(dblSuma) == 0;
            return resul;
        }

        #region Validacion de CUIL
        /// <summary>
        /// Formatea un Numero de CUIL 12-12345678-1
        /// </summary>
        /// <param name="Numero">el Numero de Expdiente a formatear</param>
        /// <param name="PonerGiones">true para ponerle los giones</param>
        /// <returns>Número de Expediente formateado.</returns>
        public static string FormateoCUIL(string Numero, bool PonerGiones)
        {
            string sCUIL = Numero.Replace("-", "");

            if (!PonerGiones)
            {
                return sCUIL;
            }
            else
            {
                if (sCUIL.Length == 11)
                {
                    sCUIL = sCUIL.Substring(0, 2).ToString() + "-" + sCUIL.Substring(2, 8).ToString() +
                            "-" + sCUIL.Substring(10, 1).ToString();
                }
            }
            return sCUIL;
        }

        public static bool ValidarCUIL(string CUIL)
        {

            string patron = @"^\d{11}$";
            Regex re = new Regex(patron);

            bool resp = re.IsMatch(CUIL);

            if (resp)
            {

                string FACTORES = "54327654321";
                double dblSuma = 0;

                if (!(CUIL.Substring(0, 1).ToString() != "3" && CUIL.Substring(0, 1).ToString() != "2"))
                {
                    for (int i = 0; i < 11; i++)
                        dblSuma = dblSuma + int.Parse(CUIL.Substring(i, 1).ToString()) * int.Parse(FACTORES.Substring(i, 1).ToString());
                }
                resp = Math.IEEERemainder(dblSuma, 11) == 0;
            }
            return resp;
        }

        #endregion Validacion de CUIL
        #endregion Valida cuil/cuit

        /// <summary>
        /// Determina si se ingreso algun texto
        /// </summary>
        /// <param name="s">Texto a validar</param>
        /// <returns>Verdadero si se ingreso texto</returns>
        public static bool EsTextoIngresado(string s)
        {
            return !s.Trim().Equals(string.Empty);
        }


        #region Validacion Numerico
        public static bool esNumerico(string Valor)
        {
            bool ValidoDatos = false;

            Regex numeros = new Regex(@"^\d+?$");

            if (Valor.Length != 0)
            {
                ValidoDatos = numeros.IsMatch(Valor);
            }
            return ValidoDatos;
        }
        #endregion Validacion Numerico

        #endregion Validaciones

    }

